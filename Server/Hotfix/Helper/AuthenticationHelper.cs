using ETHotfix.Share;
using ETModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;

namespace ETHotfix
{
    public static class AuthenticationHelper
    {

        public class ThirdPartyInfo
        {
            public string id { set; get; }
            public string name { set; get; } = "";
            public string email { set; get; } = "";
            public string gender { set; get; } = "";
            public string location { set; get; } = "";
            public string birthday { set; get; } = "";

            public int genderCode
            {
                get
                {
                    return (int)(gender == "male" ? User.Gender.Male : User.Gender.Female);
                }
            }

            public int locationCode
            {
                get
                {
                    return (int)User.Location.Usa;
                }
            }

            public int birthdayCode
            {
                get
                {
                    try
                    {
                        string[] split = this.birthday.Split('/');
                        int year, month, day;
                        int.TryParse(split[2], out year);
                        int.TryParse(split[0], out month);
                        int.TryParse(split[1], out day);
                        return year * 10000 + month * 100 + day;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
            }

            public static ThirdPartyInfo Deserialize(BsonDocument doc)
            {
                return new ThirdPartyInfo
                {
                    id = doc.Contains("id") ? doc["id"]?.ToString() : string.Empty,
                    name = doc.Contains("name") ? doc["name"]?.ToString() : string.Empty,
                    email = doc.Contains("email") ? doc["email"]?.ToString() : string.Empty,
                    gender = doc.Contains("gender") ? doc["gender"]?.ToString() : string.Empty,
                    location = doc.Contains("location") ? doc["location"]?.ToString() : string.Empty,
                    birthday = doc.Contains("birthday") ? doc["birthday"]?.ToString() : string.Empty,
                };
            }
        }

        private static async ETTask RealmToGate(Session session, User user, R2C_Authentication response, bool isRefreshToken)
        {
            Player player = await Game.Scene.GetComponent<CacheProxyComponent>().QueryById<Player>(user.Id);
            //如果沒有資料在server上就隨機分配GateServer
            StartConfig config;
            if (player == null)
            {
                // 隨機分配GateServer
                config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
            }
            else
            {
                config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress(player.gateAppId);
                if (config == null)
                {
                    Log.Error("gate server is null");
                    config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
                }
            }

            IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
            Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

            // 向Gate請求一個Key,Client可以拿這個Key連接Gate
            G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey() { Uid = user.Id });

            string outerAddress = config.GetComponent<OuterConfig>().Address2;

            // 創造權杖
            if (isRefreshToken)
            {
                SignInCryptographyHelper.Token tok = new SignInCryptographyHelper.Token
                {
                    uid = user.Id,
                    lastCreateTokenAt = user.lastCreateTokenAt,
                    salt = user.salt,
                };

                string token = SignInCryptographyHelper.EncodeToken(tok);
                response.Token = token;
            }

            StartConfigComponent startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();
            CloudStorageConfig cloudStorageConfig = startConfigComponent.HttpConfig.GetComponent<CloudStorageConfig>();

            response.Error = ErrorCode.ERR_Success;
            response.Address = outerAddress;
            response.HttpAddress = startConfigComponent.HttpConfig.GetComponent<HttpConfig>().Address; ;
            response.DBAddress = startConfigComponent.DBConfig.GetComponent<HttpConfig>().Address;
            response.Key = g2RGetLoginKey.Key;
            response.Data = new PlayerBaseInfo
            {
                Uid = user.Id,
                Name = user.name,
                Location = user.location,
                Birthday = user.birthday,
                CreateAt = user.createAt,
                // 校時用
                LastOnlineAt = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Coin = user.coin,
                Language = user.language,
                CloudStorageUri = $"{cloudStorageConfig.Uri}/{cloudStorageConfig.BucketName}",
                Identity = user.identity,
            };
            response.LinkTypes.Clear();
            response.LinkTypes.AddRange(await GetAllLinkType(user.Id));
            await RegisteTokenToHttp(user, response.Token);
        }
        /// <summary>
        /// 註冊token到 HTTP DB server
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static async ETTask RegisteTokenToHttp(User user, string token)
        {
            Session httpSession = SessionHelper.GetHttpSession();
            Session dbSession = SessionHelper.GetDBHttpSession();

            // 向 HTTP server註冊玩家的token
            await httpSession.Call(new R2H_AuthenticateHttp()
            {
                Uid = user.Id,
                Token = token
            });

            await dbSession.Call(new R2D_AuthenticateHttp()
            {
                Uid = user.Id,
                Token = token
            });
        }
        private readonly static BsonDocument log = new BsonDocument();
        private static async ETTask SignInByUid(Session session, User user, R2C_Authentication response, int language, string firebaseDeviceToken, bool isRefreshToken, string signInMethod, string signInDevice = "")
        {
            log.Clear();

            // 更新user登入資訊
            user.lastOnlineAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            log["onlineAt"] = user.lastOnlineAt;  // 登入時間
            log["signInMethod"] = signInMethod; // 登入方式
            log["signInDevice"] = signInDevice; // 登入方式
            if (user.language != language)
            {
                user.language = language;
                log["language"] = user.language; // 更新語言
            }
            if (user.firebaseDeviceToken != firebaseDeviceToken)
            {
                user.firebaseDeviceToken = firebaseDeviceToken;
                log["firebaseDeviceToken"] = user.firebaseDeviceToken; // 最後更新的FirebaseToken
            }
            if (isRefreshToken)
            {
                user.lastCreateTokenAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                log["lastCreateTokenAt"] = user.lastCreateTokenAt; // 最後更新Token的時間
            }
            log["ip"] = session.RemoteAddress.ToString(); // 登入位址
            await UserDataHelper.UpsertUser(user, DBLog.LogType.SignUserIn, log);

            // 從Realm轉登到Gate並傳給Client用戶資料
            await RealmToGate(session, user, response, isRefreshToken);
        }

        private static AuthenticationType PartyToAuthenticationType(ThirdPartyUser.Tag party)
        {
            switch (party)
            {
                case ThirdPartyUser.Tag.Account:
                    return AuthenticationType.Account;
            }
            return AuthenticationType.Token;
        }

        public static async ETTask<List<AuthenticationType>> GetAllLinkType(long uid)
        {
            List<AuthenticationType> linkTypes = new List<AuthenticationType>();
            var thirdPartyUsers = await UserDataHelper.FindAllThirdPartyUser(uid);
            for (int i = 0; i < thirdPartyUsers?.Count; i++)
            {
                linkTypes.Add(PartyToAuthenticationType((ThirdPartyUser.Tag)Enum.Parse(typeof(ThirdPartyUser.Tag), thirdPartyUsers[i].party, true)));
            }
            return linkTypes;
        }

        public static async ETTask LinkByFaceBook(Player player, LinkInfo info, L2C_Link response)
        {
            string fbToken = info.Secret;
            bool isValidToken = await FacebookHelper.ValidateFacebookToken(fbToken);
            if (!isValidToken)
            {
                response.Error = ErrorCode.ERR_LinkFailed;
                return;
            }
            ThirdPartyInfo fbInfo = await FacebookHelper.GetFacebookUserInfo(fbToken);
            if (fbInfo == null)
            {
                response.Error = ErrorCode.ERR_LinkFailed;
                return;
            }
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            ThirdPartyUser thirdPartyUser = await UserDataHelper.FindOneThirdPartyUser(fbInfo.id, ThirdPartyUser.Tag.Facebook.ToString());
            if (thirdPartyUser == null)
            {
                long uid = player.uid;
                User user = await UserDataHelper.FindOneUser(uid);
                if (user == null)
                {
                    response.Error = ErrorCode.ERR_LinkFailed;
                    return;
                }

                //綁定第三方-FB
                thirdPartyUser = ComponentFactory.CreateWithId<ThirdPartyUser>(IdGenerater.GenerateId());
                thirdPartyUser.uid = user.Id;
                thirdPartyUser.party = ThirdPartyUser.Tag.Facebook.ToString();
                thirdPartyUser.userId = fbInfo.id;
                thirdPartyUser.gender = fbInfo.gender;
                thirdPartyUser.location = fbInfo.location;
                thirdPartyUser.email = fbInfo.email;
                thirdPartyUser.name = fbInfo.name;
                thirdPartyUser.birthday = fbInfo.birthday;
                thirdPartyUser.createAt = now;
                await UserDataHelper.UpsertThirdPartyUser(thirdPartyUser);

                //取得新的第三方列表
                response.LinkTypes.Clear();
                response.LinkTypes.AddRange(await GetAllLinkType(user.Id));
            }
            else
            {
                response.Error = ErrorCode.ERR_LinkIsExist;
            }
        }

        public static async ETTask LinkByAppleId(Player player, LinkInfo info, L2C_Link response)
        {
            string appleId = CryptographyHelper.AESDecrypt(info.Secret);
            ThirdPartyInfo appleInfo = new ThirdPartyInfo
            {
                id = appleId,
            };
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            ThirdPartyUser thirdPartyUser = await UserDataHelper.FindOneThirdPartyUser(appleInfo.id, ThirdPartyUser.Tag.AppleId.ToString());
            if (thirdPartyUser == null)
            {
                long uid = player.uid;
                User user = await UserDataHelper.FindOneUser(uid);
                if (user == null)
                {
                    response.Error = ErrorCode.ERR_LinkFailed;
                    return;
                }

                // 綁定第三方-Apple
                thirdPartyUser = ComponentFactory.CreateWithId<ThirdPartyUser>(IdGenerater.GenerateId());
                thirdPartyUser.uid = user.Id;
                thirdPartyUser.party = ThirdPartyUser.Tag.AppleId.ToString();
                thirdPartyUser.userId = appleInfo.id;
                thirdPartyUser.gender = appleInfo.gender;
                thirdPartyUser.location = appleInfo.location;
                thirdPartyUser.email = appleInfo.email;
                thirdPartyUser.name = appleInfo.name;
                thirdPartyUser.birthday = appleInfo.birthday;
                thirdPartyUser.createAt = now;
                await UserDataHelper.UpsertThirdPartyUser(thirdPartyUser);

                // 取得新的第三方列表
                response.LinkTypes.Clear();
                response.LinkTypes.AddRange(await GetAllLinkType(user.Id));
            }
            else
            {
                response.Error = ErrorCode.ERR_LinkIsExist;
            }
        }

        public static User CreateNewUser(int language, User.Identity identity)
        {
            User user = ComponentFactory.CreateWithId<User>(IdGenerater.GenerateId());
            string salt = CryptographyHelper.GenerateRandomId();
            string password = CryptographyHelper.GenerateRandomId(16);
            string hashPassword = CryptographyHelper.MD5Encoding(password, salt);
            user.salt = salt;
            user.hashPassword = hashPassword;
            user.createAt = TimeHelper.NowTimeMillisecond();
            user.lastOnlineAt = 0L;
            user.name = $"{user.Id}";
            user.email = "";
            user.language = language;
            user.identity = (int)identity;
            return user;
        }

        public static async ETTask AuthenticationByToken(Session session, AuthenticationInfo info, R2C_Authentication response)
        {
            SignInCryptographyHelper.Token tok = null;
            try
            {
                tok = SignInCryptographyHelper.DecodeToken(info.Secret);
                if (tok == null)
                {
                    response.Error = ErrorCode.ERR_InvalidToken;
                    return;
                }
            }
            catch (Exception)
            {
                response.Error = ErrorCode.ERR_InvalidToken;
                return;
            }

            User user = await UserDataHelper.FindOneUser(tok.uid);
            if (user != null)
            {
                if (user.userState == (int)User.State.unUse)
                {
                    response.Error = ErrorCode.ERR_AccountDoesntExist;
                }
                else if (user.salt != tok.salt || user.lastCreateTokenAt != tok.lastCreateTokenAt)
                {
                    response.Error = ErrorCode.ERR_InvalidToken;
                }
                else
                {
                    await SignInByUid(session, user, response, info.Language, "", false, ThirdPartyUser.Tag.Token.ToString(), info?.Info?.DeviceModel);
                }
            }
            else
            {
                response.Error = ErrorCode.ERR_AccountDoesntExist;
            }
        }

        public static async ETTask<(int, long)> AuthenticationByBot(string deviceUniqueIdentifier)
        {
            ThirdPartyUser thirdPartyUser = await UserDataHelper.FindOneThirdPartyUser(deviceUniqueIdentifier, ThirdPartyUser.Tag.Guest.ToString());
            User user = null;
            if (thirdPartyUser == null)
            {
                user = CreateNewUser(10, User.Identity.TestPlayer);
                await UserDataHelper.SignUserUp(user);

                //註冊第三方-Guest
                thirdPartyUser = ComponentFactory.CreateWithId<ThirdPartyUser>(IdGenerater.GenerateId());
                thirdPartyUser.uid = user.Id;
                thirdPartyUser.party = ThirdPartyUser.Tag.Guest.ToString();
                thirdPartyUser.userId = deviceUniqueIdentifier;
                thirdPartyUser.name = "";
                thirdPartyUser.gender = "";
                thirdPartyUser.location = "";
                thirdPartyUser.email = "";
                thirdPartyUser.birthday = "";
                thirdPartyUser.createAt = user.createAt;
                await UserDataHelper.UpsertThirdPartyUser(thirdPartyUser);
                thirdPartyUser.Dispose();
                user.Dispose();
                return (ErrorCode.ERR_Success, user.Id);
            }
            else
            {
                thirdPartyUser.Dispose();
                return (ErrorCode.ERR_DeviceUniqueIdentifierIsExist, 0L);
            }
        }

        public static async ETTask AuthenticationByGuest(Session session, AuthenticationInfo info, R2C_Authentication response)
        {
            string deviceUniqueIdentifier = string.Empty;
            try
            {
                deviceUniqueIdentifier = CryptographyHelper.AESDecrypt(info.Secret);
            }
            catch (Exception)
            {
                response.Error = ErrorCode.ERR_InvalidDeviceUniqueIdentifier;
                return;
            }

            if (string.IsNullOrEmpty(deviceUniqueIdentifier))
            {
                response.Error = ErrorCode.ERR_DeviceUniqueIdentifierIsNull;
                return;
            }

            ThirdPartyUser thirdPartyUser = await UserDataHelper.FindOneThirdPartyUser(deviceUniqueIdentifier, ThirdPartyUser.Tag.Guest.ToString());
            User user = null;
            if (thirdPartyUser == null)
            {
                user = CreateNewUser(info.Language, User.Identity.Player);
                await UserDataHelper.SignUserUp(user);

                //註冊第三方-Guest
                thirdPartyUser = ComponentFactory.CreateWithId<ThirdPartyUser>(IdGenerater.GenerateId());
                thirdPartyUser.uid = user.Id;
                thirdPartyUser.party = ThirdPartyUser.Tag.Guest.ToString();
                thirdPartyUser.userId = deviceUniqueIdentifier;
                thirdPartyUser.name = "";
                thirdPartyUser.gender = "";
                thirdPartyUser.location = "";
                thirdPartyUser.email = "";
                thirdPartyUser.birthday = "";
                thirdPartyUser.createAt = user.createAt;
                await UserDataHelper.UpsertThirdPartyUser(thirdPartyUser);
                thirdPartyUser.Dispose();
                user.Dispose();
            }
            else
            {
                user = await UserDataHelper.FindOneUser(thirdPartyUser.uid);
            }

            await SignInByUid(session, user, response, info.Language, "", true, ThirdPartyUser.Tag.Guest.ToString(), info.Info.DeviceModel);
        }
    }

    public class FacebookHelper
    {
        private const string tokenToInspect = "{0}";

        private const string appToken = "{1}";

        public const string clientId = "840397989678099";

        public const string clientSecret = "329d3c0cb840d1742dc8889ec101eb02";

        public static string requestAppToken => $"https://graph.facebook.com/oauth/access_token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials";

        public static string validateClientTokenApi => $"https://graph.facebook.com/debug_token?input_token={tokenToInspect}&access_token={appToken}";

        public static string getUserInfoApi => $"https://graph.facebook.com/me?fields={string.Join(",", myInfoFields)}&access_token={tokenToInspect}";

        private static readonly string[] myInfoFields = new string[]
        {
            "id", "name", "gender", "location", "picture", "email", "birthday"
        };

        public static async ETTask<bool> ValidateFacebookToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // 請求應用程式的Token，以驗證Unity client Facebook SDK給的Token
            var appDoc = await Get(requestAppToken);
            if (appDoc == null || !appDoc.Contains("access_token"))
            {
                return false;
            }
            // 驗證Unity client給的FB Token
            var appToken = appDoc["access_token"].AsString;
            var validUrl = string.Format(validateClientTokenApi, token, appToken);
            var validDoc = await Get(validUrl);
            if (validDoc != null)
            {
                if (validDoc.Contains("data"))
                {
                    var data = validDoc["data"].AsBsonDocument;
                    if (!data.Contains("is_valid"))
                    {
                        return false;
                    }
                    else
                    {
                        var isValid = data["is_valid"].AsBoolean;
                        if (!isValid)
                        {
                            return false;
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static async ETTask<AuthenticationHelper.ThirdPartyInfo> GetFacebookUserInfo(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var userUrl = string.Format(getUserInfoApi, token);
            var userDoc = await Get(userUrl);
            if (userDoc == null)
            {
                return null;
            }
            AuthenticationHelper.ThirdPartyInfo fbInfo = AuthenticationHelper.ThirdPartyInfo.Deserialize(userDoc);
            return fbInfo;
        }

        private static async ETTask<BsonDocument> Get(string url)
        {
            try
            {
                HttpClient request = new HttpClient();
                request.DefaultRequestHeaders.Add("ContentType", "application/json");

                using (var response = await request.GetAsync(url))
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }
                    using (StreamReader reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        string responseStr = responseStr = reader.ReadToEnd();
                        var result = BsonSerializer.Deserialize<BsonDocument>(responseStr);
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
}
