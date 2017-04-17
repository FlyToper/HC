using System;
using System.Collections.Generic;
using System.Configuration;
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

        /// <summary>
        /// 【设置最近登录信息】
        ///  20170410
        /// </summary>
        /// <param name="lastInfo">最近登录信息</param>
        /// <param name="currentItem">当前登录信息</param>
        /// <returns></returns>
        public static string setLastLoginInfo(string lastInfo, string currentItem)
        {
            //lastInfo = 旧|新
            try
            {
                string[] str1 = lastInfo.Split('|');
                return str1[1] + "|" + currentItem;
            }
            catch
            {
                return "0";
            }
            
            
        }

        /// <summary>
        /// 【获取最近的登陆信息】
        ///  20170410
        /// </summary>
        /// <param name="lastInfo">最近登录信息</param>
        /// <returns></returns>
        public static string getLoginInfo(string lastInfo)
        {
            string[] str1 = lastInfo.Split('|');
            return str1[0];
        }


        /// <summary>
        /// 【获取用户状态】
        ///  20170410
        /// </summary>
        /// <param name="status">状态码</param>
        /// <returns>状态</returns>
        public static string getUserStatus(byte? status)
        {
            if (status == 0) 
            {
                return "正常";
            }
            else if (status == 11)
            {
                return "已删除";
            }
            else if (status == 12)
            {
                return "已禁用";
            }
            else 
            {
                return "未知";
            }
        }

        /// <summary>
        /// 【获取通知发送方式】
        ///  20170411
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string getNoteType(string num)
        {
            if (num == "1")
                return "邮件";
            if (num == "2")
                return "站内";
            else
                return "邮件&站内";
        }

        /// <summary>
        /// 【发送邮件通知】
        ///  20170411
        /// </summary>
        /// <param name="toEmail">接受者邮箱</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool sendNoteByEmail( string toEmail, string  title, string content)
        {
            //服务器的邮箱----发送邮件
            string serverEmail = ConfigurationManager.AppSettings["ServerEmailId"];
            string serverEmailPwd = ConfigurationManager.AppSettings["ServerEmailPwd"];
            //string url = ConfigurationManager.AppSettings["WebUrl"] + "/Public/CheckRegisterCode?registerCode=" + Basic.GetMD5(code) + "&uid=" + uid;

            string tiltle = "通知：" + title;
            

            SendEmail myemail = new SendEmail(toEmail, tiltle, content, serverEmail, serverEmailPwd);

            myemail.sendEmail();//发送邮件
            return true;
        }

        /// <summary>
        /// 【获取资讯的状态】
        ///  20170413
        /// </summary>
        /// <param name="num">状态码</param>
        /// <returns></returns>
        public static string getNewsStatus(int num)
        {
            if (num == 0)
                return "正常";
            else if (num == 11)
                return "已删除";
            else if (num == 12)
                return "热门";
            else
                return "未知";
        }

        /// <summary>
        /// 【获取医生的状态】
        ///  20170416
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string getDocStatus(int num) 
        {
            if (num == 1)
                return "邮箱认证中";
            else if (num == 2)
                return "申请审核中";
            else if (num == 3)
                return "正常";
            else if (num == 11)
                return "已删除";
            else if (num == 12)
                return "已禁用";
            else
                return "未知";

        }
    }
}