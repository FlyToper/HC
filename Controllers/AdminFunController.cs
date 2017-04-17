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
            try
            {
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
            }
            catch 
            {
                return Redirect("/User/_404");
            }
        }

        /// <summary>
        /// 【给用户发送通知】
        ///  20170411
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型</param>
        /// <param name="id">用户id</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public ActionResult SendNote(string title, string content, string type, string id, string email,string utype) 
        {
            if (!IsLogin())
                return RedirectToAction("Login", "Admin", new { referenUrl = "/Admin/User" });
            try
            {
                DBContext = new WebManagementDBEntities();
                NotifyInfo note = new NotifyInfo();
                note.UserId = Convert.ToByte(id);//用户id
                note.Email = email;//邮箱
                note.UserType = Convert.ToByte( utype);//用户类型：1--用户 2--医生
                note.Title = title;//标题
                note.Content = content;//内容
                note.Type =  Convert.ToByte(type);//通知类型：1-邮件 2-站内 3-邮件&站内
                note.DelFlag = 0;//删除标识
                note.SubDate = DateTime.Now;//提交时间
                note.SubNum = Session["AdminNum"].ToString();//发送人账号
                note.SubName = Session["AdminUser"].ToString();//发送人姓名
                //note.IsRead = 0;//是否已读

               
                if (type == "1") //邮件方式
                {
                    note.IsRead = 1; 
                }
                else if(type == "2")//站内消息
                {
                     note.IsRead = 0;//是否已读
                }
                else//邮件&站内
                {
                     note.IsRead = 0;//是否已读
                }

                DBContext.NotifyInfo.Attach(note);
                DBContext.Entry(note).State = System.Data.EntityState.Added;

                DBContext.SaveChanges();

                //设置操作记录
                AdminExecute.Insert(Session["AdminNum"].ToString(), Session["AdminUser"].ToString(), "给用户发送通知：邮箱（"+email+"),用户ID（"+id+"），通知ID（" + note.Id + "）");

                if (type != "2")
                { //包含邮件方式 1和3
                    Basic.sendNoteByEmail(email, title, content);
                }
                

                return Content("success");
            }
            catch
            {
                return Redirect("/User/_404");
            }
            
        }

        /// <summary>
        /// 【根据Id获取资讯信息作为编辑基础】
        ///  20170412
        /// </summary>
        /// <param name="id">资讯Id</param>
        /// <returns></returns>
        public ActionResult GetNewsInfo(int id) 
        { 
            if(!IsLogin())
                return RedirectToAction("Login", "Admin", new { referenUrl = "/Admin/User" });

            try
            {
                DBContext = new WebManagementDBEntities();
                var hinfo = DBContext.HealthInfo.Where(h => h.Id == id).FirstOrDefault();

                if (hinfo == null)
                    return Content("error1");//无法读取数据

                string[] from = hinfo.Remark.Split('：');
                var data = new 
                { 
                    Title = hinfo.Title,
                    Description = hinfo.Description,
                    From = from[2],
                    ImageUrl = hinfo.ImageUrls,
                    Content = hinfo.Content,
                    SubDate = hinfo.SubDate.ToString(),
                
                };

                return Json(data, JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return Redirect("/User/_404");
            }
        }

        /// <summary>
        /// 【保存编辑过的新闻资讯】
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="from"></param>
        /// <param name="image"></param>
        /// <param name="content"></param>
        /// <returns></returns>
       [ValidateInput(false)]
        public ActionResult SaveNewsInfo(int id, string title, string description, string from, string image, string content)
        {
            try
            {
                if (!IsLogin())
                {
                    return Content("error2");//请先登录
                }



                //2、判断是否为空
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(image) || string.IsNullOrEmpty(content))
                {
                    return Content("error-3");
                }

                //3、创建上下文
                DBContext = new WebManagementDBEntities();
                HealthInfo hi = DBContext.HealthInfo.Where(h=>h.Id == id).FirstOrDefault();

                //4、数据填充
                hi.Title = title;
                hi.Description = description;
                hi.Content = content;

                if(image != "null")
                    hi.ImageUrls = image;

                string[] froms = hi.Remark.Split('：');//发表时间：2013-05-22 09:45:08 来源：中新网
                
                hi.Remark = "发表时间： " + froms[1] + "：" + from;

                DBContext.HealthInfo.Attach(hi);
                DBContext.Entry(hi).State = System.Data.EntityState.Modified;

                if (DBContext.SaveChanges() > 0)//执行保存
                {
                    
                    AdminExecute.Insert(Session["AdminNum"].ToString(), Session["AdminUser"].ToString(), "修改资讯，id为：【" + hi.Id + "】");
                    return Content("success");
                }
                return Content("error-2");//保存失败



            }
            catch
            {
                return Content("error1");
            }
        }

        /// <summary>
        /// 【修改资讯状态】
        ///  20170413
        /// </summary>
        /// <param name="id">资讯Id</param>
        /// <param name="status">修改的状态</param>
        /// <returns></returns>
        public ActionResult ChangeNewsStatus(int id, int status) 
       {
           if (!IsLogin())
               return Content("error2");//请先登录

           try 
           {
               DBContext = new WebManagementDBEntities();
               var hinfo =  DBContext.HealthInfo.Where(h => h.Id == id).FirstOrDefault();

               if (hinfo == null || (status != 0 && status != 11 && status != 12))//0-正常 11-删除 12-热门
                   return Content("error3");//数据出错

                //判断热门资讯是否大于3
               var count = DBContext.HealthInfo.Where(h => h.DelFlag == 12).Count();
               if (status == 12 && count >= 3)
                   return Content("error5");
               int oldStatus = Convert.ToInt32( hinfo.DelFlag);
               hinfo.DelFlag = Convert.ToByte( status);
               DBContext.HealthInfo.Attach(hinfo);
               DBContext.Entry(hinfo).State = System.Data.EntityState.Modified;

               if (DBContext.SaveChanges() > 0)//执行保存
               {
       
                    AdminExecute.Insert(Session["AdminNum"].ToString(), Session["AdminUser"].ToString(), "修改资讯状态为（" + oldStatus + "===>"+status+"），id为：【" + hinfo.Id + "】");
                    return Content("success");
                   
               }
               return Content("error4");//保存失败

           }
           catch
           {
               return Content("error1");//系统错误

           }

       }

        /// <summary>
        /// 【工作任务---轮询通知】
        ///  20170414
        /// </summary>
        /// <returns></returns>
        public ActionResult Run(long ct) 
        {
            if (!IsLogin())
                return Content("error1");//未登陆
            try
            {
                
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                DateTime nt = startTime.AddMilliseconds(ct);//获取时间间隔

                DBContext = new WebManagementDBEntities();

                var ls = DBContext.CommentInfo.Where(c => c.SubData > nt).OrderByDescending(c => c.SubData).Select(c => new{c.Id,c.Content,c.Contact,c.SubData}).ToList();

                var data = new 
                {
                    Total = ls.Count(),
                    Data = ls,
                    CT = nt.ToString() 
                };

                return Json(data, JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return Content("error2");//系统出错
            }
        }

        /// <summary>
        /// 【修改医生的状态】
        ///  20170417
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult ChangeState(int id, int state)
        {
            if (!IsLogin())
                return Content("error1");//未登陆

            try
            {
                DBContext = new WebManagementDBEntities();
                var doc =  DBContext.DoctorInfo.Where(d => d.Id == id).FirstOrDefault();
                if (doc == null)
                    return Content("error2");
                doc.State = Convert.ToByte( state);

                DBContext.DoctorInfo.Attach(doc);
                DBContext.Entry(doc).State = System.Data.EntityState.Modified;

                if (DBContext.SaveChanges() > 0)
                    return Content("success");
                else
                    return Content("error3");
            
            }
            catch
            {
                return Content("error");
            }
        }
    }
}
