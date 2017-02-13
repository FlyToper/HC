using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Configuration;

namespace 基于云的Web管理系统.Myashx
{
    /// <summary>
    /// ValidateEmail 的摘要说明
    /// </summary>
    public class ValidateEmail : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string txtEmail = context.Request["txtEmail"];

            string emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";
            //Regex emailReg = new Regex(emailStr);
            string serverEmailId = ConfigurationManager.AppSettings["ServerEmailId"];
            string serverEmailPwd = ConfigurationManager.AppSettings["ServerEmailPwd"];


            //context.Response.Write(txtEmail);

            if (Regex.IsMatch(txtEmail,emailStr))//有效邮箱
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
                
                //CheckType 区分是注册验证邮箱的验证码还是 找回密码的验证码

                string CheckType = context.Request["CheckType"];
                
                if(!string.IsNullOrEmpty(CheckType))
                {

                    if (CheckType == "ValidateRegisterEmail")
                    {
                        context.Session["emailRegisterCode"] = number;
                        sendEmail(txtEmail, "新用户注册", "尊敬的用户：欢迎注册本系统，通过邮箱验证完成注册，请把验证码输入到注册页面的验证码文本框中<br/>您的验证码是：" + number, serverEmailId, serverEmailPwd);

                        context.Response.Write("success");
                    }
                    else if (CheckType == "ValidateFindEmail")
                    {
                        context.Session["emailFindCode"] = number;
                        sendEmail(txtEmail, "找回密码", "尊敬的用户：通过邮箱验证找回密码<br/>您的验证码是：" + number, serverEmailId, serverEmailPwd);

                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("error");
                    }

                
                }
                else//CheckType空或者null
                {
                    context.Response.Write("error");
                }

                
            }
            else
            {
                //不是有效的邮箱
                context.Response.Write("error");
            }

        }


    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="tomail">收件人邮件地址</param>
    /// <param name="title">标题</param>
    /// <param name="content">邮件正文</param>
    /// <param name="FormUser">发件人账号</param>
    /// <param name="userPwd">发件人密码</param>
    public void sendEmail(string tomail, string title, string content, string FormUser, string userPwd)
    {
        MailAddress from = new MailAddress(FormUser);
        MailAddress to = new MailAddress(tomail);
        MailMessage MyMessage = new MailMessage(from, to);
        MyMessage.Priority = MailPriority.Normal;

        MyMessage.IsBodyHtml = true;
        MyMessage.Body = content;
        MyMessage.BodyEncoding = System.Text.Encoding.UTF8;
        MyMessage.Subject = title;

        string SmtpServer = "SMTP.163.com";
        SmtpClient client = new SmtpClient(SmtpServer);
        System.Net.NetworkCredential cred = new System.Net.NetworkCredential(FormUser, userPwd);
        client.Credentials = cred;
        client.Send(MyMessage);
}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}