using ETModel;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ETHotfix
{
    [Console(AppType.DB, CommandPattern.RefreshDBUpgrade, ConsoleService.Any)]
    public class Event_RefreshDBUpgrade : AConsoleHandler<string>
    {
        public override async ETTask<ConsoleResult> Execute(string dbSteps)
        {
            var result = Regex.Split(dbSteps, @",(?=(?:[^']*'[^']*')*[^']*$)");//split commas

            for (int i = 0; i < result.Length; i++)
            {
                if (long.TryParse(result[i], out var step))
                {
                    var str = await RefreshDBUpgradeHelper.RefreshDBUpgrade(step);
                    Console.WriteLine($"result = {str}");
                }
            }

            return ConsoleResult.Ok();
        }
    }

    public static class RefreshDBUpgradeHelper
    {
        public static async ETTask<string> RefreshDBUpgrade(long step)
        {
            var dBUpgradeComponent = Game.Scene.GetComponent<DBUpgradeComponent>();
            var result = string.Empty;
            var script = dBUpgradeComponent.dBUpgradeScripts.FirstOrDefault(entity => entity.step == step);

            if (script == null)
            {
                return $"script step : {step} is invalid";
            }

            if (script.isChecked)
            {
                script.isChecked = false;
            }

            if (dBUpgradeComponent.dBScriptTable.TryGetValue(script.scriptName, out var dbBase))
            {
                await dbBase.Run();
                if (await dbBase.IsValid())
                {
                    script.isChecked = true;
                    script.checkAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    await dBUpgradeComponent.db.Add(script);
                    result = $"step:{script.step}, script:{script.scriptName} is successful";
                }
            }

            return result;
        }
    }
}
