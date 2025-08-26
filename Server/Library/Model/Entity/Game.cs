using System;
using UnityEngine;

namespace ETModel
{
	public static class Game
	{
		public static Action OnClosed;
		private static Scene scene;

		public static Scene Scene
		{
			get
			{
				if (scene != null)
				{
					return scene;
				}
				scene = new Scene();
				return scene;
			}
		}

		private static EventSystem eventSystem;

		public static EventSystem EventSystem
		{
			get
			{
				return eventSystem ?? (eventSystem = new EventSystem());
			}
		}

		private static ObjectPool objectPool;

		public static ObjectPool ObjectPool
		{
			get
			{
				return objectPool ?? (objectPool = new ObjectPool());
			}
		}
		public static void FixedUpdate()
		{
			EventSystem.FixedUpdate();
		}
#if !SERVER
		private static Hotfix hotfix;

		public static Hotfix Hotfix
		{
			get
			{
				return hotfix ?? (hotfix = new Hotfix());
			}
		}
#endif
		public static void Close()
		{
			scene.Dispose();
			scene = null;
			OnClosed?.Invoke();
			objectPool = null;
			
			eventSystem = null;
		}
	}
}