using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace 基于云的Web管理系统.Framework
{
    public class SendEmail
    {
        private string tomail;//收件人邮箱地址
        private string title;//邮箱标题
        private string content;//邮箱正文
        private string FormUser;//发件人邮箱
        private string userPwd;//发件人密码
        private string code;//验证码

        private string checkType;//需要验证方式

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tomail">收件人邮件地址</param>
        /// <param name="title">标题</param>
        /// <param name="content">邮件正文</param>
        /// <param name="FormUser">发件人账号</param>
        /// <param name="userPwd">发件人密码</param>
        public SendEmail(string tomail, string title, string content, string FormUser, string userPwd)
        {
            this.tomail = tomail;
            this.title = title;
            this.content = content;
            this.FormUser = FormUser;
            this.userPwd = userPwd;
        }

        /// <summary>
        /// 【发送邮件】
        /// </summary>
        public  void sendEmail()
        {
            try
            {
                MailAddress from = new MailAddress(FormUser);
                MailAddress to = new MailAddress(tomail);
                MailMessage MyMessage = new MailMessage(from, to);
                MyMessage.Priority = MailPriority.Normal;

                MyMessage.IsBodyHtml = true;
                MyMessage.Body = content +code;//发送邮件正文和验证码
                MyMessage.BodyEncoding = System.Text.Encoding.UTF8;
                MyMessage.Subject = title;

                string SmtpServer = "SMTP.163.com";
                SmtpClient client = new SmtpClient(SmtpServer);
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential(FormUser, userPwd);
                client.Credentials = cred;
                client.Send(MyMessage);

            }
            catch 
            {
                throw new Exception("发送邮件失败，请重试！");
            }
        }

        /// <summary>
        /// 【获取随机的验证码，保存Session】
        /// </summary>
        /// <returns></returns>
        public void setCode()
        {

            Random random = new Random();
            string number = "";
            int x;
            for (int i = 0; i < 6; i++)
            {
                x = random.Next(0, 100);
                x = x % 10;
                number += x.ToString();
            }

            this.code = number;//设置验证码
            
        }

        /// <summary>
        /// 【获取验证码】
        /// </summary>
        /// <returns>6位数的随机验证码</returns>
        public string getCode()
        {
            return this.code;
        }

    }
}