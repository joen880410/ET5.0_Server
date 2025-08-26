using System;
namespace ETModel
{
	public abstract class DBViewBase : Entity
	{
		public virtual string _DBViewCommand { set; get; }
	}

	public class DBViewDependencyAttribute : Attribute 
	{
		public Type[] Dependencies { private set; get; }

		public DBViewDependencyAttribute(params Type[] dependencies) 
		{
			this.Dependencies = dependencies;
		}
	}
}
