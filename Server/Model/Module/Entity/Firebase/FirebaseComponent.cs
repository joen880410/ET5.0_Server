using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using ETHotfix;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ETModel
{
    /// <summary>
    /// 參考文件
    /// 1.https://firebase.google.com/docs/reference/admin/dotnet/class/firebase-admin/messaging/firebase-messaging#class_firebase_admin_1_1_messaging_1_1_firebase_messaging_1a15a60af9a46d9aba14564656ccfb4847
    /// </summary>
    public class FirebaseComponent : Component
    {
        public const string firebaseUrl = "https://fcm.googleapis.com/fcm/send";
        private const string oAuth = "key=AIzaSyBC6X87T-0c_8HH5VF7ArejF1LD5IiMv1o";

        private FirebaseApp firebaseApp = null;

        private FirebaseMessaging firebaseMessaging = null;

        private HttpClient httpClient = null;

        public FirebaseAuth firebaseAuth { private set; get; } = null;

        // 推播訊息的Json格式範例
        //{
        //    "multicast_id": 7707128547180318330,
        //    "success": 0,
        //    "failure": 1,
        //    "canonical_ids": 0,
        //    "results": [
        //        {
        //            "error": "NotRegistered"
        //        }
        //    ]
        //}

        public void Awake()
        {

            const string credentialFileName = "firebase_key.json";

            // 讀取憑證文件並產生憑證物件
            GoogleCredential googleCredential = null;
            string[] paths = new string[]
            {
                Path.Combine("..", "Config", "Key", credentialFileName),
                Path.Combine("..", "..", "Config", "Key", credentialFileName)
            };
            string path = paths.FirstOrDefault(e => File.Exists(e));
            if (string.IsNullOrEmpty(path))
            {
                Log.Error($"GoogleCredential's firebase_key.json doesnt exist on the path!");
                return;
            }
            else
            {
                googleCredential = GoogleCredential.FromFile(path);
            }

            // 產生FirebaseApp實體
            firebaseApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = googleCredential,
            });

            // 產生FirebaseMessaging實體
            firebaseMessaging = FirebaseMessaging.GetMessaging(firebaseApp);

            firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
            httpClient = new HttpClient();
        }

        private async ETTask<bool> Post(string url, BsonDocument parameter = null)
        {
            try
            {
                var ByteArrayParameter = new StringContent(parameter.ToJson(), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", oAuth);
                var response = await httpClient.PostAsync(firebaseUrl, ByteArrayParameter);


                if (response.IsSuccessStatusCode)
                {
                    return false;
                }
                using (StreamReader reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    string responseStr = responseStr = reader.ReadToEnd();
                    var result = BsonSerializer.Deserialize<BsonDocument>(responseStr);
                    if (result["success"].ToString() == "0")
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async ETTask<bool> SendOneNotification(string token, string title, string body)
        {
            // 使用WebApi的方式
            //if (string.IsNullOrEmpty(token))
            //{
            //    return false;
            //}
            //BsonDocument bson = new BsonDocument();
            //bson["to"] = token;
            //bson["notification"] = new BsonDocument();
            //bson["notification"]["title"] = title;
            //bson["notification"]["body"] = body;
            //bson["notification"]["icon"] = "big_icon";
            //return await Post(firebaseUrl, bson);

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            try
            {

                Message message = new Message();
                message.Token = token;
                message.Android = new AndroidConfig
                {
                    Notification = new AndroidNotification
                    {
                        Icon = "big_icon",
                    },
                };
                message.Notification = new Notification
                {
                    Title = title,
                    Body = body,
                };

                string messageId = await firebaseMessaging.SendAsync(message);
                if (string.IsNullOrEmpty(messageId))
                {
                    Log.Error($"To send firebase notification is failed!, Token: {token}");
                    return false;
                }
                else
                {
                    Log.Info($"To send firebase notification is successful! MessageID: {messageId}");
                    return true;
                }
            }
            catch (FirebaseMessagingException messagingException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Log.Error($"To send firebase notification is failed! Message: {ex.Message}, Stack: {ex.StackTrace}");
                return false;
            }
        }
        public async ETTask<bool> SendMoreNotification(List<string> tokens, string title, string body)
        {
            var messages = new List<Message>();
            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }
                Message message = new Message();
                message.Token = token;
                message.Android = new AndroidConfig
                {
                    Notification = new AndroidNotification
                    {
                        Icon = "big_icon",
                    },
                };
                message.Notification = new Notification
                {
                    Title = title,
                    Body = body,
                };
                messages.Add(message);
            }


            try
            {



                var result = (await firebaseMessaging.SendAllAsync(messages));
                if (result.SuccessCount != tokens.Count)
                {
                    Log.Error($"To send firebase notification is failed!, SuccessCount: {result.SuccessCount},FailCount:{result.FailureCount}");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (FirebaseMessagingException messagingException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Log.Error($"To send firebase notification is failed! Message: {ex.Message}, Stack: {ex.StackTrace}");
                return false;
            }
        }
        public async ETTask SendNotifications(params Message[] messages)
        {
            if (messages.Length <= 0)
            {
                return;
            }

            try
            {
                var msgs = new List<Message>();
                for (int i = 0; i < messages.Length; i++)
                {
                    var message = messages[i];
                    if (message.Token.IsEmpty())
                    {
                        continue;
                    }

                    msgs.Add(message);
                }
                var result = await firebaseMessaging.SendAllAsync(msgs);
                Log.Info($"To send firebase notification SuccessCount : {result.SuccessCount}, FailureCount : {result.FailureCount}");
                for (int i = 0; i < result.Responses.Count; i++)
                {
                    var res = result.Responses[i];
                    if (res.IsSuccess)
                    {
                        Log.Error($"To send firebase notification is failed!, Exception.ErrorCode: {res.Exception.ErrorCode}");
                    }
                    else
                    {
                        Log.Info($"To send firebase notification is successful! MessageID: {res.MessageId}");
                    }
                }

            }
            catch (FirebaseMessagingException messagingException)
            {
                Log.Error($"To send firebase notification is failed! ErrorCode: {messagingException.ErrorCode}");
            }
            catch (Exception ex)
            {
                Log.Error($"To send firebase notification is failed! Message: {ex.Message}, Stack: {ex.StackTrace}");
            }
        }
        public static Message ToMessage(string token, string title, string body)
        {
            return new Message
            {
                Token = token,
                Android = new AndroidConfig
                {
                    Notification = new AndroidNotification
                    {
                        Icon = "big_icon",
                    },
                },
                Notification = new Notification
                {
                    Title = title,
                    Body = body,
                }
            };
        }
    }
}