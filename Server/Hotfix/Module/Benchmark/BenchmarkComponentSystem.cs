using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using ETModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Linq;

namespace ETHotfix
{
    [ObjectSystem]
    public class BenchmarkAwakeSystem : AwakeSystem<BenchmarkComponent, ClientConfig>
    {
        public override void Awake(BenchmarkComponent self, ClientConfig clientConfig)
        {
            self.Awake(clientConfig);
        }
    }

    [ObjectSystem]
    public class BenchmarkUpdateSystem : UpdateSystem<BenchmarkComponent>
    {
        public override void Update(BenchmarkComponent self)
        {
            self.Update();
        }
    }

    public static class BenchmarkComponentHelper
    {
        public static async void Awake(this BenchmarkComponent self, ClientConfig clientConfig)
        {
            try
            {
                self.timerComponent = Game.Scene.GetComponent<TimerComponent>();
                self.networkProfiler = new ProfilerUtility.NetworkProfiler();

                if (!File.Exists(clientConfig.UserCollectionPath))
                {
                    Console.WriteLine($"Invalid user data path: {clientConfig.UserCollectionPath}");
                    return;
                }
                IPEndPoint ipEndPoint = ETModel.NetworkHelper.ToIPEndPoint(clientConfig.Address);
                NetOuterComponent networkComponent = Game.Scene.GetComponent<NetOuterComponent>();

                string json = await File.ReadAllTextAsync(clientConfig.UserCollectionPath);
                List<ThirdPartyUser> users = BsonSerializer.Deserialize<List<ThirdPartyUser>>(json);
                //users = users.Where(e => e["identity"].AsInt32 == (int)User.Identity.TestPlayer).ToList();
                //GUID產生出來的字串長度為32位(去掉-後)
                users = users.Where(e => e.userId.Length == 10).ToList();

                int limit = clientConfig.Count;
                var skip = clientConfig.Count * (clientConfig.CurrentRobot - 1);
                for (int i = skip, j = 0; i < (skip + limit); i++, j++)
                {
                    //self.TestAsync(networkComponent, ipEndPoint, i);
                    if (i >= users.Count)
                    {
                        Console.WriteLine($"testing player is over count of collection");
                        break;
                    }
                    var user = users[i];
                    TestPlayerSetting testPlayerSetting = new TestPlayerSetting();
                    //testPlayerSetting.DeviceUniqueIdentifier = user["email"].AsString;
                    testPlayerSetting.DeviceUniqueIdentifier = user.userId;
                    BenchmarkClientComponent.ClientSetting clientSetting = new BenchmarkClientComponent.ClientSetting
                    {
                        networkComponent = networkComponent,
                        ipEndPoint = ipEndPoint,
                        testPlayerSetting = testPlayerSetting,
                        robotMode = (BenchmarkClientComponent.RobotMode)clientConfig.RobotMode,
                        roadSettingId = clientConfig.RoadSettingId,
                        sportType = clientConfig.SportType,
                    };
                    if ((j + 1) % Math.Max(clientConfig.SignInBatchCountWithDelay, 1) == 0)
                    {
                        float delay = clientConfig.DelaySignInSecond;
                        if (delay > 0)
                        {            
                            await Game.Scene.GetComponent<TimerComponent>().WaitForSecondAsync(delay);
                        }
                    }
                    var client = ComponentFactory.Create<BenchmarkClientComponent, BenchmarkClientComponent.ClientSetting>(clientSetting);
                    client.index = j;
                    self.clients.Add(testPlayerSetting.DeviceUniqueIdentifier, client);
                    self.clientList.Add(client);
                }

                while (!self.IsDisposed)
                {
                    await self.timerComponent.WaitForSecondAsync(1);
                    if (self.watchTargetList.Count != 0)
                    {
                        foreach (var v in self.clientList.Where(e => self.watchTargetList.Contains(e.index)))
                        {
                            v.PrintMessage();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async void Update(this BenchmarkComponent self)
        {
            if (self.isOnProfiler)
                self.UpdateFPS();

            if (!self.isNeedToUpdate)
            {
                return;
            }
            self.isNeedToUpdate = false;
            if (self.clientList.Count != 0)
            {
                for (int i = 0; i < self.clientList.Count; i++)
                {
                    self.clientList[i].Update();
                }
            }
            await self.timerComponent.WaitForSecondAsync(self.updatePeriod);
            self.isNeedToUpdate = true;
        }

        private static void UpdateFPS(this BenchmarkComponent self)
        {
            self.frameCount++;
            self.updateTimer += self.timerComponent.deltaTime;
            self.logTimer -= self.timerComponent.deltaTime;

            if (self.logTimer <= 0.0f)
            {
                var fps = (int)(self.frameCount / self.updateTimer);
                self.frameCount = 0;
                self.updateTimer = 0;
                NetOuterComponent networkComponent = Game.Scene.GetComponent<NetOuterComponent>();
                self.networkProfiler.SetConnectedSessionCount(networkComponent.Count);
                string networkInfo = self.networkProfiler.Show(fps);
                self.logTimer = BenchmarkComponent.logFreqAtSec;
                Console.WriteLine(networkInfo);
            }
        }

        public static async void TestAsync(this BenchmarkComponent self, NetOuterComponent networkComponent, IPEndPoint ipEndPoint, int j)
        {
            try
            {
                using (Session session = networkComponent.Create(ipEndPoint))
                {
                    int i = 0;
                    while (i < 100000000)
                    {
                        ++i;
                        await self.Send(session, j);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async Task Send(this BenchmarkComponent self, Session session, int j)
        {
            try
            {
                await session.Call(new C2S_Ping());
                ++self.k;

                if (self.k % 100000 != 0)
                {
                    return;
                }

                long time2 = TimeHelper.ClientNowMilliSeconds();
                long time = time2 - self.time1;
                self.time1 = time2;
                Log.Info($"Benchmark k: {self.k} 每10W次耗时: {time} ms {session.Network.Count}");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}