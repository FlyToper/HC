using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace 基于云的Web管理系统.Myashx
{
    /// <summary>
    /// CheckUserName 的摘要说明
    /// </summary>
    public class CheckUserName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string CheckType = context.Request["CheckType"];

            if (!string.IsNullOrEmpty(CheckType))//是否为注册表单的检查
            {
                if (CheckType == "CheckUserName")//检查用户名是否存在
                {
                    string username = context.Request["txtUserName"];


                    if (IsUserNameExist(username))//用户名存在
                    {
                        context.Response.Write("Exist");
                    }
                    else//用户名不存在
                    {
                        context.Response.Write("NotExist");
                    }
                }
                else if (CheckType == "CheckEmail")//检查邮箱是否存在
                {
                    string email = context.Request["txtEmail"];

                    if (IsEmailExist(email))//邮箱存在
                    {
                        context.Response.Write("Exist");
                    }
                    else 
                    {
                        context.Response.Write("NotExist");
                    }
                }
            }
            else
            {
                return;
            }

        }

        //连接字符串
       private readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>存在与否</returns>
        private bool IsUserNameExist(string username)
        {
            string sql = "Select * from UserInfo where LoginId = @LoginId";

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.Add(new SqlParameter("@LoginId", username));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
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
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns>存在与否</returns>
        private bool IsEmailExist(string email)
        {
            string sql = "Select * from UserInfo where Email = @Email";

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.Add(new SqlParameter("@Email", email));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())//邮箱已经存在！
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
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}