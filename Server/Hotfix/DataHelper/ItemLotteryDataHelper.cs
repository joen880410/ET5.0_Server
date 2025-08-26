using System.Collections.Generic;
using System.Linq;
using ETModel;

namespace ETHotfix
{
    public static class ItemLotteryDataHelper
    {
        private static DBProxyComponent dbProxy
        {
            get
            {
                return Game.Scene.GetComponent<DBProxyComponent>();
            }
        }
        
    }
}
