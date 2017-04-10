using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 基于云的Web管理系统.Framework;
using 基于云的Web管理系统.Models;

namespace 基于云的Web管理系统.Controllers
{
    public class AdminFunController : Controller
    {
        //数据上下文
        private WebManagementDBEntities DBContext;
        //
        // GET: /AdminFun/


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
        /// 【获取用户详细信息】
        ///  20170410
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public ActionResult GetUserInfo(int id)
        {
            if(!IsLogin())
                return RedirectToAction("Login", "Admin", new { referenUrl = "/Admin/User" });
            //try
            //{
                DBContext = new WebManagementDBEntities();
                var user =  DBContext.UserInfo.Where(u=>u.Id == id).FirstOrDefault();
                if(user == null)
                {
                    return Content("error1");//无法找到
                }
                var data = new
                {
                    Email = user.Email,
                    NickName = user.NickName,
                    TrueName = user.TrueName,
                    Phone = user.Phone,
                    Gender = user.Gender,
                    DeviceId = user.DeviceId,
                    LastLoginTime = Basic.getLoginInfo(user.LastLoginTime),
                    LastLoginIP = Basic.getLoginInfo(user.LastIP),
                    SubDate = user.SubDate.ToString(),
                    Status = Basic.getUserStatus(user.DelFlag)

                };

                return Json(data, JsonRequestBehavior.DenyGet);
            //}
            //catch 
            //{
            //    return Redirect("/User/_404");
            //}
        }

    }
}
