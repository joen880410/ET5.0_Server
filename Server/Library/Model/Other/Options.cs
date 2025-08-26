#if SERVER&&!LIBRARY
using CommandLine;
#endif

namespace ETModel
{
    public class Options
    {
#if SERVER && !LIBRARY
        [Option("appId", Required = false, Default = 1)]
        public int AppId { get; set; }
#endif

#if SERVER && !LIBRARY
        [Option("Console", Required = false, Default = "")]
        public string Console { get; set; }
#endif

#if SERVER && !LIBRARY
        // 没啥用，主要是在查看进程信息能区分每个app.exe的类型
        [Option("appType", Required = false, Default = AppType.Manager)]
#endif
        public AppType AppType { get; set; }

#if SERVER && !LIBRARY
        [Option("config", Required = false, Default = "../Config/StartConfig/LocalAllServer.txt")]
        public string Config { get; set; }
#endif

#if SERVER && !LIBRARY
        [Option("configOther", Required = false, Default = "../Config/{0}.txt")]
        public string ConfigOther { get; set; }
#endif
    }

}