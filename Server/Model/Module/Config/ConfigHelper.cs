using System;
using System.IO;
using System.Text;

namespace ETModel
{
    public static class ConfigHelper
    {
        public static string ConfigPath = $"../Config/{0}.txt";
        public static async ETTask<string> GetTextAsync(string key)
        {
            string path = string.Format(ConfigPath, key);
            try
            {
                string configStr = await File.ReadAllTextAsync(path);
                return configStr;
            }
            catch (Exception e)
            {
                throw new Exception($"load config file fail, path: {path} {e}");
            }
        }

        public static string GetText(string key)
        {
            string path = string.Format(ConfigPath, key);
            try
            {
                string configStr = File.ReadAllText(path);
                var file = new FileInfo(path);
                var time = file.LastWriteTimeUtc;
                return configStr;
            }
            catch (Exception e)
            {
                throw new Exception($"load config file fail, path: {path} {e}");
            }
        }
        public static FileInfo GetFile(string key)
        {
            string path = string.Format(ConfigPath, key);
            try
            {
                string configStr = File.ReadAllText(path);
                return new FileInfo(path);
            }
            catch (Exception e)
            {
                throw new Exception($"load config file fail, path: {path} {e}");
            }
        }
        public static void WriteFile(string key, string text)
        {
            string path = string.Format(ConfigPath, key);
            try
            {
                File.WriteAllText(path, text);
            }
            catch (Exception e)
            {
                throw new Exception($"load config file fail, path: {path} {e}");
            }
        }
        public static async ETTask WriteFileAsync(string key, string text)
        {
            string path = string.Format(ConfigPath, key);
            try
            {
                await File.WriteAllTextAsync(path, text);
            }
            catch (Exception e)
            {
                throw new Exception($"load config file fail, path: {path} {e}");
            }
        }
        public static T ToObject<T>(string str)
        {
            return MongoHelper.FromJson<T>(str);
        }

        public static string Convert(string type, object value)
        {
            switch (type)
            {
                case "Int[]":
                {
                    var val = value as int[];
                    var sb = new StringBuilder();
                    sb.Append("[");
                    for (int i = 0; i < val.Length; i++)
                    {
                        sb.Append($"{val[i]}");
                        if (i == val.Length - 1)
                        {
                            continue;
                        }

                        sb.Append(",");
                    }
                    sb.Append("]");
                    return sb.ToString();
                }
                case "Int32[]":
                {
                    var val = value as int[];
                    var sb = new StringBuilder();
                    sb.Append("[");
                    for (int i = 0; i < val.Length; i++)
                    {
                        sb.Append($"{val[i]}");
                        if (i == val.Length - 1)
                        {
                            continue;
                        }

                        sb.Append(",");
                    }
                    sb.Append("]");
                    return sb.ToString();
                }
                case "Int64[]":
                {
                    var val = value as long[];
                    var sb = new StringBuilder();
                    sb.Append("[");
                    for (int i = 0; i < val.Length; i++)
                    {
                        sb.Append($"{val[i]}");
                        if (i == val.Length - 1)
                        {
                            continue;
                        }

                        sb.Append(",");
                    }
                    sb.Append("]");
                    return sb.ToString();
                } 
                case "Double[]":
                {
                    var val = value as double[];
                    var sb = new StringBuilder();
                    sb.Append("[");
                    for (int i = 0; i < val.Length; i++)
                    {
                        sb.Append($"{val[i]}");
                        if (i == val.Length - 1)
                        {
                            continue;
                        }

                        sb.Append(",");
                    }
                    sb.Append("]");
                    return sb.ToString();
                }
                case "String[]":
                    return $"[{value}]";
                case "Int":
                case "Int32":
                case "Int64":
                case "long":
                //case "float": bson不支援float
                case "Double":
                    return $"{value}";
                case "String":
                    return $"\"{value}\"";
                default:
                    throw new Exception($"企劃表不支援此類型: {type}");
            }
        }
    }
}
