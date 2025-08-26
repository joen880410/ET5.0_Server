using ETModel.Share;

namespace ETModel
{
    public static class RoomBattleType
    {
#if !LIBRARY && MODEL
        
        public const int Max = 10000;
        public const int FeatureMin = Main;
        public const int FeatureMax = AI;
        public const int TypeMin = Public;
        public const int TypeMax = Private;
        
        // Feature 1 ~ 100
        public const int Main = 1;
        public const int Member = 2;
        public const int Reservation = 3;
        public const int LeaderBoard = 4;
        public const int AI = 5;

#endif
        
        // Type 101 ~ 200
        public const int BattleType = 100;
        public const int Public = BattleType + (int)RoomUtility.RoomSubType.PublicParty;
        public const int Private = BattleType + (int)RoomUtility.RoomSubType.PrivateParty;
        public const int EventPublic = BattleType + (int)RoomUtility.RoomSubType.EventPublicParty;
        public const int EventPrivate = BattleType + (int)RoomUtility.RoomSubType.EventPrivateParty;

#if !LIBRARY && MODEL
        
        public const int PublicManyPlayerVsManyPlayer = Public * (int)RoomUtility.RoomBattlePlayType.ManyPlayerVsManyPlayer; // 公開賽玩家與玩家多打多
        public const int PublicManyPlayerVsOneAI = Public * (int)RoomUtility.RoomBattlePlayType.ManyPlayerVsOneAI; // 公開賽玩家與NPC多打一
        
        public const int PrivateManyPlayerVsManyPlayer = Private * (int)RoomUtility.RoomBattlePlayType.ManyPlayerVsManyPlayer; // 揪團玩家與玩家多打多
        public const int PrivateManyPlayerVsOneAI = Private * (int)RoomUtility.RoomBattlePlayType.ManyPlayerVsOneAI; // 揪團玩家與NPC多打一
        
        public const int EventPublicManyPlayerVsManyPlayer = EventPublic * (int)RoomUtility.RoomBattlePlayType.ManyPlayerVsManyPlayer; // 活動公開賽玩家與玩家多打多
        public const int EventPublicManyPlayerVsOneAI = EventPublic * (int)RoomUtility.RoomBattlePlayType.ManyPlayerVsOneAI; // 活動公開賽玩家與NPC多打一
        
        public const int EventPrivateManyPlayerVsManyPlayer = EventPrivate * (int)RoomUtility.RoomBattlePlayType.ManyPlayerVsManyPlayer; // 活動揪團玩家與玩家多打多
        public const int EventPrivateManyPlayerVsOneAI = EventPrivate * (int)RoomUtility.RoomBattlePlayType.ManyPlayerVsOneAI; // 活動揪團玩家與NPC多打一
        
        // PublicManyPlayerVsManyPlayer
        public const int PublicManyPlayerVsManyPlayerOnMain = PublicManyPlayerVsManyPlayer * 10 + Main;
        public const int PublicManyPlayerVsManyPlayerOnMember = PublicManyPlayerVsManyPlayer * 10 + Member;
        public const int PublicManyPlayerVsManyPlayerOnLeaderBoard = PublicManyPlayerVsManyPlayer * 10 + LeaderBoard;
        
        // PublicManyPlayerVsOneAI
        public const int PublicManyPlayerVsOneAIOnMain = PublicManyPlayerVsOneAI * 10 + Main;
        public const int PublicManyPlayerVsOneAIOnMember = PublicManyPlayerVsOneAI * 10 + Member;
        public const int PublicManyPlayerVsOneAIOnLeaderBoard = PublicManyPlayerVsOneAI * 10 + LeaderBoard;
        public const int PublicManyPlayerVsOneAIOnAI = PublicManyPlayerVsOneAI * 10 + AI;

        // PrivateManyPlayerVsManyPlayer
        public const int PrivateManyPlayerVsManyPlayerOnMain = PrivateManyPlayerVsManyPlayer * 10 + Main;
        public const int PrivateManyPlayerVsManyPlayerOnMember = PrivateManyPlayerVsManyPlayer * 10 + Member;
        public const int PrivateManyPlayerVsManyPlayerOnReservation = PrivateManyPlayerVsManyPlayer * 10 + Reservation;
        public const int PrivateManyPlayerVsManyPlayerOnLeaderBoard = PrivateManyPlayerVsManyPlayer * 10 + LeaderBoard;

        // PrivateManyPlayerVsOneAI
        public const int PrivateManyPlayerVsOneAIOnMain = PrivateManyPlayerVsOneAI * 10 + Main;
        public const int PrivateManyPlayerVsOneAIOnMember = PrivateManyPlayerVsOneAI * 10 + Member;
        public const int PrivateManyPlayerVsOneAIOnReservation = PrivateManyPlayerVsOneAI * 10 + Reservation;
        public const int PrivateManyPlayerVsOneAIOnLeaderBoard = PrivateManyPlayerVsOneAI * 10 + LeaderBoard;
        public const int PrivateManyPlayerVsOneAIOnAI = PrivateManyPlayerVsOneAI * 10 + AI;
        
        // EventPublicManyPlayerVsManyPlayer
        public const int EventPublicManyPlayerVsManyPlayerOnMain = EventPublicManyPlayerVsManyPlayer * 10 + Main;
        public const int EventPublicManyPlayerVsManyPlayerOnMember = EventPublicManyPlayerVsManyPlayer * 10 + Member;
        public const int EventPublicManyPlayerVsManyPlayerOnLeaderBoard = EventPublicManyPlayerVsManyPlayer * 10 + LeaderBoard;
        
        // EventPublicManyPlayerVsOneAI
        public const int EventPublicManyPlayerVsOneAIOnMain = EventPublicManyPlayerVsOneAI * 10 + Main;
        public const int EventPublicManyPlayerVsOneAIOnMember = EventPublicManyPlayerVsOneAI * 10 + Member;
        public const int EventPublicManyPlayerVsOneAIOnLeaderBoard = EventPublicManyPlayerVsOneAI * 10 + LeaderBoard;
        public const int EventPublicManyPlayerVsOneAIOnAI = EventPublicManyPlayerVsOneAI * 10 + AI;

        // EventPrivateManyPlayerVsManyPlayer
        public const int EventPrivateManyPlayerVsManyPlayerOnMain = EventPrivateManyPlayerVsManyPlayer * 10 + Main;
        public const int EventPrivateManyPlayerVsManyPlayerOnMember = EventPrivateManyPlayerVsManyPlayer * 10 + Member;
        public const int EventPrivateManyPlayerVsManyPlayerOnReservation = EventPrivateManyPlayerVsManyPlayer * 10 + Reservation;
        public const int EventPrivateManyPlayerVsManyPlayerOnLeaderBoard = EventPrivateManyPlayerVsManyPlayer * 10 + LeaderBoard;

        // EventPrivateManyPlayerVsOneAI
        public const int EventPrivateManyPlayerVsOneAIOnMain = EventPrivateManyPlayerVsOneAI * 10 + Main;
        public const int EventPrivateManyPlayerVsOneAIOnMember = EventPrivateManyPlayerVsOneAI * 10 + Member;
        public const int EventPrivateManyPlayerVsOneAIOnReservation = EventPrivateManyPlayerVsOneAI * 10 + Reservation;
        public const int EventPrivateManyPlayerVsOneAIOnLeaderBoard = EventPrivateManyPlayerVsOneAI * 10 + LeaderBoard;
        public const int EventPrivateManyPlayerVsOneAIOnAI = EventPrivateManyPlayerVsOneAI * 10 + AI;
#endif
        public static bool IsRoomPrivate(int roomBattleType)
        { 
            return roomBattleType == Private || roomBattleType == EventPrivate;
        }
        
        public static bool IsRoomPublic(int roomBattleType)
        {
            return roomBattleType == Public || roomBattleType == EventPublic;
        }
    }
}