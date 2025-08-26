using ETModel;
namespace ETModel
{
	[Message(OuterOpcode.C2S_Ping)]
	public partial class C2S_Ping : IRequest {}

	[Message(OuterOpcode.S2C_Ping)]
	public partial class S2C_Ping : IResponse {}

}
namespace ETModel
{
	public static partial class OuterOpcode
	{
		 public const ushort C2S_Ping = 101;
		 public const ushort S2C_Ping = 102;
	}
}
