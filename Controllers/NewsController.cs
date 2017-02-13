using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 基于云的Web管理系统.Framework;
using 基于云的Web管理系统.Models;

namespace 基于云的Web管理系统.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/
        //数据上下文
        private WebManagementDBEntities DBContext;

        /// <summary>
        /// 【默认展示的页面】
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //创建数据上下文
            DBContext = new WebManagementDBEntities();
            var hotNews =  DBContext.HealthInfo.Where(u => u.IsHot == 1 && u.DelFlag == 0).Take(3);
            if (hotNews != null)
            {
                ViewBag.HotNews = hotNews;
            }
            var newsInfo = DBContext.HealthInfo.Where(u => u.DelFlag == 0 && u.IsHot == 0).Take(10);
            if (newsInfo != null)
            {
                ViewBag.NewsInfo = newsInfo;
            }
            return View();
        }

        /// <summary>
        /// 【分页加载资讯信息】
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadNewsInfo()
        {
            //创建数据上下文
            DBContext = new WebManagementDBEntities();

            //获取页码和每页条数
            int pageIndex = Request["pageIndex"] == null ? 1 : int.Parse( Request["pageIndex"]);
            int pageSize = Request["pageSize"] == null ? 10 : int.Parse(Request["pageSize"]);

            //获取相关数据的总数
            int total = DBContext.HealthInfo.Where(u=>u.DelFlag == 0 && u.IsHot == 0).Count();
            //获取数据
            var list = DBContext.HealthInfo.Where(u => u.DelFlag == 0 && u.IsHot == 0).OrderBy(u=>u.SubDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            
            //要来做数据处理的新数组
            ArrayList s = new ArrayList();
            foreach(var row in list)
            {
                var hel = new
                {
                    Id = row.Id,
                    Title = row.Title,
                    Remark = row.Remark
                };
               s.Add(hel);
            }

            //分页导航的Html代码
            string pageHtml = Page.ShowNewsPageNavigate(pageIndex, pageSize, total);

            //把返回条数，数据行，分页的html代码封装成Json格式
            var data = new
            {
                Total = s.Count,
                Row = s.ToArray(),
                PageHtml = pageHtml

            };
            return Json(data, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 【根据咨询的Id获取完整的信息】
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        public ActionResult ShowDetail(string Id )
        {
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    int newsId = int.Parse(Id);
                    //创建数据上下文，根据Id查找相关记录
                    DBContext = new WebManagementDBEntities();
                    var newsInfo = DBContext.HealthInfo.Where(u => u.Id == newsId && u.DelFlag == 0).FirstOrDefault();

                    if (newsInfo != null)
                    {
                        ////封装成json格式
                        //var info = new
                        //{
                        //    Total = 1,
                        //    Title = newsInfo.Title,
                        //    Content = newsInfo.Content,
                        //    Remark = newsInfo.Remark
                        //};

                        ////发送json格式数据
                        //return Json(info, JsonRequestBehavior.DenyGet);

                        ViewBag.Info = newsInfo;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("../User/_404");
                    }
                }
                catch
                {
                    return RedirectToAction("../User/_404");
                }
            }
            else
            {
                return RedirectToAction("../User/_404");
            }
         
        }

    }
}
