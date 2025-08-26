using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Prometheus.DotNetRuntime;
using Prometheus;
using System.Net;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    [BsonIgnoreExtraElements]
    public class ProfileConfig : AConfigComponent
    {
        public int Port { get; set; }
    }

    public class ProfileComponent : Component
    {
        private TimeSpan processStartTime;

        public double totalCpuUsage { get; private set; }

        public double lastCpuUsage { get; private set; }

        public long workSetMemoryUsage { get; private set; }

        public long privateWorkSetMemoryUsage { get; private set; }

        public int processId { get; private set; }

        private TimeSpan oldCPUTime = new TimeSpan(0);
        private DateTime lastMonitorTime = DateTime.UtcNow;
        private DateTime StartTime = DateTime.UtcNow;
        private Process process = Process.GetCurrentProcess();

        private bool isShowMessage = false;
        private DateTime lastShowMessageAt = DateTime.UtcNow;
        private int showFreqAtMilisec = 1000;

        private TimerComponent timerComponent;

        private MetricServer metricServer;

        private IDisposable metricCollector;

        private Gauge totalOuterNetworkConnectedSessionCountGauge;
        
        private Gauge totalInnerNetworkConnectedSessionCountGauge;

        private long totalSendedPackByteSize;

        private long totalSendedPackCount;

        private long totalReceivedPackByteSize;

        private long totalReceivedPackCount;

        private Gauge sendedPackKBSizePerSecondGauge;

        private Gauge sendedPackCountPerSecondGauge;

        private Gauge receivedPackKBSizePerSecondGauge;

        private Gauge receivedPackCountPerSecondGauge;

        private double maxSendedPackKBSizePerSecondGauge;

        private double maxSendedPackCountPerSecondGauge;

        private double maxReceivedPackKBSizePerSecondGauge;

        private double maxReceivedPackCountPerSecondGauge;

        private Gauge fpsGauge;

        private const float ToKB = 1f / 1024;
        private const int defaultPort = 9000;
        
        public void Awake()
        {
            int port = 0;
            try
            {
                AChannel.OnIncreaseSendedPackInfo += OnIncreaseSendedPackInfo;
                AChannel.OnIncreaseReceivedPackInfo += OnIncreaseReceivedPackInfo;

                processStartTime = process.TotalProcessorTime;
                processId = process.Id;

                timerComponent = Game.Scene.GetComponent<TimerComponent>();

                ProfileConfig profileConfig = StartConfigComponent.Instance.StartConfig.GetComponent<ProfileConfig>();
                port = profileConfig != null ? profileConfig.Port : defaultPort;
                metricServer = new MetricServer(port);
                metricServer.Start();
                metricCollector = DotNetRuntimeStatsBuilder.Default().StartCollecting();

                totalOuterNetworkConnectedSessionCountGauge = Metrics.CreateGauge("outer_network_connection_session_total", "To record outer network connected session total count");
                totalInnerNetworkConnectedSessionCountGauge = Metrics.CreateGauge("inner_network_connection_session_total", "To record inner network connected session total count");
                sendedPackKBSizePerSecondGauge = Metrics.CreateGauge("network_sended_pack_kb_size_per_second", "To record sended pack kb size per second");
                sendedPackCountPerSecondGauge = Metrics.CreateGauge("network_sended_pack_count_per_second", "To record sended pack count per second");
                receivedPackKBSizePerSecondGauge = Metrics.CreateGauge("network_received_pack_kb_size_per_second", "To record received pack kb size per second");
                receivedPackCountPerSecondGauge = Metrics.CreateGauge("network_received_pack_count_per_second", "To record received pack count per second");
                fpsGauge = Metrics.CreateGauge("server_fps", "To record server fps");
            }
            catch (HttpListenerException e)
            {
                if (e.ErrorCode == 5)
                {
                    throw new Exception($"CMD管理員輸入: netsh http add urlacl url = http://+:{port}/ user = Everyone", e);
                }
                Log.Error(e);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }

        public void Update()
        {
            try
            {
                TimeSpan newCPUTime = process.TotalProcessorTime - processStartTime;
                lastCpuUsage = (newCPUTime - oldCPUTime).TotalSeconds / (Environment.ProcessorCount * DateTime.UtcNow.Subtract(lastMonitorTime).TotalSeconds);
                lastMonitorTime = DateTime.UtcNow;
                totalCpuUsage = newCPUTime.TotalSeconds / (Environment.ProcessorCount * DateTime.UtcNow.Subtract(StartTime).TotalSeconds);
                oldCPUTime = newCPUTime;
                workSetMemoryUsage = process.WorkingSet64;
                privateWorkSetMemoryUsage = process.PrivateMemorySize64;

                UpdateMessage();
            }
            catch (Exception e)
            {
                Log.Error($"Profile update failed! Reason:{e.Message}, TraceStack:{e.StackTrace}");
            }
        }

        public string GetCpuUsageLast()
        {
            return $"{lastCpuUsage * 100:0.0}";
        }

        public string GetCpuUsageTotal()
        {
            return $"{totalCpuUsage * 100:0.0}";
        }

        public string GetWorkSetMemoryUsage()
        {
            const int KB = 1024;
            const int MB = 1024 * 1024;

            if (workSetMemoryUsage < KB)
                return $"{workSetMemoryUsage:0.00} byte";
            if (workSetMemoryUsage < MB)
                return $"{workSetMemoryUsage / KB:0.00} KB";

            return $"{workSetMemoryUsage / MB:0.00} MB";
        }

        public string GetPrivateWorkSetMemoryUsage()
        {
            const int KB = 1024;
            const int MB = 1024 * 1024;

            if (privateWorkSetMemoryUsage < KB)
                return $"{privateWorkSetMemoryUsage:0.00} byte";
            if (privateWorkSetMemoryUsage < MB)
                return $"{privateWorkSetMemoryUsage / KB:0.00} KB";

            return $"{privateWorkSetMemoryUsage / MB:0.00} MB";
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            metricServer.Stop();
            metricCollector.Dispose();
        }

        public void ShowMessage(int milisec)
        {
            showFreqAtMilisec = milisec;
            isShowMessage = true;
        }

        public void HideMessage()
        {
            isShowMessage = false;
        }

        public void UpdateMessage()
        {
            var now = DateTime.UtcNow;
            // 控制採樣頻率
            if (now.Subtract(lastShowMessageAt).TotalMilliseconds >= showFreqAtMilisec)
            {
                var durationSecond = showFreqAtMilisec * 0.001f;
                GaugeServerStatus(durationSecond);
                GaugeNetworkStatus(durationSecond);
                lastShowMessageAt = DateTime.UtcNow;
                if (isShowMessage)
                {
                    string networkInfo = ShowProfileMessage();
                    string showMsg = $"CpuUsageLast:{GetCpuUsageLast()}%, CpuUsageTotal:{GetCpuUsageTotal()}%, WorkSetMemoryUsage:{GetWorkSetMemoryUsage()}, NetworkInfo:{networkInfo}";
                    Console.WriteLine(showMsg);
                }
            }
        }

        private string ShowProfileMessage()
        {
            return string.Format("Client count: {0}, SendPackSize: {1:F2}KB/s, SendPackCount: {2}/s, ReceivePackSize: {3:F2}KB/s, ReceivePackCount: {4}/s, MaxSendPackSize: {5:F2}KB/s, MaxSendPackCount: {6}/s, MaxReceivePackSize: {7:F2}KB/s, MaxReceivePackCount: {8}/s, FPS: {9}",
                this.totalOuterNetworkConnectedSessionCountGauge.Value,
                sendedPackKBSizePerSecondGauge.Value, sendedPackCountPerSecondGauge.Value,
                receivedPackKBSizePerSecondGauge.Value, receivedPackCountPerSecondGauge.Value,
                maxSendedPackKBSizePerSecondGauge, maxSendedPackCountPerSecondGauge,
                maxReceivedPackKBSizePerSecondGauge, maxReceivedPackCountPerSecondGauge,
                Math.Floor(fpsGauge.Value));
        }

        private void GaugeServerStatus(float durationSecond)
        {
            var fps = 1 / timerComponent.deltaTime;
            fpsGauge.Set(fps);
        }

        private void GaugeNetworkStatus(float durationSecond)
        {
            // 內外網連接數
            NetOuterComponent netOuterComponent = Game.Scene.GetComponent<NetOuterComponent>();
            if (netOuterComponent != null)
            {
                totalOuterNetworkConnectedSessionCountGauge.Set(netOuterComponent.Count);
            }
            NetInnerComponent netInnerComponent = Game.Scene.GetComponent<NetInnerComponent>();
            if (netInnerComponent != null)
            {
                totalInnerNetworkConnectedSessionCountGauge.Set(netInnerComponent.Count);
            }

            // 封包流量
            float rev = 1 / durationSecond;
            sendedPackKBSizePerSecondGauge.Set(totalSendedPackByteSize * ToKB * rev);
            sendedPackCountPerSecondGauge.Set(totalSendedPackCount * rev);
            receivedPackKBSizePerSecondGauge.Set(totalReceivedPackByteSize * ToKB * rev);
            receivedPackCountPerSecondGauge.Set(totalReceivedPackCount * rev);
            maxSendedPackKBSizePerSecondGauge = Math.Max(maxSendedPackKBSizePerSecondGauge, sendedPackKBSizePerSecondGauge.Value);
            maxSendedPackCountPerSecondGauge = Math.Max(maxSendedPackCountPerSecondGauge, sendedPackCountPerSecondGauge.Value);
            maxReceivedPackKBSizePerSecondGauge = Math.Max(maxReceivedPackKBSizePerSecondGauge, receivedPackKBSizePerSecondGauge.Value);
            maxReceivedPackCountPerSecondGauge = Math.Max(maxReceivedPackCountPerSecondGauge, receivedPackCountPerSecondGauge.Value);
            totalSendedPackByteSize = 0;
            totalSendedPackCount = 0;
            totalReceivedPackByteSize = 0;
            totalReceivedPackCount = 0;
        }

        private void OnIncreaseSendedPackInfo(long size, long count)
        {
            totalSendedPackByteSize += size;
            totalSendedPackCount += count;
        }

        private void OnIncreaseReceivedPackInfo(long size, long count)
        {
            totalReceivedPackByteSize += size;
            totalReceivedPackCount += count;
        }
    }
}