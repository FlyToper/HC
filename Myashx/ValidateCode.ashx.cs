using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace 基于云的Web管理系统.Myashx
{
    /// <summary>
    /// VaildateCode 的摘要说明
    /// </summary>
    public class VaildateCode : IHttpHandler,IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string CodeType = context.Request["CodeType"];

            if (!string.IsNullOrEmpty(CodeType))//验证类型不为空
            {
                if (CodeType == "Find")//验证 找回密码的验证码
                {

                    string txtCode = context.Request["txtCode"];

                    if (txtCode.Trim() == (string)context.Session["emailFindCode"])
                    {
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("error");
                    }

                }
                else if (CodeType == "Register")//验证 注册的 验证码
                {

                    string txtCode = context.Request["txtCode"];

                    if (txtCode.Trim() == (string)context.Session["emailRegisterCode"])
                    {
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("error");
                    }
                }
                else
                {
                    context.Response.Write("error");
                }
            }
            else
            {
                context.Response.Write("");
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