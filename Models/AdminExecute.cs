using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 基于云的Web管理系统.Models
{
    public class AdminExecute
    {
        //private WebManagementDBEntities DBContext;

        /// <summary>
        /// 【管理操作入库】
        ///  20170228
        /// </summary>
        /// <param name="num">管理员的编号</param>
        /// <param name="name">管理员名字</param>
        /// <param name="description">描述</param>
        /// <returns>受影响记录</returns>
        public static int Insert(string num, string name, string description) 
        {
            WebManagementDBEntities DBContext = new WebManagementDBEntities();

            AdminExecuteInfo model = new AdminExecuteInfo();

            model.AdminNum = num;
            model.AdminName = name;
            model.ExecuteDescription = description;
            model.DelFlag = 0;
            model.ExecuteDate = DateTime.Now;

            DBContext.AdminExecuteInfo.Attach(model);
            DBContext.Entry(model).State = System.Data.EntityState.Added;

            return DBContext.SaveChanges();//执行保存

        }
    }
}