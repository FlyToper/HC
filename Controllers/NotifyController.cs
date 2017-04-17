using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 基于云的Web管理系统.Models;

namespace 基于云的Web管理系统.Controllers
{

    public class NotifyController : Controller
    {
        //数据上下文
        private WebManagementDBEntities DBContext;
        //
        // GET: /Notify/

        /// <summary>
        /// 【验证是否登陆】
        /// </summary>
        /// <returns>返回验证结果</returns>
        private bool IsLogin()
        {
            if (Session["UserName"] != null && Session["UId"] != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 【展示通知列表】
        ///  20170416
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            if (!IsLogin())
                return Content("error1");//跳转登录界面
            try
            {
                //1、创建上下文
                DBContext = new WebManagementDBEntities();
                int uid = Convert.ToInt32(Session["UId"]);//获取用户Id

                //2、查找数据
                var ls = DBContext.NotifyInfo.Where(n => n.UserId == uid && (n.Type == 2 || n.Type == 3) && n.IsRead == 0).OrderByDescending(n => n.SubDate).Select(n=>new{ n.Id,n.Title,n.Content,n.SubDate}).ToList();

                var data = new {
                    Total = ls.Count(),
                    Row = ls
                };

                return Json(data, JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return Content("error");//系统错误
            }
            //return View();
        }

        public ActionResult ReadAll()
        {
            if(!IsLogin())
                return Content("error1");//跳转登录界面
            try
            {
                DBContext = new WebManagementDBEntities();
                int uid = Convert.ToInt32( Session["UId"] );
                string sql = "UPDATE NotifyInfo SET IsRead = 1 WHERE UserId = " + uid + " and (Type = 2 or Type = 3) and IsRead = 0";

                if (DBContext.Database.ExecuteSqlCommand(@sql) > 0)
                    return Content("success");
                else
                    return Content("error3");
            }
            catch
            {
                return Content("error");//系统错误
            }
        }

    }
}
