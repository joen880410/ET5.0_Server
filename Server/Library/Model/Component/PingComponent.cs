using System;

namespace ETModel.Share
{
    [ObjectSystem]
    public partial class PingComponentAwakeSystem : AwakeSystem<PingComponent, long, Action<Exception>>
    {
        public override void Awake(PingComponent self, long waitTime, Action<Exception> action)
        {
            self.Awake(waitTime, action);
        }
    }

    /// <summary>
    /// 心跳组件+PING组件
    /// </summary>
    public class PingComponent : Component
    {
        #region 成员变量

        /// <summary>
        /// 发送时间
        /// </summary>
        private long _sendTimer;

        /// <summary>
        /// 接收时间
        /// </summary>
        private long _receiveTimer;

        /// <summary>
        /// 延时
        /// </summary>
        public long Ping = 0;

        /// <summary>
        /// 心跳协议包
        /// </summary>
        private readonly C2S_Ping _request = new C2S_Ping();

        public event Action<long> onPingChanged;
        public event Action<float> onNetSpeed;
        /// <summary>
        /// 在log顯示ping
        /// </summary>
        public bool isShowPing = false;
        #endregion

        #region Awake

        public void Awake(long waitTime, Action<Exception> action)
        {
            StartToPing(waitTime, action).Coroutine();
        }

        private async ETTask StartToPing(long waitTime, Action<Exception> action)
        {
            var timerComponent = Game.Scene.GetComponent<TimerComponent>();

            var session = this.GetParent<Session>();

            while (!this.IsDisposed)
            {
                try
                {
                    using (var cts = new ETCancellationTokenSource())
                    {
                        _sendTimer = TimeHelper.ClientNowMilliSeconds();
                        await session.Call(_request);
                        _receiveTimer = TimeHelper.ClientNowMilliSeconds();
                        // 計算延遲時間
                        Ping = ((_receiveTimer - _sendTimer) / 2) < 0 ? 0 : (_receiveTimer - _sendTimer) / 2;
                        onPingChanged?.Invoke(Ping);
                        var Time = (_receiveTimer - _sendTimer) / 1000f;
                        var Speed = (4 / Time) / Time;
                        onNetSpeed?.Invoke(Speed);
                        if (isShowPing)
                        {
                            Log.Info($"_receiveTimer{_receiveTimer}, _sendTimer{_sendTimer}, Ping:{Ping}");
                        }
                    }
                    
                }
                catch (OperationCanceledException e)
                {
                    // 執行斷線後操作
                    action?.Invoke(e);
                    Log.Warning("[Ping]網路超時");
                }
                catch (Exception e)
                {
                    // 執行斷線後操作
                    action?.Invoke(e);
                    Log.Error("[Ping]網路斷線");
                }
                await timerComponent.WaitForMilliSecondAsync(waitTime);
            }
        }

        #endregion

        public override async void Dispose()
        {
            Log.Error("[Ping]移除");
            base.Dispose();
            onPingChanged = null;
        }
    }
}