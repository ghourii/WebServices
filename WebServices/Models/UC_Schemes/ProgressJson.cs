using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models.UC_Schemes
{
    public class ProgressJson
    {
        public string imei { get; set; }
        public int? status_id { get; set; }
        public int? DesignationId { get; set; }
        public string remarks { get; set; }
        public int? uc_mc_type_id { get; set; }
        public int? tehsil_id { get; set; }
        public string unique_code { get; set; }
        public string date_time_mobile { get; set; }
        public string uc_mc_name { get; set; }
        public string Role { get; set; }
        public int? scheme_id { get; set; }
        public int? uc_id { get; set; }
        public string tehsil_name { get; set; }
        public string scheme_value { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }
        public string user_role { get; set; }
        public string col_a { get; set; }
        public string designation_double_check { get; set; }
        public string need_designation_correction { get; set; }
        public string col_b { get; set; }
        public string data_captured_from_app_version { get; set; }
        public string col_e { get; set; }
        public string col_c { get; set; }
        public string col_d { get; set; }
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


    public class ProgressJsonTpv
    {
        public string district_name { get; set; }
        public string tehsil_name { get; set; }
        public int? uc_mc_type_id { get; set; }
        public string uc_mc_name { get; set; }
        public string scheme_value { get; set; }
        public int? status_id { get; set; }
        public int? quality_of_work { get; set; }
        public int? scheme_exits { get; set; }
        public string previous_scheme_condition { get; set; }
        public int? scheme_measurement { get; set; }
        public string is_bricks_material { get; set; }
        public string is_pcc_material { get; set; }
        public string is_mortar_material { get; set; }
        public string is_plaster_material { get; set; }
        public string is_steel_material { get; set; }
        public string is_concrete_pavers_material { get; set; }
        public string remarks { get; set; }
        public string imei_surveyed_mobile { get; set; }
        public string date_time_mobile { get; set; }
        public string lat1 { get; set; }
        public string lng1 { get; set; }
        public string lat2 { get; set; }
        public string lng2 { get; set; }
        public string lat3 { get; set; }
        public string lng3 { get; set; }
        public string lat4 { get; set; }
        public string lng4 { get; set; }
        public string lat5 { get; set; }
        public string lng5 { get; set; }
        public string lat6 { get; set; }
        public string lng6 { get; set; }
        public string lat7 { get; set; }
        public string lng7 { get; set; }
        public string lat8 { get; set; }
        public string lng8 { get; set; }
        public string user_mobile { get; set; }
        public string user_name { get; set; }
        public int? scheme_id { get; set; }
        public int? uc_id { get; set; }
        public int? tehsil_id { get; set; }
        public int? district_id { get; set; }
        public string unique_code { get; set; }
        public string data_captured_from_app_version { get; set; }
        public string data_sync_time { get; set; }
        public string col_a { get; set; }
        public string col_b { get; set; }
        public string col_c { get; set; }
        public string col_d { get; set; }
        public string col_e { get; set; }
        public string is_concrete_pavers_crushing_strength_test { get; set; }
        public string is_concrete_pavers_size_test { get; set; }
        public string is_steel_tensile_and_yield_strength_test { get; set; }
        public string is_plaster_thickness_test { get; set; }
        public string is_mortar_ratio_cement_sand_test { get; set; }
        public string is_pcc_crushing_strength_test { get; set; }
        public string is_pcc_ratio_concrete_test { get; set; }
        public string is_bricks_measurements_test { get; set; }
        public string is_bricks_crushing_strength_test { get; set; }
        public string is_bricks_absorption_test { get; set; }
    }


}