using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
	public class RealmGateAddressComponentSystem : StartSystem<RealmGateAddressComponent>
	{
		public override void Start(RealmGateAddressComponent self)
		{
			self.Start();
		}
	}
	
	public static class RealmGateAddressComponentEx
	{
		public static void Start(this RealmGateAddressComponent component)
		{
			component.GateAddress.AddRange(StartConfigComponent.Instance.GateConfigs);
		}
	}
}
