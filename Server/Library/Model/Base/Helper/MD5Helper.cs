using System.IO;
using System.Security.Cryptography;

namespace ETModel
{
	public static class MD5Helper
	{
		public static string FileMD5(string filePath)
		{
			byte[] retVal;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
                using (MD5 md5 = MD5.Create())
                {
                    retVal= md5.ComputeHash(file);//固定16 bytes
                }
			}
			return retVal.ToHex("x2");
		}
	}
}
