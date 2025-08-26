using ETModel;
using System;
using System.Collections.Generic;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    public class M2A_DispatchCommandHandler : AMRpcHandler<M2A_DispatchCommand, A2M_DispatchCommand>
    {
        protected override async void Run(Session session, M2A_DispatchCommand message, Action<A2M_DispatchCommand> reply)
        {
            A2M_DispatchCommand response = new A2M_DispatchCommand();
            ConsoleComponent consoleComponent = Game.Scene.GetComponent<ConsoleComponent>();
            StartConfigComponent startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();
            Session masterSession = SessionHelper.GetMasterSession();
            try
            {
                if (consoleComponent == null)
                {
                    var appType = startConfigComponent.StartConfig.AppType;
                    var msg = $"Console mode is not supported on the server[{appType}]!";
                    response.Error = ErrorCode.ERR_ConsoleNotSupported;
                    response.Message = msg;
                    reply(response);
                    return;
                }
                
                ConsoleResult consoleResult = await consoleComponent.ExecuteCommand(message.Command);
                response.Error = consoleResult.ErrorCode;
                response.Message = consoleResult.Message;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}