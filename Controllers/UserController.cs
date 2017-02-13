using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using 基于云的Web管理系统.Models;
using System.IO;
using 基于云的Web管理系统.Framework;
using System.Text.RegularExpressions;

namespace 基于云的Web管理系统.Controllers
{
    public class UserController : Controller
    {
        // GET: /User/
        private WebManagementDBEntities DBContext;

        public ActionResult Login()
        {
            return View();
        }

      
        //public ActionResult LoginTest(FormCollection form)
        //{
        //    //ViewData["error"] = null;

        //    if (CheckLogin(form["Username"].Trim(),form["Password"].Trim()) == 1 )
        //    {
        //        Session.Add("username", form["UserName"]);


        //        return Redirect("/Home/Index");
        //    }
        //    else
        //    {
        //        ViewData["error"] = "用户名或者密码错误！";
        //        return View(ViewData);
        //    }
        //   // return View();  
        //}

        //连接字符串
        private readonly string connStr = ConfigurationManager.ConnectionStrings["connStr3"].ConnectionString;
        /// <summary>
        /// 检查用户登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>检查结果</returns>
        public ActionResult CheckLogin(string username, string password)
        {
            #region 注释
            // string sql = "select * from UserInfo where LoginId = '"+"Admin"+ "'and Pwd = '"+"123"+"'";
            //string sql = "select * from UserInfo where (LoginId = @LoginId or Email = @LoginId)  and Pwd = @Pwd";
            //Session.Add("username", "Admin");
            //return 1;

            //try
            //{
            //    using (SqlConnection con = new SqlConnection(connStr))
            //    {
            //        con.Open();
            //        using (SqlCommand cmd = con.CreateCommand())
            //        {
            //            cmd.CommandText = sql;
            //            cmd.Parameters.Add(new SqlParameter("@LoginId", name));
            //            cmd.Parameters.Add(new SqlParameter("@Pwd", pwd));


            //            using (SqlDataReader reader = cmd.ExecuteReader())
            //            {
            //                if (reader.Read())
            //                {
            //                    return 1;
            //                }
            //                else
            //                {
            //                    return 0;
            //                }
            //            }
            //            //return cmd.ExecuteNonQuery();
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            #endregion
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Content("error1");
            }
            try
            {
                //创建上下文
                DBContext = new WebManagementDBEntities();
                var user = DBContext.UserInfo.Where(u => u.Email == username && u.UserPwd == password && u.DelFlag == 0).FirstOrDefault();
                if (user == null)
                {
                    return Content("error2");
                }
                else
                {
                    if (user.State == "正常")
                    {
                        //保存相关Session
                        Session["UserName"] = username;
                        Session["NickName"] = user.NickName;

                        //执行更新
                        user.LastLoginTime = DateTime.Now;
                        user.LastIP = Request.UserHostAddress;
                        DBContext.UserInfo.Attach(user);
                        DBContext.Entry(user).State = System.Data.EntityState.Modified;
                        DBContext.SaveChanges();

                        //返回结果
                        return Content("success");
                    }
                    else
                    {
                        return Content("error3");
                    }
                }
                
            }
            catch {
                return RedirectToAction("_404");
            }

        }


        public ActionResult UpdatePwd() 
        {
            return View();
        }


       

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <returns>返回主页</returns>
        public ActionResult Exit()
        {
            Session.Remove("UserName");
            Session.Remove("NickName");
            return Redirect("/Home/Index");
        }


        public ActionResult Register()
        {
            return View("Register2");
        }

        public ActionResult FinishRegister(string txtUserName, string txtEmail, string txtCode, string txtDeviceId, string txtPwd1, string txtPwd2, string txtCode2)
        {
            if(txtCode2 == (string)Session["RegisterCode"])
            {
                if (txtPwd1 == txtPwd2)
                {
                    if (checkUserName(txtUserName))
                    {
                        if (checkEmail(txtEmail))
                        {
                            if (SaveRegisterInfo(txtUserName, txtEmail, txtPwd1, txtDeviceId))
                            {
                                return Content("success");
                            }
                            else
                            {
                                //保存用户信息失败
                                return Content("error");
                            }
                        }
                        else
                        {
                            //邮箱已经存在
                            return Content("error");
                        }
                    }
                    else
                    {
                        //用户名已经存在
                        return Content("error");
                    }
                }
                else
                {
                    //l两次名密码不一致
                    return Content("error");
                }
            }
            else
            {
                //注册验证码不正确
                return Content("error");
            }
        }

        /// <summary>
        /// 保存注册信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="email">邮箱</param>
        /// <param name="pwd">密码</param>
        /// <param name="deviceId">设备Id</param>
        /// <returns>是否成功保存</returns>
        private bool SaveRegisterInfo(string username,string email, string pwd, string deviceId)
        {
            string sql = "Insert into UserInfo(LoginId, Pwd, TrueName, Email, DelFlg,  DeviceId, Status, Power) values(@UserName, @Pwd, @TrueName, @Email, @DelFlag,  @DeviceId, @Statues, @Power)";

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.Add(new SqlParameter("@UserName", username));
                        cmd.Parameters.Add(new SqlParameter("@Pwd", pwd));
                        cmd.Parameters.Add(new SqlParameter("@TrueName", ""));
                        cmd.Parameters.Add(new SqlParameter("@Email", email));
                        cmd.Parameters.Add(new SqlParameter("@DelFlag", false));
                        cmd.Parameters.Add(new SqlParameter("@DeviceId", deviceId));
                        cmd.Parameters.Add(new SqlParameter("@Statues", 1));
                        cmd.Parameters.Add(new SqlParameter("@Power", 2));

                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        /// <summary>
        /// 检查用户名和邮箱是否存在
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="email">邮箱</param>
        /// <returns>存在与否</returns>
        private bool checkUserName(string username)
        {
            string sql1 = "select * from UserInfo where LoginId = @LoginId";
            

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql1;
                    cmd.Parameters.Add(new SqlParameter("@LoginId", username));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return true;
                        }
                        else
                        {
                            //用户名已经存在
                            return false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 检查用户提交的注册表单的 邮箱
        /// </summary>
        /// <param name="con"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool checkEmail( string email)
        {
            string sql2 = "select * from UserInfo where Email = @Email";


            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql2;
                    cmd.Parameters.Add(new SqlParameter("@Email", email));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                } 
            }
        }

        /// <summary>
        /// 【个人信息展示】
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowMyInfo() 
        {
            if (Session["UserName"] != null)
            {
                string username = Session["UserName"].ToString();
                try
                {
                    DBContext = new WebManagementDBEntities();
                    var user = DBContext.UserInfo.Where(u => u.Email == username && u.DelFlag == 0).FirstOrDefault();
                    if (user == null)
                    {
                        return RedirectToAction("_404");
                    }
                    else
                    {
                        ViewBag.UserInfo = user;
                        return View();
                    }
                
               
                }
                catch
                {
                    return RedirectToAction("_404");
                }
            }
            else 
            {
                return RedirectToAction("../Home/Login", new { referenUrl="/User/ShowMyInfo"});
            }
        }


        /// <summary>
        /// 【生成修改个人信息的验证码图片】
        /// </summary>
        /// <returns></returns>
        public void CreateCheckCode()
        {
            CreateCheckCode code = new CreateCheckCode();//创建一个验证码对象
            String number = code.GenerateCheckCode();//获取随机码
            Session.Add("UpdateInfo_Code", number);//保存到Session

            byte[] fileContents = code.CreateCheckCodeImage(number);

            //Stream ms = new MemoryStream(fileContents);
            //return ms;
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(fileContents);
        }


        /// <summary>
        /// 【发送验证邮件】
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
                    string tiltle = "新用户注册";
                    string content = "尊敬的用户：欢迎注册本系统，通过邮箱验证完成注册，请把验证码输入到注册页面的验证码文本框中<br/>您的验证码是：";

                    SendEmail myemail = new SendEmail(tomail, tiltle, content, serverEmail, serverEmailPwd);
                    myemail.setCode();//设置验证码
                    Session.Add("ValidateRegisterCode", myemail.getCode());//获取验证码码保存到Session中
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
        /// 【验证验证码】
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckCode()
        {
            string codeType = Request["CodeType"];
            string txtCode = Request["txtCode"];

            if (!string.IsNullOrEmpty(codeType) && !string.IsNullOrEmpty(txtCode))
            {
                //ValidateFindCode, ValidateRegisterCode, ValidateUpdateEmailCode, ValidateUpdatePwdCode
                string typename = "Validate" + codeType + "Code";
                if (Session[typename].ToString() == txtCode)
                {
                    Session.Remove(typename);
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
        /// 【检查并更新密码】
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckUpdatePwd()
        {
            //修改密码前，需要先验证是否登陆
            if (Session["UserName"] != null)
            {
                string pwd1 = Request["UpdatePwd1"];
                string pwd2 = Request["UpdatePwd2"];

                if (pwd1.Length <= 20 && pwd2.Length <= 20)
                {
                    if (pwd1 == pwd2)
                    {
                        //执行数据库更新
                        //string sql = "update UserInfo set "
                        try
                        {
                            string username = Session["UserName"].ToString();
                            DBContext = new WebManagementDBEntities();
                            var user = DBContext.UserInfo.Where(u => u.Email == username && u.DelFlag == 0).FirstOrDefault();
                            if (user == null)
                            {
                                return Content("error");
                            }
                            else
                            {
                                user.UserPwd = pwd1;
                                DBContext.UserInfo.Attach(user);
                                DBContext.Entry(user).State = System.Data.EntityState.Modified;
                                DBContext.SaveChanges();

                                Session.Remove("UserName");
                                Session.Remove("NickName");
                                return Content("success");
                            }
                        }
                        catch
                        {
                            return RedirectToAction("_404");
                        }
                        
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
            else
            {
                return RedirectToAction("../Home/Login", new { referenUrl = "/User/ShowMyInfo" });
            }
        }//结束

        /// <summary>
        /// 【修改个人信息】
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateUserInfo()
        {
            string trueName = Request["TrueName"];
            string gender = Request["Gender"];
            string deviceId = Request["DeviceId"];
            string phone = Request["Phone"];
            string code = Request["Code"];

            if (!string.IsNullOrEmpty(trueName) && !string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(deviceId) && !string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(code))
            {
                if (code == Session["UpdateInfo_Code"].ToString())
                {
                    string username = Session["UserName"].ToString();
                    try
                    {
                        //执行更新操作
                        DBContext = new WebManagementDBEntities();
                        var user = DBContext.UserInfo.Where(u=>u.Email == username).FirstOrDefault();
                        if (user == null)
                        {
                            return Content("error1");
                        }
                        else
                        {
                            user.TrueName = trueName;
                            user.DeviceId = deviceId;
                            if (gender == "man") 
                            {
                                user.Gender = "男";
                            }
                            else if (gender == "woman")
                            {
                                user.Gender = "女";
                            }
                            user.Phone = phone;
                            DBContext.UserInfo.Attach(user);
                            DBContext.Entry(user).State = System.Data.EntityState.Modified;
                            DBContext.SaveChanges();
                            return Content("success");
                        }
                    }
                    catch 
                    {
                        return RedirectToAction("_404");
                    }
                }
                else
                {
                    return Content("error2");
                }
            }
            else
            {
                return Content("error3");
            }
        }

        public ActionResult _404() 
        {
            return View();
        }
    }
}
