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
                            Session["AdminUser"] = user.AdminName;
                            Session["AdminNum"] = user.Email;

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
                Session.Remove("AdminUser");//用户名
                Session.Remove("AdminNum");//用户邮箱
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
                    DBContext = new WebManagementDBEntities();

                    //1、查询需要处理的留言
                    var comments = DBContext.CommentInfo.Where(u => u.IsRead == 0).OrderByDescending(u => u.SubData);

                    //2.1 查询最新资讯距今的天数
                    var  news = DBContext.HealthInfo.Where(u => u.DelFlag == 0).OrderByDescending(u => u.SubDate).Take(1);

                    TimeSpan ts = new TimeSpan();
                    foreach (HealthInfo n in news)
                    {   
                        ts = (TimeSpan)(DateTime.Now - n.SubDate);
                    }
                    
                    //2.2 查询医生认证的人数

                    //2.3 查询待审阅的建议条数
                    ViewBag.Coment_Count = DBContext.CommentInfo.Where(u => u.IsRead == 0).Count();
                   

                   

                    ViewBag.Comment = comments;
                    ViewBag.NewsDays = Convert.ToInt32(ts.TotalDays);
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

        /// <summary>
        /// 【修改建议的状态--已读】
        ///  20170228
        /// </summary>
        /// <returns>成功与否标识</returns>
        public ActionResult CheckCommentStatus() 
        {
            try
            {
                if(!IsLogin())
                    return Content("error3");

                int cid = Convert.ToInt32(Request["id"]);
                if (cid != 0)
                {
                    DBContext = new WebManagementDBEntities();
                    var comment = DBContext.CommentInfo.Where(u => u.Id == cid && u.IsRead == 0).FirstOrDefault();

                    if (comment != null)
                    {
                        comment.IsRead = 1;
                        comment.ReDate = DateTime.Now;

                        DBContext.CommentInfo.Attach(comment);
                        DBContext.Entry(comment).State = System.Data.EntityState.Modified;
                        DBContext.SaveChanges();

                        AdminExecute.Insert(Session["AdminNum"].ToString(), Session["AdminUser"].ToString(), "操作用户建议为已读，id为：【"+cid+"】");


                        return Content("success");

                    }
                    else 
                    {
                        return Content("error1");
                    }
                }
                else
                {
                    return Content("error1");
                }
            }
            catch
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 【发布新闻的视图】
        ///  20170308
        /// </summary>
        /// <returns></returns>
        public ActionResult News() {
            if (IsLogin())
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", new { referenUrl = "/Admin/News" });
            }
        }

        /// <summary>
        /// 【保存新闻】
        ///  20170309
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SaveNews()
        {
            //try
            //{
                if(!IsLogin())
                {
                    return Content("error2");//请先登录
                }

                //1、获取参数
                string title = Request["title"];//标题
                string description = Request["description"];//描述
                string from = Request["from"];//来源
                string image = Request["image"];//封面地址
                string content = Request["content"];//主题内容

                //2、判断是否为空
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(image) || string.IsNullOrEmpty(content)) {
                    return Content("error-3");
                }

                //3、创建上下文
                DBContext = new WebManagementDBEntities();
                HealthInfo hi = new HealthInfo();
                
                //4、数据填充
                hi.Title = title;
                hi.Description = description;
                hi.Content = content;
                hi.IsHot = 0;
                hi.ImageUrls = image;
                hi.SubDate = DateTime.Now;
                hi.SubNum = Session["AdminNum"].ToString();
                hi.SubName = Session["AdminUser"].ToString();
                hi.DelFlag = 0;
                hi.Remark = "发表时间： " + DateTime.Now + "来源：" + from;

                DBContext.HealthInfo.Attach(hi);
                DBContext.Entry(hi).State = System.Data.EntityState.Added;

                if (DBContext.SaveChanges() > 0)//执行保存
                {
                    var model = DBContext.HealthInfo.Where(u => u.Title == title).FirstOrDefault();
                    if (model != null)
                    {
                        AdminExecute.Insert(Session["AdminNum"].ToString(), Session["AdminUser"].ToString(), "发布新资讯，id为：【" + model.Id + "】");
                        return Content("success");
                    }
                }
                return Content("error-2");//保存失败


                
            //}
            //catch
            //{
            //    return Content("error-1");
            //}
        }


        /// <summary>
        /// 【查看新闻资讯】
        ///  20170309
        /// </summary>
        /// <returns></returns>
        public ActionResult NewsRecord() 
        {
            if (IsLogin())
            {
                try
                {
                    DBContext = new WebManagementDBEntities();
                    ViewBag.HInfos = DBContext.HealthInfo.Where(h => h.DelFlag == 0).OrderByDescending(h => h.SubDate);

                    return View();
                }
                catch
                {
                    return Redirect("/User/_404");
                }
               
            }
            else
            {
                return RedirectToAction("Login", new { referenUrl = "/Admin/NewsRecord" });
            }
        }

        /// <summary>
        /// 【浏览历史留言】
        ///  20170408
        /// </summary>
        /// <returns></returns>
        public ActionResult CommentRecord()
        {
            if (IsLogin())
            {
                try
                {
                    DBContext = new WebManagementDBEntities();
                    ViewBag.CInfos  = DBContext.CommentInfo.Where(c=>c.Id >0).OrderByDescending(c=>c.SubData);



                    return View();
                }
                catch 
                {
                    return Redirect("/User/_404");
                }
                
                
            }
            else 
            {
                return RedirectToAction("Login", new { referenUrl = "/Admin/CommentRecord" });
            }
        }

        /// <summary>
        /// 【最新留言--即系未读留言展示】
        ///  20170409
        /// </summary>
        /// <returns></returns>
        public ActionResult Comment() 
        {
            if (IsLogin())
            {
                try
                {
                    DBContext = new WebManagementDBEntities();
                    ViewBag.CInfos = DBContext.CommentInfo.Where(c => c.IsRead == 0).OrderByDescending(c => c.SubData).ToList();

                    return View();
                }
                catch
                {
                    return Redirect("/User/_404");
                }
            }
            else 
            {
                return RedirectToAction("Login", new { referenUrl = "/Admin/Comment" });
            }
        }

        /// <summary>
        /// 【用户管理】
        /// 20170409
        /// </summary>
        /// <returns></returns>
        public ActionResult User()
        {
            if (IsLogin())
            {
                try
                {
                    DBContext = new WebManagementDBEntities();
                    ViewBag.UInfos = DBContext.UserInfo.Where(u => u.Id > 0 && u.IsRegister == 1).OrderByDescending(u=>u.Id).ToList();
                    return View();

                }
                catch
                {
                    return Redirect("/User/_404");
                }
            }
            else 
            {
                return RedirectToAction("Login", new { referenUrl = "/Admin/User" });
            }
        }

        /// <summary>
        /// 【浏览未完成注册的用户】
        ///  20170409
        /// </summary>
        /// <returns></returns>
        public ActionResult NRUser()
        {
            if(!IsLogin())
                return RedirectToAction("Login", new { referenUrl = "/Admin/NRUser" });
            try
            {
                DBContext = new WebManagementDBEntities();
                ViewBag.UInfos = DBContext.UserInfo.Where(u => u.IsRegister == 0).OrderByDescending(u => u.Id).ToList();

                return View();
            }
            catch
            {
                return Redirect("/User/_404");
            }
        }

        /// <summary>
        /// 【修改用户的状态】
        ///  20170409
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult ChangeUserStatus(int id, int status) 
        {
            if (!IsLogin())
                return RedirectToAction("Login", new { referenUrl = "/Admin/User" });
            try
            {
            DBContext = new WebManagementDBEntities();
                var user = DBContext.UserInfo.Where(u => u.Id == id).FirstOrDefault();
                if (user == null)
                    return Content("error1");//找不到用户
                user.DelFlag = (byte)status;
                DBContext.UserInfo.Attach(user);
                DBContext.Entry(user).State = System.Data.EntityState.Modified;

                if (DBContext.SaveChanges() > 0)
                {
                    AdminExecute.Insert(Session["AdminNum"].ToString(), Session["AdminUser"].ToString(), "修改用户状态为"+status+"，用户id为：【" + id + "】");
                    
                    return Content("success");
                }
                else
                {
                    return Content("error2");//保存失败
                }

            }
            catch
            {
                return Redirect("/User/_404");
            }
        }
    }
}
