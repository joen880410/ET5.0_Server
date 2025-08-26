using ETModel;
namespace ETHotfix
{
	[Message(HotfixOpcode.PlayerBaseInfo)]
	public partial class PlayerBaseInfo {}

	[Message(HotfixOpcode.G2C_MessageTip)]
	public partial class G2C_MessageTip : IActorMessage {}

	[Message(HotfixOpcode.MapUnitInfo)]
	public partial class MapUnitInfo {}

	[Message(HotfixOpcode.M2C_MapUnitCreate)]
	public partial class M2C_MapUnitCreate : IActorMessage {}

	[Message(HotfixOpcode.M2C_MapUnitUpdate)]
	public partial class M2C_MapUnitUpdate : IActorMessage {}

	[Message(HotfixOpcode.M2C_MapUnitDestroy)]
	public partial class M2C_MapUnitDestroy : IActorMessage {}

	[Message(HotfixOpcode.C2M_MapUnitUpdate)]
	public partial class C2M_MapUnitUpdate : IActorLocationMessage {}

	[Message(HotfixOpcode.RoomInfo)]
	public partial class RoomInfo {}

	[Message(HotfixOpcode.G2C_ForceDisconnect)]
	public partial class G2C_ForceDisconnect : IActorMessage {}

	[Message(HotfixOpcode.PlayerStateData)]
	public partial class PlayerStateData {}

	[Message(HotfixOpcode.UserInfo)]
	public partial class UserInfo {}

	[Message(HotfixOpcode.AccountInfo)]
	public partial class AccountInfo {}

	[Message(HotfixOpcode.AuthenticationInfo)]
	public partial class AuthenticationInfo {}

	[Message(HotfixOpcode.C2R_Authentication)]
	public partial class C2R_Authentication : IRequest {}

	[Message(HotfixOpcode.R2C_Authentication)]
	public partial class R2C_Authentication : IResponse {}

//錯誤列表
//需要使用的資料
//連接第三方平台
	[Message(HotfixOpcode.C2G_Logout)]
	public partial class C2G_Logout : IRequest {}

	[Message(HotfixOpcode.G2C_Logout)]
	public partial class G2C_Logout : IResponse {}

	[Message(HotfixOpcode.LinkInfo)]
	public partial class LinkInfo {}

	[Message(HotfixOpcode.C2L_Link)]
	public partial class C2L_Link : IActorLocationRequest {}

	[Message(HotfixOpcode.L2C_Link)]
	public partial class L2C_Link : IActorLocationResponse {}

	[Message(HotfixOpcode.C2L_UpdateUserProfile)]
	public partial class C2L_UpdateUserProfile : IActorLocationRequest {}

	[Message(HotfixOpcode.L2C_UpdateUserProfile)]
	public partial class L2C_UpdateUserProfile : IActorLocationResponse {}

	[Message(HotfixOpcode.C2L_UpdateUserLanguage)]
	public partial class C2L_UpdateUserLanguage : IActorLocationRequest {}

	[Message(HotfixOpcode.L2C_UpdateUserLanguage)]
	public partial class L2C_UpdateUserLanguage : IActorLocationResponse {}

	[Message(HotfixOpcode.C2G_LoginGate)]
	public partial class C2G_LoginGate : IRequest {}

	[Message(HotfixOpcode.G2C_LoginGate)]
	public partial class G2C_LoginGate : IResponse {}

	[Message(HotfixOpcode.C2L_SyncPlayerState)]
	public partial class C2L_SyncPlayerState : IActorLocationRequest {}

	[Message(HotfixOpcode.L2C_SyncPlayerState)]
	public partial class L2C_SyncPlayerState : IActorLocationResponse {}

	[Message(HotfixOpcode.L2M_TeamModifyMember)]
	public partial class L2M_TeamModifyMember : IRequest {}

	[Message(HotfixOpcode.M2L_TeamModifyMember)]
	public partial class M2L_TeamModifyMember : IResponse {}

	[Message(HotfixOpcode.L2M_TeamLose)]
	public partial class L2M_TeamLose : IRequest {}

	[Message(HotfixOpcode.M2L_TeamLose)]
	public partial class M2L_TeamLose : IResponse {}

	[Message(HotfixOpcode.C2L_DeleteAccount)]
	public partial class C2L_DeleteAccount : IActorLocationRequest {}

	[Message(HotfixOpcode.L2C_DeleteAccount)]
	public partial class L2C_DeleteAccount : IActorLocationResponse {}

}
namespace ETHotfix
{
	public static partial class HotfixOpcode
	{
		 public const ushort PlayerBaseInfo = 10001;
		 public const ushort G2C_MessageTip = 10002;
		 public const ushort MapUnitInfo = 10003;
		 public const ushort M2C_MapUnitCreate = 10004;
		 public const ushort M2C_MapUnitUpdate = 10005;
		 public const ushort M2C_MapUnitDestroy = 10006;
		 public const ushort C2M_MapUnitUpdate = 10007;
		 public const ushort RoomInfo = 10008;
		 public const ushort G2C_ForceDisconnect = 10009;
		 public const ushort PlayerStateData = 10010;
		 public const ushort UserInfo = 10011;
		 public const ushort AccountInfo = 10012;
		 public const ushort AuthenticationInfo = 10013;
		 public const ushort C2R_Authentication = 10014;
		 public const ushort R2C_Authentication = 10015;
		 public const ushort C2G_Logout = 10016;
		 public const ushort G2C_Logout = 10017;
		 public const ushort LinkInfo = 10018;
		 public const ushort C2L_Link = 10019;
		 public const ushort L2C_Link = 10020;
		 public const ushort C2L_UpdateUserProfile = 10021;
		 public const ushort L2C_UpdateUserProfile = 10022;
		 public const ushort C2L_UpdateUserLanguage = 10023;
		 public const ushort L2C_UpdateUserLanguage = 10024;
		 public const ushort C2G_LoginGate = 10025;
		 public const ushort G2C_LoginGate = 10026;
		 public const ushort C2L_SyncPlayerState = 10027;
		 public const ushort L2C_SyncPlayerState = 10028;
		 public const ushort L2M_TeamModifyMember = 10029;
		 public const ushort M2L_TeamModifyMember = 10030;
		 public const ushort L2M_TeamLose = 10031;
		 public const ushort M2L_TeamLose = 10032;
		 public const ushort C2L_DeleteAccount = 10033;
		 public const ushort L2C_DeleteAccount = 10034;
	}
}
