using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ETModel;

#if HOTFIX
using ETHotfix;
namespace ETHotfix.Share
{
#else
namespace ETModel.Share
{
#endif

    public static class ManagerUtility
    {
        public const long INVALID_ID = -1L;

        public const int DEFAULT_ID = 0;

        public enum CommunityPostSelectType
        {
            Allserver,
            OneUser,
            EverySelectTimeNew,
        }
        public enum CommunityChannelSelectType
        {
            Allserver,
            OneUser,
            OneUserInfo,
            OneChannelInfo,
        }
        public enum CommunityClubSelectType
        {
            Allserver,
            ClubUserRank,
            EveryWeekCalories,
            AllServerClubExistUserCount,
            OneClubInfo,
            ClubFlagRank
        }
    }
}