using System;

namespace ETModel
{
    public static class ProfilerUtility
    {
        public class NetworkProfiler
        {
            public long sendCount = 0;
            public double sendSize = 0;
            public long recevieCount = 0;
            public double recevieSize = 0;
            public long totalSendedCountOnDuration = 0;
            public long totalSendedSizeOnDuration = 0;
            public long totalReceivedCountOnDuration = 0;
            public long totalReceivedSizeOnDuration = 0;

            public double maxSendSize = 0;
            public double maxReceiveSize = 0;
            public long maxSendCount = 0;
            public long maxReceiveCount = 0;

            public int totalConnectedSessionCount { private set; get; }
            public long totalSendedCount { private set; get; }
            public long totalSendedSize { private set; get; }
            public long totalReceivedCount { private set; get; }
            public long totalReceivedSize { private set; get; }

            public NetworkProfiler() 
            {
                AChannel.OnIncreaseSendedPackInfo += OnIncreaseSendedPackInfo;
                AChannel.OnIncreaseReceivedPackInfo += OnIncreaseReceivedPackInfo;
            }

            private void OnIncreaseReceivedPackInfo(long size, long count)
            {
                totalReceivedCount += count;
                totalReceivedSize += size;
            }

            private void OnIncreaseSendedPackInfo(long size, long count)
            {
                totalSendedCount += count;
                totalSendedSize += size;
            }

            public void SetConnectedSessionCount(int count)
            {
                totalConnectedSessionCount = count;
            }

            public string Show(int fps)
            {
                sendSize = totalSendedSizeOnDuration;
                sendCount = totalSendedCountOnDuration;
                recevieSize = totalReceivedSizeOnDuration;
                recevieCount = totalReceivedCountOnDuration;

                totalSendedCountOnDuration = totalSendedCount;
                totalSendedSizeOnDuration = totalSendedSize;
                totalReceivedCountOnDuration = totalReceivedCount;
                totalReceivedSizeOnDuration = totalReceivedSize;

                sendSize = totalSendedSizeOnDuration - sendSize;
                sendCount = totalSendedCountOnDuration - sendCount;
                recevieSize = totalReceivedSizeOnDuration - recevieSize;
                recevieCount = totalReceivedCountOnDuration - recevieCount;
                sendSize /= 1024;
                recevieSize /= 1024;

                maxSendSize = Math.Max(maxSendSize, sendSize);
                maxReceiveSize = Math.Max(maxReceiveSize, recevieSize);
                maxSendCount = Math.Max(maxSendCount, sendCount);
                maxReceiveCount = Math.Max(maxReceiveCount, recevieCount);

                return $"Client count: {totalConnectedSessionCount}, Send: {sendSize:F2}KB/{sendCount}, Receive: {recevieSize:F2}KB/{recevieCount}, MaxSend: {maxSendSize:F2}KB/{maxSendCount}, MaxReceive: {maxReceiveSize:F2}KB/{maxReceiveCount}, FPS: {fps}";
            }

            public void Reset()
            {
                maxSendSize = 0;
                maxReceiveSize = 0;
                maxSendCount = 0;
                maxReceiveCount = 0;
            }
        }
    }
}