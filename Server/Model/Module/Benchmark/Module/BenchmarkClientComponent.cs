using System.Net;
using System.Collections.Generic;
using System;

namespace ETModel
{
    public class BenchmarkClientComponent : Entity
    {
        public enum RobotMode
        {
            Roaming,
            Party,
            Signin,
            PetExplore,
            Unknown,
        }

        public class ClientSetting
        {
            public NetOuterComponent networkComponent;

            public IPEndPoint ipEndPoint;

            public TestPlayerSetting testPlayerSetting;

            public RobotMode robotMode;

            public int roadSettingId { get; set; }
            public int sportType { get; set; }
        }

        public NetOuterComponent networkComponent;

        public IPEndPoint ipEndPoint;

        public TestPlayerSetting testPlayerSetting;

        public RobotMode robotMode;

        public long roadSettingId;
        public long sportType;

        public Session session;

        public const long pingWaitMilisec = 5000L;
        public Random random = new Random((int)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        public const long LeaveRoomSec = 30L;

        public readonly string[] Chat = new string[] { "AAA", "BBB", "★★?", "迷蚪?", "0蚌}", "淏❥😆" };

        public StateMachine<BenchmarkClientComponent> stateMachine;

        public StateMachine<BenchmarkClientComponent>.State login, roaming;

        public int index = 0;

        public List<BenchmarkComponent.IUpdate> components = new List<BenchmarkComponent.IUpdate>();

        public long currentPing;

        public string userName;
    }
}