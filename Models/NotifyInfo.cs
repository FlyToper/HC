//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace 基于云的Web管理系统.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NotifyInfo
    {
        public long Id { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<byte> UserType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Nullable<byte> Type { get; set; }
        public Nullable<byte> DelFlag { get; set; }
        public Nullable<System.DateTime> SubDate { get; set; }
        public string SubNum { get; set; }
        public string SubName { get; set; }
        public Nullable<byte> IsRead { get; set; }
        public Nullable<System.DateTime> ReDate { get; set; }
        public string Email { get; set; }
    }
}
