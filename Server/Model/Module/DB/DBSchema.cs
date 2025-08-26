using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ETHotfix;
using Google.Protobuf.Collections;
using ETModel.Share;
namespace ETModel
{
    #region 用法說明

    //TODO:禁用Collection的別名，統一用小寫類別名稱當作Collection名稱，底層會自動做
    //[RedisCache(typeof(User), "user", 0)]

    //DBSchema用法
    //[DBSchema(true)]
    //這裡宣告的物件都要附加屬性DBSchema，表示MongoDB的表
    //屬性alter表示是否在Server重開時，同步欄位跟索引
    //DBIndex用法
    //new string[] { "account", "1", "password": "-1" }
    //1:升序,-1:降序

    //例子:
    //[DBSchema(true)]
    //[DBIndex("account", new [] { "account", "1" }, true)]
    //public class User : Entity
    //{
    //    [BsonElement("account")]
    //    public string account { get; set; }
    //}

    /***************快取模式***************/

    // 快取模式分為5種
    // 1.DBOnly
    // 必要屬性{DBSchema}
    // 可選屬性{DBSchema, DBIndex}
    // 必用組件{DBProxyComponent}
    // 僅對DB做CRUD

    // 2.RedisCacheOnly
    // 必要屬性{RedisCache}
    // 可選屬性{DBIndex}
    // 必用組件{CacheProxyComponent}
    // 僅對Redis做CRUD

    // 3.MemorySyncByRedis
    // 必要屬性{RedisCache, MemorySync}
    // 可選屬性{DBIndex, SyncIgnore(僅用來修飾在(Property { set; get; })上)}
    // 必用組件{CacheProxyComponent}
    // MemorySync可以附加需要同步的Server的類型
    // 使用Redis對記憶體容器做CRUD的資料同步(Redis會有該份資料)

    // 4.MemorySyncByDB(尚未實作)
    // 必要屬性{DBSchema, MemorySync}
    // 可選屬性{DBIndex, SyncIgnore(僅用來修飾在(Property { set; get; })上)}
    // 必用組件{CacheProxyComponent}
    // MemorySync可以附加需要同步的Server的類型
    // 使用DB對記憶體容器做CRUD的資料同步(DB會有該份資料)

    // 5.DBCache(尚未實作)
    // 必要屬性{DBSchema, RedisCache}
    // 可選屬性{DBIndex, SyncIgnore(僅用來修飾在(Property { set; get; })上)}
    // 必用組件{CacheProxyComponent}
    // MemorySync可以附加需要同步的Server的類型
    // 使用Redis當DB的快取，以加速查找(Redis跟DB會有該份資料)

    #endregion

    [DBSchema(true)]
    [DBIndex("name", new[] { "name", "1" })]
    [DBIndex("userTaskCapacity.run", new[] {
                                         "userTaskCapacity.runActivityCountPerDay", "1",
                                         "userTaskCapacity.runMileagePerDay", "1"})]

    [DBIndex("userTaskCapacity.ride", new[] {
                                         "userTaskCapacity.rideMileagePerDay", "1" ,
                                         "userTaskCapacity.rideActivityCountPerDay", "1" })]

    [DBIndex("userPetCapacity", new[] {  "userPetCapacity.FeedFreeTime", "1" ,
                                         "userPetCapacity.PrankFreeTime", "1",
                                         "userPetCapacity.SensibilityFreeTime", "1"})]
    [DBIndex("userPetCapacity.Explore", new[] { "userPetCapacity.ExploreFreeTime", "1" })]
    public class User : Entity
    {
        [BsonElement("salt")]
        public string salt { get; set; }

        [BsonElement("hashPassword")]
        public string hashPassword { get; set; }

        [BsonElement("email"), BsonDefaultValue("")]
        public string email { get; set; }

        //預設值 = "玩家" + id.ToString()
        [BsonElement("name"), BsonDefaultValue("")]
        public string name { get; set; }

        [BsonElement("sex"), BsonDefaultValue(0)]
        public int gender { get; set; }

        [BsonElement("height"), BsonDefaultValue(0d)]
        public double height { get; set; }

        [BsonElement("weight"), BsonDefaultValue(0d)]
        public double weight { get; set; }

        [BsonElement("bodyFatPercentage"), BsonDefaultValue(0d)]
        public double bodyFatPercentage { get; set; }

        /// <summary>
        /// 國籍
        /// 預設在美國
        /// </summary>
        [BsonElement("location"), BsonDefaultValue((int)Location.Usa)]
        public int location { get; set; }

        [BsonElement("birthday"), BsonDefaultValue(19000101)]
        public int birthday { get; set; }

        [BsonElement("createAt"), BsonDefaultValue(0L)]
        public long createAt { get; set; }

        [BsonElement("lastOnlineAt"), BsonDefaultValue(0L)]
        public long lastOnlineAt { get; set; }

        [BsonElement("coin"), BsonDefaultValue(0L)]
        public long coin { get; set; }

        [BsonElement("language"), BsonDefaultValue(10)]//en = 10
        public int language { get; set; }

        [BsonElement("firebaseDeviceToken"), BsonDefaultValue("")]
        public string firebaseDeviceToken { get; set; }

        [BsonElement("lastCreateTokenAt"), BsonDefaultValue(0L)]
        public long lastCreateTokenAt { get; set; }

        [BsonElement("identity"), BsonDefaultValue((int)Identity.Player)]
        public int identity { get; set; }

        [BsonElement("userLeaderCapacity"), BsonDefaultValue(null)]
        public BsonDocument userLeaderCapacity { get; set; }

        [BsonElement("userBagCapacity"), BsonDefaultValue(null)]
        public BsonDocument userBagCapacity { get; set; }

        [BsonElement("userTaskCapacity"), BsonDefaultValue(null)]
        public BsonDocument userTaskCapacity { get; set; }

        [BsonElement("userPetCapacity"), BsonDefaultValue(null)]
        public BsonDocument userPetCapacity { get; set; }

        [BsonElement("hobby"), BsonDefaultValue("")]
        public string hobby { get; set; }

        [BsonElement("selfIntroduction"), BsonDefaultValue("")]
        public string selfIntroduction { get; set; }
        [BsonElement("userState"), BsonDefaultValue(0)]
        public int userState { get; set; }

        public enum Gender
        {
            Unknown,
            Male,
            Female,
        }
        [Flags]
        public enum State
        {
            Use,
            unUse,
        }
        public enum Location
        {
            Usa = 0,
        }

        [Flags]
        public enum Identity
        {
            Player = 1 << 0,

            TestPlayer = 1 << 1,

            Super = 1 << 2,
        }
    }

    [DBSchema(true)]
    [DBIndex("uid", new[] { "uid", "1" })]
    [DBIndex("partyId", new[] { "party", "1", "userId", "1" }, true)]
    [DBIndex("idParty", new[] { "userId", "1", "party", "1" }, true)]
    public class ThirdPartyUser : Entity
    {
        [BsonElement("uid")]
        public long uid { get; set; }

        [BsonElement("party"), BsonDefaultValue("")]
        public string party { get; set; }

        [BsonElement("userId"), BsonDefaultValue("")]
        public string userId { get; set; }

        [BsonElement("name"), BsonDefaultValue("")]
        public string name { get; set; }

        [BsonElement("gender"), BsonDefaultValue("")]
        public string gender { get; set; }

        [BsonElement("location"), BsonDefaultValue("")]
        public string location { get; set; }

        [BsonElement("email"), BsonDefaultValue("")]
        public string email { get; set; }

        [BsonElement("birthday"), BsonDefaultValue("")]
        public string birthday { get; set; }

        [BsonElement("createAt"), BsonDefaultValue(0L)]
        public long createAt { get; set; }

        public enum Tag
        {
            Token,
            Guest,
            Facebook,
            AppleId,
            Account,
            Google,
            JAccount,
            JSport
        }
    }
    [DBSchema(true)]
    public class ThirdPartyUserDeleted : Entity
    {
        [BsonElement("doc"), BsonDefaultValue(null)]
        public BsonArray docs { get; set; }

        [BsonElement("createAt"), BsonDefaultValue(0)]
        public long createAt { get; set; }

    }

    /// <summary>
    /// API授權表
    /// </summary>
    [DBSchema(true)]
    public class APIAuthorization : Entity
    {
        [BsonElement("APIName")]
        public string APIName { get; set; }

        [BsonElement("HashKey")]
        public string HashKey { get; set; }

        [BsonElement("APIType")]
        public int APIType { get; set; }

        /// <summary>
        /// 紀錄於...
        /// </summary>
        [BsonElement("createAt"), BsonDefaultValue(default)]
        public BsonDateTime createAt { get; set; }
    }

    [DBSchema(true)]
    [DBIndex("fileName", new[] { "fileName", "1" }, true)]
    public class Config : Entity
    {
        [BsonElement("context")]
        public string context { get; set; }

        [BsonElement("fileName")]
        public string fileName { get; set; }
        [BsonElement("createAt")]
        public long createAt { get; set; }
    }


    /// <summary>
    /// 資料庫升級腳本
    /// </summary>
    [DBSchema(true)]
    [DBIndex("step", new[] { "step", "1" }, true)]
    [DBIndex("scriptName", new[] { "scriptName", "1" }, true)]
    public class DBUpgradeScript : Entity
    {
        [BsonElement("step"), BsonDefaultValue(1)]
        public long step { get; set; }

        [BsonElement("scriptName"), BsonDefaultValue("InvalidScript")]
        public string scriptName { get; set; }

        [BsonElement("isChecked"), BsonDefaultValue(false)]
        public bool isChecked { get; set; }

        [BsonElement("checkAt"), BsonDefaultValue(0L)]
        public long checkAt { get; set; }

        [BsonElement("createAt"), BsonDefaultValue(0L)]
        public long createAt { get; set; }
    }

    /// <summary>
    /// 禮包碼
    /// </summary>

    [DBSchema(true)]
    public class RewardCode : Entity
    {
        public enum RewardType
        {
            None,
            OnlyOneUse,
            OneUserOneTime,
        }

        /// <summary>
        /// 禮包碼
        /// </summary>
        [BsonElement("rewardcode"), BsonDefaultValue("")]
        public string rewardcode { get; set; }
        /// <summary>
        /// 禮包內容(對應rewardSetting的ID)
        /// </summary>
        [BsonElement("rewardIId"), BsonDefaultValue(0)]
        public int rewardId { get; set; }
        /// <summary>
        /// 使用類型
        /// </summary>
        [BsonElement("rewardType"), BsonDefaultValue((int)RewardType.None)]
        public int rewardType { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        [BsonElement("updateAt"), BsonDefaultValue(0L)]
        public long updateAt { get; set; }
        /// <summary>
        /// 可使用次數
        /// </summary>
        [BsonElement("count"), BsonDefaultValue(0L)]
        public int count { get; set; }
        /// <summary>
        /// 創建時間
        /// </summary>
        [BsonElement("createAt"), BsonDefaultValue(0L)]
        public long createAt { get; set; }

        /// <summary>
        /// 過期時間
        /// </summary>
        [BsonElement("expireAt"), BsonDefaultValue(0L)]
        public long expireAt { get; set; }
    }

    /// <summary>
    /// 禮包碼紀錄
    /// </summary>
    [DBSchema(true)]
    [DBIndex("uid", new[] { "uid", "1" })]
    public class RewardCodeRecord : Entity
    {
        /// <summary>
        /// 玩家Id
        /// </summary>
        [BsonElement("uid"), BsonDefaultValue(0L)]
        public long uid { get; set; }

        /// <summary>
        /// 兌換過的禮包碼列表
        /// </summary>
        [BsonElement("rewardcodes"), BsonDefaultValue(null)]
        public RepeatedField<long> rewardcodes { get; set; } = new RepeatedField<long>();
        /// <summary>
        /// 創建時間
        /// </summary>
        [BsonElement("createAt"), BsonDefaultValue(0L)]
        public long createAt { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        [BsonElement("updateAt"), BsonDefaultValue(0L)]
        public long updateAt { get; set; }
    }

    /// <summary>
    /// Log紀錄表
    /// </summary>
    [DBSchema(false)]
    //[DBIndex("uid", new[] { "uid", "1" }, false)]
    [DBIndex("logType", new[] { "logType", "1" }, false)]
    [DBIndex("recordId", new[] { "uid", "1", "logType", "1" }, false)]
    public class DBLog : Entity
    {
        public enum LogType
        {
            Unknown = 0,

            // 使用者Log 1~9999
            SignUserUp = 1, // 使用者註冊
            SignUserIn = 2, // 使用者登入
            UserDelete = 4, // 使用者刪除
            BindThirdPartyUser = 3, // 綁定第三方使用者
            UpdateUserRideTotalRecord = 100, // 更新使用者騎乘記錄
            UpdateUserRunTotalRecord = 101, // 更新使用者跑步記錄
            UpdateUserCharacterSetting = 200, // 更新使用者腳色設定
            UpdateUserBagSlotCount = 201, // 更新使用者背包數
            UpdateCharacterSetting = 202, // 更新腳色設定
            UpdateUserProgress = 203, // 更新使用者任務進度
            UpdateUserCoin = 204, // 更新使用者J幣
            UpdatePetSetting = 205, // 更新寵物設定
            UpdateUserLeader = 206, // 更新目前選擇的角色
            UpdateUserProfiler = 300, // 更新使用者的簡介
            UpdateUserLanguage = 301, // 更新使用者語系
            UpsertPicture = 302, // 上傳圖片(兼更新)
            DeletePicture = 303, // 刪除圖片
            UpsertPictureMultiple = 304, // 上傳圖片(依照tag到一對多表)
            DeletePictureMultiple = 305, // 刪除圖片(依照tag到一對多表)
            UpdateUserPetCapacity = 306, // 更新玩家寵物容器資料
            JcionRefund = 307, //金幣退費
            ShareOnSocial = 500, // 分享到社群
            AttachAccount = 501, // 綁定帳號密碼
            UpdateUserPassword = 502, // 更新使用者密碼
            ResetTask = 503, // 重製任務
            UpdateUserVerifyRecord = 504, // 更新驗證碼
            UpdateUserSportType = 505, // 更新使用者運動類型

            //configLog 1000~1010
            NewConfig = 1000,
            UpdateConfig = 1001,
            // 好友關係Log 10000~19999
            Relationship = 10000,
            RelationshipApply = 10001,

            // 道具Log 20000~29999
            AddEquipment = 20000, // 新增道具
            SubtractEquipment = 20001, // 移除道具
            MergeEquipment = 20002, // 可疊道具如果出現2筆以上，把她合並成一筆

            // 場景Log 30000~39999
            UpdateScene = 30000, // 更新場景

            // 升級腳本Log 40000~49999
            CreateTaskProgress = 40000, // 新增任務進度
            CreateDefaultAchieve = 40001, // 新增預設成就
            PetConvertToCoin = 40002, // 舊寵物換J幣
            CreateDefaultScene = 40003, // 新增預設場景
            CreateLeader = 40004, // 新增各運動類型和獎牌LeaderID
            EffectConvertToCoin = 40005, // 舊特效換J幣
            CreateDefaultCharacter = 40006, // 新增預設角色
            CreateDefaultEquip = 40007, // 新增預設裝備

            //Task 300000~399999
            TaskSuccessGetReward = 300000, // 任務成功領取獎勵

            //Task 400000~499999
            APIAuthorizationSignUp = 400000, // Api授權註冊
        }

        /// <summary>
        /// Log紀錄的對象
        /// </summary>
        [BsonElement("uid"), BsonDefaultValue(0L)]
        public long uid { get; set; }

        /// <summary>
        /// Log型態
        /// </summary>
        [BsonElement("logType"), BsonDefaultValue((int)LogType.Unknown)]
        public int logType { get; set; }

        /// <summary>
        /// Log紀錄
        /// </summary>
        [BsonElement("document"), BsonDefaultValue(null)]
        public BsonDocument document { get; set; }

        /// <summary>
        /// 紀錄於...
        /// </summary>
        [BsonElement("createAt"), BsonDefaultValue(0L)]
        public long createAt { get; set; }
    }

    /// <summary>
    /// 數據Log紀錄表(沒再用)
    /// </summary>
    [DBIndex("uid", new[] { "uid", "1" }, false)]
    [DBIndex("logType", new[] { "logType", "1" }, false)]
    [DBIndex("recordId", new[] { "uid", "1", "logType", "1" }, false)]
    public class RecordLog : Entity
    {
        public enum LogType
        {
            Unknown = 0
        }

        /// <summary>
        /// Log紀錄的對象
        /// </summary>
        [BsonElement("uid"), BsonDefaultValue(0L)]
        public long uid { get; set; }

        /// <summary>
        /// Log型態
        /// </summary>
        [BsonElement("logType"), BsonDefaultValue((int)LogType.Unknown)]
        public int logType { get; set; }

        /// <summary>
        /// Log紀錄
        /// </summary>
        [BsonElement("document"), BsonDefaultValue(null)]
        public BsonDocument document { get; set; }

        /// <summary>
        /// 紀錄於...
        /// </summary>
        [BsonElement("createAt"), BsonDefaultValue(0L)]
        public long createAt { get; set; }
    }

    [RedisCache]
    [MemorySync(AppType.Gate | AppType.Realm | AppType.Map | AppType.Lobby | AppType.Master | AppType.Http)]
    public partial class Player : Entity
    {
        [SyncOnlyOn(AppType.Gate | AppType.Lobby | AppType.Map | AppType.Master | AppType.Http)]
        public long gateSessionActorId { get; set; }

        public long mapUnitId { get; set; }

        public int gateAppId { get; set; }

        public int lobbyAppId { get; set; }

        public int mapAppId { get; set; }

        public long roomID { get; set; }

        #region Online/Offline

        public bool isOnline { get; set; } = true;

        public long disconnectTime { get; set; } = 0;

        public long mapUnitDisconnectTime { get; set; } = 0;

        #endregion

        #region PlayerStateData

        public PlayerStateData playerStateData { get; set; } = new PlayerStateData();

        #endregion
    }

    [RedisCache]
    [MemorySync(AppType.Gate | AppType.Realm | AppType.Map | AppType.Lobby)]
    public partial class Room : Entity
    {
        // TODO:複雜型別會被強轉Json
        // 可以考慮把欄位全部拆出來或轉Bson或Byte[]，不確定Redis可不可以直接吃二進制
        public RoomInfo info { get; set; }

        public int state { get; set; } = (int)RoomState.Ready;

        public int type { get; set; }

        public int roomBattlePlayType { get; set; }
    }
   
}