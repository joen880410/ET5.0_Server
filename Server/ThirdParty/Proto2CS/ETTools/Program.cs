using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using ETHotfix;
using ETModel;
using Google.Protobuf;
using MongoDB.Bson;

namespace ETTools
{
    public static class Program
    {
        private static string[] protoNeedless = new string[] { "RpcId", "ActorId", "Parser" };
        private static string[] clientProto = new string[] { "C2R", "C2L", "C2M" };

        private static Regex clientProtoRegex = new Regex(@"^C2", RegexOptions.IgnoreCase);

        private const string protoPath = ".";

        private const string clientMessagePath = "../Server/Library/Model/Module/Message/";

        private const string hotfixMessagePath = "../Server/Library/Hotfix/Module/Message/";

        private const string SwaggerWebPath = "../SwaggerWeb";

        private static readonly char[] splitChars = new char[2]
        {
            ' ',
            '\t'
        };

        private static readonly List<OpcodeInfo> msgOpcode = new List<OpcodeInfo>();

        public static void Main()
        {
            string protoc = ((!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) ? "protoc" : "protoc.exe");
            ProcessHelper.Run(protoc, $"--csharp_out=\"{clientMessagePath}\" --proto_path=\"./\" OuterMessage.proto", protoPath, waitExit: true);
            ProcessHelper.Run(protoc, $"--csharp_out=\"{hotfixMessagePath}\" --proto_path=\"./\" HotfixMessage.proto", protoPath, waitExit: true);
            //ProcessHelper.Run(Path.Combine(SwaggerWebPath, "proto2js.bat"), "", protoPath, waitExit: true);
            InnerProto2CS.Proto2CS();
            Proto2CS("ETModel", "OuterMessage.proto", $"{clientMessagePath}", "OuterOpcode", 100);
            Proto2CS("ETHotfix", "HotfixMessage.proto", $"{hotfixMessagePath}", "HotfixOpcode", 10000);
            //NodeOpcode();
            //CopyHotfixProto();
            Console.WriteLine("proto2cs succeed!");
        }

        public static void Proto2CS(string ns, string protoName, string outputPath, string opcodeClassName, int startOpcode, bool isClient = true)
        {
            msgOpcode.Clear();
            string proto = Path.Combine(".", protoName);
            string s = File.ReadAllText(proto);
            StringBuilder sb = new StringBuilder();
            sb.Append("using ETModel;\n");
            sb.Append("namespace " + ns + "\n");
            sb.Append("{\n");
            bool isMsgStart = false;
            string[] array = s.Split('\n');
            foreach (string line in array)
            {
                string newline = line.Trim();
                if (newline == "")
                {
                    continue;
                }
                if (newline.StartsWith("//"))
                {
                    sb.Append(newline + "\n");
                }
                if (newline.StartsWith("message"))
                {
                    string parentClass = "";
                    isMsgStart = true;
                    string msgName = newline.Split(splitChars, StringSplitOptions.RemoveEmptyEntries)[1];

                    string[] ss = newline.Split(new string[1]
                    {
                        "//"
                    }, StringSplitOptions.RemoveEmptyEntries);
                    parentClass = ((ss.Length != 2) ? "" : ss[1].Trim());
                    msgOpcode.Add(new OpcodeInfo
                    {
                        Name = msgName,
                        Opcode = ++startOpcode
                    });
                    sb.Append("\t[Message(" + opcodeClassName + "." + msgName + ")]\n");
                    sb.Append("\tpublic partial class " + msgName + " ");
                    if (parentClass != "")
                    {
                        sb.Append(": " + parentClass + " ");
                    }
                    sb.Append("{}\n\n");
                }
                if (isMsgStart && newline == "}")
                {
                    isMsgStart = false;
                }
            }
            sb.Append("}\n");
            GenerateOpcode(ns, opcodeClassName, outputPath, sb);

        }

        private static void GenerateOpcode(string ns, string outputFileName, string outputPath, StringBuilder sb)
        {
            sb.AppendLine("namespace " + ns);
            sb.AppendLine("{");
            sb.AppendLine("\tpublic static partial class " + outputFileName);
            sb.AppendLine("\t{");
            foreach (OpcodeInfo info in msgOpcode)
            {
                sb.AppendLine($"\t\t public const ushort {info.Name} = {info.Opcode};");
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            string csPath = Path.Combine(outputPath, outputFileName + ".cs");
            File.WriteAllText(csPath, sb.ToString());
        }
        private static string MatchTags(string str)
        {
            return str.StartsWith(clientProto[0]) ? "UserRealm"
                 : str.StartsWith(clientProto[1]) ? "UserLobby"
                 : "UserMap";
        }
        private static StringBuilder AppendProperties(Type type, StringBuilder sb, PropertyInfo[] properties)
        {
            var tags = MatchTags(type.Name);
            sb.AppendLine($"\t app.post('/{tags}/{type.Name}', (req,res) => {{");
            sb.AppendLine($"\t\t /*  #swagger.tags = ['{MatchTags(type.Name)}']");
            sb.AppendLine($"\t\t\t #swagger.description = '{type.Name}' */\n");
            sb.AppendLine($"\t\t /*  #swagger.responses[200] = {{ description: \"Result\" }} */");
            foreach (var propertie in properties)
            {
                if (protoNeedless.Any(e => e.Contains(propertie.Name)))
                {
                    continue;
                }
                sb.AppendLine($"\t\t const {propertie.Name} = req.query.{propertie.Name}");
                sb.AppendLine($"\t\t /*  #swagger.parameters = ['{propertie.Name}'] = {{");
                sb.Append("\t\t\t").Convert2Swagger(propertie);
                sb.AppendLine($"\t\t }}*/");
            }
            sb.AppendLine($"\t\t var info = {{");
            foreach (var propertie in properties)
            {
                if (propertie.Name == "RpcId")
                {
                    sb.AppendLine($"\t\t {propertie.Name} : RpcId,");
                    continue;
                }
                if (protoNeedless.Any(e => e.Contains(propertie.Name)))
                {
                    continue;
                }
                sb.AppendLine($"\t\t {propertie.Name} :{Convert2Proto(propertie)},");
            }
            sb.AppendLine($"\t\t }}");
            sb.AppendLine($"\t\t call.Send(\"{type.Name}\",info).then((result) => {{");
            sb.AppendLine($"\t\t\t RpcId = result.{protoNeedless[0]} ");
            sb.AppendLine($"\t\t\t res.status(200).send({{");
            sb.AppendLine($"\t\t\t\t success: \"true\",");
            sb.AppendLine($"\t\t\t\t  Result: result,");
            sb.AppendLine($"\t\t\t }});");
            sb.AppendLine($"\t\t }})");
            sb.AppendLine($"\t }})");
            return sb;
        }
        private static void CopyHotfixProto()
        {
            File.Copy("HotfixMessage.proto", Path.Combine(SwaggerWebPath, "HotfixMessage.proto"), true);
        }
        private static string Convert2Proto(PropertyInfo type)
        {

            switch (type.PropertyType.Name)
            {
                case nameof(Int16):
                case nameof(Int32):
                case nameof(Int64):
                case nameof(Single):
                case nameof(Double):
                    return $"Number({type.Name})";
                case nameof(String):
                    return type.Name;
                default:
                    return $"JSON.parse({type.Name})";
            }
        }
        private static StringBuilder Convert2Swagger(this StringBuilder sb, PropertyInfo type)
        {

            switch (type.PropertyType.Name)
            {
                case nameof(Int16):
                case nameof(Int32):
                case nameof(Int64):
                case nameof(Single):
                case nameof(Double):
                    return sb.AppendLine("type:'number'");
                case nameof(String):
                    return sb.AppendLine("type:'string'");
                default:
                    return sb.AppendLine("type:'object'");
            }
        }
    }
}
