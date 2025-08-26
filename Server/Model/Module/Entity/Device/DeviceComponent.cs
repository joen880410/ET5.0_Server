using System;

namespace ETModel
{
    public sealed class DeviceComponent : Component
    {
        public MultiDictionary<int, int, Type> DeviceHandlerDict { get; set; } = new MultiDictionary<int, int, Type>();

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();
            
            this.DeviceHandlerDict?.Clear();
        }
    }
}