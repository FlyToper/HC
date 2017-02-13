using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace 基于云的Web管理系统.Myashx
{
    /// <summary>
    /// UpdatePwd 的摘要说明
    /// </summary>
    public class UpdatePwd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string pwd1 = context.Request["txtPwd1"];
            string pwd2 = context.Request["txtPwd2"];
            string email = context.Request["txtEmail"];

            if (pwd1 == pwd2)
            {
                if (setPwd(pwd1.Trim(), email.Trim()) >0)
                {
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.Write("error");
                }
            }
            else//密码还是不一致
            {
                context.Response.Write("error");
            }
            
            
        }

        /// <summary>
        ///  更改密码
        /// </summary>
        /// <param name="pwd">新密码</param>
        /// <param name="email">邮箱名称</param>
        /// <returns>受影响行数</returns>
        private int setPwd( string pwd, string email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "update UserInfo set Pwd = @Pwd where Email = @Email";
                        cmd.Parameters.Add(new SqlParameter("@Pwd", pwd));
                        cmd.Parameters.Add(new SqlParameter("@Email", email));

                        return cmd.ExecuteNonQuery();
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