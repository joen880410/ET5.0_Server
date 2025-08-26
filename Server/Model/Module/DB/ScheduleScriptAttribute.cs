using CronNET;
using System.Threading;

namespace ETModel
{
    public class ScheduleAttribute : BaseAttribute
    {
        //public string repeatRate { get; }
        public string name { get; }

        public ScheduleAttribute(string name) //, string repeatRate)
        {
            this.name = name;

            //this.repeatRate = repeatRate;
        }
    }
}
