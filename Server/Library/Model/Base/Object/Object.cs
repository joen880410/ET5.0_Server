using System.Collections.Generic;
using System.ComponentModel;

namespace ETModel
{
	public abstract class Object: ISupportInitialize
	{
		public virtual void BeginInit()
		{
		}
		public virtual void BeginDBInit(string ts)
		{
		}
		public virtual void EndInit()
		{
		}
	}
}