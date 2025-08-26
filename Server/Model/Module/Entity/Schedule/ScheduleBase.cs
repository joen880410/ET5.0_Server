using System;
using System.Threading.Tasks;

namespace ETModel
{
    public abstract class ScheduleBase
    {
        public ScheduleComponent scheduleComponent;
        public string RepeatRate { get; set; }
        public string NextTime { get; set; }
        public abstract ETTask Run();
        public async Task Start()
        {
            try
            {
                await Run();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Log.Error(e);
            }
        }
    }
}
