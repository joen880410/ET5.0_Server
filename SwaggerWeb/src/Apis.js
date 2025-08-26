/*  DO NOT EDIT!*/
let RpcId = 0;
module.exports.RpcId = RpcId
module.exports = function (app) {
	 const call = require("./../Client2Server.js");
	 app.post('/UserMap/C2M_MapUnitMoveOnBike', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_MapUnitMoveOnBike' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const DistanceTravelledTarget = req.query.DistanceTravelledTarget
		 /*  #swagger.parameters = ['DistanceTravelledTarget'] = {
			type:'number'
		 }*/
		 const SpeedMS = req.query.SpeedMS
		 /*  #swagger.parameters = ['SpeedMS'] = {
			type:'number'
		 }*/
		 const Calories = req.query.Calories
		 /*  #swagger.parameters = ['Calories'] = {
			type:'number'
		 }*/
		 const Power = req.query.Power
		 /*  #swagger.parameters = ['Power'] = {
			type:'number'
		 }*/
		 const DeviceType = req.query.DeviceType
		 /*  #swagger.parameters = ['DeviceType'] = {
			type:'number'
		 }*/
		 const HeartRate = req.query.HeartRate
		 /*  #swagger.parameters = ['HeartRate'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 DistanceTravelledTarget :Number(DistanceTravelledTarget),
		 SpeedMS :Number(SpeedMS),
		 Calories :Number(Calories),
		 Power :Number(Power),
		 DeviceType :Number(DeviceType),
		 HeartRate :Number(HeartRate),
		 }
		 call.Send("C2M_MapUnitMoveOnBike",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_MapUnitMoveOnRun', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_MapUnitMoveOnRun' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const DistanceTravelledTarget = req.query.DistanceTravelledTarget
		 /*  #swagger.parameters = ['DistanceTravelledTarget'] = {
			type:'number'
		 }*/
		 const SpeedMS = req.query.SpeedMS
		 /*  #swagger.parameters = ['SpeedMS'] = {
			type:'number'
		 }*/
		 const BattleCalories = req.query.BattleCalories
		 /*  #swagger.parameters = ['BattleCalories'] = {
			type:'number'
		 }*/
		 const Cadence = req.query.Cadence
		 /*  #swagger.parameters = ['Cadence'] = {
			type:'number'
		 }*/
		 const Pace = req.query.Pace
		 /*  #swagger.parameters = ['Pace'] = {
			type:'number'
		 }*/
		 const CardioCalories = req.query.CardioCalories
		 /*  #swagger.parameters = ['CardioCalories'] = {
			type:'number'
		 }*/
		 const TotalStepCount = req.query.TotalStepCount
		 /*  #swagger.parameters = ['TotalStepCount'] = {
			type:'number'
		 }*/
		 const DeviceType = req.query.DeviceType
		 /*  #swagger.parameters = ['DeviceType'] = {
			type:'number'
		 }*/
		 const HeartRate = req.query.HeartRate
		 /*  #swagger.parameters = ['HeartRate'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 DistanceTravelledTarget :Number(DistanceTravelledTarget),
		 SpeedMS :Number(SpeedMS),
		 BattleCalories :Number(BattleCalories),
		 Cadence :Number(Cadence),
		 Pace :Number(Pace),
		 CardioCalories :Number(CardioCalories),
		 TotalStepCount :Number(TotalStepCount),
		 DeviceType :Number(DeviceType),
		 HeartRate :Number(HeartRate),
		 }
		 call.Send("C2M_MapUnitMoveOnRun",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_MapUnitOnBoxing', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_MapUnitOnBoxing' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Calories = req.query.Calories
		 /*  #swagger.parameters = ['Calories'] = {
			type:'number'
		 }*/
		 const BoxingData = req.query.BoxingData
		 /*  #swagger.parameters = ['BoxingData'] = {
			type:'object'
		 }*/
		 const Score = req.query.Score
		 /*  #swagger.parameters = ['Score'] = {
			type:'number'
		 }*/
		 const CalculateScore = req.query.CalculateScore
		 /*  #swagger.parameters = ['CalculateScore'] = {
			type:'number'
		 }*/
		 const BoxingCombo = req.query.BoxingCombo
		 /*  #swagger.parameters = ['BoxingCombo'] = {
			type:'number'
		 }*/
		 const HeartRate = req.query.HeartRate
		 /*  #swagger.parameters = ['HeartRate'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Calories :Number(Calories),
		 BoxingData :JSON.parse(BoxingData),
		 Score :Number(Score),
		 CalculateScore :Number(CalculateScore),
		 BoxingCombo :Number(BoxingCombo),
		 HeartRate :Number(HeartRate),
		 }
		 call.Send("C2M_MapUnitOnBoxing",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_MapUnitReceiveReward', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_MapUnitReceiveReward' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ItemIdList = req.query.ItemIdList
		 /*  #swagger.parameters = ['ItemIdList'] = {
			type:'object'
		 }*/
		 const SurpriseId = req.query.SurpriseId
		 /*  #swagger.parameters = ['SurpriseId'] = {
			type:'number'
		 }*/
		 const Count = req.query.Count
		 /*  #swagger.parameters = ['Count'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ItemIdList :JSON.parse(ItemIdList),
		 SurpriseId :Number(SurpriseId),
		 Count :Number(Count),
		 }
		 call.Send("C2M_MapUnitReceiveReward",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_MapUnitReceiveCardio', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_MapUnitReceiveCardio' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2M_MapUnitReceiveCardio",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_LookAtTarget', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_LookAtTarget' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2M_LookAtTarget",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_EnforceSyncMapUnitsInfo', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_EnforceSyncMapUnitsInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const MapUnitIdList = req.query.MapUnitIdList
		 /*  #swagger.parameters = ['MapUnitIdList'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 MapUnitIdList :JSON.parse(MapUnitIdList),
		 }
		 call.Send("C2M_EnforceSyncMapUnitsInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TeamGoBattleRun', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TeamGoBattleRun' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2M_TeamGoBattleRun",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_SaveRideRecord', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_SaveRideRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Note = req.query.Note
		 /*  #swagger.parameters = ['Note'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Note :Note,
		 }
		 call.Send("C2M_SaveRideRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TeamInvite', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TeamInvite' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ReceiverUid = req.query.ReceiverUid
		 /*  #swagger.parameters = ['ReceiverUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ReceiverUid :Number(ReceiverUid),
		 }
		 call.Send("C2M_TeamInvite",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TeamGoBattleProgress', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TeamGoBattleProgress' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Progress = req.query.Progress
		 /*  #swagger.parameters = ['Progress'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Progress :Number(Progress),
		 }
		 call.Send("C2M_TeamGoBattleProgress",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TeamReady', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TeamReady' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const IsReady = req.query.IsReady
		 /*  #swagger.parameters = ['IsReady'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 IsReady :JSON.parse(IsReady),
		 }
		 call.Send("C2M_TeamReady",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TeamRun', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TeamRun' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2M_TeamRun",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TeamKick', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TeamKick' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2M_TeamKick",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TeamDeliveryLeader', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TeamDeliveryLeader' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2M_TeamDeliveryLeader",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TeamDisband', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TeamDisband' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2M_TeamDisband",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ActivityTeamInvite', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ActivityTeamInvite' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2M_ActivityTeamInvite",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2G_SyncPlayerState_Return', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2G_SyncPlayerState_Return' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const IsReturn = req.query.IsReturn
		 /*  #swagger.parameters = ['IsReturn'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 IsReturn :JSON.parse(IsReturn),
		 }
		 call.Send("C2G_SyncPlayerState_Return",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserRealm/C2R_SignUp', (req,res) => {
		 /*  #swagger.tags = ['UserRealm']
			 #swagger.description = 'C2R_SignUp' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Email = req.query.Email
		 /*  #swagger.parameters = ['Email'] = {
			type:'string'
		 }*/
		 const Password = req.query.Password
		 /*  #swagger.parameters = ['Password'] = {
			type:'string'
		 }*/
		 const Secret = req.query.Secret
		 /*  #swagger.parameters = ['Secret'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Email :Email,
		 Password :Password,
		 Secret :Secret,
		 }
		 call.Send("C2R_SignUp",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserRealm/C2R_Authentication', (req,res) => {
		 /*  #swagger.tags = ['UserRealm']
			 #swagger.description = 'C2R_Authentication' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Info = req.query.Info
		 /*  #swagger.parameters = ['Info'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Info :JSON.parse(Info),
		 }
		 call.Send("C2R_Authentication",info).then((result) => {
			 RpcId = result.RpcId 
				 call.Send("C2G_LoginGate",{ RpcId: RpcId, Key: result.Key }).then((result2) => {
			 res.status(200).send({
				 success: "true",
				  Result: result2,
			 });
		 })
	 })
	 })
	 app.post('/UserRealm/C2R_RegisterAccount', (req,res) => {
		 /*  #swagger.tags = ['UserRealm']
			 #swagger.description = 'C2R_RegisterAccount' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Info = req.query.Info
		 /*  #swagger.parameters = ['Info'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Info :Info,
		 }
		 call.Send("C2R_RegisterAccount",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2G_Logout', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2G_Logout' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2G_Logout",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_Link', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_Link' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Info = req.query.Info
		 /*  #swagger.parameters = ['Info'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Info :JSON.parse(Info),
		 }
		 call.Send("C2L_Link",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateUserProfile', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateUserProfile' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Name = req.query.Name
		 /*  #swagger.parameters = ['Name'] = {
			type:'string'
		 }*/
		 const Sex = req.query.Sex
		 /*  #swagger.parameters = ['Sex'] = {
			type:'number'
		 }*/
		 const Height = req.query.Height
		 /*  #swagger.parameters = ['Height'] = {
			type:'number'
		 }*/
		 const Weight = req.query.Weight
		 /*  #swagger.parameters = ['Weight'] = {
			type:'number'
		 }*/
		 const Location = req.query.Location
		 /*  #swagger.parameters = ['Location'] = {
			type:'number'
		 }*/
		 const Birthday = req.query.Birthday
		 /*  #swagger.parameters = ['Birthday'] = {
			type:'number'
		 }*/
		 const Hobby = req.query.Hobby
		 /*  #swagger.parameters = ['Hobby'] = {
			type:'string'
		 }*/
		 const SelfIntroduction = req.query.SelfIntroduction
		 /*  #swagger.parameters = ['SelfIntroduction'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Name :Name,
		 Sex :Number(Sex),
		 Height :Number(Height),
		 Weight :Number(Weight),
		 Location :Number(Location),
		 Birthday :Number(Birthday),
		 Hobby :Hobby,
		 SelfIntroduction :SelfIntroduction,
		 }
		 call.Send("C2L_UpdateUserProfile",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateUserCharacter', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateUserCharacter' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const CharSettingList = req.query.CharSettingList
		 /*  #swagger.parameters = ['CharSettingList'] = {
			type:'object'
		 }*/
		 const LeaderId = req.query.LeaderId
		 /*  #swagger.parameters = ['LeaderId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 CharSettingList :JSON.parse(CharSettingList),
		 LeaderId :Number(LeaderId),
		 }
		 call.Send("C2L_UpdateUserCharacter",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateUserPet', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateUserPet' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetSettingList = req.query.PetSettingList
		 /*  #swagger.parameters = ['PetSettingList'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetSettingList :JSON.parse(PetSettingList),
		 }
		 call.Send("C2L_UpdateUserPet",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateUserLanguage', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateUserLanguage' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Language = req.query.Language
		 /*  #swagger.parameters = ['Language'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Language :Number(Language),
		 }
		 call.Send("C2L_UpdateUserLanguage",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ShareOnSocial', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ShareOnSocial' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SocialType = req.query.SocialType
		 /*  #swagger.parameters = ['SocialType'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SocialType :JSON.parse(SocialType),
		 }
		 call.Send("C2L_ShareOnSocial",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUserAllEquipment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUserAllEquipment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetUserAllEquipment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UnLockUserEquipment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UnLockUserEquipment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ItemId = req.query.ItemId
		 /*  #swagger.parameters = ['ItemId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ItemId :Number(ItemId),
		 }
		 call.Send("C2L_UnLockUserEquipment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_BuyUserEquipment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_BuyUserEquipment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ItemId = req.query.ItemId
		 /*  #swagger.parameters = ['ItemId'] = {
			type:'number'
		 }*/
		 const Count = req.query.Count
		 /*  #swagger.parameters = ['Count'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ItemId :Number(ItemId),
		 Count :Number(Count),
		 }
		 call.Send("C2L_BuyUserEquipment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetRelationshipList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetRelationshipList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetRelationshipList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_AddRelationship', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_AddRelationship' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_AddRelationship",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RemoveRelationship', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RemoveRelationship' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_RemoveRelationship",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RefreshStranger', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RefreshStranger' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Skip = req.query.Skip
		 /*  #swagger.parameters = ['Skip'] = {
			type:'number'
		 }*/
		 const Limit = req.query.Limit
		 /*  #swagger.parameters = ['Limit'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Skip :Number(Skip),
		 Limit :Number(Limit),
		 }
		 call.Send("C2L_RefreshStranger",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetRelationshipApplyList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetRelationshipApplyList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const IsRequested = req.query.IsRequested
		 /*  #swagger.parameters = ['IsRequested'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 IsRequested :JSON.parse(IsRequested),
		 }
		 call.Send("C2L_GetRelationshipApplyList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RefreshRelationshipApply', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RefreshRelationshipApply' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const IsRequested = req.query.IsRequested
		 /*  #swagger.parameters = ['IsRequested'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 IsRequested :JSON.parse(IsRequested),
		 }
		 call.Send("C2L_RefreshRelationshipApply",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_AcceptRelationshipApply', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_AcceptRelationshipApply' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ApplyId = req.query.ApplyId
		 /*  #swagger.parameters = ['ApplyId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ApplyId :Number(ApplyId),
		 }
		 call.Send("C2L_AcceptRelationshipApply",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RefuseRelationshipApply', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RefuseRelationshipApply' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ApplyId = req.query.ApplyId
		 /*  #swagger.parameters = ['ApplyId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ApplyId :Number(ApplyId),
		 }
		 call.Send("C2L_RefuseRelationshipApply",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_QueryRelationshipByUids', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_QueryRelationshipByUids' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uids = req.query.Uids
		 /*  #swagger.parameters = ['Uids'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uids :JSON.parse(Uids),
		 }
		 call.Send("C2L_QueryRelationshipByUids",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_QueryRelationshipByName', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_QueryRelationshipByName' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Name = req.query.Name
		 /*  #swagger.parameters = ['Name'] = {
			type:'string'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Name :Name,
		 Page :Number(Page),
		 }
		 call.Send("C2L_QueryRelationshipByName",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CancelRelationshipApply', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CancelRelationshipApply' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ApplyId = req.query.ApplyId
		 /*  #swagger.parameters = ['ApplyId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ApplyId :Number(ApplyId),
		 }
		 call.Send("C2L_CancelRelationshipApply",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_GiveEmoticon', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_GiveEmoticon' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const MapUnitId = req.query.MapUnitId
		 /*  #swagger.parameters = ['MapUnitId'] = {
			type:'number'
		 }*/
		 const EmoticonIndex = req.query.EmoticonIndex
		 /*  #swagger.parameters = ['EmoticonIndex'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 MapUnitId :Number(MapUnitId),
		 EmoticonIndex :Number(EmoticonIndex),
		 }
		 call.Send("C2M_GiveEmoticon",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_GiveAisatsu', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_GiveAisatsu' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const MapUnitId = req.query.MapUnitId
		 /*  #swagger.parameters = ['MapUnitId'] = {
			type:'number'
		 }*/
		 const Content = req.query.Content
		 /*  #swagger.parameters = ['Content'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 MapUnitId :Number(MapUnitId),
		 Content :Content,
		 }
		 call.Send("C2M_GiveAisatsu",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_AnnouncementNormal', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_AnnouncementNormal' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_AnnouncementNormal",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserRealm/C2R_Login', (req,res) => {
		 /*  #swagger.tags = ['UserRealm']
			 #swagger.description = 'C2R_Login' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Account = req.query.Account
		 /*  #swagger.parameters = ['Account'] = {
			type:'string'
		 }*/
		 const Password = req.query.Password
		 /*  #swagger.parameters = ['Password'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Account :Account,
		 Password :Password,
		 }
		 call.Send("C2R_Login",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2G_LoginGate', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2G_LoginGate' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Key = req.query.Key
		 /*  #swagger.parameters = ['Key'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Key :Number(Key),
		 }
		 call.Send("C2G_LoginGate",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TestActorRequest', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TestActorRequest' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Info = req.query.Info
		 /*  #swagger.parameters = ['Info'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Info :Info,
		 }
		 call.Send("C2M_TestActorRequest",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2G_PlayerInfo', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2G_PlayerInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2G_PlayerInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_SetSportType', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_SetSportType' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const CurrentSportType = req.query.CurrentSportType
		 /*  #swagger.parameters = ['CurrentSportType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 CurrentSportType :Number(CurrentSportType),
		 }
		 call.Send("C2L_SetSportType",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RoamingGetList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RoamingGetList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_RoamingGetList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RoamingEnter', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RoamingEnter' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RoamingRoomId = req.query.RoamingRoomId
		 /*  #swagger.parameters = ['RoamingRoomId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RoamingRoomId :Number(RoamingRoomId),
		 }
		 call.Send("C2L_RoamingEnter",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamGetList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamGetList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const IsReservation = req.query.IsReservation
		 /*  #swagger.parameters = ['IsReservation'] = {
			type:'object'
		 }*/
		 const RoomBattlePlayType = req.query.RoomBattlePlayType
		 /*  #swagger.parameters = ['RoomBattlePlayType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 IsReservation :JSON.parse(IsReservation),
		 RoomBattlePlayType :Number(RoomBattlePlayType),
		 }
		 call.Send("C2L_TeamGetList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamInviteAccept', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamInviteAccept' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const InviteId = req.query.InviteId
		 /*  #swagger.parameters = ['InviteId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 InviteId :Number(InviteId),
		 }
		 call.Send("C2L_TeamInviteAccept",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamEnter', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamEnter' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const TeamRoomId = req.query.TeamRoomId
		 /*  #swagger.parameters = ['TeamRoomId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 TeamRoomId :Number(TeamRoomId),
		 }
		 call.Send("C2L_TeamEnter",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamCreate', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamCreate' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RoadSettingId = req.query.RoadSettingId
		 /*  #swagger.parameters = ['RoadSettingId'] = {
			type:'number'
		 }*/
		 const RoundCount = req.query.RoundCount
		 /*  #swagger.parameters = ['RoundCount'] = {
			type:'number'
		 }*/
		 const RoomBattlePlayType = req.query.RoomBattlePlayType
		 /*  #swagger.parameters = ['RoomBattlePlayType'] = {
			type:'number'
		 }*/
		 const RoomExtraType = req.query.RoomExtraType
		 /*  #swagger.parameters = ['RoomExtraType'] = {
			type:'number'
		 }*/
		 const AIData = req.query.AIData
		 /*  #swagger.parameters = ['AIData'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RoadSettingId :Number(RoadSettingId),
		 RoundCount :Number(RoundCount),
		 RoomBattlePlayType :Number(RoomBattlePlayType),
		 RoomExtraType :Number(RoomExtraType),
		 AIData :JSON.parse(AIData),
		 }
		 call.Send("C2L_TeamCreate",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamReservationJoin', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamReservationJoin' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ReservationId = req.query.ReservationId
		 /*  #swagger.parameters = ['ReservationId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ReservationId :Number(ReservationId),
		 }
		 call.Send("C2L_TeamReservationJoin",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamReservationCreate', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamReservationCreate' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RoadSettingId = req.query.RoadSettingId
		 /*  #swagger.parameters = ['RoadSettingId'] = {
			type:'number'
		 }*/
		 const StartUTCTimeTick = req.query.StartUTCTimeTick
		 /*  #swagger.parameters = ['StartUTCTimeTick'] = {
			type:'number'
		 }*/
		 const RoundCount = req.query.RoundCount
		 /*  #swagger.parameters = ['RoundCount'] = {
			type:'number'
		 }*/
		 const RoomBattlePlayType = req.query.RoomBattlePlayType
		 /*  #swagger.parameters = ['RoomBattlePlayType'] = {
			type:'number'
		 }*/
		 const RoomExtraType = req.query.RoomExtraType
		 /*  #swagger.parameters = ['RoomExtraType'] = {
			type:'number'
		 }*/
		 const AIData = req.query.AIData
		 /*  #swagger.parameters = ['AIData'] = {
			type:'object'
		 }*/
		 const MemberUid = req.query.MemberUid
		 /*  #swagger.parameters = ['MemberUid'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RoadSettingId :Number(RoadSettingId),
		 StartUTCTimeTick :Number(StartUTCTimeTick),
		 RoundCount :Number(RoundCount),
		 RoomBattlePlayType :Number(RoomBattlePlayType),
		 RoomExtraType :Number(RoomExtraType),
		 AIData :JSON.parse(AIData),
		 MemberUid :JSON.parse(MemberUid),
		 }
		 call.Send("C2L_TeamReservationCreate",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamInviteGetList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamInviteGetList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_TeamInviteGetList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamInviteRefuseAll', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamInviteRefuseAll' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_TeamInviteRefuseAll",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamInviteRefuse', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamInviteRefuse' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const InviteId = req.query.InviteId
		 /*  #swagger.parameters = ['InviteId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 InviteId :Number(InviteId),
		 }
		 call.Send("C2L_TeamInviteRefuse",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamReservationCancel', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamReservationCancel' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ReservationId = req.query.ReservationId
		 /*  #swagger.parameters = ['ReservationId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ReservationId :Number(ReservationId),
		 }
		 call.Send("C2L_TeamReservationCancel",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamReservationGetList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamReservationGetList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_TeamReservationGetList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetReservationRoomInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetReservationRoomInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ReservationId = req.query.ReservationId
		 /*  #swagger.parameters = ['ReservationId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ReservationId :Number(ReservationId),
		 }
		 call.Send("C2L_GetReservationRoomInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RoamingLeave', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RoamingLeave' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_RoamingLeave",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TeamLeave', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TeamLeave' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_TeamLeave",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ActivityTeamGetCount', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ActivityTeamGetCount' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_ActivityTeamGetCount",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_SyncPlayerState', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_SyncPlayerState' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const StateData = req.query.StateData
		 /*  #swagger.parameters = ['StateData'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 StateData :JSON.parse(StateData),
		 }
		 call.Send("C2L_SyncPlayerState",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetFileNameOfRideDetailedBriefInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetFileNameOfRideDetailedBriefInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RideRecordId = req.query.RideRecordId
		 /*  #swagger.parameters = ['RideRecordId'] = {
			type:'number'
		 }*/
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RideRecordId :Number(RideRecordId),
		 SportType :Number(SportType),
		 }
		 call.Send("C2L_GetFileNameOfRideDetailedBriefInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSelectDistanceCount', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSelectDistanceCount' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SelectUser = req.query.SelectUser
		 /*  #swagger.parameters = ['SelectUser'] = {
			type:'object'
		 }*/
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SelectUser :JSON.parse(SelectUser),
		 SportType :Number(SportType),
		 }
		 call.Send("C2L_ManagerSelectDistanceCount",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSelectUserCreateCount', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSelectUserCreateCount' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SelectUser = req.query.SelectUser
		 /*  #swagger.parameters = ['SelectUser'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SelectUser :JSON.parse(SelectUser),
		 }
		 call.Send("C2L_ManagerSelectUserCreateCount",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSelectUserTeaminfoCount', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSelectUserTeaminfoCount' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SelectUser = req.query.SelectUser
		 /*  #swagger.parameters = ['SelectUser'] = {
			type:'object'
		 }*/
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SelectUser :JSON.parse(SelectUser),
		 SportType :Number(SportType),
		 }
		 call.Send("C2L_ManagerSelectUserTeaminfoCount",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGift', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGift' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const GiftId = req.query.GiftId
		 /*  #swagger.parameters = ['GiftId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 GiftId :Number(GiftId),
		 }
		 call.Send("C2L_ManagerGift",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerDelete', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerDelete' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const GiftId = req.query.GiftId
		 /*  #swagger.parameters = ['GiftId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 GiftId :Number(GiftId),
		 }
		 call.Send("C2L_ManagerDelete",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSelectUserCreate', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSelectUserCreate' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SelectUser = req.query.SelectUser
		 /*  #swagger.parameters = ['SelectUser'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SelectUser :JSON.parse(SelectUser),
		 }
		 call.Send("C2L_ManagerSelectUserCreate",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSelectUserOnline', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSelectUserOnline' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SelectUser = req.query.SelectUser
		 /*  #swagger.parameters = ['SelectUser'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SelectUser :JSON.parse(SelectUser),
		 }
		 call.Send("C2L_ManagerSelectUserOnline",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSelectDistance', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSelectDistance' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SelectUser = req.query.SelectUser
		 /*  #swagger.parameters = ['SelectUser'] = {
			type:'object'
		 }*/
		 const Type = req.query.Type
		 /*  #swagger.parameters = ['Type'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SelectUser :JSON.parse(SelectUser),
		 Type :Number(Type),
		 }
		 call.Send("C2L_ManagerSelectDistance",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSendAttachment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSendAttachment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uids = req.query.Uids
		 /*  #swagger.parameters = ['Uids'] = {
			type:'object'
		 }*/
		 const Attachments = req.query.Attachments
		 /*  #swagger.parameters = ['Attachments'] = {
			type:'object'
		 }*/
		 const Message = req.query.Message
		 /*  #swagger.parameters = ['Message'] = {
			type:'object'
		 }*/
		 const LanguageType = req.query.LanguageType
		 /*  #swagger.parameters = ['LanguageType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uids :JSON.parse(Uids),
		 Attachments :JSON.parse(Attachments),
		 Message :JSON.parse(Message),
		 LanguageType :Number(LanguageType),
		 }
		 call.Send("C2L_ManagerSendAttachment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSendAttachmentForAllUser', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSendAttachmentForAllUser' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Attachments = req.query.Attachments
		 /*  #swagger.parameters = ['Attachments'] = {
			type:'object'
		 }*/
		 const Message = req.query.Message
		 /*  #swagger.parameters = ['Message'] = {
			type:'object'
		 }*/
		 const LanguageType = req.query.LanguageType
		 /*  #swagger.parameters = ['LanguageType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Attachments :JSON.parse(Attachments),
		 Message :JSON.parse(Message),
		 LanguageType :Number(LanguageType),
		 }
		 call.Send("C2L_ManagerSendAttachmentForAllUser",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSendCoin', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSendCoin' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const Coin = req.query.Coin
		 /*  #swagger.parameters = ['Coin'] = {
			type:'number'
		 }*/
		 const IsSendAllUser = req.query.IsSendAllUser
		 /*  #swagger.parameters = ['IsSendAllUser'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 Coin :Number(Coin),
		 IsSendAllUser :JSON.parse(IsSendAllUser),
		 }
		 call.Send("C2L_ManagerSendCoin",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSelectUser', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSelectUser' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Error = req.query.Error
		 /*  #swagger.parameters = ['Error'] = {
			type:'number'
		 }*/
		 const Message = req.query.Message
		 /*  #swagger.parameters = ['Message'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Error :Number(Error),
		 Message :Message,
		 }
		 call.Send("C2L_ManagerSelectUser",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerSelectUserTeaminfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerSelectUserTeaminfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SelectUser = req.query.SelectUser
		 /*  #swagger.parameters = ['SelectUser'] = {
			type:'object'
		 }*/
		 const Type = req.query.Type
		 /*  #swagger.parameters = ['Type'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SelectUser :JSON.parse(SelectUser),
		 Type :Number(Type),
		 }
		 call.Send("C2L_ManagerSelectUserTeaminfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerServerConfig', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerServerConfig' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Error = req.query.Error
		 /*  #swagger.parameters = ['Error'] = {
			type:'number'
		 }*/
		 const Message = req.query.Message
		 /*  #swagger.parameters = ['Message'] = {
			type:'string'
		 }*/
		 const MessageTip = req.query.MessageTip
		 /*  #swagger.parameters = ['MessageTip'] = {
			type:'string'
		 }*/
		 const Announcement = req.query.Announcement
		 /*  #swagger.parameters = ['Announcement'] = {
			type:'string'
		 }*/
		 const FileName = req.query.FileName
		 /*  #swagger.parameters = ['FileName'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Error :Number(Error),
		 Message :Message,
		 MessageTip :MessageTip,
		 Announcement :Announcement,
		 FileName :FileName,
		 }
		 call.Send("C2L_ManagerServerConfig",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerRoamingGetList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerRoamingGetList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_ManagerRoamingGetList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerTeamGetList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerTeamGetList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 const IsReservation = req.query.IsReservation
		 /*  #swagger.parameters = ['IsReservation'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SportType :Number(SportType),
		 IsReservation :JSON.parse(IsReservation),
		 }
		 call.Send("C2L_ManagerTeamGetList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerTeamPlayerList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerTeamPlayerList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Error = req.query.Error
		 /*  #swagger.parameters = ['Error'] = {
			type:'number'
		 }*/
		 const Message = req.query.Message
		 /*  #swagger.parameters = ['Message'] = {
			type:'string'
		 }*/
		 const RoomId = req.query.RoomId
		 /*  #swagger.parameters = ['RoomId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Error :Number(Error),
		 Message :Message,
		 RoomId :Number(RoomId),
		 }
		 call.Send("C2L_ManagerTeamPlayerList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_SelectUserInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_SelectUserInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlayerUID = req.query.PlayerUID
		 /*  #swagger.parameters = ['PlayerUID'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlayerUID :Number(PlayerUID),
		 }
		 call.Send("C2L_SelectUserInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerFireBase', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerFireBase' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Message = req.query.Message
		 /*  #swagger.parameters = ['Message'] = {
			type:'string'
		 }*/
		 const LanguageType = req.query.LanguageType
		 /*  #swagger.parameters = ['LanguageType'] = {
			type:'number'
		 }*/
		 const IsFirebase = req.query.IsFirebase
		 /*  #swagger.parameters = ['IsFirebase'] = {
			type:'object'
		 }*/
		 const IsMassageTip = req.query.IsMassageTip
		 /*  #swagger.parameters = ['IsMassageTip'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Message :Message,
		 LanguageType :Number(LanguageType),
		 IsFirebase :JSON.parse(IsFirebase),
		 IsMassageTip :JSON.parse(IsMassageTip),
		 }
		 call.Send("C2L_ManagerFireBase",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerUserPetConfigId', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerUserPetConfigId' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_ManagerUserPetConfigId",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerUserPetInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerUserPetInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const PetID = req.query.PetID
		 /*  #swagger.parameters = ['PetID'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 PetID :Number(PetID),
		 }
		 call.Send("C2L_ManagerUserPetInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerUserPetLogs', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerUserPetLogs' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const PetID = req.query.PetID
		 /*  #swagger.parameters = ['PetID'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 PetID :Number(PetID),
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerUserPetLogs",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerUserPetTownLogs', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerUserPetTownLogs' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const PetID = req.query.PetID
		 /*  #swagger.parameters = ['PetID'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 PetID :Number(PetID),
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerUserPetTownLogs",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerAutoBroadcast', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerAutoBroadcast' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Info = req.query.Info
		 /*  #swagger.parameters = ['Info'] = {
			type:'object'
		 }*/
		 const RoomInfos = req.query.RoomInfos
		 /*  #swagger.parameters = ['RoomInfos'] = {
			type:'object'
		 }*/
		 const TaskIds = req.query.TaskIds
		 /*  #swagger.parameters = ['TaskIds'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Info :JSON.parse(Info),
		 RoomInfos :JSON.parse(RoomInfos),
		 TaskIds :JSON.parse(TaskIds),
		 }
		 call.Send("C2L_ManagerAutoBroadcast",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerDeleteAutoBroadcast', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerDeleteAutoBroadcast' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const BroadcastId = req.query.BroadcastId
		 /*  #swagger.parameters = ['BroadcastId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 BroadcastId :Number(BroadcastId),
		 }
		 call.Send("C2L_ManagerDeleteAutoBroadcast",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetAllAutoBroadcast', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetAllAutoBroadcast' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_ManagerGetAllAutoBroadcast",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetUserAllEquipment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetUserAllEquipment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_ManagerGetUserAllEquipment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetUserPostCount', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetUserPostCount' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const FromTime = req.query.FromTime
		 /*  #swagger.parameters = ['FromTime'] = {
			type:'number'
		 }*/
		 const ToTime = req.query.ToTime
		 /*  #swagger.parameters = ['ToTime'] = {
			type:'number'
		 }*/
		 const SelectType = req.query.SelectType
		 /*  #swagger.parameters = ['SelectType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 FromTime :Number(FromTime),
		 ToTime :Number(ToTime),
		 SelectType :Number(SelectType),
		 }
		 call.Send("C2L_ManagerGetUserPostCount",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetUserChannelCount', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetUserChannelCount' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const SelectType = req.query.SelectType
		 /*  #swagger.parameters = ['SelectType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 SelectType :Number(SelectType),
		 Page :Number(Page),
		 ChannelId :Number(ChannelId),
		 }
		 call.Send("C2L_ManagerGetUserChannelCount",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetUserClubCount', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetUserClubCount' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const SelectType = req.query.SelectType
		 /*  #swagger.parameters = ['SelectType'] = {
			type:'number'
		 }*/
		 const FlagType = req.query.FlagType
		 /*  #swagger.parameters = ['FlagType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 SelectType :Number(SelectType),
		 FlagType :Number(FlagType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetUserClubCount",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerUserTownConfigData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerUserTownConfigData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_ManagerUserTownConfigData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerUserTownData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerUserTownData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const TownId = req.query.TownId
		 /*  #swagger.parameters = ['TownId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 TownId :Number(TownId),
		 }
		 call.Send("C2L_ManagerUserTownData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerShopData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerShopData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const ItemType = req.query.ItemType
		 /*  #swagger.parameters = ['ItemType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 ItemType :Number(ItemType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerShopData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerFarmData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerFarmData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_ManagerFarmData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreEpisodeData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreEpisodeData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const MapId = req.query.MapId
		 /*  #swagger.parameters = ['MapId'] = {
			type:'number'
		 }*/
		 const StartTime = req.query.StartTime
		 /*  #swagger.parameters = ['StartTime'] = {
			type:'number'
		 }*/
		 const EndTime = req.query.EndTime
		 /*  #swagger.parameters = ['EndTime'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 MapId :Number(MapId),
		 StartTime :Number(StartTime),
		 EndTime :Number(EndTime),
		 }
		 call.Send("C2L_ManagerExploreEpisodeData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreRecordData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreRecordData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const EpisodeId = req.query.EpisodeId
		 /*  #swagger.parameters = ['EpisodeId'] = {
			type:'number'
		 }*/
		 const StartTime = req.query.StartTime
		 /*  #swagger.parameters = ['StartTime'] = {
			type:'number'
		 }*/
		 const EndTime = req.query.EndTime
		 /*  #swagger.parameters = ['EndTime'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 EpisodeId :Number(EpisodeId),
		 StartTime :Number(StartTime),
		 EndTime :Number(EndTime),
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerExploreRecordData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreEpisodeConfigData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreEpisodeConfigData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const EpisodeType = req.query.EpisodeType
		 /*  #swagger.parameters = ['EpisodeType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 EpisodeType :Number(EpisodeType),
		 }
		 call.Send("C2L_ManagerExploreEpisodeConfigData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreDialogStatisticsData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreDialogStatisticsData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const EpisodeId = req.query.EpisodeId
		 /*  #swagger.parameters = ['EpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 EpisodeId :Number(EpisodeId),
		 }
		 call.Send("C2L_ManagerExploreDialogStatisticsData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreQuizStatisticsData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreQuizStatisticsData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const EpisodeId = req.query.EpisodeId
		 /*  #swagger.parameters = ['EpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 EpisodeId :Number(EpisodeId),
		 }
		 call.Send("C2L_ManagerExploreQuizStatisticsData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreRockAndPaperAndScissorsStatisticsData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreRockAndPaperAndScissorsStatisticsData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const EpisodeId = req.query.EpisodeId
		 /*  #swagger.parameters = ['EpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 EpisodeId :Number(EpisodeId),
		 }
		 call.Send("C2L_ManagerExploreRockAndPaperAndScissorsStatisticsData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreDetonateBombStatisticsData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreDetonateBombStatisticsData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const EpisodeId = req.query.EpisodeId
		 /*  #swagger.parameters = ['EpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 EpisodeId :Number(EpisodeId),
		 }
		 call.Send("C2L_ManagerExploreDetonateBombStatisticsData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreLittleGameStatisticsData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreLittleGameStatisticsData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const EpisodeId = req.query.EpisodeId
		 /*  #swagger.parameters = ['EpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 EpisodeId :Number(EpisodeId),
		 }
		 call.Send("C2L_ManagerExploreLittleGameStatisticsData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreMapConfigData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreMapConfigData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_ManagerExploreMapConfigData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerExploreDoorStatisticsData', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerExploreDoorStatisticsData' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const MapId = req.query.MapId
		 /*  #swagger.parameters = ['MapId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 MapId :Number(MapId),
		 }
		 call.Send("C2L_ManagerExploreDoorStatisticsData",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_SendMail', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_SendMail' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Email = req.query.Email
		 /*  #swagger.parameters = ['Email'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Email :JSON.parse(Email),
		 }
		 call.Send("C2L_SendMail",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetMail', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetMail' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlayerUID = req.query.PlayerUID
		 /*  #swagger.parameters = ['PlayerUID'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlayerUID :Number(PlayerUID),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetMail",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetRewardAttachment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetRewardAttachment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const EmailId = req.query.EmailId
		 /*  #swagger.parameters = ['EmailId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 EmailId :Number(EmailId),
		 }
		 call.Send("C2L_GetRewardAttachment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_AcceptInviteAttachment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_AcceptInviteAttachment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const EmailId = req.query.EmailId
		 /*  #swagger.parameters = ['EmailId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 EmailId :Number(EmailId),
		 }
		 call.Send("C2L_AcceptInviteAttachment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RefuseInviteAttachment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RefuseInviteAttachment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const EmailId = req.query.EmailId
		 /*  #swagger.parameters = ['EmailId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 EmailId :Number(EmailId),
		 }
		 call.Send("C2L_RefuseInviteAttachment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_IsReadMail', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_IsReadMail' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Email = req.query.Email
		 /*  #swagger.parameters = ['Email'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Email :JSON.parse(Email),
		 }
		 call.Send("C2L_IsReadMail",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DeleteMail', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DeleteMail' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Email = req.query.Email
		 /*  #swagger.parameters = ['Email'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Email :JSON.parse(Email),
		 }
		 call.Send("C2L_DeleteMail",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_TeamChat', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_TeamChat' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Chat = req.query.Chat
		 /*  #swagger.parameters = ['Chat'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Chat :JSON.parse(Chat),
		 }
		 call.Send("C2M_TeamChat",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateAchieveLeader', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateAchieveLeader' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const MedalLeaderId = req.query.MedalLeaderId
		 /*  #swagger.parameters = ['MedalLeaderId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 MedalLeaderId :Number(MedalLeaderId),
		 }
		 call.Send("C2L_UpdateAchieveLeader",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetReward', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetReward' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const TaskId = req.query.TaskId
		 /*  #swagger.parameters = ['TaskId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 TaskId :Number(TaskId),
		 }
		 call.Send("C2L_GetReward",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetTaskList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetTaskList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetTaskList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetAllTopLevelAchieve', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetAllTopLevelAchieve' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetAllTopLevelAchieve",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UnLockScene', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UnLockScene' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SceneId = req.query.SceneId
		 /*  #swagger.parameters = ['SceneId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SceneId :Number(SceneId),
		 }
		 call.Send("C2L_UnLockScene",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUserAllScene', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUserAllScene' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetUserAllScene",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UnLockSticker', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UnLockSticker' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const StickerId = req.query.StickerId
		 /*  #swagger.parameters = ['StickerId'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 StickerId :JSON.parse(StickerId),
		 }
		 call.Send("C2L_UnLockSticker",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUserAllSticker', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUserAllSticker' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetUserAllSticker",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UnlockCharacter', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UnlockCharacter' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const CharacterId = req.query.CharacterId
		 /*  #swagger.parameters = ['CharacterId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 CharacterId :Number(CharacterId),
		 }
		 call.Send("C2L_UnlockCharacter",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUserAllCharacter', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUserAllCharacter' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetUserAllCharacter",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetPlayerCharacter', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetPlayerCharacter' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetPlayerCharacter",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetPlayerPet', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetPlayerPet' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetPlayerPet",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_EvolveOnePet', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_EvolveOnePet' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetId :Number(PetId),
		 }
		 call.Send("C2L_EvolveOnePet",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DevolveOnePet', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DevolveOnePet' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetId :Number(PetId),
		 }
		 call.Send("C2L_DevolveOnePet",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UseFoodUpdatePetNumerical', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UseFoodUpdatePetNumerical' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 const PetFeedId = req.query.PetFeedId
		 /*  #swagger.parameters = ['PetFeedId'] = {
			type:'number'
		 }*/
		 const Count = req.query.Count
		 /*  #swagger.parameters = ['Count'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 PetId :Number(PetId),
		 PetFeedId :Number(PetFeedId),
		 Count :Number(Count),
		 }
		 call.Send("C2L_UseFoodUpdatePetNumerical",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UseTouchUpdatePetIntimate', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UseTouchUpdatePetIntimate' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 PetId :Number(PetId),
		 }
		 call.Send("C2L_UseTouchUpdatePetIntimate",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UsePrankUpdatePetNumerical', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UsePrankUpdatePetNumerical' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 PetId :Number(PetId),
		 }
		 call.Send("C2L_UsePrankUpdatePetNumerical",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UseMakeUpTicketUpdatePetFurColor', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UseMakeUpTicketUpdatePetFurColor' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 const PetFurItemId = req.query.PetFurItemId
		 /*  #swagger.parameters = ['PetFurItemId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetId :Number(PetId),
		 PetFurItemId :Number(PetFurItemId),
		 }
		 call.Send("C2L_UseMakeUpTicketUpdatePetFurColor",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdatePetNickName', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdatePetNickName' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 const NickName = req.query.NickName
		 /*  #swagger.parameters = ['NickName'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetId :Number(PetId),
		 NickName :NickName,
		 }
		 call.Send("C2L_UpdatePetNickName",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UnlockPet', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UnlockPet' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetId :Number(PetId),
		 }
		 call.Send("C2L_UnlockPet",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UnlockPetTown', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UnlockPetTown' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SceneId = req.query.SceneId
		 /*  #swagger.parameters = ['SceneId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SceneId :Number(SceneId),
		 }
		 call.Send("C2L_UnlockPetTown",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetOwnedPetTown', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetOwnedPetTown' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetOwnedPetTown",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUserAllPet', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUserAllPet' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetUserAllPet",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetPetItems', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetPetItems' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetPetItems",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetPetLogs', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetPetLogs' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetId :Number(PetId),
		 Uid :Number(Uid),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetPetLogs",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_MoveToPetTown', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_MoveToPetTown' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_MoveToPetTown",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_LeaveToPetTown', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_LeaveToPetTown' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_LeaveToPetTown",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetPetTownLogs', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetPetTownLogs' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetPetTownLogs",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateVisitPetTown', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateVisitPetTown' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetTownId = req.query.PetTownId
		 /*  #swagger.parameters = ['PetTownId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetTownId :Number(PetTownId),
		 }
		 call.Send("C2L_UpdateVisitPetTown",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_EditorPetsOnPetTown', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_EditorPetsOnPetTown' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetIds = req.query.PetIds
		 /*  #swagger.parameters = ['PetIds'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetIds :JSON.parse(PetIds),
		 }
		 call.Send("C2L_EditorPetsOnPetTown",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_PetTownSendEvaluation', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_PetTownSendEvaluation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const TownOwnerUid = req.query.TownOwnerUid
		 /*  #swagger.parameters = ['TownOwnerUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 TownOwnerUid :Number(TownOwnerUid),
		 }
		 call.Send("C2L_PetTownSendEvaluation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RecyclePetsInBag', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RecyclePetsInBag' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetIds = req.query.PetIds
		 /*  #swagger.parameters = ['PetIds'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetIds :JSON.parse(PetIds),
		 }
		 call.Send("C2L_RecyclePetsInBag",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_EditorDecorationsOnPetTown', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_EditorDecorationsOnPetTown' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const TownDecorationInfos = req.query.TownDecorationInfos
		 /*  #swagger.parameters = ['TownDecorationInfos'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 TownDecorationInfos :JSON.parse(TownDecorationInfos),
		 }
		 call.Send("C2L_EditorDecorationsOnPetTown",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RecycleDecorationsInBag', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RecycleDecorationsInBag' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const DecorationIds = req.query.DecorationIds
		 /*  #swagger.parameters = ['DecorationIds'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 DecorationIds :JSON.parse(DecorationIds),
		 }
		 call.Send("C2L_RecycleDecorationsInBag",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateFixFacilitiesStyle', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateFixFacilitiesStyle' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const FixTownFacilityInfos = req.query.FixTownFacilityInfos
		 /*  #swagger.parameters = ['FixTownFacilityInfos'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 FixTownFacilityInfos :JSON.parse(FixTownFacilityInfos),
		 }
		 call.Send("C2L_UpdateFixFacilitiesStyle",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_BuyPetItem', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_BuyPetItem' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ItemId = req.query.ItemId
		 /*  #swagger.parameters = ['ItemId'] = {
			type:'number'
		 }*/
		 const Count = req.query.Count
		 /*  #swagger.parameters = ['Count'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ItemId :Number(ItemId),
		 Count :Number(Count),
		 }
		 call.Send("C2L_BuyPetItem",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_SellPetItem', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_SellPetItem' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ItemId = req.query.ItemId
		 /*  #swagger.parameters = ['ItemId'] = {
			type:'number'
		 }*/
		 const Count = req.query.Count
		 /*  #swagger.parameters = ['Count'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ItemId :Number(ItemId),
		 Count :Number(Count),
		 }
		 call.Send("C2L_SellPetItem",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_MoveToPetFarm', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_MoveToPetFarm' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_MoveToPetFarm",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_LeaveToPetFarm', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_LeaveToPetFarm' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_LeaveToPetFarm",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_PlantTheSeed', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_PlantTheSeed' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const PetFarmLandId = req.query.PetFarmLandId
		 /*  #swagger.parameters = ['PetFarmLandId'] = {
			type:'number'
		 }*/
		 const SeedItemId = req.query.SeedItemId
		 /*  #swagger.parameters = ['SeedItemId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 PetFarmLandId :Number(PetFarmLandId),
		 SeedItemId :Number(SeedItemId),
		 }
		 call.Send("C2L_PlantTheSeed",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_WaterPetLand', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_WaterPetLand' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const PetFarmLandId = req.query.PetFarmLandId
		 /*  #swagger.parameters = ['PetFarmLandId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 PetFarmLandId :Number(PetFarmLandId),
		 }
		 call.Send("C2L_WaterPetLand",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_HarvestPetCrop', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_HarvestPetCrop' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const PetFarmLandId = req.query.PetFarmLandId
		 /*  #swagger.parameters = ['PetFarmLandId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 PetFarmLandId :Number(PetFarmLandId),
		 }
		 call.Send("C2L_HarvestPetCrop",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetPetExploreMaps', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetPetExploreMaps' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetPetExploreMaps",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateExplorePet', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateExplorePet' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PetId = req.query.PetId
		 /*  #swagger.parameters = ['PetId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PetId :Number(PetId),
		 }
		 call.Send("C2L_UpdateExplorePet",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_MoveToPetExploreMap', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_MoveToPetExploreMap' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreId = req.query.ExploreId
		 /*  #swagger.parameters = ['ExploreId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreId :Number(ExploreId),
		 }
		 call.Send("C2L_MoveToPetExploreMap",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_LeaveToPetExploreMap', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_LeaveToPetExploreMap' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RoomId = req.query.RoomId
		 /*  #swagger.parameters = ['RoomId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RoomId :Number(RoomId),
		 }
		 call.Send("C2M_LeaveToPetExploreMap",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_MoveToNextPetExploreEpisode', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_MoveToNextPetExploreEpisode' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const DoorId = req.query.DoorId
		 /*  #swagger.parameters = ['DoorId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 DoorId :Number(DoorId),
		 }
		 call.Send("C2M_MoveToNextPetExploreEpisode",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreQuizAnimation', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreQuizAnimation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreQuizAnimation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreQuizEpisode', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreQuizEpisode' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const Answer = req.query.Answer
		 /*  #swagger.parameters = ['Answer'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 Answer :Number(Answer),
		 }
		 call.Send("C2M_ReplyPetExploreQuizEpisode",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreDialogEpisode', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreDialogEpisode' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreDialogEpisode",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreTurnBaseAnimation', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreTurnBaseAnimation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreTurnBaseAnimation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreGamebleAnimation', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreGamebleAnimation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreGamebleAnimation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreRockAndPaperAndScissorsStart', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreRockAndPaperAndScissorsStart' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const PlayExType = req.query.PlayExType
		 /*  #swagger.parameters = ['PlayExType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 PlayExType :Number(PlayExType),
		 }
		 call.Send("C2M_ReplyPetExploreRockAndPaperAndScissorsStart",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreRockAndPaperAndScissors', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreRockAndPaperAndScissors' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const PlayerOutPunch = req.query.PlayerOutPunch
		 /*  #swagger.parameters = ['PlayerOutPunch'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 PlayerOutPunch :Number(PlayerOutPunch),
		 }
		 call.Send("C2M_ReplyPetExploreRockAndPaperAndScissors",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreRoulette', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreRoulette' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreRoulette",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreDiceBetSize', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreDiceBetSize' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreDiceBetSize",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreScratch', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreScratch' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreScratch",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreSlot', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreSlot' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreSlot",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExplorePassWord', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExplorePassWord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const PlayerPassWord = req.query.PlayerPassWord
		 /*  #swagger.parameters = ['PlayerPassWord'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 PlayerPassWord :Number(PlayerPassWord),
		 }
		 call.Send("C2M_ReplyPetExplorePassWord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreDetonateBomb', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreDetonateBomb' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const PlayerCountDown = req.query.PlayerCountDown
		 /*  #swagger.parameters = ['PlayerCountDown'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 PlayerCountDown :Number(PlayerCountDown),
		 }
		 call.Send("C2M_ReplyPetExploreDetonateBomb",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExplorePassBomb', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExplorePassBomb' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const HitCount = req.query.HitCount
		 /*  #swagger.parameters = ['HitCount'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 HitCount :Number(HitCount),
		 }
		 call.Send("C2M_ReplyPetExplorePassBomb",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreTugOfWar', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreTugOfWar' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const RopePosition = req.query.RopePosition
		 /*  #swagger.parameters = ['RopePosition'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 RopePosition :Number(RopePosition),
		 }
		 call.Send("C2M_ReplyPetExploreTugOfWar",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreCardCompare', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreCardCompare' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const PointCompareType = req.query.PointCompareType
		 /*  #swagger.parameters = ['PointCompareType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 PointCompareType :Number(PointCompareType),
		 }
		 call.Send("C2M_ReplyPetExploreCardCompare",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreStartGuessBallInCup', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreStartGuessBallInCup' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreStartGuessBallInCup",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreFindBallInCup', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreFindBallInCup' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const IsAnswerCup = req.query.IsAnswerCup
		 /*  #swagger.parameters = ['IsAnswerCup'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 IsAnswerCup :JSON.parse(IsAnswerCup),
		 }
		 call.Send("C2M_ReplyPetExploreFindBallInCup",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreFortuneStick', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreFortuneStick' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 }
		 call.Send("C2M_ReplyPetExploreFortuneStick",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ReplyPetExploreLadderLottery', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ReplyPetExploreLadderLottery' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ExploreEpisodeId = req.query.ExploreEpisodeId
		 /*  #swagger.parameters = ['ExploreEpisodeId'] = {
			type:'number'
		 }*/
		 const Order = req.query.Order
		 /*  #swagger.parameters = ['Order'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ExploreEpisodeId :Number(ExploreEpisodeId),
		 Order :Number(Order),
		 }
		 call.Send("C2M_ReplyPetExploreLadderLottery",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserMap/C2M_ChangeBattleRecordNote', (req,res) => {
		 /*  #swagger.tags = ['UserMap']
			 #swagger.description = 'C2M_ChangeBattleRecordNote' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const RecordId = req.query.RecordId
		 /*  #swagger.parameters = ['RecordId'] = {
			type:'number'
		 }*/
		 const Note = req.query.Note
		 /*  #swagger.parameters = ['Note'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 RecordId :Number(RecordId),
		 Note :Note,
		 }
		 call.Send("C2M_ChangeBattleRecordNote",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ChangeLobbyRecordNote', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ChangeLobbyRecordNote' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RecordId = req.query.RecordId
		 /*  #swagger.parameters = ['RecordId'] = {
			type:'number'
		 }*/
		 const Note = req.query.Note
		 /*  #swagger.parameters = ['Note'] = {
			type:'string'
		 }*/
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RecordId :Number(RecordId),
		 Note :Note,
		 SportType :Number(SportType),
		 }
		 call.Send("C2L_ChangeLobbyRecordNote",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_TagRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_TagRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RecordId = req.query.RecordId
		 /*  #swagger.parameters = ['RecordId'] = {
			type:'number'
		 }*/
		 const Tag = req.query.Tag
		 /*  #swagger.parameters = ['Tag'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RecordId :Number(RecordId),
		 Tag :Number(Tag),
		 }
		 call.Send("C2L_TagRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityHomePagePlayerIdentityCard', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityHomePagePlayerIdentityCard' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetCommunityHomePagePlayerIdentityCard",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityHomePagePlayerBaseInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityHomePagePlayerBaseInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetCommunityHomePagePlayerBaseInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityHomePagePlayerSportRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityHomePagePlayerSportRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetCommunityHomePagePlayerSportRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityHomePageVisibility', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityHomePageVisibility' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetCommunityHomePageVisibility",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ChangeCommunityHomePageVisibility', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ChangeCommunityHomePageVisibility' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlayerHomePageInfoVisibility = req.query.PlayerHomePageInfoVisibility
		 /*  #swagger.parameters = ['PlayerHomePageInfoVisibility'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlayerHomePageInfoVisibility :Number(PlayerHomePageInfoVisibility),
		 }
		 call.Send("C2L_ChangeCommunityHomePageVisibility",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ChangeFurnitureSetting', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ChangeFurnitureSetting' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlayerFurnitureSetting = req.query.PlayerFurnitureSetting
		 /*  #swagger.parameters = ['PlayerFurnitureSetting'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlayerFurnitureSetting :JSON.parse(PlayerFurnitureSetting),
		 }
		 call.Send("C2L_ChangeFurnitureSetting",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UnlockFurnitureSetting', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UnlockFurnitureSetting' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ItemId = req.query.ItemId
		 /*  #swagger.parameters = ['ItemId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ItemId :Number(ItemId),
		 }
		 call.Send("C2L_UnlockFurnitureSetting",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityMoodPost', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityMoodPost' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PostId = req.query.PostId
		 /*  #swagger.parameters = ['PostId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PostId :Number(PostId),
		 }
		 call.Send("C2L_GetCommunityMoodPost",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetFriendCommunityMoodPost', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetFriendCommunityMoodPost' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetFriendCommunityMoodPost",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityMoodPostComment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityMoodPostComment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PostId = req.query.PostId
		 /*  #swagger.parameters = ['PostId'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PostId :Number(PostId),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetCommunityMoodPostComment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ChangeCommunityMoodPost', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ChangeCommunityMoodPost' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Post = req.query.Post
		 /*  #swagger.parameters = ['Post'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Post :Post,
		 }
		 call.Send("C2L_ChangeCommunityMoodPost",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreateCommunityMoodPost', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreateCommunityMoodPost' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Post = req.query.Post
		 /*  #swagger.parameters = ['Post'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Post :Post,
		 }
		 call.Send("C2L_CreateCommunityMoodPost",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreateCommunityMoodPostComment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreateCommunityMoodPostComment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PostId = req.query.PostId
		 /*  #swagger.parameters = ['PostId'] = {
			type:'number'
		 }*/
		 const Comment = req.query.Comment
		 /*  #swagger.parameters = ['Comment'] = {
			type:'string'
		 }*/
		 const StickerId = req.query.StickerId
		 /*  #swagger.parameters = ['StickerId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PostId :Number(PostId),
		 Comment :Comment,
		 StickerId :Number(StickerId),
		 }
		 call.Send("C2L_CreateCommunityMoodPostComment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DeleteCommunityMoodPost', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DeleteCommunityMoodPost' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PostId = req.query.PostId
		 /*  #swagger.parameters = ['PostId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PostId :Number(PostId),
		 }
		 call.Send("C2L_DeleteCommunityMoodPost",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DeleteCommunityMoodPostComment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DeleteCommunityMoodPostComment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PostId = req.query.PostId
		 /*  #swagger.parameters = ['PostId'] = {
			type:'number'
		 }*/
		 const CommentId = req.query.CommentId
		 /*  #swagger.parameters = ['CommentId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PostId :Number(PostId),
		 CommentId :Number(CommentId),
		 }
		 call.Send("C2L_DeleteCommunityMoodPostComment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ChangeCommunityMoodPostSensibility', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ChangeCommunityMoodPostSensibility' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PostId = req.query.PostId
		 /*  #swagger.parameters = ['PostId'] = {
			type:'number'
		 }*/
		 const Sensibility = req.query.Sensibility
		 /*  #swagger.parameters = ['Sensibility'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PostId :Number(PostId),
		 Sensibility :Number(Sensibility),
		 }
		 call.Send("C2L_ChangeCommunityMoodPostSensibility",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ChangeCommunityMoodCommentSensibility', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ChangeCommunityMoodCommentSensibility' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PostId = req.query.PostId
		 /*  #swagger.parameters = ['PostId'] = {
			type:'number'
		 }*/
		 const CommentId = req.query.CommentId
		 /*  #swagger.parameters = ['CommentId'] = {
			type:'number'
		 }*/
		 const Sensibility = req.query.Sensibility
		 /*  #swagger.parameters = ['Sensibility'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PostId :Number(PostId),
		 CommentId :Number(CommentId),
		 Sensibility :Number(Sensibility),
		 }
		 call.Send("C2L_ChangeCommunityMoodCommentSensibility",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityMoodSmallRoom', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityMoodSmallRoom' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const TargetUid = req.query.TargetUid
		 /*  #swagger.parameters = ['TargetUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 TargetUid :Number(TargetUid),
		 }
		 call.Send("C2L_GetCommunityMoodSmallRoom",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ChangeCommunityMoodInvitation', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ChangeCommunityMoodInvitation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const TargetUid = req.query.TargetUid
		 /*  #swagger.parameters = ['TargetUid'] = {
			type:'number'
		 }*/
		 const Invitation = req.query.Invitation
		 /*  #swagger.parameters = ['Invitation'] = {
			type:'number'
		 }*/
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 TargetUid :Number(TargetUid),
		 Invitation :Number(Invitation),
		 ClubId :Number(ClubId),
		 }
		 call.Send("C2L_ChangeCommunityMoodInvitation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClub', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClub' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetCommunityClub",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreateCommunityClub', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreateCommunityClub' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ConfigId = req.query.ConfigId
		 /*  #swagger.parameters = ['ConfigId'] = {
			type:'number'
		 }*/
		 const ClubName = req.query.ClubName
		 /*  #swagger.parameters = ['ClubName'] = {
			type:'string'
		 }*/
		 const Introduction = req.query.Introduction
		 /*  #swagger.parameters = ['Introduction'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ConfigId :Number(ConfigId),
		 ClubName :ClubName,
		 Introduction :Introduction,
		 }
		 call.Send("C2L_CreateCommunityClub",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateCommunityClub', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateCommunityClub' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const ConfigId = req.query.ConfigId
		 /*  #swagger.parameters = ['ConfigId'] = {
			type:'number'
		 }*/
		 const ClubName = req.query.ClubName
		 /*  #swagger.parameters = ['ClubName'] = {
			type:'string'
		 }*/
		 const Introduction = req.query.Introduction
		 /*  #swagger.parameters = ['Introduction'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 ConfigId :Number(ConfigId),
		 ClubName :ClubName,
		 Introduction :Introduction,
		 }
		 call.Send("C2L_UpdateCommunityClub",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DisbandCommunityClub', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DisbandCommunityClub' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 }
		 call.Send("C2L_DisbandCommunityClub",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_QueryCommunityClub', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_QueryCommunityClub' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubName = req.query.ClubName
		 /*  #swagger.parameters = ['ClubName'] = {
			type:'string'
		 }*/
		 const ClubType = req.query.ClubType
		 /*  #swagger.parameters = ['ClubType'] = {
			type:'number'
		 }*/
		 const HotType = req.query.HotType
		 /*  #swagger.parameters = ['HotType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubName :ClubName,
		 ClubType :Number(ClubType),
		 HotType :Number(HotType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_QueryCommunityClub",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_JoinCommunityClub', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_JoinCommunityClub' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 }
		 call.Send("C2L_JoinCommunityClub",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_LeaveCommunityClub', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_LeaveCommunityClub' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 }
		 call.Send("C2L_LeaveCommunityClub",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClubMember', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClubMember' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetCommunityClubMember",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetMyAllCommunityClubMember', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetMyAllCommunityClubMember' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const UId = req.query.UId
		 /*  #swagger.parameters = ['UId'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 UId :Number(UId),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetMyAllCommunityClubMember",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClubMemberCommentRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClubMemberCommentRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const StartAt = req.query.StartAt
		 /*  #swagger.parameters = ['StartAt'] = {
			type:'number'
		 }*/
		 const EndAt = req.query.EndAt
		 /*  #swagger.parameters = ['EndAt'] = {
			type:'number'
		 }*/
		 const Skip = req.query.Skip
		 /*  #swagger.parameters = ['Skip'] = {
			type:'number'
		 }*/
		 const Limit = req.query.Limit
		 /*  #swagger.parameters = ['Limit'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 StartAt :Number(StartAt),
		 EndAt :Number(EndAt),
		 Skip :Number(Skip),
		 Limit :Number(Limit),
		 }
		 call.Send("C2L_GetCommunityClubMemberCommentRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreateCommunityClubMemberCommentRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreateCommunityClubMemberCommentRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const Content = req.query.Content
		 /*  #swagger.parameters = ['Content'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 Content :JSON.parse(Content),
		 }
		 call.Send("C2L_CreateCommunityClubMemberCommentRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DeleteCommunityClubMemberCommentRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DeleteCommunityClubMemberCommentRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const CommentId = req.query.CommentId
		 /*  #swagger.parameters = ['CommentId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 CommentId :Number(CommentId),
		 }
		 call.Send("C2L_DeleteCommunityClubMemberCommentRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetDeletedCommunityClubCommentRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetDeletedCommunityClubCommentRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 }
		 call.Send("C2L_GetDeletedCommunityClubCommentRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClubInvitationApply', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClubInvitationApply' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetCommunityClubInvitationApply",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_InviteUserAsCommunityClubMember', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_InviteUserAsCommunityClubMember' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const TargetUid = req.query.TargetUid
		 /*  #swagger.parameters = ['TargetUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 TargetUid :Number(TargetUid),
		 }
		 call.Send("C2L_InviteUserAsCommunityClubMember",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_InviteUsersAsCommunityClubMembers', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_InviteUsersAsCommunityClubMembers' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const Uids = req.query.Uids
		 /*  #swagger.parameters = ['Uids'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 Uids :JSON.parse(Uids),
		 }
		 call.Send("C2L_InviteUsersAsCommunityClubMembers",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_AcceptCommunityClubInvitationApply', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_AcceptCommunityClubInvitationApply' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const TargetUid = req.query.TargetUid
		 /*  #swagger.parameters = ['TargetUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 TargetUid :Number(TargetUid),
		 }
		 call.Send("C2L_AcceptCommunityClubInvitationApply",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_RefuseCommunityClubInvitationApply', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_RefuseCommunityClubInvitationApply' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const TargetUid = req.query.TargetUid
		 /*  #swagger.parameters = ['TargetUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 TargetUid :Number(TargetUid),
		 }
		 call.Send("C2L_RefuseCommunityClubInvitationApply",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_KickCommunityClubMember', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_KickCommunityClubMember' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const TargetUid = req.query.TargetUid
		 /*  #swagger.parameters = ['TargetUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 TargetUid :Number(TargetUid),
		 }
		 call.Send("C2L_KickCommunityClubMember",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_AppointCommunityClubNewLeader', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_AppointCommunityClubNewLeader' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const TargetUid = req.query.TargetUid
		 /*  #swagger.parameters = ['TargetUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 TargetUid :Number(TargetUid),
		 }
		 call.Send("C2L_AppointCommunityClubNewLeader",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_QueryCommunityClubRelationshipByName', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_QueryCommunityClubRelationshipByName' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Name = req.query.Name
		 /*  #swagger.parameters = ['Name'] = {
			type:'string'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Name :Name,
		 Page :Number(Page),
		 }
		 call.Send("C2L_QueryCommunityClubRelationshipByName",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClubMemberStackDayLeaderboard', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClubMemberStackDayLeaderboard' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 SportType :Number(SportType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetCommunityClubMemberStackDayLeaderboard",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClubMemberMileageLeaderboard', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClubMemberMileageLeaderboard' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 SportType :Number(SportType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetCommunityClubMemberMileageLeaderboard",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClubMemberActivityCountLeaderboard', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClubMemberActivityCountLeaderboard' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 SportType :Number(SportType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetCommunityClubMemberActivityCountLeaderboard",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClubMemberBoxingWinRateLeaderboard', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClubMemberBoxingWinRateLeaderboard' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const BattleType = req.query.BattleType
		 /*  #swagger.parameters = ['BattleType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 BattleType :Number(BattleType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetCommunityClubMemberBoxingWinRateLeaderboard",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClubAnnouncement', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClubAnnouncement' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 }
		 call.Send("C2L_GetCommunityClubAnnouncement",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCommunityClubAnnouncementStateRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCommunityClubAnnouncementStateRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetCommunityClubAnnouncementStateRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ChangeCommunityClubAnnouncement', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ChangeCommunityClubAnnouncement' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const Announcement = req.query.Announcement
		 /*  #swagger.parameters = ['Announcement'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 Announcement :Announcement,
		 }
		 call.Send("C2L_ChangeCommunityClubAnnouncement",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetOneClubMemberCommentInformation', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetOneClubMemberCommentInformation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetOneClubMemberCommentInformation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetClubMemberLeader', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetClubMemberLeader' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 }
		 call.Send("C2L_GetClubMemberLeader",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetClubActivityInformation', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetClubActivityInformation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetClubActivityInformation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ExpansionClubMaxMember', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ExpansionClubMaxMember' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ClubId = req.query.ClubId
		 /*  #swagger.parameters = ['ClubId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ClubId :Number(ClubId),
		 }
		 call.Send("C2L_ExpansionClubMaxMember",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_StealthModeCommentChannelRead', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_StealthModeCommentChannelRead' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const CommentIds = req.query.CommentIds
		 /*  #swagger.parameters = ['CommentIds'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 CommentIds :JSON.parse(CommentIds),
		 }
		 call.Send("C2L_StealthModeCommentChannelRead",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreateCommunityChannel', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreateCommunityChannel' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelName = req.query.ChannelName
		 /*  #swagger.parameters = ['ChannelName'] = {
			type:'string'
		 }*/
		 const Introduction = req.query.Introduction
		 /*  #swagger.parameters = ['Introduction'] = {
			type:'string'
		 }*/
		 const StealthMode = req.query.StealthMode
		 /*  #swagger.parameters = ['StealthMode'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelName :ChannelName,
		 Introduction :Introduction,
		 StealthMode :JSON.parse(StealthMode),
		 }
		 call.Send("C2L_CreateCommunityChannel",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateCommunityChannel', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateCommunityChannel' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 const ChannelName = req.query.ChannelName
		 /*  #swagger.parameters = ['ChannelName'] = {
			type:'string'
		 }*/
		 const Introduction = req.query.Introduction
		 /*  #swagger.parameters = ['Introduction'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 ChannelName :ChannelName,
		 Introduction :Introduction,
		 }
		 call.Send("C2L_UpdateCommunityChannel",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DeleteCommunityChannel', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DeleteCommunityChannel' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 }
		 call.Send("C2L_DeleteCommunityChannel",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetJoinedCommunityChannel', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetJoinedCommunityChannel' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const Skip = req.query.Skip
		 /*  #swagger.parameters = ['Skip'] = {
			type:'number'
		 }*/
		 const Limit = req.query.Limit
		 /*  #swagger.parameters = ['Limit'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 Skip :Number(Skip),
		 Limit :Number(Limit),
		 }
		 call.Send("C2L_GetJoinedCommunityChannel",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCreatedCommunityChannel', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCreatedCommunityChannel' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const Skip = req.query.Skip
		 /*  #swagger.parameters = ['Skip'] = {
			type:'number'
		 }*/
		 const Limit = req.query.Limit
		 /*  #swagger.parameters = ['Limit'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 Skip :Number(Skip),
		 Limit :Number(Limit),
		 }
		 call.Send("C2L_GetCreatedCommunityChannel",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetChannelMemberInformation', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetChannelMemberInformation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 const Skip = req.query.Skip
		 /*  #swagger.parameters = ['Skip'] = {
			type:'number'
		 }*/
		 const Limit = req.query.Limit
		 /*  #swagger.parameters = ['Limit'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 Skip :Number(Skip),
		 Limit :Number(Limit),
		 }
		 call.Send("C2L_GetChannelMemberInformation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetChannelOneMemberInformation', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetChannelOneMemberInformation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_GetChannelOneMemberInformation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_JoinCommunityChannel', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_JoinCommunityChannel' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 }
		 call.Send("C2L_JoinCommunityChannel",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_FireCommunityChannelMember', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_FireCommunityChannelMember' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 const MemberUid = req.query.MemberUid
		 /*  #swagger.parameters = ['MemberUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 MemberUid :Number(MemberUid),
		 }
		 call.Send("C2L_FireCommunityChannelMember",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DissociateCommunityChannel', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DissociateCommunityChannel' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 }
		 call.Send("C2L_DissociateCommunityChannel",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreateCommunityChannelComment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreateCommunityChannelComment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 const Content = req.query.Content
		 /*  #swagger.parameters = ['Content'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 Content :JSON.parse(Content),
		 }
		 call.Send("C2L_CreateCommunityChannelComment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DeleteCommunityChannelComment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DeleteCommunityChannelComment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const CommentId = req.query.CommentId
		 /*  #swagger.parameters = ['CommentId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 CommentId :Number(CommentId),
		 }
		 call.Send("C2L_DeleteCommunityChannelComment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetDeletedCommunityChannelCommentRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetDeletedCommunityChannelCommentRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 }
		 call.Send("C2L_GetDeletedCommunityChannelCommentRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_AppointCommunityChannelNewLeader', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_AppointCommunityChannelNewLeader' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 const LeaderUid = req.query.LeaderUid
		 /*  #swagger.parameters = ['LeaderUid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 LeaderUid :Number(LeaderUid),
		 }
		 call.Send("C2L_AppointCommunityChannelNewLeader",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_InviteFriendAsCommunityChannelMember', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_InviteFriendAsCommunityChannelMember' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 const FriendUids = req.query.FriendUids
		 /*  #swagger.parameters = ['FriendUids'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 FriendUids :JSON.parse(FriendUids),
		 }
		 call.Send("C2L_InviteFriendAsCommunityChannelMember",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetChannelCommentRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetChannelCommentRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ChannelId = req.query.ChannelId
		 /*  #swagger.parameters = ['ChannelId'] = {
			type:'number'
		 }*/
		 const Skip = req.query.Skip
		 /*  #swagger.parameters = ['Skip'] = {
			type:'number'
		 }*/
		 const Limit = req.query.Limit
		 /*  #swagger.parameters = ['Limit'] = {
			type:'number'
		 }*/
		 const StartAt = req.query.StartAt
		 /*  #swagger.parameters = ['StartAt'] = {
			type:'number'
		 }*/
		 const EndAt = req.query.EndAt
		 /*  #swagger.parameters = ['EndAt'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ChannelId :Number(ChannelId),
		 Skip :Number(Skip),
		 Limit :Number(Limit),
		 StartAt :Number(StartAt),
		 EndAt :Number(EndAt),
		 }
		 call.Send("C2L_GetChannelCommentRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetNotificationRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetNotificationRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetNotificationRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_MarkNotificationWithRead', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_MarkNotificationWithRead' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const NotificationType = req.query.NotificationType
		 /*  #swagger.parameters = ['NotificationType'] = {
			type:'number'
		 }*/
		 const NotificationId = req.query.NotificationId
		 /*  #swagger.parameters = ['NotificationId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 NotificationType :Number(NotificationType),
		 NotificationId :Number(NotificationId),
		 }
		 call.Send("C2L_MarkNotificationWithRead",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetBoxingGetNpcList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetBoxingGetNpcList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetBoxingGetNpcList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreatePrivacyPolicyRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreatePrivacyPolicyRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Version = req.query.Version
		 /*  #swagger.parameters = ['Version'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Version :Version,
		 }
		 call.Send("C2L_CreatePrivacyPolicyRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetPrivacyPolicyRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetPrivacyPolicyRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetPrivacyPolicyRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DeletePrivacyPolicyRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DeletePrivacyPolicyRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 }
		 call.Send("C2L_DeletePrivacyPolicyRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserRealm/C2R_GetSportType', (req,res) => {
		 /*  #swagger.tags = ['UserRealm']
			 #swagger.description = 'C2R_GetSportType' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2R_GetSportType",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DeleteAccount', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DeleteAccount' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_DeleteAccount",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ExchangeReward', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ExchangeReward' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const AwardCode = req.query.AwardCode
		 /*  #swagger.parameters = ['AwardCode'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 AwardCode :AwardCode,
		 }
		 call.Send("C2L_ExchangeReward",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreateOutDoorRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreateOutDoorRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const BlueToothMAC = req.query.BlueToothMAC
		 /*  #swagger.parameters = ['BlueToothMAC'] = {
			type:'string'
		 }*/
		 const Step = req.query.Step
		 /*  #swagger.parameters = ['Step'] = {
			type:'number'
		 }*/
		 const Calories = req.query.Calories
		 /*  #swagger.parameters = ['Calories'] = {
			type:'number'
		 }*/
		 const ExchangeCoin = req.query.ExchangeCoin
		 /*  #swagger.parameters = ['ExchangeCoin'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 BlueToothMAC :BlueToothMAC,
		 Step :Number(Step),
		 Calories :Number(Calories),
		 ExchangeCoin :Number(ExchangeCoin),
		 }
		 call.Send("C2L_CreateOutDoorRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetOutDoorRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetOutDoorRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetOutDoorRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetRideRecordInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetRideRecordInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RoomType = req.query.RoomType
		 /*  #swagger.parameters = ['RoomType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RoomType :Number(RoomType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetRideRecordInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetRunRecordInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetRunRecordInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RoomType = req.query.RoomType
		 /*  #swagger.parameters = ['RoomType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RoomType :Number(RoomType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetRunRecordInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetBoxingRecordInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetBoxingRecordInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const RoomType = req.query.RoomType
		 /*  #swagger.parameters = ['RoomType'] = {
			type:'number'
		 }*/
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 RoomType :Number(RoomType),
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetBoxingRecordInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUserTotalInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUserTotalInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetUserTotalInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerUploadEventDailyConfig', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerUploadEventDailyConfig' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const AnnounceJson = req.query.AnnounceJson
		 /*  #swagger.parameters = ['AnnounceJson'] = {
			type:'string'
		 }*/
		 const EventDailyJson = req.query.EventDailyJson
		 /*  #swagger.parameters = ['EventDailyJson'] = {
			type:'string'
		 }*/
		 const MapJson = req.query.MapJson
		 /*  #swagger.parameters = ['MapJson'] = {
			type:'string'
		 }*/
		 const RewardJson = req.query.RewardJson
		 /*  #swagger.parameters = ['RewardJson'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 AnnounceJson :AnnounceJson,
		 EventDailyJson :EventDailyJson,
		 MapJson :MapJson,
		 RewardJson :RewardJson,
		 }
		 call.Send("C2L_ManagerUploadEventDailyConfig",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerUploadEventPeriodConfig', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerUploadEventPeriodConfig' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const AnnounceJson = req.query.AnnounceJson
		 /*  #swagger.parameters = ['AnnounceJson'] = {
			type:'string'
		 }*/
		 const EventPeriodJson = req.query.EventPeriodJson
		 /*  #swagger.parameters = ['EventPeriodJson'] = {
			type:'string'
		 }*/
		 const FestivalJson = req.query.FestivalJson
		 /*  #swagger.parameters = ['FestivalJson'] = {
			type:'string'
		 }*/
		 const MissionJson = req.query.MissionJson
		 /*  #swagger.parameters = ['MissionJson'] = {
			type:'string'
		 }*/
		 const RoadRewardJson = req.query.RoadRewardJson
		 /*  #swagger.parameters = ['RoadRewardJson'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 AnnounceJson :AnnounceJson,
		 EventPeriodJson :EventPeriodJson,
		 FestivalJson :FestivalJson,
		 MissionJson :MissionJson,
		 RoadRewardJson :RoadRewardJson,
		 }
		 call.Send("C2L_ManagerUploadEventPeriodConfig",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerUploadEventClubConfig', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerUploadEventClubConfig' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const AnnounceJson = req.query.AnnounceJson
		 /*  #swagger.parameters = ['AnnounceJson'] = {
			type:'string'
		 }*/
		 const EventClubJson = req.query.EventClubJson
		 /*  #swagger.parameters = ['EventClubJson'] = {
			type:'string'
		 }*/
		 const MissionJson = req.query.MissionJson
		 /*  #swagger.parameters = ['MissionJson'] = {
			type:'string'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 AnnounceJson :AnnounceJson,
		 EventClubJson :EventClubJson,
		 MissionJson :MissionJson,
		 }
		 call.Send("C2L_ManagerUploadEventClubConfig",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetSleepDailyEventInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetSleepDailyEventInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetSleepDailyEventInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetSleepPeriodEventInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetSleepPeriodEventInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetSleepPeriodEventInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetSleepClubEventInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetSleepClubEventInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetSleepClubEventInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetOnlineDailyEventInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetOnlineDailyEventInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetOnlineDailyEventInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetOnlinePeriodEventInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetOnlinePeriodEventInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetOnlinePeriodEventInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetOnlineClubEventInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetOnlineClubEventInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetOnlineClubEventInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetExpiredDailyEventInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetExpiredDailyEventInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetExpiredDailyEventInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetExpiredPeriodEventInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetExpiredPeriodEventInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetExpiredPeriodEventInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_ManagerGetExpiredClubEventInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_ManagerGetExpiredClubEventInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_ManagerGetExpiredClubEventInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetEventDailyAnnouncements', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetEventDailyAnnouncements' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetEventDailyAnnouncements",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetEventPeriodAnnouncements', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetEventPeriodAnnouncements' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetEventPeriodAnnouncements",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetEventClubAnnouncements', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetEventClubAnnouncements' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Page = req.query.Page
		 /*  #swagger.parameters = ['Page'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Page :Number(Page),
		 }
		 call.Send("C2L_GetEventClubAnnouncements",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_SignUpEvent', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_SignUpEvent' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const EventId = req.query.EventId
		 /*  #swagger.parameters = ['EventId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 EventId :Number(EventId),
		 }
		 call.Send("C2L_SignUpEvent",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetEventReservationRoomInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetEventReservationRoomInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ReservationId = req.query.ReservationId
		 /*  #swagger.parameters = ['ReservationId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ReservationId :Number(ReservationId),
		 }
		 call.Send("C2L_GetEventReservationRoomInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCalenderNotification', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCalenderNotification' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Year = req.query.Year
		 /*  #swagger.parameters = ['Year'] = {
			type:'number'
		 }*/
		 const Month = req.query.Month
		 /*  #swagger.parameters = ['Month'] = {
			type:'number'
		 }*/
		 const TimeOffset = req.query.TimeOffset
		 /*  #swagger.parameters = ['TimeOffset'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Year :Number(Year),
		 Month :Number(Month),
		 TimeOffset :Number(TimeOffset),
		 }
		 call.Send("C2L_GetCalenderNotification",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetCalenderDateInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetCalenderDateInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const CalenderDateId = req.query.CalenderDateId
		 /*  #swagger.parameters = ['CalenderDateId'] = {
			type:'number'
		 }*/
		 const TimeOffset = req.query.TimeOffset
		 /*  #swagger.parameters = ['TimeOffset'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 CalenderDateId :Number(CalenderDateId),
		 TimeOffset :Number(TimeOffset),
		 }
		 call.Send("C2L_GetCalenderDateInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateCalenderDatePlanInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateCalenderDatePlanInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const CalenderDateId = req.query.CalenderDateId
		 /*  #swagger.parameters = ['CalenderDateId'] = {
			type:'number'
		 }*/
		 const Weight = req.query.Weight
		 /*  #swagger.parameters = ['Weight'] = {
			type:'number'
		 }*/
		 const SleepTime = req.query.SleepTime
		 /*  #swagger.parameters = ['SleepTime'] = {
			type:'number'
		 }*/
		 const WaterML = req.query.WaterML
		 /*  #swagger.parameters = ['WaterML'] = {
			type:'number'
		 }*/
		 const TimeOffset = req.query.TimeOffset
		 /*  #swagger.parameters = ['TimeOffset'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 CalenderDateId :Number(CalenderDateId),
		 Weight :Number(Weight),
		 SleepTime :Number(SleepTime),
		 WaterML :Number(WaterML),
		 TimeOffset :Number(TimeOffset),
		 }
		 call.Send("C2L_UpdateCalenderDatePlanInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreateOnePlan', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreateOnePlan' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlanConfigId = req.query.PlanConfigId
		 /*  #swagger.parameters = ['PlanConfigId'] = {
			type:'number'
		 }*/
		 const StartTime = req.query.StartTime
		 /*  #swagger.parameters = ['StartTime'] = {
			type:'number'
		 }*/
		 const EndTime = req.query.EndTime
		 /*  #swagger.parameters = ['EndTime'] = {
			type:'number'
		 }*/
		 const TimeOffset = req.query.TimeOffset
		 /*  #swagger.parameters = ['TimeOffset'] = {
			type:'number'
		 }*/
		 const Sex = req.query.Sex
		 /*  #swagger.parameters = ['Sex'] = {
			type:'number'
		 }*/
		 const Height = req.query.Height
		 /*  #swagger.parameters = ['Height'] = {
			type:'number'
		 }*/
		 const Weight = req.query.Weight
		 /*  #swagger.parameters = ['Weight'] = {
			type:'number'
		 }*/
		 const BodyFatPercentage = req.query.BodyFatPercentage
		 /*  #swagger.parameters = ['BodyFatPercentage'] = {
			type:'number'
		 }*/
		 const PlanName = req.query.PlanName
		 /*  #swagger.parameters = ['PlanName'] = {
			type:'string'
		 }*/
		 const SelectDayOfWeekInfos = req.query.SelectDayOfWeekInfos
		 /*  #swagger.parameters = ['SelectDayOfWeekInfos'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlanConfigId :Number(PlanConfigId),
		 StartTime :Number(StartTime),
		 EndTime :Number(EndTime),
		 TimeOffset :Number(TimeOffset),
		 Sex :Number(Sex),
		 Height :Number(Height),
		 Weight :Number(Weight),
		 BodyFatPercentage :Number(BodyFatPercentage),
		 PlanName :PlanName,
		 SelectDayOfWeekInfos :JSON.parse(SelectDayOfWeekInfos),
		 }
		 call.Send("C2L_CreateOnePlan",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CreateOneCustomPlan', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CreateOneCustomPlan' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlanConfigId = req.query.PlanConfigId
		 /*  #swagger.parameters = ['PlanConfigId'] = {
			type:'number'
		 }*/
		 const StartTime = req.query.StartTime
		 /*  #swagger.parameters = ['StartTime'] = {
			type:'number'
		 }*/
		 const EndTime = req.query.EndTime
		 /*  #swagger.parameters = ['EndTime'] = {
			type:'number'
		 }*/
		 const TimeOffset = req.query.TimeOffset
		 /*  #swagger.parameters = ['TimeOffset'] = {
			type:'number'
		 }*/
		 const Sex = req.query.Sex
		 /*  #swagger.parameters = ['Sex'] = {
			type:'number'
		 }*/
		 const Height = req.query.Height
		 /*  #swagger.parameters = ['Height'] = {
			type:'number'
		 }*/
		 const Weight = req.query.Weight
		 /*  #swagger.parameters = ['Weight'] = {
			type:'number'
		 }*/
		 const BodyFatPercentage = req.query.BodyFatPercentage
		 /*  #swagger.parameters = ['BodyFatPercentage'] = {
			type:'number'
		 }*/
		 const PlanName = req.query.PlanName
		 /*  #swagger.parameters = ['PlanName'] = {
			type:'string'
		 }*/
		 const SelectDayOfWeekInfos = req.query.SelectDayOfWeekInfos
		 /*  #swagger.parameters = ['SelectDayOfWeekInfos'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlanConfigId :Number(PlanConfigId),
		 StartTime :Number(StartTime),
		 EndTime :Number(EndTime),
		 TimeOffset :Number(TimeOffset),
		 Sex :Number(Sex),
		 Height :Number(Height),
		 Weight :Number(Weight),
		 BodyFatPercentage :Number(BodyFatPercentage),
		 PlanName :PlanName,
		 SelectDayOfWeekInfos :JSON.parse(SelectDayOfWeekInfos),
		 }
		 call.Send("C2L_CreateOneCustomPlan",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUserTotalCumulativeTimePerMonth', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUserTotalCumulativeTimePerMonth' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 const Year = req.query.Year
		 /*  #swagger.parameters = ['Year'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SportType :Number(SportType),
		 Year :Number(Year),
		 }
		 call.Send("C2L_GetUserTotalCumulativeTimePerMonth",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUserTotalCaloriesPerMonth', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUserTotalCaloriesPerMonth' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 const Year = req.query.Year
		 /*  #swagger.parameters = ['Year'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SportType :Number(SportType),
		 Year :Number(Year),
		 }
		 call.Send("C2L_GetUserTotalCaloriesPerMonth",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUserTotalMileagePerMonth', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUserTotalMileagePerMonth' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const SportType = req.query.SportType
		 /*  #swagger.parameters = ['SportType'] = {
			type:'number'
		 }*/
		 const Year = req.query.Year
		 /*  #swagger.parameters = ['Year'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 SportType :Number(SportType),
		 Year :Number(Year),
		 }
		 call.Send("C2L_GetUserTotalMileagePerMonth",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetPlanInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetPlanInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetPlanInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetPlanDayOfWeekInfos', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetPlanDayOfWeekInfos' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlanId = req.query.PlanId
		 /*  #swagger.parameters = ['PlanId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlanId :Number(PlanId),
		 }
		 call.Send("C2L_GetPlanDayOfWeekInfos",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetRecentHistoryPlanTotalRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetRecentHistoryPlanTotalRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 var info = {
		 RpcId : RpcId,
		 }
		 call.Send("C2L_GetRecentHistoryPlanTotalRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetHistoryPlanInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetHistoryPlanInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlanId = req.query.PlanId
		 /*  #swagger.parameters = ['PlanId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlanId :Number(PlanId),
		 }
		 call.Send("C2L_GetHistoryPlanInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetHistoryPlanCaloriesPerDayInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetHistoryPlanCaloriesPerDayInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlanId = req.query.PlanId
		 /*  #swagger.parameters = ['PlanId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlanId :Number(PlanId),
		 }
		 call.Send("C2L_GetHistoryPlanCaloriesPerDayInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetHistoryPlanWeightPerDayInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetHistoryPlanWeightPerDayInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlanId = req.query.PlanId
		 /*  #swagger.parameters = ['PlanId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlanId :Number(PlanId),
		 }
		 call.Send("C2L_GetHistoryPlanWeightPerDayInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetHistoryPlanSleepTimePerDayInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetHistoryPlanSleepTimePerDayInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlanId = req.query.PlanId
		 /*  #swagger.parameters = ['PlanId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlanId :Number(PlanId),
		 }
		 call.Send("C2L_GetHistoryPlanSleepTimePerDayInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetHistoryPlanWaterMLPerDayInfo', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetHistoryPlanWaterMLPerDayInfo' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlanId = req.query.PlanId
		 /*  #swagger.parameters = ['PlanId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlanId :Number(PlanId),
		 }
		 call.Send("C2L_GetHistoryPlanWaterMLPerDayInfo",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_CancelOnePlan', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_CancelOnePlan' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const PlanId = req.query.PlanId
		 /*  #swagger.parameters = ['PlanId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 PlanId :Number(PlanId),
		 }
		 call.Send("C2L_CancelOnePlan",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdatePlanOfWeekProgress', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdatePlanOfWeekProgress' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const ProgressInfos = req.query.ProgressInfos
		 /*  #swagger.parameters = ['ProgressInfos'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 ProgressInfos :JSON.parse(ProgressInfos),
		 }
		 call.Send("C2L_UpdatePlanOfWeekProgress",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_JSGUnlockStage', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_JSGUnlockStage' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Uid = req.query.Uid
		 /*  #swagger.parameters = ['Uid'] = {
			type:'number'
		 }*/
		 const JSGGameType = req.query.JSGGameType
		 /*  #swagger.parameters = ['JSGGameType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Uid :Number(Uid),
		 JSGGameType :Number(JSGGameType),
		 }
		 call.Send("C2L_JSGUnlockStage",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetSelfHighestPoint', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetSelfHighestPoint' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const GameType = req.query.GameType
		 /*  #swagger.parameters = ['GameType'] = {
			type:'number'
		 }*/
		 const LevelType = req.query.LevelType
		 /*  #swagger.parameters = ['LevelType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 GameType :Number(GameType),
		 LevelType :Number(LevelType),
		 }
		 call.Send("C2L_GetSelfHighestPoint",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetRankList', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetRankList' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const GameType = req.query.GameType
		 /*  #swagger.parameters = ['GameType'] = {
			type:'number'
		 }*/
		 const LevelType = req.query.LevelType
		 /*  #swagger.parameters = ['LevelType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 GameType :Number(GameType),
		 LevelType :Number(LevelType),
		 }
		 call.Send("C2L_GetRankList",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_GetUnlockLevels', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_GetUnlockLevels' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const GameType = req.query.GameType
		 /*  #swagger.parameters = ['GameType'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 GameType :Number(GameType),
		 }
		 call.Send("C2L_GetUnlockLevels",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateJsgBoxingRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateJsgBoxingRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Record = req.query.Record
		 /*  #swagger.parameters = ['Record'] = {
			type:'object'
		 }*/
		 const StagesId = req.query.StagesId
		 /*  #swagger.parameters = ['StagesId'] = {
			type:'object'
		 }*/
		 const LevelId = req.query.LevelId
		 /*  #swagger.parameters = ['LevelId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Record :JSON.parse(Record),
		 StagesId :JSON.parse(StagesId),
		 LevelId :Number(LevelId),
		 }
		 call.Send("C2L_UpdateJsgBoxingRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateJsgBasketballRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateJsgBasketballRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Record = req.query.Record
		 /*  #swagger.parameters = ['Record'] = {
			type:'object'
		 }*/
		 const StagesId = req.query.StagesId
		 /*  #swagger.parameters = ['StagesId'] = {
			type:'object'
		 }*/
		 const LevelId = req.query.LevelId
		 /*  #swagger.parameters = ['LevelId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Record :JSON.parse(Record),
		 StagesId :JSON.parse(StagesId),
		 LevelId :Number(LevelId),
		 }
		 call.Send("C2L_UpdateJsgBasketballRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateJsgSpaceshipRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateJsgSpaceshipRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Record = req.query.Record
		 /*  #swagger.parameters = ['Record'] = {
			type:'object'
		 }*/
		 const StageId = req.query.StageId
		 /*  #swagger.parameters = ['StageId'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Record :JSON.parse(Record),
		 StageId :Number(StageId),
		 }
		 call.Send("C2L_UpdateJsgSpaceshipRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpdateJsgFishingRecord', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpdateJsgFishingRecord' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Record = req.query.Record
		 /*  #swagger.parameters = ['Record'] = {
			type:'object'
		 }*/
		 const StagesId = req.query.StagesId
		 /*  #swagger.parameters = ['StagesId'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Record :JSON.parse(Record),
		 StagesId :JSON.parse(StagesId),
		 }
		 call.Send("C2L_UpdateJsgFishingRecord",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpsertUserCoin', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpsertUserCoin' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Count = req.query.Count
		 /*  #swagger.parameters = ['Count'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Count :Number(Count),
		 }
		 call.Send("C2L_UpsertUserCoin",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpsertUserEquipment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpsertUserEquipment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const EquipmentID = req.query.EquipmentID
		 /*  #swagger.parameters = ['EquipmentID'] = {
			type:'number'
		 }*/
		 const Count = req.query.Count
		 /*  #swagger.parameters = ['Count'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 EquipmentID :Number(EquipmentID),
		 Count :Number(Count),
		 }
		 call.Send("C2L_UpsertUserEquipment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_DeleteUserEquipment', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_DeleteUserEquipment' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const EquipmentID = req.query.EquipmentID
		 /*  #swagger.parameters = ['EquipmentID'] = {
			type:'number'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 EquipmentID :Number(EquipmentID),
		 }
		 call.Send("C2L_DeleteUserEquipment",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
	 app.post('/UserLobby/C2L_UpsertPetInformation', (req,res) => {
		 /*  #swagger.tags = ['UserLobby']
			 #swagger.description = 'C2L_UpsertPetInformation' */

		 /*  #swagger.responses[200] = { description: "Result" } */
		 const Pet = req.query.Pet
		 /*  #swagger.parameters = ['Pet'] = {
			type:'object'
		 }*/
		 var info = {
		 RpcId : RpcId,
		 Pet :JSON.parse(Pet),
		 }
		 call.Send("C2L_UpsertPetInformation",info).then((result) => {
			 RpcId = result.RpcId 
			 res.status(200).send({
				 success: "true",
				  Result: result,
			 });
		 })
	 })
};
