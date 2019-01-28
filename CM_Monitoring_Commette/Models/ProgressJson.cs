using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CM_Monitoring_Commette.Models
{
    public class ProgressJson
    {
        public Nullable<int> tehsilId { get; set; }
        public Nullable<int> auto_pk { get; set; }
        public Nullable<int> quality_of_work { get; set; }
        public Nullable<int> DivisonId { get; set; }
        public string lng { get; set; }
        public string remarks { get; set; }
        public Nullable<int> ucId { get; set; }
        public Nullable<int> serial_no_ADP { get; set; }
        public string col_a { get; set; }
        public string col_b { get; set; }
        public string data_captured_from_app_version { get; set; }
        public string username { get; set; }
        public Nullable<int> districtId { get; set; }
        public string col_e { get; set; }
        public string col_c { get; set; }
        public string col_d { get; set; }
        public Nullable<int> CommitteId { get; set; }
        public string userId { get; set; }
        public string lat { get; set; }
        public string ProjectName { get; set; }
        public string imei { get; set; }
        public string unique_code { get; set; }
        public string uc_name { get; set; }
        public Nullable<System.DateTime> date_time_mobile { get; set; }
        public string district_name { get; set; }
        public Nullable<int> uc_available_status { get; set; }
        public string tehsil_name { get; set; }
    }
}