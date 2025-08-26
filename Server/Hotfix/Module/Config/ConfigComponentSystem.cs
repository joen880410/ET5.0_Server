using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ETModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace ETHotfix
{
    [ObjectSystem]
    public class ConfigAwakeSystem : AwakeSystem<ConfigComponent>
    {
        public override void Awake(ConfigComponent self)
        {
            self.Load();
        }
    }

    [ObjectSystem]
    public class ConfigLoadSystem : LoadSystem<ConfigComponent>
    {
        public override void Load(ConfigComponent self)
        {
            self.Load();
        }
    }

    public static class ConfigComponentHelper
    {
        public static void Load(this ConfigComponent self)
        {
            AppType appType = StartConfigComponent.Instance.StartConfig.AppType;
            self.AllConfig.Clear();
            List<Type> types = Game.EventSystem.GetTypes(typeof(ConfigAttribute));
            foreach (Type type in types)
            {

                object[] attrs = type.GetCustomAttributes(typeof(ConfigAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                ConfigAttribute configAttribute = attrs[0] as ConfigAttribute;
                // 只加载指定的配置
                if (!configAttribute.Type.Is(appType))
                {
                    continue;
                }
                object obj = Activator.CreateInstance(type);
                ACategory iCategory = obj as ACategory;
                if (iCategory == null)
                {
                    throw new Exception($"class: {type.Name} not inherit from ACategory");
                }

                iCategory.BeginInit();
                iCategory.EndInit();
                self.AllConfig[iCategory.ConfigType] = iCategory;
                self.AllConfigType[iCategory.ConfigType.Name] = iCategory.ConfigType;
            }
            self.LocalConfigToDB().Coroutine();
        }

        public static async ETTask LoadToDB(this ConfigComponent self)
        {
            try
            {
                AppType appType = StartConfigComponent.Instance.StartConfig.AppType;
                List<Type> types = Game.EventSystem.GetTypes(typeof(ConfigAttribute));

                foreach (Type type in types)
                {
                    object[] attrs = type.GetCustomAttributes(typeof(ConfigAttribute), false);
                    if (attrs.Length == 0)
                    {
                        continue;
                    }

                    ConfigAttribute configAttribute = attrs[0] as ConfigAttribute;

                    if (!configAttribute.Type.Is(appType))
                    {
                        continue;
                    }
                    object obj = Activator.CreateInstance(type);

                    ACategory iCategory = obj as ACategory;
                    if (iCategory == null)
                    {
                        throw new Exception($"class: {type.Name} not inherit from ACategory");
                    }

                    var message = await ConfigDataHelper.FindConfig(iCategory.ConfigType.Name);
                    if (message == null)
                    {
                        continue;
                    }

                    iCategory.BeginDBInit(message.context);
                    iCategory.EndInit();
                    self.AllConfig[iCategory.ConfigType] = iCategory;
                }
                self.isLoadDB = true;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        public static async ETTask LocalConfigToDB(this ConfigComponent self)
        {
            try
            {
                var configLocalList = self.AllConfig.ToList();
                var configTypeNames = configLocalList.Select(e => e.Key.Name).ToList();
                for (int i = 0; i < configLocalList.Count; i++)
                {
                    var typeName = configLocalList[i].Key.Name;
                    var configLocalStr = ConfigHelper.GetText(typeName);
                    using (await ComponentFactory.Create<LockEvent, string>(typeName).Wait())
                    {
                        var excel = await ConfigDataHelper.FindConfig(typeName);
                        if (excel == null)
                        {
                            await ConfigDataHelper.LoadConfig(configLocalStr, typeName);
                        }
                        else
                        {
                            if (!configTypeNames.Contains(excel.fileName))
                            {
                                await ConfigDataHelper.DeleteConfig(excel.fileName);
                                continue;
                            }
                            if (string.Compare(excel.context, configLocalStr) != 0)
                            {
                                excel.context = configLocalStr;
                                excel.createAt = TimeHelper.NowTimeMillisecond();
                                var log = new BsonDocument
                                {
                                    ["fileName"] = excel.fileName,
                                    ["context"] = excel.context
                                };
                                await ConfigDataHelper.UpsertConfig(excel, DBLog.LogType.UpdateConfig, log);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Log.Error(e);
            }

        }

        public static void ReloadOnly(this ConfigComponent self, Type type)
        {
            object obj = Activator.CreateInstance(type);

            ACategory iCategory = obj as ACategory;
            if (iCategory == null)
            {
                throw new Exception($"class: {type.Name} not inherit from ACategory");
            }
            iCategory.BeginInit();
            iCategory.EndInit();

            self.AllConfig[iCategory.ConfigType] = iCategory;
        }

        public static async ETTask ReloadDbOnly(this ConfigComponent self, Type type)
        {
            object obj = Activator.CreateInstance(type);

            ACategory iCategory = obj as ACategory;
            if (iCategory == null)
            {
                throw new Exception($"class: {type.Name} not inherit from ACategory");
            }

            var message = await ConfigDataHelper.FindConfig(iCategory.ConfigType.Name);
            if (message == null)
            {
                return;
            }

            iCategory.BeginDBInit(message.context);
            iCategory.EndInit();
            self.AllConfig[iCategory.ConfigType] = iCategory;

            await ETTask.CompletedTask;
        }

        public static ACategory GetCategory(this ConfigComponent self, Type type)
        {
            ACategory configCategory;
            bool ret = self.AllConfig.TryGetValue(type, out configCategory);
            return ret ? configCategory : null;
        }
    }
}