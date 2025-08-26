using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    //TODO：要改用新的DB組件
    public static class ConfigDataHelper
    {
        private static DBProxyComponent dbProxy
        {
            get
            {
                return Game.Scene.GetComponent<DBProxyComponent>();
            }
        }

        /// 新增一筆ConfigToDB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="zh_tw_context"></param>
        /// <returns></returns>
        public static async ETTask<int> LoadConfig(string Json, string Filename)
        {
            Config config = ComponentFactory.CreateWithId<Config>(IdGenerater.GenerateId());
            config.context = Json;
            config.fileName = Filename;
            config.createAt = TimeHelper.NowTimeMillisecond();
            BsonDocument doc = new BsonDocument();
            doc["fileName"] = config.fileName;
            doc["context"] = config.context;
            await dbProxy.Save(config);
            //await dbProxy.SaveLog(config.Id, DBLog.LogType.NewConfig, doc);
            return ErrorCode.ERR_Success;
        }

        /// <summary>
        /// 插入一筆ConfigToDB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="zh_tw_context"></param>
        /// <returns></returns>
        public static async ETTask UpsertConfig(Config excalToDB, DBLog.LogType logType, BsonDocument log)
        {
            await dbProxy.Save(excalToDB);
        }
        /// <summary>
        /// 刪除一筆Config
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static async ETTask DeleteConfig(string FileName)
        {
            await dbProxy.DeleteJson<Config>(entity => entity.fileName == FileName);
        }
        public static async ETTask DeleteConfig(List<Config> configs)
        {
            ETTask[] eTTasks = new ETTask[configs.Count];
            foreach (var config in configs)
            {
                eTTasks.Append(dbProxy.DeleteJson<Config>(entity => entity.fileName == config.fileName));
            };
            await ETTask.WaitAll(eTTasks, null);
        }
        /// <summary>
        /// 搜尋一筆Config
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static async ETTask<Config> FindConfig(string FileName)
        {
            var result = await dbProxy.Query<Config>(entity => entity.fileName == FileName);
            if (result.Any())
            {
                return (Config)result[0];
            }
            return null;
        }
        public static async ETTask<List<Config>> FindALLConfig()
        {
            var result = await dbProxy.Query<Config>(entity => true);
            return result.OfType<Config>().ToList();
        }

        public static async ETTask<int> UpdateConfig(Type type, string context, string command)
        {
            if (string.IsNullOrEmpty(context))
            {
                return ErrorCode.ERR_UploadEventContextIsNull;
            }

            if (!JsonHelper.TryFromJson(type, context, out _))
            {
                return ErrorCode.ERR_NotSupportedType;
            }

            var config = await FindConfig(type.Name);
            if (config == null)
            {
                await LoadConfig(context, type.Name);
            }
            else
            {
                if (config.context != context)
                {
                    config.context = context;
                    var log = new BsonDocument
                    {
                        ["context"] = config.context,
                        ["fileName"] = config.fileName
                    };

                    await UpsertConfig(config, DBLog.LogType.UpdateConfig, log);
                }
            }

            // 更新Server企劃資料
            return await Game.Scene.GetComponent<ConsoleComponent>().BroadcastCommand(command);
        }

        public static async ETTask<int> UpdateSingleConfig<T>(List<T> configs, string context, string command) where T : IConfig
        {
            var consoleComponent = Game.Scene.GetComponent<ConsoleComponent>();
            var config = await FindConfig(nameof(T));
            if (config == null)
            {
                // 更新Config DB
                // 更新Server企劃資料
                await LoadConfig(context, nameof(T));
                return await consoleComponent.BroadcastCommand(command);
            }

            T c = default;
            foreach (string str in context.Split(new[] { "\n" }, StringSplitOptions.None))
            {
                try
                {
                    string str2 = str.Trim();
                    if (str2 == "")
                    {
                        continue;
                    }
                    c = ConfigHelper.ToObject<T>(str2);
                }
                catch (Exception e)
                {
                    throw new Exception($"parser json fail: {str}", e);
                }
            }

            if (c == null)
            {
                return ErrorCode.ERR_ConfigIsInvalid;
            }

            var isChange = false;
            var sb = new StringBuilder();
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var ctx = configs.FirstOrDefault(e => e.Id == c.Id);
            if (ctx != null)
            {
                sb.Append("{");
                sb.Append($"\"_id\":{ConfigHelper.Convert(ctx.Id.GetType().Name, ctx.Id.ToString())},");
                for (int i = 0; i < fields.Length; i++)
                {
                    var field = fields[i];
                    sb.Append($"\"{field.Name}\":{ConfigHelper.Convert(field.FieldType.Name, field.GetValue(ctx))},");
                }
                sb.Remove(sb.Length - 1, 1); // 去最後逗點
                sb.Append("}");
                sb.Append("\r\n");

                isChange = sb.ToString() != context;
            }
            else
            {
                if (configs.Count <= 0)
                {
                    configs.Insert(0, c);
                }
                else
                {
                    configs.Insert(configs.Count, c);
                    configs = configs.OrderBy(e => e.Id).ToList();
                }

                isChange = true;
            }

            if (isChange)
            {
                sb.Clear();
                for (int i = 0; i < configs.Count; i++)
                {
                    var setting = configs[i];
                    sb.Append("{");
                    sb.Append($"\"_id\":{ConfigHelper.Convert(setting.Id.GetType().Name, setting.Id.ToString())},");
                    for (int j = 0; j < fields.Length; j++)
                    {
                        var field = fields[j];
                        sb.Append($"\"{field.Name}\":{ConfigHelper.Convert(field.FieldType.Name, field.GetValue(setting))},");
                    }
                    sb.Remove(sb.Length - 1, 1); // 去最後逗點
                    sb.Append("}");
                    sb.Append("\r\n");
                }

                config.context = sb.ToString();
                var log = new BsonDocument
                {
                    ["context"] = config.context,
                    ["fileName"] = config.fileName
                };

                // 更新Config DB
                // 更新Server企劃資料
                await UpsertConfig(config, DBLog.LogType.UpdateConfig, log);
                await consoleComponent.BroadcastCommand(command);
            }

            return ErrorCode.ERR_Success;
        }
    }
}
