using System;

namespace ETModel
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class EventAttribute: BaseAttribute
	{
		public string EventId { get; }

		public EventAttribute(string type)
		{
			this.EventId = type;
		}
	}
}