using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 基于云的Web管理系统.Models;
using 基于云的Web管理系统.Framework;

namespace 基于云的Web管理系统.Controllers
{
    public class ForumController : Controller
    {
        private WebManagementDBEntities DBContext;
        //
        // GET: /Forum/
        public ActionResult Index()
        {
            //创建数据上下文
            DBContext = new WebManagementDBEntities();

            var forumInfo = DBContext.ForumInfo.Where(f => f.DelFlag == 0).OrderByDescending(f => f.ViewNumber).Take(10);
            ViewBag.ForumInfo = forumInfo;
            return View();
        }

        /// <summary>
        ///【 论坛分类信息展示】
        /// </summary>
        /// <returns></returns>
        public ActionResult Show()
        {
            //ViewData["ForumType"] = forumType;
            //return View();
            //获取每页的条数和页码
            int pageSize = Request["pageSize"] == null ? 10 : int.Parse(Request["pageSize"]);
            int pageIndex = Request["pageIndex"] == null ? 1 : int.Parse(Request["pageIndex"]);
            int forumType = Request["forumType"] == null ? 1 : int.Parse(Request["forumType"]);

            DBContext = new WebManagementDBEntities();
            string type;
            #region 字符串转义
            if (forumType == 1)//营养美食
            {
                type = "YYMS";
            }
            else if (forumType == 2)//塑身美体
            {
                type = "SSMT";
            }
            else if (forumType == 3)//健康宝典
            {
                type = "JKBD";
            }
            else if (forumType == 4)//育儿宝典
            {
                type = "YEBD";
            }
            else if (forumType == 5)//娱乐杂评
            {
                type = "YLZP";
            }
            else//男人女人
            {
                type = "NRNR";
            }
            #endregion
            //List<UserInfo> users = new List<UserInfo>();
            //for (int i = 0; i < 20; i++) 
            //{
            //    UserInfo u = new UserInfo();
            //    u.TrueName = "Tom" +i;
            //    u.Email = "874847721@qq.com";
            //    users.Add(u);
            //}

            //计算相关数据的总条数
            int total = DBContext.ForumInfo.Where(f => f.ForumType == type).Count();

            //获取相关分页数据
            var forumInfo = DBContext.ForumInfo.Where(f => f.ForumType == type && f.DelFlag == 0).OrderBy(f => f.SubDate).Skip(pageSize * (pageIndex - 1)).Take(pageSize);

            //要来处理数据的新数组
            ArrayList list = new ArrayList();
            foreach (var row in forumInfo)
            {
                var f = new
                {
                    Id = row.Id,
                    Title = row.Title,
                    SubName = row.SubName,
                    FloorCount = row.FloorCount,
                    SubDate = row.SubDate.ToString()
                };
                list.Add(f);
            }

            var data = new
            {
                Total = list.Count,
                Row = list.ToArray(),
                PageNumber = Page.ShowPageNavigate(pageIndex, pageSize, list.Count, forumType)//这个方法为分页导航条的字符串
            };


            //ViewData["pageSize"] = pageSize;
            //ViewData["pageIndex"] = pageIndex;
            //ViewData["total"] = list.Count;
            return Json(data, JsonRequestBehavior.AllowGet);//返回数据
        }

        /// <summary>
        ///【展示论坛文章细节】
        /// </summary>
        /// <returns></returns>
        public ActionResult Showdetail(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    int forumId = int.Parse(Id);

                    //创建数据上下文
                    DBContext = new WebManagementDBEntities();

                    //根据论坛信息的Id去读取论坛详细信息
                    var forumInfo = DBContext.ForumInfo.Where(f => f.Id == forumId && f.DelFlag == 0).FirstOrDefault();

                    if (forumInfo != null)
                    {
                        //读取其他人的回复信息
                        var answerInfo = DBContext.AnswerInfo.Where(a => a.ForumInfoId == forumId).OrderBy(a => a.FloorNumber);

                        if (answerInfo != null)
                        {
                            ViewBag.ForumInfo = forumInfo;
                            ViewBag.AnswerInfo = answerInfo;
                            ViewData["reply_id"] = Id;

                            //更新一下查看次数
                            forumInfo.ViewNumber += 1;
                            DBContext.ForumInfo.Attach(forumInfo);
                            DBContext.Entry(forumInfo).State = System.Data.EntityState.Modified;

                            DBContext.SaveChanges();

                            return View();
                        }
                        else
                        {
                            return RedirectToAction("../User/_404");
                        }


                    }
                    else
                    {
                        return RedirectToAction("../User/_404");
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                return RedirectToAction("../User/_404");
            }
        }


        public ActionResult Count(string type, string countType, string id)
        {
            try
            {
                //创建数据上下文
                DBContext = new WebManagementDBEntities();
                int Id = int.Parse(id);

                if (type == "1") //论坛信息
                {
                    //获取当前信息的对象
                    var forumInfo = DBContext.ForumInfo.Where(f => f.Id == Id).FirstOrDefault();

                    if (countType == "1")//对我有用统计
                    {
                        forumInfo.UsefulCount += 1;
                    }
                    else if (countType == "2")//丢个板砖统计
                    {
                        forumInfo.UselessCount += 1;
                    }

                    DBContext.ForumInfo.Attach(forumInfo);
                    DBContext.Entry(forumInfo).State = System.Data.EntityState.Modified;
                    DBContext.SaveChanges();

                    return Content("success");
                }
                else if (type == "2")//论坛回复信息
                {
                    //创建一个回复信息的对象
                    var answerInfo = DBContext.AnswerInfo.Where(a => a.Id == Id).FirstOrDefault();


                    if (countType == "1")//对我有用统计
                    {
                        answerInfo.UsefulCount += 1;
                    }
                    else if (countType == "2")//丢个板砖统计
                    {
                        answerInfo.UselessCount += 1;
                    }


                    DBContext.AnswerInfo.Attach(answerInfo);
                    DBContext.Entry(answerInfo).State = System.Data.EntityState.Modified;
                    DBContext.SaveChanges();

                    return Content("success");
                }

                return Content("error");
            }
            catch
            {
                return RedirectToAction("../User/_404");
            }
        }

        /// <summary>
        /// 【论坛图片上传保存】
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveImage()
        {
            try
            {
                if (Request.Files.Count > 0)
                {

                    //System.Threading.Thread.Sleep(10000);//进程睡10秒
                    //HttpPostedFileBase proImage = Request.Files["uploadImage"];//获取上传的图片
                    HttpPostedFileBase proImage = Request.Files["upload"];//获取上传的图片


                    //判断上传文件大小
                    if (proImage.ContentLength > 5 * 1024 * 1024)
                    {
                        return Content("Error1");
                    }

                    string[] filetypes = proImage.ContentType.Split('/');//截取图片类型：image/png

                    //判断文件的类型
                    if (filetypes[1] == "jpg" || filetypes[1] == "gif" || filetypes[1] == "png" || filetypes[1] == "bmg" || filetypes[1] == "jpeg")
                    {
                        //给上传文件重命名
                        string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString();

                        string filesavepath = Server.MapPath("~/Uploadfile/ForumImages/" + filename + "." + filetypes[1]);
                        proImage.SaveAs(filesavepath);



                        string callback = Request["CKEditorFuncNum"];
                        //string rst1 = "<script type='text/javascript'> window.parent.CKEDITOR.tools.callFunction("+callback+","+"Uploadfile/ForumImages/" +filename+"."+filetypes[1]+", ''"+");</script>";
                        string rst2 = "<script type='text/javascript'>window.parent.CKEDITOR.tools.callFunction(" + callback + ",  '" + "/Uploadfile/ForumImages/" + filename + "." + filetypes[1] + " ', '插入成功 ' );" + "</script>";


                        //  string rst3 = "<script type='text/javascript'>$(function(){ $('#cke_76_textInput').val('" + "http:localhost:6008/UploadFile/ForumImages/" + filename + "." + filetypes[1] + "');  });</script>";
                        //string rst4 = "<script type='text/javascript'> alert('success'); var content =  document.getElementById('cke_76_textInput');alert(content.value); content.value = '" + "http:localhost:6008/UploadFile/ForumImages/" + filename + "." + filetypes[1] + "' ;  </script>";
                        return Content(rst2);
                        // return Content(filename + "." + filetypes[1]);
                    }
                    else
                    {
                        string callback = Request["CKEditorFuncNum"];
                        return Content("<script type='text/javascript'> window.parent.CKEDITOR.tools.callFunction(" + callback + ",''," + "'文件格式不正确（必须为.jpg/.gif/.bmp/.png文件）');</script> ");
                        //return Content("<script type='text/javascript'> alert('文件格式不正确（必须为.jpg/.gif/.bmp/.png文件）');</script> ");
                    }


                }
                else
                {
                    return Content("Error1");
                }
            }
            catch
            {
                return Content("Error2");
            }
        }

        /// <summary>
        /// 【生成回复的验证码】
        /// </summary>
        public void CreateReplyCode()
        {
            //调用生成验证码的方法
            CreateCode("ReplyCode");
        }

        /// <summary>
        /// 【保存用户提交的回复内容】
        /// </summary>
        /// <param name="code"></param>
        /// <param name="replyContent"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateInput(false)] 
        public ActionResult SubReplContent(string code, string replyContent, string id)
        {
            //判断是否登陆
            if (Session["UserName"] != null)
            {
                if (!string.IsNullOrEmpty(code) && Session["ReplyCode"].ToString() == code)
                {
                    //对数据合法性进行判断
                    if (!string.IsNullOrEmpty(replyContent) && !string.IsNullOrEmpty(id))
                    {
                        try
                        {
                            //新建一个数据上下文
                            DBContext = new WebManagementDBEntities();
                            int id2 = Convert.ToInt32(id);

                            //获取论坛楼主层信息
                            var f1 = DBContext.ForumInfo.Where(f => f.Id == id2 && f.DelFlag == 0).FirstOrDefault();


                           
                            //新建一个回复内容对象
                            AnswerInfo a1 = new AnswerInfo();
                            a1.ForumInfoId = id2;
                            a1.Content = replyContent;
                            a1.FromNum = Session["UserName"].ToString();
                            a1.FromName = Session["NickName"].ToString();
                           
                            a1.FloorNumber = f1.FloorCount + 1;
                            a1.SubDate = DateTime.Now;
                            a1.DelFlag = 0;
                            a1.UsefulCount = 0;
                            a1.UselessCount = 0;

                            //附加新建回复内容对象a1
                            DBContext.AnswerInfo.Attach(a1);
                            DBContext.Entry(a1).State = System.Data.EntityState.Added;

                            //修改楼主层的信息
                            f1.FloorCount += 1;
                            DBContext.ForumInfo.Attach(f1);
                            DBContext.Entry(f1).State = System.Data.EntityState.Modified;


                            //执行保存
                            DBContext.SaveChanges();

                            return Content("success");
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
                else
                {
                    return Content("error1");
                }

            }
            else
            {
                return RedirectToAction("../Home/Login", new { referenUrl = "/Forum/Showdetail?id=" + id });
            }
        }


        /// <summary>
        /// 【发帖】
        /// </summary>
        /// <returns></returns>
        public ActionResult PostForum()
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string forumType = Request["forumType"];
                    if (!string.IsNullOrEmpty(forumType))
                    {
                        ViewData["forumType"] = forumType;
                    }
                    else
                    {
                        ViewData["forumType"] = 0;
                    }
                    return View();
                }
                catch
                {
                    return RedirectToAction("../User/_404");
                }
            }
            else
            {
                return RedirectToAction("../Home/Login", new { referenUrl = "/Forum/PostForum" });
            }
          
        }

        /// <summary>
        /// 【生成提交发帖验证码】
        /// </summary>
        public void CreatePostForumCode()
        {
            //调用生成验证码
            CreateCode("PostForumCode");
        }

        /// <summary>
        /// 【根据验证码类似生成验证码并保存到session中】
        /// </summary>
        /// <param name="codeType">【验证码类型】</param>
        private void CreateCode(string codeType)
        {
            try
            {
                CreateCheckCode code = new CreateCheckCode();//创建一个验证码对象
                String number = code.GenerateCheckCode();//获取随机码
                Session.Add(codeType, number);//保存到Session

                byte[] fileContents = code.CreateCheckCodeImage(number);

                //Stream ms = new MemoryStream(fileContents);
                //return ms;
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(fileContents);
            }
            catch
            {
                 RedirectToAction("../User/_404");
            }
        }


        /// <summary>
        /// 【插入上传的发帖内容】
        /// </summary>
        /// <param name="code">【验证码】</param>
        /// <param name="forumType">【论坛类型】</param>
        /// <param name="title">【标题】</param>
        /// <param name="content">【内容】</param>
        /// <returns></returns>
        [ValidateInput(false)] 
        public ActionResult SubPostForum(string code, string forumType, string title, string content)
        {
            if (Session["UserName"] != null)
            {
                if (!string.IsNullOrEmpty(code) &&  Session["PostForumCode"].ToString() == code)
                {
                    if (!string.IsNullOrEmpty(forumType) && !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(content))
                    {
                        if(title.Length <= 250)
                        {
                            try{
                                string forumType2 = "";
                                string forumName = "";
                                #region 论坛类型转换
                                if (forumType == "1")
                                {
                                    forumType2 = "YYMS";
                                    forumName = "营养美食";
                                }
                                else if (forumType == "2")
                                {
                                    forumType2 = "SSMT";
                                    forumName = "塑身美体";
                                }
                                else if (forumType == "3")
                                {
                                    forumType2 = "JKBD";
                                    forumName = "健康宝典";
                                }
                                else if (forumType == "4")
                                {
                                    forumType2 = "YEBD";
                                    forumName = "育儿宝典";
                                }
                                else if (forumType == "5")
                                {
                                    forumType2 = "YLZP";
                                    forumName = "娱乐杂评";
                                }
                                else if (forumType == "6")
                                {
                                    forumType2 = "NRNR";
                                    forumName = "男人女人";
                                }
                                else
                                {
                                    return Content("error2");
                                }
                                #endregion
                                //创建上下文和论坛信息对象
                                DBContext = new WebManagementDBEntities();
                                ForumInfo f1 = new ForumInfo();

                                //填充一个论坛信息对象
                                f1.Title = title;//标题
                                f1.ForumType = forumType2;//论坛类型编号
                                f1.ForumName = forumName;//论坛类型名字
                                f1.Content = content;//内容
                                f1.FloorCount = 0;//楼层数
                                f1.ViewNumber = 0;//查看数目
                                f1.SubDate = DateTime.Now;//提交时间
                                f1.SubNum = Session["UserName"].ToString();//提交人编号
                                f1.SubName = Session["NickName"].ToString();//提交人昵称
                                f1.UsefulCount = 0;//觉得有用的数目
                                f1.UselessCount = 0;//觉得没用的数目
                                f1.DelFlag = 0;//删除标识

                                //附加并保存
                                DBContext.ForumInfo.Attach(f1);
                                DBContext.Entry(f1).State = System.Data.EntityState.Added;

                                DBContext.SaveChanges();



                                return Content("success");
                            }
                            catch
                            {
                                return RedirectToAction("../User/_404");
                            }
                        }
                        else
                        {
                            return Content("error2");//标题长度过长
                        }
                    }
                    else
                    {
                        return RedirectToAction("../User/_404");
                    }
                }
                else
                {
                    return Content("error1");//验证码错误
                }
            }
            else
            {
                return RedirectToAction("../Home/Login", new { referenUrl = "/Forum/PostForum" });
            }
        }
    }
}
