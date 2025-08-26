namespace ETModel
{
	[Config((int)(  AppType.Realm | AppType.Gate | AppType.Map | AppType.Lobby))]
	public partial class LanguageSetting_ServerCategory : ACategory<LanguageSetting_Server>
	{
	}

	public class LanguageSetting_Server: IConfig
	{
		public long Id { get; set; }
		public string zh_tw;
		public string en;
		public string ja;
	}
}
