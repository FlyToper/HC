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
    public class PublicController : Controller
    {
        //
        // GET: /Public/

        private WebManagementDBEntities DBContext;
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 【发送验证邮件】
        ///  20170227
        /// </summary>
        public ActionResult SendEmail()
        {
            string checkType = Request["CheckType"];

            if (!string.IsNullOrEmpty(checkType))
            {

                string emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";
                string tomail = Request["txtEmail"];

                if (!Regex.IsMatch(tomail, emailStr))//邮箱格式不正确
                {
                    return Content("error");
                }

                //服务器的邮箱
                string serverEmail = ConfigurationManager.AppSettings["ServerEmailId"];
                string serverEmailPwd = ConfigurationManager.AppSettings["ServerEmailPwd"];

                if (checkType == "ValidateRegister")//【新用户注册】
                {
                    string registerCode = Request["registerCode"];
                    string uid = Request["id"];

                    string tiltle = "新用户注册";
                    string content = "尊敬的用户：欢迎注册本系统，通过邮箱验证完成注册，如果确认为本人操作，请点击如下链接完成注册<br/><a href='http://localhost:6008/Public/CheckRegisterCode?' ></a>：";

                    SendEmail myemail = new SendEmail(tomail, tiltle, content, serverEmail, serverEmailPwd);
                    myemail.setCode();//设置验证码
                    //Session.Add("ValidateRegisterCode", myemail.getCode());//获取验证码码保存到Session中
                    myemail.sendEmail();//发送邮件

                    return Content("success");
                }
                else if (checkType == "ValidateFind")//【找回密码】
                {
                    string tiltle = "找回密码";
                    string content = "尊敬的用户：您正在通过邮箱验证找回密码。如果不是本人操作，请忽略！<br/>您的验证码是：";

                    SendEmail myemail = new SendEmail(tomail, tiltle, content, serverEmail, serverEmailPwd);
                    myemail.setCode();//设置验证码
                    Session.Add("ValidateFindCode", myemail.getCode());//获取验证码码保存到Session中
                    myemail.sendEmail();//发送邮件

                    return Content("success");
                }
                else if (checkType == "ValidateUpdateEmail")//【更新邮箱】
                {
                    string tiltle = "邮箱修改";
                    string content = "尊敬的用户：您正在修改邮箱，为了确保你的账号安全，请把验证码输入到注册页面的验证码文本框中。如果不是本人操作，请忽略！<br/>您的验证码是：";

                    SendEmail myemail = new SendEmail(tomail, tiltle, content, serverEmail, serverEmailPwd);
                    myemail.setCode();//设置验证码
                    Session.Add("ValidateUpdateEmailCode", myemail.getCode());//获取验证码码保存到Session中
                    myemail.sendEmail();//发送邮件

                    return Content("success");
                }
                else if (checkType == "ValidateUpdatePwd")//【修改密码】
                {
                    string tiltle = "修改密码";
                    string content = "尊敬的用户：您正在进行密码修改，为了确保您的账号安全，请输入验证码。如果不是本人操作，请忽略！<br/>您的验证码是：";

                    SendEmail myemail = new SendEmail(tomail, tiltle, content, serverEmail, serverEmailPwd);
                    myemail.setCode();//设置验证码
                    Session.Add("ValidateUpdatePwdCode", myemail.getCode());//获取验证码码保存到Session中
                    myemail.sendEmail();//发送邮件

                    return Content("success");
                }
                else
                {
                    return Content("error");
                }
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 【验证邮箱注册码】
        ///  20170227
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckRegisterCode() 
        {
            
            try
            {
                int uid = Convert.ToInt32(Request["uid"]);
                string code = Request["registerCode"];

                DBContext = new WebManagementDBEntities();

                //1、查找是否有此用户
                var user = DBContext.UserInfo.Where( u => u.Id == uid && u.RegisterCode == code ).FirstOrDefault();

                //验证
                if (user != null)
                {
                    user.IsRegister = 1;
                    DBContext.UserInfo.Attach(user);
                    DBContext.Entry(user).State = System.Data.EntityState.Modified;
                    DBContext.SaveChanges();

                    ViewBag.UserEmail = user.Email;//邮箱
                    ViewBag.Title = "用户注册---完成邮箱验证";
                    return View();
                    //return Content("success");
                }
                else
                {
                    return Content("error");
                    
                }
              
            }
            catch
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 【保存异步上传图片】
        ///  20170309
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveImage() {
            try
            {
                if (Request.Files.Count > 0)
                {
                    //1、获取文件
                    HttpPostedFileBase proImage = Request.Files["uploadImage"];//获取上传图片

                    //2、判断大小
                    if (proImage.ContentLength > 5 * 1024 * 1024) 
                    {
                        return Content("error1");
                    }

                    //3、截取图片类型 image/png
                    string[] filetype = proImage.ContentType.Split('/');

                    //4、判断图片类型
                    if (filetype[1] == "jpg" || filetype[1] == "jpeg" || filetype[1] == "png" || filetype[1] == "gif")
                    {
                        //5、给上传文件重命名
                        string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString();

                        //6、文件保存路径
                        string serverpath = "/Uploadfile/AjaxFiles/"+filename+"."+filetype[1];
                        string filepath = Server.MapPath("~" + serverpath );

                        //7、保存图片
                        proImage.SaveAs(filepath);

                        return Content(serverpath);
                    }
                    else
                    {
                        return Content("error2");
                    }
                    
                }
                else
                {
                    //图片数目小于0
                    return Content("error2");
                }
            }
            catch
            {
                //上传失败
                return Content("error2");
            }
        }

        /// <summary>
        /// 【验证邮箱注册码】
        ///  20170417
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckApplyCode()
        {

            try
            {
                int uid = Convert.ToInt32(Request["uid"]);
                string code = Request["registerCode"];

                DBContext = new WebManagementDBEntities();

                //1、查找是否有此用户
                var doc = DBContext.DoctorInfo.Where(u => u.Id == uid && u.RegisterCode == code).FirstOrDefault();

                //验证
                if (doc != null)
                {
                    doc.State = 2;
                    DBContext.DoctorInfo.Attach(doc);
                    DBContext.Entry(doc).State = System.Data.EntityState.Modified;
                    DBContext.SaveChanges();

                    ViewBag.UserEmail = doc.Email;
                    ViewBag.Title = "申请认证医生---完成邮箱验证";

                    return View("CheckRegisterCode");
                    //return Content("success");
                }
                else
                {
                    return Content("error");

                }

            }
            catch
            {
                return Content("error");
            }
        }
    }
}
