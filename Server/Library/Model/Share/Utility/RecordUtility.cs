using System;
using ETModel;

#if HOTFIX
using ETHotfix;

namespace ETHotfix.Share
{
#else
namespace ETModel.Share
{
#endif
    public static class RecordUtility
    {
        public const long INVALID_ID = -1L;
        public const int DEFAULT_ID = 0;

        [Flags]
        public enum RecordTagType
        {
            None = 0,
            Like = 1 << 0, // 喜愛
            Delete = 1 << 1, // 隱藏
            All = Like | Delete // 全包
        }
    }
}
