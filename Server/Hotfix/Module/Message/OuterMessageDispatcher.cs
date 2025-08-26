﻿using ETModel;
using static Google.Apis.Requests.BatchRequest;

namespace ETHotfix
{
    public class OuterMessageDispatcher : IMessageDispatcher
    {
        public void Dispatch(Session session, ushort opcode, object message)
        {
            DispatchAsync(session, opcode, message).Coroutine();
        }

        public async ETVoid DispatchAsync(Session session, ushort opcode, object message)
        {
            // 根据消息接口判断是不是Actor消息，不同的接口做不同的处理
            switch (message)
            {
                case IActorLocationRequest actorLocationRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                    {
                        long actorId = DispatchActorId(session, opcode);
                        if (actorId == 0L)
                        {
                            var retOpcode = MessageHelper.GetResponseOpcode(opcode);
                            var instance = Game.Scene.GetComponent<OpcodeTypeComponent>().GetInstance(retOpcode) as IResponse;
                            instance.RpcId = actorLocationRequest.RpcId;
                            instance.Error = ErrorCode.ERR_NotFoundActor;
                            session.Send(instance);
                            return;
                        }
                        ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(actorId);

                        int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                        long instanceId = session.InstanceId;
                        IResponse response = await actorLocationSender.Call(actorLocationRequest);
                        response.RpcId = rpcId;

                        // session可能已经断开了，所以这里需要判断
                        if (session.InstanceId == instanceId)
                        {
                            session.Send(response);
                        }

                        break;
                    }
                case IActorLocationMessage actorLocationMessage:
                    {
                        long actorId = DispatchActorId(session, opcode);
                        if (actorId == 0L)
                        {
                            return;
                        }
                        ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(actorId);
                        actorLocationSender.Send(actorLocationMessage);
                        break;
                    }
                case IActorRequest actorRequest:  // 分发IActorRequest消息，目前没有用到，需要的自己添加
                    {
                        break;
                    }
                case IActorMessage actorMessage:  // 分发IActorMessage消息，目前没有用到，需要的自己添加
                    {
                        Game.Scene.GetComponent<MessageDispatcherComponent>().Handle(session, new MessageInfo(opcode, message));
                        break;
                    }
                default:
                    {
                        // 非Actor消息
                        Game.Scene.GetComponent<MessageDispatcherComponent>().Handle(session, new MessageInfo(opcode, message));
                        break;
                    }
            }
        }

        private long DispatchActorId(Session session, ushort opcode)
        {
            AppType appType = MessageHelper.Get(opcode);
            long actorId = 0L;
            var sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayerComponent == null)
            {
                Log.Warning($"sessionPlayerComponent is null appType: {appType}, opcode: {opcode}");
                return actorId;
            }

            Player player = sessionPlayerComponent.Player;
            switch (appType)
            {
                case AppType.Lobby:
                    actorId = player.uid;
                    break;
                case AppType.Map:
                    actorId = player.mapUnitId;
                    break;
                default:
                    actorId = 0L;
                    break;
            }

            if (actorId == 0L)
            {
                Log.Warning($"OuterMessageDispatcher.DispatchActorId actorId is zero. appType: {appType}, opcode: {opcode}");
            }
            return actorId;
        }
    }
}
