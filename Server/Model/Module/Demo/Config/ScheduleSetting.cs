namespace ETModel
{
	[Config((int)(AppType.Master))]
	public partial class ScheduleSettingCategory : ACategory<ScheduleSetting>
	{
	}

	public class ScheduleSetting: IConfig
	{
		public long Id { get; set; }
		public string _Func;
		public string _CronNet;
	}
}
