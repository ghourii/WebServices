using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models.CM_Monitoring_Committe
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
        public string uc_available_status { get; set; }
        public string tehsil_name { get; set; }
    }

    public class ProgressJsonMonitoring
    {
        public string is_payment_issue { get; set; }
        public string other_issue { get; set; }
        public string lng { get; set; }
        public string remarks { get; set; }
        public string contractors_issue { get; set; }
        public int tehsil_id { get; set; }
        public string is_time_delay_issue { get; set; }
        public string user_role { get; set; }
        public string is_quality_issue { get; set; }
        public int scheme_id { get; set; }
        public int uc_id { get; set; }
        public string is_other_issue { get; set; }
        public string lat { get; set; }
        public string drawing_issue { get; set; }
        public string is_drawing_issue { get; set; }
        public string is_consultant_issue { get; set; }
        public string time_delay_issue { get; set; }
        public string quality_issue { get; set; }
        public string imei { get; set; }
        public string consultant_issue { get; set; }
        public string issue_status { get; set; }
        public int status_id { get; set; }
        public int DesignationId { get; set; }
        public string payment_issue { get; set; }
        public int uc_mc_type_id { get; set; }
        public string is_land_issue { get; set; }
        public string unique_code { get; set; }
        public string date_time_mobile { get; set; }
        public string land_issue { get; set; }
        public string is_contractors_issue { get; set; }
        public string uc_mc_name { get; set; }
        public string tehsil_name { get; set; }
        public string scheme_value { get; set; }
    }
}