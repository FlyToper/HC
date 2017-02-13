using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 基于云的Web管理系统.Models;
using System.Security.Cryptography;
using System.Text;

namespace 基于云的Web管理系统.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        //数据上下文
        private WebManagementDBEntities DBContext;

       


        /// <summary>
        /// 【登录页面】
        /// </summary>
        /// <returns></returns>
        public ActionResult Login() 
        {

            if (string.IsNullOrEmpty(Request["referenUrl"]))
            {
                ViewData["Referen"] = "/Admin";
            }
            else
            {
                ViewData["Referen"] = Request["referenUrl"];
            }
            return View();
        }

        /// <summary>
        /// 【生成验证码】
        /// </summary>
        /// <param name="type">验证码类型</param>
        public void CreateCode(string type)
        {
            try
            {
                
                CreateCheckCode code = new CreateCheckCode();//创建一个验证码对象
                String number = code.GenerateCheckCode();//获取随机码
                Session.Add("admin_" + type, number);//保存到Session

                byte[] fileContents = code.CreateCheckCodeImage(number);

                //Stream ms = new MemoryStream(fileContents);
                //return ms;
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(fileContents);
            }
            catch 
            { 
            }
        }

        private bool CheckCode(string type, string code) {
           if(code == (string)Session["admin_" + type])
               return true;
            else 
               return false;
            
        }

        /// <summary>
        /// 【登录验证】
        /// 20170122
        /// </summary>
        /// <returns>返回验证结果</returns>
        public ActionResult CheckLogin()
        {
            try
            {
                string username = Request["email"];
                string pwd = Request["pwd"];
                string code = Request["code"];

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(pwd) && !string.IsNullOrEmpty(code))
                {
                    if (this.CheckCode("login", code))
                    {
                        DBContext = new WebManagementDBEntities();
                        string pwd2 = GetMD5(pwd);
                        var user = DBContext.AdminInfo.Where(u => u.Email == username && u.AdminPwd == pwd2 && u.DelFlag == 0).FirstOrDefault();

                        if (user == null)
                        {
                            //用户名或者密码出错
                            return Content("error2");
                        }
                        else 
                        {
                            Session["AdminUser"] = username;
                            return Content("success");
                        }
                    }
                    else
                    {
                        return Content("error1");

                    }
                }
                else
                {
                    return Content("error2");
                }

                
            }catch
            {
                return Redirect("/User/_404");
            }
            
        }


        /// <summary>
        /// 【获取字符串的MD5值】
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

        public ActionResult Exit() {
            if (Session["AdminUser"] == null)
                return Content("error");
            else {
                Session.Remove("AdminUser");
                return Content("success");
            }
        }


        public ActionResult Show() {
          
            return Content(GetMD5("123"));
        }

        /// <summary>
        /// 【验证是否登陆】
        /// </summary>
        /// <returns>返回验证结果</returns>
        private bool IsLogin() {
            if (Session["AdminUser"] != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 【后台管理首页】
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            try
            {
                if (IsLogin())
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login",new { referenUrl = "/Admin" });
                }
            }
            catch
            {
                return Redirect("/User/_404");
            }
        }

    }
}
