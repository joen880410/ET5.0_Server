using System;
using System.Collections.Generic;
using System.IO;
using ETModel;
using ETHotfix.Share;
using System.Linq;

namespace ETHotfix
{
    public static class LanguageComponentEx
    {
        public static void Awake(this LanguageComponent self)
        {
            self._configComponent = Game.Scene.GetComponent<ConfigComponent>();
        }

        public static string GetString(this LanguageComponent self, int type, long id)
        {
            var languageSettingServer = self._configComponent.Get(typeof(LanguageSetting_Server), id) as LanguageSetting_Server;
            if (languageSettingServer != null)
            {
                switch (type)
                {
                    //SystemLanguage.English
                    case (int)LanguageUtility.LanguageSystemType.En:
                        {
                            return languageSettingServer.en;
                        }

                    // ------------Saitou Add Japanese--------------
                    case (int)LanguageUtility.LanguageSystemType.Ja:
                        {
                            return languageSettingServer.ja;
                        }
                    // ---------end Saitou Add Janaese--------------

                    //SystemLanguage.ChineseTraditional
                    case (int)LanguageUtility.LanguageSystemType.Zh_Tw:
                        {
                            return languageSettingServer.zh_tw;
                        }
                }
            }
            return id.ToString();
        }
        public static string GetString(this LanguageComponent self, LanguageSetting_Server message, int type)
        {
            var languageSettingServer = message;
            if (languageSettingServer != null)
            {
                switch (type)
                {
                    //SystemLanguage.English
                    case (int)LanguageUtility.LanguageSystemType.En:
                        return languageSettingServer.en;

                    // ------------Saitou Add Japanese--------------
                    case (int)LanguageUtility.LanguageSystemType.Ja:
                        return languageSettingServer.ja;
                    // ---------end Saitou Add Janaese--------------

                    //SystemLanguage.ChineseTraditional
                    case (int)LanguageUtility.LanguageSystemType.Zh_Tw:
                        return languageSettingServer.zh_tw;
                }
            }
            return languageSettingServer.ToString();
        }
    }
}
