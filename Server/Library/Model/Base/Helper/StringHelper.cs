using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ETModel
{
    public static class StringHelper
    {
        static readonly StringBuilder sb = new StringBuilder();
        public static IEnumerable<byte> ToBytes(this string str)
        {
            byte[] byteArray = Encoding.Default.GetBytes(str);
            return byteArray;

        }

        public static byte[] ToByteArray(this string str)
        {
            byte[] byteArray = Encoding.Default.GetBytes(str);
            return byteArray;
        }
        public static byte[] ToByteArray(this string str, Encoding encoding)
        {
            byte[] byteArray = encoding.GetBytes(str);
            return byteArray;
        }
        public static T[] String2Array<T>(this string[] str) where T : IConvertible
        {
            return str.Select(e => (T)Convert.ChangeType(e, typeof(T))).ToArray();
        }
        public static byte[] ToUtf8(this string str)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            return byteArray;
        }

        public static byte[] HexToBytes(this string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            var hexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < hexAsBytes.Length; index++)
            {
                string byteValue = "";
                byteValue += hexString[index * 2];
                byteValue += hexString[index * 2 + 1];
                hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return hexAsBytes;
        }

        public static string Fmt(this string text, params object[] args)
        {
            return string.Format(text, args);
        }

        public static string ListToString<T>(this List<T> list)
        {
            sb.Clear();
            for (int i = 0; i < list.Count(); i++)
            {
                sb.Append(list[i]);
                if (i != list.Count() - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
        public static string ArrayToString<T>(this T[] array)
        {
            sb.Clear();
            for (int i = 0; i < array.Count(); i++)
            {
                sb.Append(array[i]);
                if (i != array.Count() - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
        public static string ListToString<T>(this List<T> list, bool isSplit = false, string value = "")
        {
            sb.Clear();
            foreach (T t in list)
            {
                sb.Append(t);
                if (isSplit)
                {
                    sb.Append(value);
                }
            }
            return sb.ToString();
        }
        public static string MessageToStr(object message)
        {
            return message.ToJson();
        }
        public static bool IsEmpty(this string text) => text == string.Empty;
        public static string CombineText(params string[] texts)
        {
            if (texts.Length <= 0) return string.Empty;
            sb.Clear();
            for (int i = 0; i < texts.Length; i++)
            {
                sb.Append(texts[i]);
            }
            return sb.ToString();
        }

        private static readonly Regex s_EmojiRegex =
            new Regex(@"(<U[+])[0-9A-F]{5}(>)", RegexOptions.Singleline);
        //https://unicode.org/emoji/charts/full-emoji-list.html
        public static string Convert2Emoji(this string str)
        {
            byte[] emojiCode = new byte[4];
            foreach (Match match in s_EmojiRegex.Matches(str))
            {
                var a = match.Groups[0];
                var startIndex = str.IndexOf('<');
                var endIndex = str.IndexOf('>');
                if (startIndex == -1 || endIndex == -1)
                {
                    return str;
                }
                string s = str.Substring(startIndex + 1, endIndex - startIndex - 1);
                s = a.Value.Replace("U+", "").Replace("<", "").Replace(">", ""); ;
                for (int i = s.Length, k = 0, j = 2; i > 0; k++)
                {
                    _ = (i - j) < 0 ? j = 1 : j = 2;
                    i -= j;
                    var e = s.Substring(i, j);
                    //文字使用十六進制轉byte[]
                    emojiCode[k] = Convert.ToByte(e, 16);
                }
                str = str.Replace(a.Value, Encoding.UTF32.GetString(emojiCode));
            }
            return str;
        }
        /// <summary>
        ///    Escapes a minimal set of characters (\, *, +, ?, |, {, [, (,), ^, $,., #, and
        ///     white space) by replacing them with their escape codes. 
        /// </summary>
        /// <returns></returns>
        public static string Escape(this string str)
        {
            return Regex.Escape(str);
        }
    }
}