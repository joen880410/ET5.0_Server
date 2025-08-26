using ETModel;
using System;

namespace ETHotfix
{
    [Console(AppType.AllServer | AppType.Benchmark, CommandPattern.ShowProfiler, ConsoleService.Self)]
    public class Event_ShowProfiler : AConsoleHandler<int>
    {
        public override async ETTask<ConsoleResult> Execute(int milisec)
        {
            var profileComponent = Game.Scene.GetComponent<ProfileComponent>();
            if (profileComponent != null)
            {
                profileComponent.ShowMessage(milisec);
                return ConsoleResult.Ok();
            }
            var benchmarkComponent = Game.Scene.GetComponent<BenchmarkComponent>();
            if (benchmarkComponent != null)
            {
                benchmarkComponent.isOnProfiler = true;
                return ConsoleResult.Ok();
            }
            await ETTask.CompletedTask;
            return ConsoleResult.NotSupported();
        }
    }

    [Console(AppType.AllServer | AppType.Benchmark, CommandPattern.ShowProfilerAll, ConsoleService.All)]
    public class Event_ShowAllProfiler : AConsoleHandler<int>
    {
        public override async ETTask<ConsoleResult> Execute(int milisec)
        {
            var profileComponent = Game.Scene.GetComponent<ProfileComponent>();
            if (profileComponent != null)
            {
                ConsoleComponent consoleComponent = Game.Scene.GetComponent<ConsoleComponent>();
                await consoleComponent.BroadcastCommandOnStructValue(CommandPattern.ShowProfiler,milisec);
                return ConsoleResult.Ok();
            }
            var benchmarkComponent = Game.Scene.GetComponent<BenchmarkComponent>();
            if (benchmarkComponent != null)
            {
                benchmarkComponent.isOnProfiler = true;
                return ConsoleResult.Ok();
            }
            await ETTask.CompletedTask;
            return ConsoleResult.NotSupported();
        }
    }

    [Console(AppType.AllServer | AppType.Benchmark, CommandPattern.HideProfiler, ConsoleService.Self)]
    public class Event_HideProfiler : AConsoleHandler
    {
        public override async ETTask<ConsoleResult> Execute()
        {
            var profileComponent = Game.Scene.GetComponent<ProfileComponent>();
            if (profileComponent != null)
            {
                profileComponent.HideMessage();
                return ConsoleResult.Ok();
            }
            var benchmarkComponent = Game.Scene.GetComponent<BenchmarkComponent>();
            if (benchmarkComponent != null)
            {
                benchmarkComponent.isOnProfiler = false;
                return ConsoleResult.Ok();
            }
            await ETTask.CompletedTask;
            return ConsoleResult.NotSupported();
        }
    }
}