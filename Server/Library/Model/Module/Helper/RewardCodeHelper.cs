using System;
using System.Text;

namespace ETModel
{
    public static class RewardCodeHelper
    {
        static readonly char[] AwardCodeDict = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J',
                                                 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U',
                                                 'V', 'W', 'X', 'Y', 'Z', '1','2','3','4','5','6','7','8','9'};
        const int AWARD_CODE_BIT = 4;
        const int AWARD_CODE_NUM = 18;
        private static readonly Random rand = new Random();
        public static string GeneraterRewardCode(long rewardId)
        {
            StringBuilder sb = new StringBuilder(AWARD_CODE_NUM);
            ulong codeVal = 0;
            for (int i = 0; i < AWARD_CODE_NUM; i++)
            {
                ulong key = (ulong)(rand.Next() % AwardCodeDict.Length);
                sb.Append(AwardCodeDict[key]);
            }
            codeVal |= (ulong)rewardId ;
            bool notEnd = true;
            while (notEnd)
            {
                ulong key = codeVal & ((1 << AWARD_CODE_BIT) - 1);
                sb.Append(AwardCodeDict[key]);
                codeVal >>= AWARD_CODE_BIT;
                if (codeVal == 0)
                {
                    notEnd = !notEnd;
                }
            }
            return sb.ToString();
        }
        public static int GetRewardId(string RewardCode)
        {
            RewardCode = RewardCode.ToUpper();
            if (string.IsNullOrEmpty(RewardCode) || RewardCode.Length <= AWARD_CODE_NUM)
            {
                return 0;
            }
            int awardId = 0;
            for (int i = AWARD_CODE_NUM; i < RewardCode.Length; i++)
            {
                var a = RewardCode[i];
                var val = Array.FindIndex(AwardCodeDict, (e) => e == RewardCode[i]);
                awardId |= val << (AWARD_CODE_BIT * (i - AWARD_CODE_NUM));
            }
            return awardId;
        }
    }
}
