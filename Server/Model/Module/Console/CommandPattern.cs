using System;

namespace ETModel
{
    public static class CommandPattern
    {
        public const string CreateManagerUser = "create user -m * * *";
        public const string ShowSuperUser = "show user -s";
        public const string AttachSuperUser = "attach user -m * * * *";
        public const string ChangeSuperUserPasswd = "change user -p * *";

        public const string CreateApi = "create api * *";

        public const string CreateUserRandomly = "create user -r *";
        public const string CreateUserRandomlyCommand = "create user -r -c *";

        public const string DeleteAllTestUser = "delete user -t -a";
        public const string DeleteUser = "delete user *";

        public const string ShowMapUnit = "mapunit -s *";

        public const string ShowPlayer = "player -s *";
        public const string HideProfiler = "profiler -h";
        public const string ShowProfiler = "profiler -s *"; //profiler -s 1000
        public const string ShowProfilerAll = "profiler -s -a *";


        public const string Reload = "reload";
        public const string ReloadConfig = "reload config";
        public const string ShowRoom = "room -s *";

        // dbUpgrade
        public const string RefreshDBUpgrade = "dbupgrade *";

    }
    public static class EventIdType
    {
        public const string SyncAllRoamingRoom = "SyncAllRoamingRoom";
    }
}