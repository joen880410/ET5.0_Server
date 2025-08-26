using ETHotfix;

namespace ETModel
{
    public interface IMapUnitFsmMoveComponent
    {
        void Reset();
        void Awake();
        void Update();
        void UpdateBattleSync();
        void Destroy();
    }
}