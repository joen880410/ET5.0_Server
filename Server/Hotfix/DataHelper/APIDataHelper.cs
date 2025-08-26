using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    public static class APIDataHelper
    {

        private static DBProxyComponent dbProxy
        {
            get
            {
                return Game.Scene.GetComponent<DBProxyComponent>();
            }
        }

        /// <summary>
        /// 註冊api使用權限
        /// </summary>
        /// <param name="apiType"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static async ETTask<APIAuthorization> SignUpAPIAuthorization(ApiType apiType, string apiName)
        {
            APIAuthorization apiAuthorization = ComponentFactory.CreateWithId<APIAuthorization>(IdGenerater.GenerateId());
            apiAuthorization.APIType = (int)apiType;
            apiAuthorization.APIName = apiName;
            apiAuthorization.HashKey = CryptographyHelper.GenerateRandomId(32);
            apiAuthorization.createAt = DateTime.UtcNow;
            await ETTask.WaitAll(new ETTask[]
            {
                dbProxy.Save(apiAuthorization),
                dbProxy.SaveLog(apiAuthorization.Id, DBLog.LogType.APIAuthorizationSignUp, apiAuthorization),
            });
            return apiAuthorization;
        }

        /// <summary>
        /// 註銷使用者
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async ETTask DeleteAPIAuthorization(ApiType apiType, string apiName)
        {
            await dbProxy.DeleteJson<APIAuthorization>(entity => entity.APIName == apiName && entity.APIType == (int)apiType);
        }
        /// <summary>
        /// 查詢API紀錄
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="party"></param>
        /// <returns></returns>
        public static async ETTask<APIAuthorization> FindOneAPIAuthorization(ApiType apiType, string apiName)
        {
            var list = await dbProxy.Query<APIAuthorization>(entity => entity.APIName == apiName && entity.APIType == (int)apiType);
            if (list.Any())
            {
                return (APIAuthorization)list.First();
            }
            return null;
        }
        public static async ETTask<List<APIAuthorization>> FindAPIWithCommand()
        {
            List<Expression<Func<APIAuthorization, object>>> projections = new List<Expression<Func<APIAuthorization, object>>>()
            {
                e=>e.Id,
                e=>e.HashKey,
                e=>e.APIType
            };
            return await dbProxy.Query(entity => entity.APIType != (int)ApiType.App, projections);
            
        }
    }
}
