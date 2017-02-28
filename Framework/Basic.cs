using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 基于云的Web管理系统.Framework
{
    public class Basic
    {
        /// <summary>
        /// 【获取字符串的MD5值】
        /// 20170225
        /// </summary>
        /// <param name="sDataIn">字符串</param>
        /// <returns>加密的md5字符串</returns>
        public static string GetMD5(string sDataIn)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }
    }
}