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

        /// <summary>
        /// 【字符串转换成日期类型】
        ///  20170407
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>DateTime类型</returns>
        public static DateTime str2DT(string str)
        {
            if (str == "最近一天")
                return DateTime.Now.AddDays(-1);
            else if (str == "最近一周")
                return DateTime.Now.AddDays(-7);
            else if (str == "最近一个月")
                return DateTime.Now.AddMonths(-1);
            else if (str == "最近一年")
                return DateTime.Now.AddYears(-1);
            else
                return DateTime.Now.AddDays(-7);
        }

        /// <summary>
        /// 【时间选择的编号转换成DateTime】
        /// </summary>
        /// <param name="num">编号</param>
        /// <returns>日期类型</returns>
        public static DateTime num2DT(string num) 
        {
            if (num == "1")
                return DateTime.Now.AddDays(-1);
            else if (num == "2")
                return DateTime.Now.AddDays(-7);
            else if (num == "3")
                return DateTime.Now.AddMonths(-1);
            else if (num == "4")
                return DateTime.Now.AddYears(-1);
            else
                return DateTime.Now.AddDays(-7);
        }

        /// <summary>
        /// 【论坛分类的数字编号转换成英文的编号】
        ///  20170407
        /// </summary>
        /// <param name="forumType">数字编号</param>
        /// <returns>英文编号</returns>
        public static string num2En(int forumType) 
        {
            string type = "YYMS";
            if (forumType == 1)//营养美食
            {
                type = "YYMS";
            }
            else if (forumType == 2)//塑身美体
            {
                type = "SSMT";
            }
            else if (forumType == 3)//健康宝典
            {
                type = "JKBD";
            }
            else if (forumType == 4)//育儿宝典
            {
                type = "YEBD";
            }
            else if (forumType == 5)//娱乐杂评
            {
                type = "YLZP";
            }
            else//男人女人
            {
                type = "NRNR";
            }

            return type;
        }

        /// <summary>
        /// 【论坛英文编号转换成数字编号】
        ///  20170407
        /// </summary>
        /// <param name="type">英文编号</param>
        /// <returns>数字编号</returns>
        public static int en2Num(string type)
        {
            if (type == "YYMS")
                return 1;
            else if (type == "SSMT")
                return 2;
            else if (type == "JKBD")
                return 3;
            else if (type == "YEBD")
                return 4;
            else if (type == "YLZP")
                return 5;
            else if (type == "NRNR")
                return 6;
            return 1;
        }

        /// <summary>
        /// 【论坛类型名字转换成英文编号】
        /// 20170407
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string name2type(string name) 
        {
            if (name == "营养美食")
                return "YYMS";
            else if (name == "塑身美体")
                return "SSMT";
            else if (name == "健康宝典")
                return "JKBD";
            else if (name == "育儿宝典")
                return "YEBD";
            else if (name == "娱乐杂评")
                return "YLZP";
            else if (name == "男人女人")
                return "NRNR";
            else if (name == "0")
                return "0";
            else
                return "YYMS";
        
        }
    }
}