namespace ETModel
{
    public static class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000 以上，避免跟SocketError冲突
        public const int ERR_MyErrorCode = 100000;

        public const int ERR_ActorNoMailBoxComponent = 100003;
        public const int ERR_ActorRemove = 100004;
        public const int ERR_PacketParserError = 100005;

        public const int ERR_KcpCantConnect = 102005;
        public const int ERR_KcpChannelTimeout = 102006;
        public const int ERR_KcpRemoteDisconnect = 102007;
        public const int ERR_PeerDisconnect = 102008;
        public const int ERR_SocketCantSend = 102009;
        public const int ERR_SocketError = 102010;
        public const int ERR_KcpWaitSendSizeTooLarge = 102011;

        public const int ERR_WebsocketPeerReset = 103001;
        public const int ERR_WebsocketMessageTooBig = 103002;
        public const int ERR_WebsocketError = 103003;
        public const int ERR_WebsocketConnectError = 103004;
        public const int ERR_WebsocketSendError = 103005;
        public const int ERR_WebsocketRecvError = 103006;

        public const int ERR_RpcFail = 102001;
        public const int ERR_ReloadFail = 102003;
        public const int ERR_ConnectGateKeyError = 100105;
        public const int ERR_ActorLocationNotFound = 102004;
        //-----------------------------------
        // 小于这个Rpc会抛异常，大于这个异常的error需要自己判断处理，也就是说需要处理的错误应该要大于该值
        public const int ERR_Exception = 200000;

        public const int ERR_NotFoundActor = 200002;

        public const int ERR_AccountOrPasswordError = 200102;

        public const int ERR_LocationWaitLock = 200200;
        public const int ERR_LocationUnlockFailed = 200201;
        public const int ERR_LanguageDoesntExist = 200202;
        public const int ERR_DataTooLarge = 200203;
        //------開始頁面相關------

        /*註冊*/
        public const int ERR_SignUpFailed = 300000;
        public const int ERR_AccountSisnUpRepeatly = 300001;
        public const int ERR_InvalidEmailFormat = 300002;
        public const int ERR_InvalidPasswordFormat = 300003;
        public const int ERR_DoubleCheckPasswordFailed = 300004;
        public const int ERR_SignUpByEmailFailed = 300005;
        public const int ERR_SignUpByFirebaseFailed_UserExist = 300006;
        public const int ERR_InvalidAccountFormat = 300007;
        /*登陸*/
        public const int ERR_SignInFailed = 300100;
        public const int ERR_AccountDoesntExist = 300101;
        public const int ERR_PasswordIncorrect = 300102;
        public const int ERR_InvalidToken = 300103;
        public const int ERR_FBSignInFailed = 300104;
        public const int ERR_AuthenticationIsNull = 300105;
        public const int ERR_AuthenticationTypeError = 300106;
        public const int ERR_InvalidDeviceUniqueIdentifier = 300107;
        public const int ERR_DeviceUniqueIdentifierIsNull = 300108;
        public const int ERR_DeviceUniqueIdentifierIsExist = 300109;
        public const int ERR_VerifyIncorrect = 300110;

        /*綁定*/
        public const int ERR_LinkFailed = 300150;
        public const int ERR_LinkIsExist = 300151;

        /*登出*/
        public const int ERR_LogoutFailed = 300200;
        /*連線*/
        public const int ERR_SyncPlayerStateError = 300300;
        public const int ERR_RegisterServerRepeatly = 300301;

        //------END------

        //------戰鬥相關400000~400999------

        public const int ERR_RoomIdNotFound = 400000;
        public const int ERR_RoomTypeError = 400001;
        public const int ERR_RoomTeamComponentNull = 400002;
        public const int ERR_RoomTeamIsNotLeader = 400003;
        public const int ERR_RoomTeamStateCanNotToRun = 400004;
        public const int ERR_RoomTeamStateCanNotKick = 400005;
        public const int ERR_RoomTeamCanNotFindPlayerForKick = 400006;
        public const int ERR_RoomTeamStateCanNotInvite = 400007;
        public const int ERR_RoomTeamCanNotFindPlayerForInvite = 400008;
        public const int ERR_RoomTeamMemberIsFull = 400009;
        public const int ERR_RoomTeamStateCanNotEnter = 400010;
        public const int ERR_RoomTeamHaveNotReady = 400011;
        public const int ERR_MapUnitMissing = 400012;
        public const int ERR_TimeNotUp = 400013;
        public const int ERR_NotSupportedType = 400014;
        public const int ERR_PlayerSportTypeNotMatchRoomInfo = 400015;
        public const int ERR_TooManyRooms = 400016;
        public const int ERR_RoomRoamingMemberIsFull = 400017;
        public const int ERR_OnlyActivityRoomCanInvite = 400018;
        public const int ERR_RoomTeamControllerNull = 400019;
        public const int ERR_RoomBattleComponentNull = 400020;
        public const int ERR_RoomBattleControllerNull = 400021;
        public const int ERR_RoomBattleLoadingIsCanceled = 400022;

        //------END------

        //------邀請相關401000~401999------

        public const int ERR_InviteIdNotFind = 401000;
        public const int ERR_InviteNotSelf = 401001;

        //------END------

        //------預約相關402000~402999------

        public const int ERR_ReservationIsFull = 402000;
        public const int ERR_ReservationIsNotLeader = 402001;
        public const int ERR_ReservationRoomStateCanNotToRemove = 402002;
        public const int ERR_ReservationIdNotFind = 402003;
        public const int ERR_ReservationRoomNotFind = 402004;
        public const int ERR_ReservationNotTheOwner = 402005;

        //------END------

        //------關係相關301000~301999------

        public const int ERR_AddRelationshipRepeatedly = 301000;
        public const int ERR_RelationshipApplyInfo_NotFind = 301001;
        public const int ERR_RelationshipApplyInfo_NotReceiver = 301003;
        public const int ERR_RelationshipApplyInfo_AddFailed = 301004;
        public const int ERR_RelationshipIsNotFriend = 301005;

        //------END------

        //------大廳相關302000~302999------

        public const int ERR_LobbyUnitMissing = 302000;
        public const int ERR_InvalidSportType = 302001;
        public const int ERR_CharacterDoesntExist = 302002;
        public const int ERR_OutDoorRecordNotFind = 302003;

        //------END------

        //------道具相關303000~303999------

        public const int ERR_EquipmentUnavailable = 303000;
        public const int ERR_EquipmentNotDefined = 303001;
        public const int ERR_EquipmentRecordError = 303002;
        public const int ERR_EquipmentBagOverLimit = 303003;
        public const int ERR_EquipmentBagIsEmpty = 303004;
        public const int ERR_EquipmentInvalidCount = 303005;
        public const int ERR_EquipmentNotEnough = 303006;
        public const int ERR_EquipmentOverOwnedLimit = 303007;
        public const int ERR_EquipmentIsFree = 303008;
        public const int ERR_EquipmentIsAlreadyUnlock = 303009;
        public const int ERR_ItemIsNotEquipmentType = 303010;
        public const int ERR_ItemIsNotThisRange = 303011;
        public const int ERR_EquipmentIsStack = 303012;
        public const int ERR_EquipmentIsNotStack = 303013;

        //------END------

        //------個人資訊相關304000~304999------

        public const int ERR_UploadImageWithError = 304000;
        public const int ERR_UploadImageFormatInvalid = 304001;
        public const int ERR_ExceedImageSizeLimit = 304002;
        public const int ERR_UploadInvalidRideDetailedRecordFormat = 304003;
        public const int ERR_UploadNullRideDetailedRecord = 304004;
        public const int ERR_UploadRideDetailedRecordWithError = 304005;
        public const int ERR_RideRecordIsNull = 304006;
        public const int ERR_UploadInvalidRunDetailedRecordFormat = 304007;
        public const int ERR_UploadNullRunDetailedRecord = 304008;
        public const int ERR_UploadRunDetailedRecordWithError = 304009;
        public const int ERR_RunRecordIsNull = 304010;
        public const int ERR_CloudStorageObjectExisting = 304011;
        public const int ERR_RideDetailedBriefInfoExisting = 304012;
        public const int ERR_RunDetailedBriefInfoExisting = 304013;
        public const int ERR_WordCountOverLimit = 304014;
        public const int ERR_OtherUserRecordCanNotChange = 304015;

        //------END------

        /*------共用區------*/

        public const int ERR_UpdateFailed = 900000;
        public const int ERR_PlayerDoesntExist = 900001;
        public const int ERR_ConsoleError = 900002;
        public const int ERR_HttpError = 900003;
        public const int ERR_ConsoleNotImplement = 900004;
        public const int ERR_ConsoleNotSupported = 900005;
        public const int ERR_HttpTextToSpeachNoText = 900006;
        public const int ERR_UserIdentityNotPlayer = 900007;
        public const int ERR_UserNotEounghCoin = 900008;
        public const int ERR_ConfigIsInvalid = 900009;
        public const int ERR_OperationError = 900010;
        /*------END------*/

        //------郵件相關1000000~1029999------

        public const int ERR_MailMessageIsNull = 10000000;
        public const int ERR_MailRecipientIsNotFind = 10000001;
        public const int ERR_MailDoesntExist = 10000002;
        public const int ERR_MailDoesntRead = 10000003;
        public const int ERR_AttachmentIsNull = 10000004;
        public const int ERR_AttachmentIsAlreadyGet = 10000005;
        public const int ERR_AttachmentTypeIsNone = 10000006;
        public const int ERR_SendMailWithError = 10000007;
        public const int ERR_MailHandlerError = 10000008;

        //------END------

        //------任務相關1030000~1059999------

        public const int ERR_TaskIdIsNull = 1030000;
        public const int ERR_TaskIsNotRange = 1030001;
        public const int ERR_RewardIsAlreadyReceive = 1030002;
        public const int ERR_TaskIsNotDone = 1030003;
        public const int ERR_TaskTypeIsNull = 1030004;
        public const int ERR_TaskIsClose = 1030005;
        public const int ERR_TaskControllerIsNull = 1030006;
        public const int ERR_TaskComponentIsNull = 1030007;
        public const int ERR_TaskIsAlreadyDone = 1030008;

        //------END------

        //------寵物相關1060000~1089999------

        public const int ERR_PetInformationDoesntExist = 1060000;
        public const int ERR_PetFoodIsInvalid = 1060001;
        public const int ERR_PetIntimateIsMax = 1060002;
        public const int ERR_PetIsFree = 1060003;
        public const int ERR_PetIsAlreadyUnlock = 1060004;
        public const int ERR_PetIsNotThisRange = 1060005;
        public const int ERR_PetIsLock = 1060006;
        public const int ERR_PetIsOverTouchTimes = 1060007;
        public const int ERR_PetTouchIsInvalid = 1060008;
        public const int ERR_PetStageIsMax = 1060009;
        public const int ERR_PetStageIsMin = 1060010;
        public const int ERR_PetLvLessEvolveLevel = 1060011;
        public const int ERR_OnlyFriendCanPrankPet = 1060012;
        public const int ERR_PrankFreeTimeIsNull = 1060013;
        public const int ERR_FeedFreeTimeIsNull = 1060014;
        public const int ERR_PetTownIsNull = 1060015;
        public const int ERR_PetTownSkinIsLock = 1060016;
        public const int ERR_PetFixTownFacilityFormatIsError = 1060017;
        public const int ERR_PetTownDecorationFormatIsError = 1060018;
        public const int ERR_PetItemCountIsOverLimit = 1060019;
        public const int ERR_PetItemCountLessSellCount = 1060020;
        public const int ERR_PetItemIsNull = 1060021;
        public const int ERR_PetItemCountIsNull = 1060022;
        public const int ERR_PetItemIdIsNotMakeUpRange = 1060023;
        public const int ERR_PetFarmIsNull = 1060024;
        public const int ERR_PetFarmLandIsNull = 1060025;
        public const int ERR_OtherUserFarmCanNotPlant = 1060026;
        public const int ERR_PetFarmLandHaveCrop = 1060027;
        public const int ERR_PetFarmLandNoCrop = 1060028;
        public const int ERR_PetFarmCropStageTypeIsHarvest = 1060029;
        public const int ERR_PetFarmCropIsNotHarvest = 1060030;
        public const int ERR_PetFarmLandIsNotWaterTime = 1060031;
        public const int ERR_PlantIsNotSeed = 1060032;
        public const int ERR_OtherCanNotGetPetTownLog = 1060033;
        public const int ERR_PetLevelNotEnough = 1060034;
        public const int ERR_ExploreNotEnter = 1060035;
        public const int ERR_ExplorePetIsNull = 1060036;
        public const int ERR_PetRoomIsNull = 1060037;
        public const int ERR_ExploreComponentIsNull = 1060038;
        public const int ERR_ExploreControllerIsNull = 1060039;
        public const int ERR_ExploreEpisodeIsOver = 1060040;
        public const int ERR_ExploreEpisodeTypeIsNotQuiz = 1060041;
        public const int ERR_ExploreEpisodeTypeIsNotDialog = 1060042;
        public const int ERR_ExploreEpisodeTypeIsNotExploreGame = 1060043;
        public const int ERR_ExploreEpisodeIsNull = 1060044;
        public const int ERR_PetTownEvaluationUidSame = 1060045;
        public const int ERR_ExploreEpisodeDoorIsNull = 1060046;
        public const int ERR_ExploreEpisodeDoorTypeIsNotEnterType = 1060047;
        public const int ERR_ExploreMapNotExsist = 1060048;
        public const int ERR_ExploreGameEpisodeTypeNotCorrect = 1060049;
        public const int ERR_ExploreEpisodeIdNotFind = 1060050;

        //------END------

        //------場景相關1090000~1199999------

        public const int ERR_SceneIsNotThisRange = 1090000;
        public const int ERR_SceneIsAlreadyUnlock = 1090001;
        public const int ERR_SceneIsFree = 1090002;
        public const int ERR_SceneIsLock = 1090003;

        //------END------

        //------角色相關1120000~1149999------

        public const int ERR_CharacterIsFree = 1120000;
        public const int ERR_CharacterIsAlreadyUnlock = 1120001;
        public const int ERR_CharacterIsNotThisRange = 1120002;
        public const int ERR_CharacterIsLock = 1120003;
        public const int ERR_CharacterGenderNotFound = 1120004;
        //------END------

        //------節慶相關1150000~1179999------

        public const int ERR_FestivalIsInvalid = 1150000;
        public const int ERR_FestivalIsNull = 1150001;
        public const int ERR_FestivalIsClose = 1150002;

        //------社群相關1180000~1209999------

        public const int ERR_PlayerDataVisibilityIsPrivate = 1180000;
        public const int ERR_PlayerDataVisibilityInfoDoesntExist = 1180001;
        public const int ERR_MoodPostDoesntExist = 1180002;
        public const int ERR_MoodPostIsExist = 1180003;
        public const int ERR_MoodCommentDoesntExist = 1180004;
        public const int ERR_IsNotMyPost = 1180005;
        public const int ERR_OtherUserPostCommentIsNotMine = 1180006;
        public const int ERR_OnlyFriendCanCreateCommentToMood = 1180007;
        public const int ERR_ClubDoesntExist = 1180008;
        public const int ERR_ClubAlreadyCreate = 1180009;
        public const int ERR_ClubTypeDoesntExist = 1180010;
        public const int ERR_IsNotDirector = 1180011;
        public const int ERR_IsOverClubMemberCount = 1180012;
        public const int ERR_ClubInvitationDoesntExist = 1180013;
        public const int ERR_ClubMemberDoesntExist = 1180014;
        public const int ERR_ClubCommentDoesntExist = 1180015;
        public const int ERR_NotOwnerForClubComment = 1180016;
        public const int ERR_AlreadyJoinMaxClub = 1180017;
        public const int ERR_ClubAnnouncementDoesntExist = 1180018;
        public const int ERR_ArriveEveryChannelLimit = 1180019;
        public const int ERR_ChannelDoesntExist = 1180020;
        public const int ERR_ChannelMemberHasExisted = 1180021;
        public const int ERR_ChannelMemberDoesntExist = 1180022;
        public const int ERR_CantFireChannelLeader = 1180023;
        public const int ERR_CantDissociateChannelByLeader = 1180024;
        public const int ERR_ChannelOldLeaderOrNewLeaderAnyoneDoesntExist = 1180025;
        public const int ERR_MustBeChannelLeaderToAppointNewChannelLeader = 1180026;
        public const int ERR_ChannelOldLeaderAndNewLeaderIsSame = 1180027;
        public const int ERR_ChannelCommentDoesntExistWithGivenCommentId = 1180028;
        public const int ERR_NotOwnerForChannelComment = 1180029;
        public const int ERR_ChannelCommentContentCantBeEmpty = 1180030;
        public const int ERR_ChannelCommentDoesntExist = 1180031;
        public const int ERR_ChannelMemberHasExceededMaxCount = 1180032;
        public const int ERR_ClubMemberHasExist = 1180033;
        public const int ERR_ClubMemberAlreadyApply = 1180034;
        public const int ERR_IsOverLimitClubMemberCount = 1180035;
        public const int ERR_CantDeleteSelfCommand = 1180036;

        //------自動推播相關1210000~1239999-----
        public const int ERR_BroadcastIsInvalid = 1210000;
        public const int ERR_AutoBroadcastComponentIsNull = 1210001;
        //------END------
        
        //------JEvent1240000~1269999-----
        public const int ERR_ApplyIsNull = 1240000;
        public const int ERR_ApplyCanNotSignUp = 1240001;
        public const int ERR_ApplyHasExisted = 1240002;
        public const int ERR_ApplyNotExisted = 1240003;
        public const int ERR_ApplySignUpMemberIsOver = 1240004;
        public const int ERR_UploadEventContextIsNull = 1240005;
        public const int ERR_EventComponentIsNull = 1240006;
        public const int ERR_EventRoomTypeIsInvalid = 1240007;
        public const int ERR_EventRoomDisable = 1240008;
        public const int ERR_EventRoomComponentIsNull = 1240009;
        public const int ERR_EventIsNull = 1240010;
        public const int ERR_EventRoomIsNull = 1240011;
        public const int ERR_ReservationRoomMemberIsOver = 1240012;
        public const int ERR_ReservationRoomIncludeUser = 1240013;
        public const int ERR_EventRedisIsNull = 1240014;
        public const int ERR_EventRedisDicIsNull = 1240015;
        public const int ERR_EventSignUpStartComponentIsNull = 1240016;
        public const int ERR_EventSignUpEndComponentIsNull = 1240017;
        //------END------

        //-----隱私權相關1310000~1339999--------
        public const int ERR_PrivacyPolicyDoesntExist = 1310000;
        //------END------
        
        //-----AI相關1340000~1359999-----------
        public const int ERR_AIIsNull = 1340000;
        public const int ERR_IsNotIncludeAI = 1340001;
        //------END------
        
        //-----設備相關1360000~1379999--------
        public const int ERR_DeviceComponentNull = 1360000;
        public const int ERR_DeviceControllerNull = 1360001;
        //------END------

        //-----禮包相關1410000~1439999--------
        public const int ERR_RewardCodeLengthDoesntExist = 1410000;
        public const int ERR_RewardCodeDoesntExist = 1410001;
        public const int ERR_RewardCodeAreadlyUse = 1410002;

        //-----WorkOut相關1440000~1469999--------
        public const int ERR_CalenderDateRecordDoesntExist = 1440000;
        public const int ERR_PlanRecordDoesntExist = 1440001;
        public const int ERR_PlanRecordOverSelectDayCount = 1440002;
        public const int ERR_PlanRecordSelectDayOfWeekExist = 1440003;
        public const int ERR_CustomPlanCanNotCreate = 1440004;
        public const int ERR_OnlyCanCreateCustomPlan = 1440005;
        public const int ERR_PlanRecordExist = 1440006;
        public const int ERR_PlanRecordIsEnd = 1440007;
        public const int ERR_NoSelectDayOfWeek = 1440008;
        public const int ERR_CustomPlanSelectDayOfWeekOverTargetMinuteRange = 1440009;
        public const int ERR_PlanRecordOverTime = 1440010;
        public const int ERR_PlanHistoryRecordNotExist = 1440011;
        public const int ERR_PlanWeekHistoryRecordExist = 1440012;
        public const int ERR_PlanIsExpired = 1440013;

        //------END------

        //-----貼圖相關1510000~1539999--------
        public const int ERR_StickerIsAlreadyUnlock = 1510000;
        public const int ERR_StickerIsNotThisRange = 1510001;
        public const int ERR_StickerIsFree = 1510002;
        //-----------------------------------

        //-----JSG相關1540000~1549999--------
        public const int ERR_JSGRecordIsNull = 1540000;
        public const int ERR_GameTypeNotExist = 1540001;
        public const int ERR_LevelTypeNotExist = 1540002;
        public const int ERR_GameNotExistLevels = 1540003;
        public const int ERR_UploadJsgMultiRecordWithError = 1540004;
        //-----------------------------------






        //-----JSG相關1550000~1559999--------
        public const int ERR_APIAuthorizationDoesntExist = 1550000;
        //-----------------------------------

        public static bool IsRpcNeedThrowException(int error)
        {
            if (error == 0)
            {
                return false;
            }

            if (error > ERR_Exception)
            {
                return false;
            }

            return true;
        }
    }
}