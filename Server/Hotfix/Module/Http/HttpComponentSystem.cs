using ETModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix.Module.Http
{
    [ObjectSystem]
    public class HttpComponentAwakeSystem : AwakeSystem<HttpComponent>
    {
        public override async void Awake(HttpComponent self)
        {
            self.Awake();
            await LoadApiKey(self);
        }
        public async ETTask LoadApiKey(HttpComponent self)
        {
            try
            {

                List<APIAuthorization> results = await APIDataHelper.FindAPIWithCommand();
                for (int i = 0; i < results.Count; i++)
                {
                    var result = results[i];
                    self.AddIntoWhiteMap(result.HashKey, result.Id);
                }
            }
            catch (Exception e)
            {

                Log.Error($"LoadApiKey fail error : {e} ");
            }
        }
    }

    [ObjectSystem]
    public class HttpComponentComponentLoadSystem : LoadSystem<HttpComponent>
    {
        public override void Load(HttpComponent self)
        {
            self.Load();
        }
    }

    [ObjectSystem]
    public class HttpComponentComponentStartSystem : StartSystem<HttpComponent>
    {
        public override void Start(HttpComponent self)
        {
            self.Start();
        }
    }
}
