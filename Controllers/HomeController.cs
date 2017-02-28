using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using 基于云的Web管理系统.Models;

namespace 基于云的Web管理系统.Controllers
{
    
    public class HomeController : Controller
    {
        private string  Apikey{get;set;}
        private string DeviceId { get;set; }
        private Dictionary<string, string> sensorsId { get; set; }

        private string L_Apikey { get; set; }//乐联key
        private string L_DeviceId { get; set; }
        private Dictionary<string, string> L_sensorsId { get; set; }


        //数据上下文
        private WebManagementDBEntities DBContext;

        public HomeController()
        {
            Apikey = "36225e7016a2134f6c10cee07142eba5";
            DeviceId = "19210";
            sensorsId = new Dictionary<string, string>
            {
                {"空气温度","33674"},
                {"浇水开关","33765"},
                {"土壤湿度","35108"},
                {"光照强度","35109"},
                {"空气湿度","35113"},
                {"照明开关","38096"},
                {"PM2.5","38187"},
                {"CO浓度","38189"}
            };

            //乐联的Apikey
            L_Apikey = "ff9bf77c827b4a4f86f4bd6b20ea9989";
            L_DeviceId = "13131";
            L_sensorsId = new Dictionary<string, string>
            {
                {"心率","19136"},
                {"体温","19137"},
                {"体重","19138"},
                {"收缩压","19139"},
                {"舒张压","19140"}
            };
        }


        // GET: /Home/

        public ActionResult Index()
        {   //创建数据上下文
            var DBContext = new WebManagementDBEntities();
            //获取数据
            var list = DBContext.HealthInfo.Where(u => u.DelFlag == 0 && u.IsHot == 0).OrderBy(u => u.SubDate).Take(3);

            ViewBag.News = list;
            return View();
        }

        //MyInfo
        public ActionResult MyInfo()
        {
            return Content("");
            //if (Session["username"] != null)
            //{
            //    //读取用户信息
            //    DataTable dt = new DataTable();
            //    //dt = getUserInfoByLoginId((string)Session["username"]);
            //    UserInfo user = new UserInfo();
            //    user.LoginId = "Admin";
            //    user.Email = "874847721@qq.com";

            //    if (dt.Rows.Count > 0)
            //    {
            //        ViewData["dt"] = user;
            //        return View();
            //    }
            //    else
            //    {
            //        ViewData["errorMsg"] = "读取用户信息失败";
            //        return View();
            //    }
            //}
            //else
            //{
            //    return Redirect("/Home/Login");
            //}
        }

        //连接字符串
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["connStr3"].ConnectionString;
        
        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <param name="LoginId">用户登录Id</param>
        /// <returns>用户信息记录表</returns>
        private DataTable getUserInfoByLoginId(string LoginId)
        {
            string sql = "select * from UserInfo where  (LoginId = @LoginId or Email = @LoginId) ";

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.Add(new SqlParameter("@LoginId", LoginId));

                        using (SqlDataAdapter adapt = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapt.Fill(dt);

                            return dt;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //修改用户信息
        public ActionResult UpdateUserInfo(string item,string data)
        {
            if (Session["username"] != null)
            {
                //根据更新的项，来生成不同的 SQL 语句
                string sql = "";
                if (item == "TrueName")
                {
                    sql = "Update UserInfo set TrueName = @data where (LoginId = @LoginId or Email = @LoginId)";
                }
                else if (item == "Email")
                {
                    sql = "Update UserInfo set Email = @data where (LoginId = @LoginId or Email = @LoginId)";
                }
                else if( item == "DeviceId")
                {
                    sql = "Update UserInfo set DeviceId = @data where (LoginId = @LoginId or Email = @LoginId)";
                }
                else if (item == "Phone")
                {
                    sql = "Update UserInfo set Phone = @data where (LoginId = @LoginId or Email = @LoginId)";
                }
                else
                {
                    return Content("error");
                }

                try
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = sql;
                            cmd.Parameters.Add(new SqlParameter("@data", data));
                            //cmd.Parameters.Add(new SqlParameter("@item", item));
                            cmd.Parameters.Add(new SqlParameter("@LoginId", (string)Session["username"]));

                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                return Content("success");
                            }
                            else
                            {
                                return Content("error");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                return Content("error");
            }
        }



        public ActionResult MyData()
        {
            if(Session["username"] == null)
            {
                return View();
            }
            else
            {
                return Redirect("/Home/Login");
            }
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public ActionResult GetDataPoint(string sensor) 
        {
            //【①】
            var client = new WebClient();
            client.Headers.Add("U-Apikey", Apikey);
            string url = string.Format("http://api.yeelink.net/v1.1/device/{0}/sensor/{1}/datapoints", DeviceId, sensorsId[sensor]);
            string content = client.DownloadString(url);


            //【②】
            //var client2 = new WebClient();
            //client2.Headers.Add("userkey", "ff9bf77c827b4a4f86f4bd6b20ea9989");
            //string content = client2.DownloadString("http://www.lewei50.com/api/V1/user/getSensorsWithGateway");

            //byte[] byteArray = Encoding.Default.GetBytes(content);
            //string content2 = Encoding.UTF8.GetString(byteArray);

            //【③】历史数据
            //var client3 = new WebClient();
            //client3.Headers.Add("userkey", "ff9bf77c827b4a4f86f4bd6b20ea9989");
            //string content3 = client3.DownloadString("http://www.lewei50.com/api/V1/sensor/GetHistoryData/19137");


            
            //return Json(content,JsonRequestBehavior.AllowGet);
            return Content(content);
        }


        /// <summary>
        /// 展示我的数据视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowMyData() 
        {

            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/Home/Login");
            }
        }

        /// <summary>
        /// 获取我的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMyDataPoint()
        {

            var client3 = new WebClient();
            client3.Headers.Add("userkey", L_Apikey);
            string url = string.Format("http://www.lewei50.com/api/V1/sensor/GetHistoryData/{0}?StartTime=&EndTime=&Interval=&Start=&Limit=1&Order=0",L_sensorsId[Request["sensor"].ToString()]);

            string content3 = client3.DownloadString(url);//取得最新的那个数据点


            return Json(content3);
        }

        /// <summary>
        /// 返回【健康资讯】的界面
        /// </summary>
        /// <returns></returns>
        public ActionResult HealthInfo()
        {
            return View();
        }


        /// <summary>
        /// 返回登录界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login() 
        {
            //ViewData["Referen"] = Request.UrlReferrer;
            //ViewData["Referen"] = Request["referenUrl"] == null?Request.UrlReferrer:Request["referenUrl"].ToString();
            if (string.IsNullOrEmpty(Request["referenUrl"]))
            {
                ViewData["Referen"] = Request.UrlReferrer;
            }
            else 
            {
                ViewData["Referen"] = Request["referenUrl"];
            }
            return View();
        }


        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckLogin(string fromUrl)
        {
            return Content("");
        }

        /// <summary>
        /// 【联系我们】
        /// </summary>
        /// <returns></returns>
        public ActionResult ContactUs()
        {
            
            return View();
        }


        /// <summary>
        /// 【生成验证码】
        ///  20170209
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

        /// <summary>
        /// 【验证验证码】
        ///  20170209
        /// </summary>
        /// <param name="type">类型：admin_comment</param>
        /// <param name="code">验证码：78942</param>
        /// <returns>验证结果</returns>
        private bool CheckCode(string type, string code)
        {
            if (code == (string)Session["admin_" + type])
                return true;
            else
                return false;

        }

        public ActionResult UpComment()
        {
            try
            {
                string code = Request["code"];
                string comment = Request["comment"];
                string contact = Request["contact"];

                if (!string.IsNullOrEmpty(comment) && !string.IsNullOrEmpty(contact) && !string.IsNullOrEmpty(code))
                {
                    if (this.CheckCode("comment",code))
                    {
                        DBContext = new WebManagementDBEntities();
                        CommentInfo com = new CommentInfo();

                        //填充信息
                        com.Contact = contact;
                        com.Content = comment;
                        com.SubData = DateTime.Now;
                        com.IsRead = 0;
                        

                        //附加并保存
                        DBContext.CommentInfo.Attach(com);
                        DBContext.Entry(com).State = System.Data.EntityState.Added;

                        DBContext.SaveChanges();

                        return Content("success");
                        
                    }
                    else
                    {
                        return Content("error");
                    }
                }
                else
                {
                    return Content("error1");//数据不完整
                }

            }
            catch 
            {
                return Content("error2");
            }
            
        }
    }
}
