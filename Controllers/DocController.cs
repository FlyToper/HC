using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using 基于云的Web管理系统.Framework;
using 基于云的Web管理系统.Models;

namespace 基于云的Web管理系统.Controllers
{
    public class DocController : Controller
    {
        //
        // GET: /Doc/
        //数据上下文
        private WebManagementDBEntities DBContext;

        public ActionResult Index()
        {
            if (!IsLogin())
                return RedirectToAction("Login");
            return View();
        }

        /// <summary>
        /// 【验证验证码】
        ///  20170416
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool CheckCode(string type, string code)
        {
            if (code == (string)Session["admin_" + type])
                return true;
            else
                return false;

        }

        /// <summary>
        /// 【申请认证界面】
        ///  20170416
        /// </summary>
        /// <returns></returns>
        public ActionResult Apply() 
        {
            return View();
        }

        /// <summary>
        ///  【检查邮箱是否存在】
        ///  20170416
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckEmail()
        {
            try
            {
                string email = Request["email"];
                DBContext = new WebManagementDBEntities();

                var user = DBContext.DoctorInfo.Where(u => u.Email == email).FirstOrDefault();

                if (user == null)
                {
                    return Content("success");
                }
                else
                {
                    return Content("error");
                }


            }
            catch
            {
                return Redirect("/User/_404");//跳转404
            }
        }

        /// <summary>
        /// 【医生认证申请信息入库】
        ///  20170416
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password1"></param>
        /// <param name="password2"></param>
        /// <param name="phone"></param>
        /// <param name="truename"></param>
        /// <param name="reason"></param>
        /// <param name="image"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult ApplyIn(string email, string password1, string password2, string phone, string truename, string reason, string image, string code)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password1) || string.IsNullOrEmpty(password2) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(truename) || string.IsNullOrEmpty(reason) ||  string.IsNullOrEmpty(code))
                return Content("error1");//数据不完整
            string emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";


            if (!Regex.IsMatch(email, emailStr))//邮箱格式不正确
            {
                return Content("error3");
            }

            if (password1 != password2)
            { 
                return Content("error4");//两次输入的密码不一致
            }

            if (!CheckCode("apply",code))
                return Content("error5");//验证码错误

            //执行数据入库
            DBContext = new WebManagementDBEntities();
            var doc = new DoctorInfo();
            doc.Email = email;
            doc.Pwd = password1;
            doc.Name = truename;
            doc.Phone = phone;
            doc.Reason = reason;
            doc.ImageUrls = image;
            doc.RegisterCode = Basic.GetMD5(code);
            doc.State = 1;//邮箱认证中

            DBContext.DoctorInfo.Attach(doc);
            DBContext.Entry(doc).State = System.Data.EntityState.Added;
            DBContext.SaveChanges();


            //服务器的邮箱----发送邮件
            string serverEmail = ConfigurationManager.AppSettings["ServerEmailId"];
            string serverEmailPwd = ConfigurationManager.AppSettings["ServerEmailPwd"];
            string url = ConfigurationManager.AppSettings["WebUrl"] + "/Public/CheckApplyCode?registerCode=" + Basic.GetMD5(code) + "&uid=" + doc.Id;

            string tiltle = "申请认证医生邮箱---" + ConfigurationManager.AppSettings["SystemName"];
            string content = "尊敬的用户：欢迎申请认证医生，通过邮箱验证完成第一步，如果确认为本人操作，请点击如下链接完成注册<br/><a href='" + url + "' >" + "点击这里（或复制链接）完成验证" + "</a>";

            SendEmail myemail = new SendEmail(email, tiltle, content, serverEmail, serverEmailPwd);

            myemail.sendEmail();//发送邮件

            return Content("success");
        }

        /// <summary>
        /// 【医生登录界面】
        ///  20170418
        /// </summary>
        /// <returns></returns>
        public ActionResult Login() 
        {
            try
            {
                return View();
            }
            catch
            {
                return Redirect("/User/_404");
            }
        }

        /// <summary>
        /// 【验证是否登陆】
        ///  20170418
        /// </summary>
        /// <returns></returns>
        private bool IsLogin()
        {
            if (Session["DocNum"] != null)
                return true;
            return false;
                    
        }

        /// <summary>
        /// 【退出系统】
        ///  20170418
        /// </summary>
        /// <returns></returns>
        public ActionResult Exit()
        {
            try
            {
                Session.Remove("DocNum");
                Session.Remove("DocName");

                return Content("success");
            }
            catch
            {
                return Content("error");
            }
 
        }

        /// <summary>
        /// 【医生验证登录页面】
        ///  20170418
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ActionResult CheckLogin(string email, string pwd, string code)
        {
            if (!CheckCode("doc_login", code))
                return Content("error5");

            try
            {
                DBContext = new WebManagementDBEntities();
                var doc = DBContext.DoctorInfo.Where(d => d.Email == email && d.Pwd == pwd).FirstOrDefault();
                if (doc == null)
                    return Content("error1");//找不到用户
                if (doc.State == 1)
                    return Content("error2");//邮箱未完成验证
                if (doc.State == 2)
                    return Content("error3");//管理员未审核
                if (doc.State == 11 || doc.State == 12)
                    return Content("error4");

                Session.Add("DocNum",doc.Email);//邮箱
                Session.Add("DocName", doc.Name);//姓名
                return Content("success");


            }
            catch
            {
                return Content("error");//系统错误
            }
        }
    }
}
