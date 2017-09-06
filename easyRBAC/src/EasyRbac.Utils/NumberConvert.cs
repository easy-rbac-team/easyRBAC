using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyRbac.Utils
{
    public class NumberConvert : INumberConvert
    {

        public string Characters
        {
            get;
            set;
        }

        public int Length
        {
            get
            {
                if (Characters != null)
                    return Characters.Length;
                else
                    return 0;
            }

        }

        public NumberConvert()
        {
            Characters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        public NumberConvert(string characters)
        {
            Characters = characters;
        }

        /// <summary>
        /// 数字转换为指定的进制形式字符串
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string ToString(long number)
        {
            List<string> result = new List<string>();
            long t = number;

            if (number == 0)
            {
                return this.Characters[0].ToString();
            }

            while (t > 0)
            {
                var mod = t % Length;
                t = Math.Abs(t / Length);
                var character = Characters[Convert.ToInt32(mod)].ToString();
                result.Insert(0, character);
            }

            return string.Join("", result.ToArray());
        }

        /// <summary>
        /// 指定字符串转换为指定进制的数字形式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public long FromString(string str)
        {
            long result = 0;
            int j = 0;
            foreach (var ch in new string(str.ToCharArray().Reverse().ToArray()))
            {
                if (Characters.Contains(ch))
                {
                    result += Characters.IndexOf(ch) * ((long)Math.Pow(Length, j));
                    j++;
                }
            }
            return result;
        }


    }
}
