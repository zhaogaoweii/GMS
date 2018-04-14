using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 与加密/解密 有关的公共方法
    /// </summary>
    public static class EncryptionHelper
    {
        private static string encryptKey = "ciic";

        /// <summary>
        /// 功能：将加密的字符解析出来(Base64)
        /// 时间：2013-03-25        
        /// </summary>     
        /// <param name="decryptString">加密过的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string GetDecryptionByBase64(string decryptString)
        {
            if (String.IsNullOrEmpty(decryptString))
            {
                return "";
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[decryptString.Length / 2];
            for (int x = 0; x < decryptString.Length / 2; x++)
            {
                int i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(encryptKey); //Key
            des.IV = ASCIIEncoding.ASCII.GetBytes(encryptKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder(); //CreateDecrypt
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// 功能：将加密的字符解析出来(Base64)
        /// 时间：2013-03-25        
        /// </summary>     
        /// <param name="decryptString">加密过的字符串</param>
        ///<param name="key">加密Key</param>
        /// <returns>解密后的字符串</returns>
        public static string GetDecryptionByBase64(string decryptString, string key)
        {
            encryptKey = key;
            return GetDecryptionByBase64(decryptString);
        }

        /// <summary>
        /// 功能：将传入的参数进行加密(Base64)
        /// 时间：2013-03-25        
        /// </summary>     
        /// <param name="encryptString">未加密码的字符串</param>
        /// <returns>加密之后的字符串</returns>
        public static string GetEncryptionByBase64(string encryptString)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(encryptString);
            des.Key = ASCIIEncoding.ASCII.GetBytes(encryptKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(encryptKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// 功能：将传入的参数进行加密(Base64)
        /// 时间：2013-03-25        
        /// </summary>     
        /// <param name="encryptString">未加密码的字符串</param>
        ///<param name="key">加密Key</param>
        /// <returns>加密之后的字符串</returns>
        public static string GetEncryptionByBase64(string encryptString,string key)
        {
            encryptKey = key;
            return GetEncryptionByBase64(encryptString);
        }

        /// <summary>
        /// 对字符串进行Base64编码
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string ToBase64(this string value)
        {
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 对字符串进行Base64解码
        /// </summary>
        /// <param name="value">Base64编码的字符串</param>
        /// <returns>Base64解码的字符串</returns>
        public static string FromBase64(this string value)
        {
            byte[] encodedBytes = Convert.FromBase64String(value);
            return UTF8Encoding.UTF8.GetString(encodedBytes);
        }
    }
}
