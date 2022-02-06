/// <summary>
/// Ryan Taylor
/// Created: 2021/02/17
/// 
/// This class is for helping with paswordHashes.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public static class StringHelpers
    {
        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/17
        /// 
        /// This method is for turning strings into SHA256.
        /// </summary>
        /// 
        ///<param name="source">The string password</param>
        ///<exception></exception>
        ///<returns>A string unreadable by humans</returns>
        public static string SHA256Value(this string source)
        {
            string result = "";

            byte[] data;
            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            var s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();
            return result;
        }
    }
}
