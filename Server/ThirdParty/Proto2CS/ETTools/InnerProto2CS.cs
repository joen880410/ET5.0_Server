using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ETTools
{
	public static class InnerProto2CS
	{
		private const string protoPath = ".";

		private const string serverMessagePath = "../Server/Model/Module/Message/";

		private static readonly char[] splitChars = new char[2]
		{
			' ',
			'\t'
		};

		private static readonly List<OpcodeInfo> msgOpcode = new List<OpcodeInfo>();

		public static void Proto2CS()
		{
			msgOpcode.Clear();
			Proto2CS("ETModel", "InnerMessage.proto", serverMessagePath, "InnerOpcode", 1000);
			GenerateOpcode("ETModel", "InnerOpcode", serverMessagePath);
		}

		public static void Proto2CS(string ns, string protoName, string outputPath, string opcodeClassName, int startOpcode)
		{
			msgOpcode.Clear();
			string proto = Path.Combine(".", protoName);
			string csPath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(proto) + ".cs");
			string s = File.ReadAllText(proto);
			StringBuilder sb = new StringBuilder();
			sb.Append("using ETModel;\n");
			sb.Append("using System.Collections.Generic;\n");
			sb.Append("using MongoDB.Bson;\n");
			sb.Append("namespace " + ns + "\n");
			sb.Append("{\n");
			bool isMsgStart = false;
			string parentClass = "";
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
					parentClass = "";
					isMsgStart = true;
					string msgName = newline.Split(splitChars, StringSplitOptions.RemoveEmptyEntries)[1];
					string[] ss = newline.Split(new string[1]
					{
						"//"
					}, StringSplitOptions.RemoveEmptyEntries);
					if (ss.Length == 2)
					{
						parentClass = ss[1].Trim();
					}
					msgOpcode.Add(new OpcodeInfo
					{
						Name = msgName,
						Opcode = ++startOpcode
					});
					sb.Append("\t[Message(" + opcodeClassName + "." + msgName + ")]\n");
					sb.Append("\tpublic partial class " + msgName);
					int num;
					switch (parentClass)
					{
					default:
						num = ((parentClass == "IFrameMessage") ? 1 : 0);
						break;
					case "IActorMessage":
					case "IActorRequest":
					case "IActorResponse":
						num = 1;
						break;
					}
					if (num != 0)
					{
						sb.Append(": " + parentClass + "\n");
					}
					else if (parentClass != "")
					{
						sb.Append(": " + parentClass + "\n");
					}
					else
					{
						sb.Append("\n");
					}
				}
				else
				{
					if (!isMsgStart)
					{
						continue;
					}
					if (newline == "{")
					{
						sb.Append("\t{\n");
					}
					else if (newline == "}")
					{
						isMsgStart = false;
						sb.Append("\t}\n\n");
					}
					else if (newline.Trim().StartsWith("//"))
					{
						sb.AppendLine(newline);
					}
					else if (newline.Trim() != "" && newline != "}")
					{
						if (newline.StartsWith("repeated"))
						{
							Repeated(sb, ns, newline);
						}
						else
						{
							Members(sb, newline, isRequired: true);
						}
					}
				}
			}
			sb.Append("}\n");
			File.WriteAllText(csPath, sb.ToString());
		}

		private static void GenerateOpcode(string ns, string outputFileName, string outputPath)
		{
			StringBuilder sb = new StringBuilder();
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

		private static void Repeated(StringBuilder sb, string ns, string newline)
		{
			try
			{
				int index = newline.IndexOf(";");
				newline = newline.Remove(index);
				string[] ss = newline.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
				string type = ss[1];
				type = ConvertType(type);
				string name = ss[2];
				sb.Append("\t\tpublic List<" + type + "> " + name + " = new List<" + type + ">();\n\n");
			}
			catch (Exception e)
			{
				Console.WriteLine($"{newline}\n {e}");
			}
		}

		private static string ConvertType(string type)
		{
			string typeCs = "";
			return type switch
			{
				"int16" => "short", 
				"int32" => "int", 
				"bytes" => "byte[]", 
				"uint32" => "uint", 
				"long" => "long", 
				"int64" => "long", 
				"uint64" => "ulong", 
				"uint16" => "ushort", 
				_ => type, 
			};
		}

		private static void Members(StringBuilder sb, string newline, bool isRequired)
		{
			try
			{
				int index = newline.IndexOf(";");
				newline = newline.Remove(index);
				string[] ss = newline.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
				string type = ss[0];
				string name = ss[1];
				string typeCs = ConvertType(type);
				sb.Append("\t\tpublic " + typeCs + " " + name + " { get; set; }\n\n");
			}
			catch (Exception e)
			{
				Console.WriteLine($"{newline}\n {e}");
			}
		}
	}
}
