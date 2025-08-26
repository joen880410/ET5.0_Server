using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class DBViewComponentAwakeSystem : AwakeSystem<DBViewComponent>
    {
        public override async void Awake(DBViewComponent self)
        {
            self.Awake();
            await self.CreateBsonView();
        }
    }

    [ObjectSystem]
    public class DBViewComponentDestroySystem : DestroySystem<DBViewComponent>
    {
        public override void Destroy(DBViewComponent self)
        {
            self.Destroy();
        }
    }

    public static class DBViewComponentHelper
    {
        public static async ETTask CreateBsonView(this DBViewComponent self)
        {
            await self.CreateAllViewAsync();
        }
    }
}
