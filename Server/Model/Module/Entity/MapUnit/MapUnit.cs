using ETHotfix;
using System;

namespace ETModel
{
    public enum MapUnitType
    {
        Hero,
        Npc
    }
#if !LIBRARY
    public sealed class MapUnit : Entity
    {
        public long Uid { get; set; } = 0;

        public MapUnitType MapUnitType { get; private set; }

        public int Point { get; private set; }
        public long Money { get; private set; }

        public void Awake(MapUnitType mapUnitType)
        {
            MapUnitType = mapUnitType;
            Room = null;
            Uid = 0;
            RoomId = 0;
            Info = null;
            StartRideTimeUtcTick = -1;
            EndRideTimeUtcTick = -1;
            Point = 0;
            Money = 0;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            
            base.Dispose();
        }

        #region Info

        public MapUnitInfo Info { get; set; }


        public void SetInfo(MapUnitInfo mapUnitInfo)
        {
            StartRideTimeUtcTick = mapUnitInfo.StartUTCTick;
            mapUnitInfo.MapUnitId = Id;
            Info = mapUnitInfo;
            Info.Uid = Uid;
        }
        #endregion

        #region Room

        public Room Room { get; set; }

        public long RoomId { get; set; } = 0;

        #endregion

        #region Start/End Time

        private const int secondForRecord = 300;

        public long StartRideTimeUtcTick { get; set; } = -1;
        public long EndRideTimeUtcTick { get; private set; } = -1;

        public void TrySetStartTime(bool isForce = false)
        {
            if (StartRideTimeUtcTick < 0 || isForce)
            {
                StartRideTimeUtcTick = DateTime.UtcNow.Ticks;
            }
        }

        public void TrySetEndTime()
        {
            EndRideTimeUtcTick = DateTime.UtcNow.Ticks;
        }

        #endregion

        #region CumulativeTime

        /// <summary>
        /// 單位(秒)
        /// </summary>
        /// <returns></returns>
        public long GetCumulativeTime()
        {
            if (StartRideTimeUtcTick <= 0 || EndRideTimeUtcTick <= 0)
            {
                Log.Error($"mapUnit uid:{Uid} StartRideTimeUtcTick:{StartRideTimeUtcTick}--EndRideTimeUtcTick:{EndRideTimeUtcTick}");
                return 0;
            }
            return TimeHelper.TickConvertToSeconds(EndRideTimeUtcTick - StartRideTimeUtcTick);
        }

        /// <summary>
        /// 判斷是否超過5分鐘，超過才可以存紀錄
        /// </summary>
        /// <returns></returns>
        public bool CanSaveRecord()
        {
            var second = GetCumulativeTime();
            return second > secondForRecord;
        }

        #endregion

        #region Note
        /// <summary>
        /// 附註(只限漫騎)
        /// </summary>
        public string Note { get; private set; } = string.Empty;

        public void SetNote(string note)
        {
            Note = note;
        }

        #endregion

        #region DeviceType

        public int DeviceType { get; private set; } = 0;

        public void SetDeviceType(int deviceType)
        {
            DeviceType = deviceType;
        }

        #endregion

        public void AddPoint(int point)
        {
            Point += point;
        }

        public void AddMoney(long money)
        {
            Money += money;
        }
    }
#endif
}
