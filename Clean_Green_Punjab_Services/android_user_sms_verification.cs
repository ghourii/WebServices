//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clean_Green_Punjab_Services
{
    using System;
    using System.Collections.Generic;
    
    public partial class android_user_sms_verification
    {
        public int pk_id { get; set; }
        public string mobile { get; set; }
        public string imei { get; set; }
        public string sms_code { get; set; }
        public Nullable<bool> is_verified { get; set; }
        public Nullable<System.DateTime> code_create_date_time { get; set; }
        public Nullable<System.DateTime> code_verified_date_time { get; set; }
    }
}
