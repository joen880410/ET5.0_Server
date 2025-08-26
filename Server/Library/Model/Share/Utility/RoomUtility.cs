using System;
using System.Collections.Generic;
using ETModel;

#if HOTFIX
using ETHotfix;

namespace ETHotfix.Share
{
#else
namespace ETModel.Share
{
#endif
    public class RoomUtility
    {
        public enum RoomType
        {
            Roaming,
            Team,
            EventTeam
        }

        [Flags]
        public enum RoomBattlePlayType
        {
            None = 0,
            OnePlayerVsOnePlayer = 1 << 0,
            ManyPlayerVsManyPlayer = 1 << 1,
            OnePlayerVsOneAI = 1 << 2,
            ManyPlayerVsOneAI = 1 << 3,
            OnePlayerVsOneBossAI = 1 << 4,
            ManyPlayerVsOneBossAI = 1 << 5,
            OnePlayerPractice = 1 << 6,
            OnePlayerLevelPractice = 1 << 7,

            OneVsOne = OnePlayerVsOnePlayer | OnePlayerVsOneAI | OnePlayerVsOneBossAI,
            ManyVsOne = ManyPlayerVsOneAI | ManyPlayerVsOneBossAI,
            ManyVsMany = ManyPlayerVsManyPlayer,
            OnlyOne = OnePlayerPractice | OnePlayerLevelPractice,

            BattleOnlyHavePlayer = OnePlayerVsOnePlayer | ManyPlayerVsManyPlayer,
            BattleHaveAI = OnePlayerVsOneAI | ManyPlayerVsOneAI,
            BattleHaveBossAI = OnePlayerVsOneBossAI | ManyPlayerVsOneBossAI,
            HaveAI = OnePlayerVsOneAI | OnePlayerVsOneBossAI | ManyPlayerVsOneAI | ManyPlayerVsOneBossAI
        }

        [Flags]
        public enum RoomSubType
        {
            None = 0,
            
            /// <summary>
            /// 漫跑
            /// </summary>
            Roam = 1 << 0,

            /// <summary>
            /// 公開賽
            /// </summary>
            PublicParty = 1 << 1,

            /// <summary>
            /// 揪團
            /// </summary>
            PrivateParty = 1 << 2,

            /// <summary>
            /// 活動公開賽
            /// </summary>
            EventPublicParty = 1 << 3,

            /// <summary>
            /// 活動揪團
            /// </summary>
            EventPrivateParty = 1 << 4,
            
            /// <summary>
            /// 競賽
            /// </summary>
            Party = PublicParty | PrivateParty | EventPublicParty | EventPrivateParty
        }

#if !DBVIEWGENERATOR

        public static RoomBattlePlayType FindRoomBattleGroupType(int roomBattlePlayType)
        {
            if (roomBattlePlayType <= (int)RoomBattlePlayType.None)
            {
                return RoomBattlePlayType.None;
            }

            if (OtherHelper.IsMatchingType((int)RoomBattlePlayType.OneVsOne, roomBattlePlayType))
            {
                return RoomBattlePlayType.OneVsOne;
            }

            if (OtherHelper.IsMatchingType((int)RoomBattlePlayType.ManyVsOne, roomBattlePlayType))
            {
                return RoomBattlePlayType.ManyVsOne;
            }

            if (OtherHelper.IsMatchingType((int)RoomBattlePlayType.ManyVsMany, roomBattlePlayType))
            {
                return RoomBattlePlayType.ManyVsMany;
            }

            if (OtherHelper.IsMatchingType((int)RoomBattlePlayType.OnlyOne, roomBattlePlayType))
            {
                return RoomBattlePlayType.OnlyOne;
            }

            return RoomBattlePlayType.None;
        }
        public static int GetMaxMember(int roomBattlePlayType)
        {
            var maxMember = 0;
            var type = FindRoomBattleGroupType(roomBattlePlayType);
            switch (type)
            {
                case RoomBattlePlayType.OneVsOne:
                    maxMember = 2;
                    break;
                case RoomBattlePlayType.ManyVsOne:
                    maxMember = 8;
                    break;
                case RoomBattlePlayType.ManyVsMany:
                    maxMember = 8;
                    break;
                case RoomBattlePlayType.OnlyOne:
                    maxMember = 1;
                    break;
                default:
                    Log.Error($"RoomBattlePlayType:{roomBattlePlayType} have not group.");
                    break;
            }

            return maxMember;
        }

        public static bool IsRoomBattleTypeIncludeBattleHaveAI(int roomBattlePlayType)
        {
            if (roomBattlePlayType <= (int)RoomBattlePlayType.None)
            {
                return false;
            }

            return OtherHelper.IsMatchingType((int)RoomBattlePlayType.BattleHaveAI, roomBattlePlayType);
        }

        public static bool IsRoomBattleTypeIncludeBattleHaveBossAI(int roomBattlePlayType)
        {
            if (roomBattlePlayType <= (int)RoomBattlePlayType.None)
            {
                return false;
            }

            return OtherHelper.IsMatchingType((int)RoomBattlePlayType.BattleHaveBossAI, roomBattlePlayType);
        }

        public static bool IsRoomBattleTypeIncludeAI(int roomBattlePlayType)
        {
            if (roomBattlePlayType <= (int)RoomBattlePlayType.None)
            {
                return false;
            }

            return OtherHelper.IsMatchingType((int)RoomBattlePlayType.HaveAI, roomBattlePlayType);
        }
        
        public static bool IsTeamRoom(int roomType)
        {
            return roomType == (int)RoomType.Team || roomType == (int)RoomType.EventTeam;
        }
        
#endif
    }
}