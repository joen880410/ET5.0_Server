using ETModel;
using System.Collections.Generic;
using MongoDB.Bson;
namespace ETModel
{
	[Message(InnerOpcode.M2A_Reload)]
	public partial class M2A_Reload: IRequest
	{
		public int RpcId { get; set; }

	}

	[Message(InnerOpcode.A2M_Reload)]
	public partial class A2M_Reload: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.G2G_LockRequest)]
	public partial class G2G_LockRequest: IRequest
	{
		public int RpcId { get; set; }

		public long Id { get; set; }

		public string Address { get; set; }

	}

	[Message(InnerOpcode.G2G_LockResponse)]
	public partial class G2G_LockResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.G2G_LockReleaseRequest)]
	public partial class G2G_LockReleaseRequest: IRequest
	{
		public int RpcId { get; set; }

		public long Id { get; set; }

		public string Address { get; set; }

	}

	[Message(InnerOpcode.G2G_LockReleaseResponse)]
	public partial class G2G_LockReleaseResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.DBSaveRequest)]
	public partial class DBSaveRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public ComponentWithId Component { get; set; }

	}

	[Message(InnerOpcode.DBSaveBatchResponse)]
	public partial class DBSaveBatchResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.DBSaveBatchRequest)]
	public partial class DBSaveBatchRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public List<ComponentWithId> Components = new List<ComponentWithId>();

	}

	[Message(InnerOpcode.DBSaveResponse)]
	public partial class DBSaveResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.DBQueryRequest)]
	public partial class DBQueryRequest: IRequest
	{
		public int RpcId { get; set; }

		public long Id { get; set; }

		public string CollectionName { get; set; }

	}

	[Message(InnerOpcode.DBQueryResponse)]
	public partial class DBQueryResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public ComponentWithId Component { get; set; }

	}

	[Message(InnerOpcode.DBQueryBatchRequest)]
	public partial class DBQueryBatchRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public List<long> IdList = new List<long>();

	}

	[Message(InnerOpcode.DBQueryBatchResponse)]
	public partial class DBQueryBatchResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public List<ComponentWithId> Components = new List<ComponentWithId>();

	}

	[Message(InnerOpcode.DBQueryJsonRequest)]
	public partial class DBQueryJsonRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.DBQueryJsonResponse)]
	public partial class DBQueryJsonResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public List<ComponentWithId> Components = new List<ComponentWithId>();

	}

	[Message(InnerOpcode.DBQueryJsonSortRequest)]
	public partial class DBQueryJsonSortRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public string Json { get; set; }

		public string SortJson { get; set; }

		public int Skip { get; set; }

		public int Limit { get; set; }

	}

	[Message(InnerOpcode.DBQueryJsonSortResponse)]
	public partial class DBQueryJsonSortResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public List<ComponentWithId> Components = new List<ComponentWithId>();

	}

	[Message(InnerOpcode.DBQueryCountCommandRequest)]
	public partial class DBQueryCountCommandRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CommandJson { get; set; }

	}

	[Message(InnerOpcode.DBQueryCountCommandResponse)]
	public partial class DBQueryCountCommandResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long Count { get; set; }

	}

	[Message(InnerOpcode.DBQueryCommandRequest)]
	public partial class DBQueryCommandRequest: IRequest
	{
		public int RpcId { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.DBQueryCommandResponse)]
	public partial class DBQueryCommandResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public List<BsonDocument> BsonDocuments = new List<BsonDocument>();

	}

	[Message(InnerOpcode.DBQueryJsonCountRequest)]
	public partial class DBQueryJsonCountRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.DBQueryJsonSkipLimitRequest)]
	public partial class DBQueryJsonSkipLimitRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public string Json { get; set; }

		public long Skip { get; set; }

		public long Limit { get; set; }

	}

	[Message(InnerOpcode.DBQueryJsonCountResponse)]
	public partial class DBQueryJsonCountResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long Count { get; set; }

	}

	[Message(InnerOpcode.DBAggregateJsonRequest)]
	public partial class DBAggregateJsonRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.DBAggregateJsonResponse)]
	public partial class DBAggregateJsonResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public List<BsonDocument> BsonDocuments = new List<BsonDocument>();

	}

	[Message(InnerOpcode.DBDeleteJsonRequest)]
	public partial class DBDeleteJsonRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.DBDeleteJsonResponse)]
	public partial class DBDeleteJsonResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.DBUploadFileRequest)]
	public partial class DBUploadFileRequest: IRequest
	{
		public int RpcId { get; set; }

		public string FileName { get; set; }

		public byte[] Source { get; set; }

		public BsonDocument Meta { get; set; }

	}

	[Message(InnerOpcode.DBUploadFileResponse)]
	public partial class DBUploadFileResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public ObjectId Id { get; set; }

	}

	[Message(InnerOpcode.DBDownloadFileRequest)]
	public partial class DBDownloadFileRequest: IRequest
	{
		public int RpcId { get; set; }

		public ObjectId Id { get; set; }

	}

	[Message(InnerOpcode.DBDownloadFileResponse)]
	public partial class DBDownloadFileResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public byte[] Source { get; set; }

	}

//Cache
//*************Start*************
	[Message(InnerOpcode.CacheQueryByIdRequest)]
	public partial class CacheQueryByIdRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public long Id { get; set; }

		public List<string> Fields = new List<string>();

	}

	[Message(InnerOpcode.CacheQueryByUniqueRequest)]
	public partial class CacheQueryByUniqueRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public string UniqueName { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.CacheCreateRequest)]
	public partial class CacheCreateRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public ComponentWithId Component { get; set; }

	}

	[Message(InnerOpcode.CacheUpdateByIdRequest)]
	public partial class CacheUpdateByIdRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public long Id { get; set; }

		public string DataJson { get; set; }

	}

	[Message(InnerOpcode.CacheQueryResponse)]
	public partial class CacheQueryResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public ComponentWithId Component { get; set; }

	}

	[Message(InnerOpcode.CacheDeleteByIdRequest)]
	public partial class CacheDeleteByIdRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public long Id { get; set; }

	}

	[Message(InnerOpcode.CacheDeleteByUniqueRequest)]
	public partial class CacheDeleteByUniqueRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

		public string UniqueName { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.CacheDeleteResponse)]
	public partial class CacheDeleteResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public bool IsSuccessful { get; set; }

	}

	[Message(InnerOpcode.CacheGetAllIdsRequest)]
	public partial class CacheGetAllIdsRequest: IRequest
	{
		public int RpcId { get; set; }

		public string CollectionName { get; set; }

	}

	[Message(InnerOpcode.CacheGetAllIdsResponse)]
	public partial class CacheGetAllIdsResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public List<long> IdList = new List<long>();

	}

//*************END*************
	[Message(InnerOpcode.ObjectAddRequest)]
	public partial class ObjectAddRequest: IRequest
	{
		public int RpcId { get; set; }

		public long Key { get; set; }

		public long InstanceId { get; set; }

	}

	[Message(InnerOpcode.ObjectAddResponse)]
	public partial class ObjectAddResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectRemoveRequest)]
	public partial class ObjectRemoveRequest: IRequest
	{
		public int RpcId { get; set; }

		public long Key { get; set; }

	}

	[Message(InnerOpcode.ObjectRemoveResponse)]
	public partial class ObjectRemoveResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectLockRequest)]
	public partial class ObjectLockRequest: IRequest
	{
		public int RpcId { get; set; }

		public long Key { get; set; }

		public long InstanceId { get; set; }

		public int Time { get; set; }

	}

	[Message(InnerOpcode.ObjectLockResponse)]
	public partial class ObjectLockResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectUnLockRequest)]
	public partial class ObjectUnLockRequest: IRequest
	{
		public int RpcId { get; set; }

		public long Key { get; set; }

		public long OldInstanceId { get; set; }

		public long InstanceId { get; set; }

	}

	[Message(InnerOpcode.ObjectUnLockResponse)]
	public partial class ObjectUnLockResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectGetRequest)]
	public partial class ObjectGetRequest: IRequest
	{
		public int RpcId { get; set; }

		public long Key { get; set; }

	}

	[Message(InnerOpcode.ObjectGetResponse)]
	public partial class ObjectGetResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long InstanceId { get; set; }

	}

	[Message(InnerOpcode.R2G_GetLoginKey)]
	public partial class R2G_GetLoginKey: IRequest
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

	}

	[Message(InnerOpcode.G2R_GetLoginKey)]
	public partial class G2R_GetLoginKey: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long Key { get; set; }

	}

	[Message(InnerOpcode.L2M_MapUnitCreate)]
	public partial class L2M_MapUnitCreate: IRequest
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

		public long GateSessionId { get; set; }

		public string MapUnitInfo { get; set; }

	}

	[Message(InnerOpcode.M2L_MapUnitCreate)]
	public partial class M2L_MapUnitCreate: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long MapUnitId { get; set; }

		public string MapUnitInfo { get; set; }

	}

	[Message(InnerOpcode.L2M_TeamCreate)]
	public partial class L2M_TeamCreate: IRequest
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

		public string Info { get; set; }

		public string Data { get; set; }

	}

	[Message(InnerOpcode.M2L_TeamCreate)]
	public partial class M2L_TeamCreate: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long RoomId { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.L2M_GetTeamData)]
	public partial class L2M_GetTeamData: IRequest
	{
		public int RpcId { get; set; }

		public long RoomId { get; set; }

	}

	[Message(InnerOpcode.M2L_GetTeamData)]
	public partial class M2L_GetTeamData: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public string RoomJson { get; set; }

		public string TeamData { get; set; }

		public List<string> TeamMember = new List<string>();

	}

	[Message(InnerOpcode.L2M_DestroyRoom)]
	public partial class L2M_DestroyRoom: IRequest
	{
		public int RpcId { get; set; }

		public long RoomId { get; set; }

	}

	[Message(InnerOpcode.M2L_DestroyRoom)]
	public partial class M2L_DestroyRoom: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.L2M_GetTeamMember)]
	public partial class L2M_GetTeamMember: IRequest
	{
		public int RpcId { get; set; }

		public long RoomId { get; set; }

		public long Uid { get; set; }

	}

	[Message(InnerOpcode.M2L_GetTeamMember)]
	public partial class M2L_GetTeamMember: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public string MemberData { get; set; }

	}

	[Message(InnerOpcode.M2L_CreateInvite)]
	public partial class M2L_CreateInvite: IRequest
	{
		public int RpcId { get; set; }

		public string InviteInfo { get; set; }

	}

	[Message(InnerOpcode.L2M_CreateInvite)]
	public partial class L2M_CreateInvite: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long InviteId { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.L2M_DestroyMapUnit)]
	public partial class L2M_DestroyMapUnit: IRequest
	{
		public int RpcId { get; set; }

		public long mapUnitId { get; set; }

	}

	[Message(InnerOpcode.M2L_DestroyMapUnit)]
	public partial class M2L_DestroyMapUnit: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.L2M_TeamLeave)]
	public partial class L2M_TeamLeave: IRequest
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

	}

	[Message(InnerOpcode.M2L_TeamLeave)]
	public partial class M2L_TeamLeave: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.G2L_LobbyUnitCreate)]
	public partial class G2L_LobbyUnitCreate: IRequest
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

		public long GateSessionId { get; set; }

		public int GateAppId { get; set; }

		public int LobbyAppId { get; set; }

	}

	[Message(InnerOpcode.L2G_LobbyUnitCreate)]
	public partial class L2G_LobbyUnitCreate: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long Uid { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.G2L_LobbyUnitUpdate)]
	public partial class G2L_LobbyUnitUpdate: IRequest
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

		public bool IsOnline { get; set; }

	}

	[Message(InnerOpcode.L2G_LobbyUnitUpdate)]
	public partial class L2G_LobbyUnitUpdate: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.G2L_LobbyUnitDestroy)]
	public partial class G2L_LobbyUnitDestroy: IRequest
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

	}

	[Message(InnerOpcode.L2G_LobbyUnitDestroy)]
	public partial class L2G_LobbyUnitDestroy: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.L2M_SessionDisconnect)]
	public partial class L2M_SessionDisconnect: IActorLocationMessage
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

	}

	[Message(InnerOpcode.L2H_SessionDisconnect)]
	public partial class L2H_SessionDisconnect: IMessage
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

	}

	[Message(InnerOpcode.L2D_SessionDisconnect)]
	public partial class L2D_SessionDisconnect: IMessage
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

	}

	[Message(InnerOpcode.R2H_AuthenticateHttp)]
	public partial class R2H_AuthenticateHttp: IRequest
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

		public string Token { get; set; }

	}

	[Message(InnerOpcode.H2R_AuthenticateHttp)]
	public partial class H2R_AuthenticateHttp: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.R2D_AuthenticateHttp)]
	public partial class R2D_AuthenticateHttp: IRequest
	{
		public int RpcId { get; set; }

		public long Uid { get; set; }

		public string Token { get; set; }

	}

	[Message(InnerOpcode.D2R_AuthenticateHttp)]
	public partial class D2R_AuthenticateHttp: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.L2H_CreateLinkHttp)]
	public partial class L2H_CreateLinkHttp: IRequest
	{
		public int RpcId { get; set; }

		public long JacfitUid { get; set; }

		public long JSportUid { get; set; }

	}

	[Message(InnerOpcode.H2L_CreateLinkHttp)]
	public partial class H2L_CreateLinkHttp: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.L2M_GetUserList)]
	public partial class L2M_GetUserList: IRequest
	{
		public int RpcId { get; set; }

		public long RoomId { get; set; }

	}

	[Message(InnerOpcode.M2L_GetUserList)]
	public partial class M2L_GetUserList: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public string info { get; set; }

		public List<long> UserList = new List<long>();

	}

	[Message(InnerOpcode.L2M_AICreate)]
	public partial class L2M_AICreate: IRequest
	{
		public int RpcId { get; set; }

		public long RoomId { get; set; }

	}

	[Message(InnerOpcode.M2L_AICreate)]
	public partial class M2L_AICreate: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.S2M_RegisterService)]
	public partial class S2M_RegisterService: IRequest
	{
		public int RpcId { get; set; }

		public ComponentWithId Component { get; set; }

	}

	[Message(InnerOpcode.M2S_RegisterService)]
	public partial class M2S_RegisterService: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public List<ComponentWithId> Components = new List<ComponentWithId>();

	}

	[Message(InnerOpcode.M2A_RegisterService)]
	public partial class M2A_RegisterService: IMessage
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public ComponentWithId Component { get; set; }

	}

	[Message(InnerOpcode.S2A_ConnectService)]
	public partial class S2A_ConnectService: IRequest
	{
		public int RpcId { get; set; }

	}

	[Message(InnerOpcode.A2S_ConnectService)]
	public partial class A2S_ConnectService: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.S2M_DispatchCommand)]
	public partial class S2M_DispatchCommand: IRequest
	{
		public int RpcId { get; set; }

		public string Command { get; set; }

		public int Apptype { get; set; }

		public int ConsoleService { get; set; }

		public int AppId { get; set; }

	}

	[Message(InnerOpcode.M2S_DispatchCommand)]
	public partial class M2S_DispatchCommand: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.M2A_DispatchCommand)]
	public partial class M2A_DispatchCommand: IRequest
	{
		public int RpcId { get; set; }

		public string Command { get; set; }

	}

	[Message(InnerOpcode.A2M_DispatchCommand)]
	public partial class A2M_DispatchCommand: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.M2Map_RoomExpiredRequest)]
	public partial class M2Map_RoomExpiredRequest: IRequest
	{
		public int RpcId { get; set; }

	}

	[Message(InnerOpcode.Map2M_RoomExpiredResponse)]
	public partial class Map2M_RoomExpiredResponse: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.L2Master_PlanCreate)]
	public partial class L2Master_PlanCreate: IRequest
	{
		public int RpcId { get; set; }

		public long PlanId { get; set; }

	}

	[Message(InnerOpcode.Master2L_PlanCreate)]
	public partial class Master2L_PlanCreate: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.L2Master_PlanRewardSend)]
	public partial class L2Master_PlanRewardSend: IRequest
	{
		public int RpcId { get; set; }

		public long PlanId { get; set; }

	}

	[Message(InnerOpcode.Master2L_PlanRewardSend)]
	public partial class Master2L_PlanRewardSend: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.S2L_LockEvent)]
	public partial class S2L_LockEvent: IRequest
	{
		public int RpcId { get; set; }

		public long Id { get; set; }

		public long Key { get; set; }

		public long Timeout { get; set; }

	}

	[Message(InnerOpcode.L2S_LockEvent)]
	public partial class L2S_LockEvent: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.S2L_UnlockEvent)]
	public partial class S2L_UnlockEvent: IRequest
	{
		public int RpcId { get; set; }

		public long Id { get; set; }

		public long Key { get; set; }

	}

	[Message(InnerOpcode.L2S_UnlockEvent)]
	public partial class L2S_UnlockEvent: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.L2S_ReceiveUnlockEvent)]
	public partial class L2S_ReceiveUnlockEvent: IMessage
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long Key { get; set; }

		public long Id { get; set; }

	}

	[Message(InnerOpcode.L2G_WriteMapUnitInCache)]
	public partial class L2G_WriteMapUnitInCache: IRequest
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public string Json { get; set; }

	}

	[Message(InnerOpcode.G2L_WriteMapUnitInCache)]
	public partial class G2L_WriteMapUnitInCache: IResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

}
