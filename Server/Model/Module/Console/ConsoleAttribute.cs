using System;

namespace ETModel
{
    public enum ConsoleService
    {
        All,
        Any,
        Self,
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ConsoleAttribute : BaseAttribute
    {
        public ConsoleService ConsoleService { get; }

        public AppType AppType { get; }

        public string CommandPattern { get; }
        public Type[] parameterTypes { set; get; }

        public ConsoleAttribute(AppType appType, string commandPattern, ConsoleService consoleService = ConsoleService.Self)
        {
            this.ConsoleService = consoleService;

            this.AppType = appType;

            this.CommandPattern = commandPattern;

        }
    }
}