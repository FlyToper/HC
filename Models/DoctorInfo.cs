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
    
    public partial class DoctorInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string RegisterCode { get; set; }
        public Nullable<byte> State { get; set; }
        public string Remark { get; set; }
        public string ImageUrls { get; set; }
        public string Reason { get; set; }
    }
}
