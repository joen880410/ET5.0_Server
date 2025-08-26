using System;
using System.Reflection;

namespace ETModel
{
	public static class MethodInfoHelper
	{
		public static void Run(this MethodInfo methodInfo, object obj, params object[] param)
		{

			if (methodInfo.IsStatic)
			{
				object[] p = new object[param.Length + 1];
				p[0] = obj;
				for (int i = 0; i < param.Length; ++i)
				{
					p[i + 1] = param[i];
				}
				methodInfo.Invoke(null, p);
			}
			else
			{
				methodInfo.Invoke(obj, param);
			}
		}
        /// <summary>
        /// 尋找對應參數info
        /// </summary>
        /// <param name="method">方法名稱</param>
        /// <param name="parameterName">參數名稱</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ParameterInfo GetParameterByName(this MethodInfo method, string parameterName)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (string.IsNullOrEmpty(parameterName)) throw new ArgumentNullException(nameof(parameterName));

            foreach (var parameter in method.GetParameters())
            {
                if (parameter.Name == parameterName)
                    return parameter;
            }

            return null; // 找不到則返回 null
        }
        public static ParameterInfo GetParameterByType(this MethodInfo method, Type parameterType)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (parameterType == null) throw new ArgumentNullException(nameof(parameterType));

            foreach (var parameter in method.GetParameters())
            {
                if (parameter.ParameterType == parameterType)
                    return parameter;
            }

            return null; // 找不到則返回 null
        }
    }
}