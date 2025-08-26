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
    public static class LanguageUtility
    {
        [Flags]
        public enum LanguageType
        {
            None = 0,
            En = 1 << 0,
            Ja = 1 << 1,
            Zh_Tw = 1 << 2
        }

        public enum LanguageSystemType
        {
            En = 10,
            Ja = 22,
            Zh_Tw = 41,
            Unknown = 42
        }

        private static readonly Dictionary<int, LanguageType> getLanguageFlagMethod = new Dictionary<int, LanguageType>
        {
            { (int)LanguageSystemType.En, LanguageType.En },
            { (int)LanguageSystemType.Ja, LanguageType.Ja },
            { (int)LanguageSystemType.Zh_Tw, LanguageType.Zh_Tw }
        };

        public static bool CheckLanguage(int languageType, int userLanguageType)
        {
            var type = userLanguageType % (int)LanguageSystemType.Unknown;
            if (!getLanguageFlagMethod.TryGetValue(type, out var flag))
            {
                Log.Error($"Language Type:{languageType} is not exist.");
                return false;
            }

            return OtherHelper.IsMatchingType(languageType, (int)flag);
        }
        public static bool GetLanguage(int languageType, out LanguageType language)
        {
            language = LanguageType.None;
            if (!getLanguageFlagMethod.TryGetValue(languageType, out var flag))
            {
                Log.Error($"Language Type:{languageType} is not exist.");
                return false;
            }
            language = getLanguageFlagMethod[languageType];
            return true;
        }
    }
}
