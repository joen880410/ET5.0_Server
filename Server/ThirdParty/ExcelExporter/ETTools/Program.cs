using System.Text;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace ETTools
{
    public static class Program
    {
        private const string ExcelDir = "../../Excel";
        private const string ClassDir = "../../Server/Model/Module/Demo/Config";
        private const string JsonDir = "../../Config";
     
        private static string template = string.Empty;
        private static Dictionary<string, Table> tables = new Dictionary<string, Table>();
        private static Dictionary<string, ExcelPackage> packages = new Dictionary<string, ExcelPackage>();

        public class Table : IDisposable
        {
            public string AppType { get; set; } = string.Empty;
            public List<HeadInfo> HeadInfos = new List<HeadInfo>();

            public void Dispose()
            {
                this.HeadInfos?.Clear();
            }
        }
        public class HeadInfo : IDisposable
        {
            public string FieldName;
            public string FieldType;
            public HeadInfo(string name, string type)
            {
                this.FieldName = name;
                this.FieldType = type;
            }

            public void Dispose()
            {
            }
        }
        public static void Main()
        {
            Export();
        }

        public static Table GetTable(string fileName)
        {
            if (!tables.TryGetValue(fileName, out var table))
            {
                table = new Table();
                tables[fileName] = table;
            }

            return table;
        }

        public static ExcelPackage GetPackage(string filePath)
        {
            if (!packages.TryGetValue(filePath, out var package))
            {
                using Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                package = new ExcelPackage(stream);
                packages[filePath] = package;
            }

            return package;
        }

        public static string GetCell(this ExcelWorksheet sheet, int row, int column)
        {
            return sheet.Cells[row, column].Text.Trim();
        }

        public static void Export()
        {
            try
            {
                template = File.ReadAllText("../Template.txt");
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                foreach (string path in Directory.GetFiles(ExcelDir))
                {
                    var fileName = Path.GetFileName(path);
                    if (!fileName.EndsWith(".xlsx") || fileName.StartsWith("~$") || fileName.Contains("#"))
                    {
                        continue;
                    }
                    
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    var package = GetPackage(Path.GetFullPath(path));
                    var table = GetTable(fileNameWithoutExtension);
                    
                    ExportExcelSheet(package, table);
                    ExportJson(fileNameWithoutExtension, package, table);
                }

                foreach (var kv in tables)
                {
                    ExportClass(kv.Key, kv.Value);
                }

                Console.WriteLine($"Excel Success!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                tables.Clear();
                foreach (var kv in packages)
                {
                    kv.Value.Dispose();
                }

                packages.Clear();
            }
        }

        public static void ExportExcelSheet(ExcelPackage package, Table table)
        {
            foreach (var sheet in package.Workbook.Worksheets)
            {
                ExportExcelSheet(sheet, table);
            }
        }

        public static void ExportExcelSheet(ExcelWorksheet sheet, Table table)
        {
            var appType = sheet.GetCell(1, 1);
            if (string.IsNullOrEmpty(appType))
            {
                return;
            }

            table.AppType = appType;
            for (int column = 3; column <= sheet.Dimension.End.Column; column++)
            {
                if (sheet.Name.StartsWith("#"))
                {
                    continue;
                }

                var fieldDesc = sheet.GetCell(3, column);
                if (string.IsNullOrEmpty(fieldDesc) || fieldDesc.Contains("#"))
                {
                    continue;
                }
                
                var fieldName = sheet.GetCell(4, column);
                if (string.IsNullOrEmpty(fieldName))
                {
                    continue;
                }

                if (table.HeadInfos.Select(e => e.FieldName).Contains(fieldName))
                {
                    continue;
                }

                var fieldType = sheet.GetCell(5, column);
                if (string.IsNullOrEmpty(fieldType))
                {
                    continue;
                }
                
                table.HeadInfos.Add(new HeadInfo(fieldName, fieldType));
            }
        }

        public static void ExportClass(string fileName, Table table)
        {
            var csPath = Path.Combine(ClassDir, $"{fileName}.cs");
            
            if (File.Exists(csPath))
            {
                File.Delete(csPath);
            }

            var sb = new StringBuilder();
            foreach (var headInfo in table.HeadInfos)
            {
                if (headInfo.FieldName == "_id")
                {
                    sb.Append($"\t\tpublic long Id {{ get; set; }}\n");
                    continue;
                }
                
                sb.Append($"\t\tpublic {headInfo.FieldType} {headInfo.FieldName};\n");
            }
            
            sb.Remove(sb.Length - 1, 1); // 最後去換行

            var content = template.Replace("(AppType)", $"({table.AppType})").Replace("(ConfigName)", fileName).Replace("(Fields)", sb.ToString());
            using (var txt = new FileStream(csPath, FileMode.Create))
            {
                using (var sw = new StreamWriter(txt))
                {
                    sw.Write(content);
                }
            }
        }

        public static void ExportJson(string fileName, ExcelPackage package, Table table)
        {
            var sb = new StringBuilder();
            foreach (var sheet in package.Workbook.Worksheets)
            {
                sb.Append(ExportJson(sheet, table));
            }
            
            var jsonPath = Path.Combine(JsonDir, $"{fileName}.txt");
            
            if (File.Exists(jsonPath))
            {
                File.Delete(jsonPath);
            }
            
            using (var txt = new FileStream(jsonPath, FileMode.Create))
            {
                using (var sw = new StreamWriter(txt))
                {
                    sw.Write(sb.ToString());
                }
            }
        }

        public static string ExportJson(ExcelWorksheet sheet, Table table)
        {
            var sb = new StringBuilder();
            for (int row = 6; row <= sheet.Dimension.End.Row; row++)
            {
                if (string.IsNullOrEmpty(sheet.GetCell(row, 3)))
                {
                    continue;
                }

                sb.Append("{");
                for (int column = 3; column <= sheet.Dimension.End.Column; column++)
                {
                    var fieldName = sheet.GetCell(4, column);
                    var headInfo = table.HeadInfos.FirstOrDefault(e => e.FieldName == fieldName);
                    if (headInfo == null)
                    {
                        continue;
                    }

                    var cell = sheet.GetCell(row, column);
                    if (string.IsNullOrEmpty(cell))
                    {
                        continue;
                    }

                    if (fieldName == "Id")
                    {
                        sb.Append($"\"_id\":{Convert(headInfo.FieldType, cell)},");
                        continue;
                    }
                    
                    sb.Append($"\"{fieldName}\":{Convert(headInfo.FieldType, cell)},");
                    if (column >= sheet.Dimension.End.Column)
                    {
                        sb.Remove(sb.Length - 1, 1); // 去最後逗點
                    }
                }
                
                sb.Append("}\n");
            }
            //sb.Remove(sb.Length - 1, 1); // 去最後逗點
            return sb.ToString();
        }

        private static string Convert(string type, string value)
        {
            switch (type)
            {
                case "uint[]":
                case "int[]":
                case "int32[]":
                case "long[]":
                {
                    value = value.Replace("{", "").Replace("}", "");
                    return $"[{value}]";
                }
                case "string[]":
                case "int[][]":
                    return $"[{value}]";
                case "int":
                case "uint":
                case "int32":
                case "int64":
                case "long":
                case "float":
                case "double":
                {
                    value = value.Replace("{", "").Replace("}", "");
                    if (value == "")
                    {
                        return "0";
                    }
                    return value;
                }
                case "string":
                    return $"\"{value}\"";
                case "AttrConfig":
                    string[] ss = value.Split(':');
                    return "{\"_t\":\"AttrConfig\"," + "\"Ks\":" + ss[0] + ",\"Vs\":" + ss[1] + "}";
                default:
                    throw new Exception($"不支持此类型: {type}");
            }
        }
    }
}