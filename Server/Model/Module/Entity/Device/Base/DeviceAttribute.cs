using System;

namespace ETModel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DeviceAttribute: BaseAttribute
    {
        public int DeviceType { get; }

        public DeviceAttribute(int deviceType)
        {
            this.DeviceType = deviceType;
        }
    }
}