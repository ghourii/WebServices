//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UC_SchemesDataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tpv_progress
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> db_date_time { get; set; }
        public string app_version { get; set; }
        public string imei { get; set; }
        public Nullable<int> local_id { get; set; }
        public string mobile_no { get; set; }
        public string img_scheme_1 { get; set; }
        public System.Data.Entity.Spatial.DbGeometry geom_scheme_1 { get; set; }
        public string img_scheme_2 { get; set; }
        public System.Data.Entity.Spatial.DbGeometry geom_scheme_2 { get; set; }
        public string img_bricks { get; set; }
        public System.Data.Entity.Spatial.DbGeometry geom_bricks { get; set; }
        public string img_pcc { get; set; }
        public System.Data.Entity.Spatial.DbGeometry geom_pcc { get; set; }
        public string img_mortor { get; set; }
        public System.Data.Entity.Spatial.DbGeometry geom_mortor { get; set; }
        public string img_plaster { get; set; }
        public System.Data.Entity.Spatial.DbGeometry geom_plaster { get; set; }
        public string img_steel { get; set; }
        public System.Data.Entity.Spatial.DbGeometry geom_steel { get; set; }
        public string img_concrete { get; set; }
        public System.Data.Entity.Spatial.DbGeometry geom_concrete { get; set; }
        public string user_role { get; set; }
        public Nullable<bool> Added { get; set; }
        public string is_bricks_measurements_test { get; set; }
        public Nullable<bool> scheme_measurement { get; set; }
        public string is_bricks_crushing_strength_test { get; set; }
        public string is_steel_tensile_and_yield_strength_test { get; set; }
        public Nullable<int> tehsil_id { get; set; }
        public string data_captured_from_app_version { get; set; }
        public string previous_scheme_condition { get; set; }
        public string is_pcc_material { get; set; }
        public string is_concrete_pavers_material { get; set; }
        public Nullable<int> scheme_id { get; set; }
        public string is_plaster_thickness_test { get; set; }
        public string imei_surveyed_mobile { get; set; }
        public string is_pcc_ratio_concrete_test { get; set; }
        public string is_concrete_pavers_size_test { get; set; }
        public Nullable<int> district_id { get; set; }
        public Nullable<int> status_id { get; set; }
        public string lng3 { get; set; }
        public string lng4 { get; set; }
        public string lng1 { get; set; }
        public string lng2 { get; set; }
        public string lng8 { get; set; }
        public string user_name { get; set; }
        public string lng7 { get; set; }
        public string lng6 { get; set; }
        public string lng5 { get; set; }
        public string date_time_mobile { get; set; }
        public string is_bricks_material { get; set; }
        public Nullable<bool> scheme_exits { get; set; }
        public string is_mortar_ratio_cement_sand_test { get; set; }
        public Nullable<int> quality_of_work { get; set; }
        public string remarks { get; set; }
        public string is_bricks_absorption_test { get; set; }
        public string col_a { get; set; }
        public string col_b { get; set; }
        public string is_pcc_crushing_strength_test { get; set; }
        public string col_e { get; set; }
        public string col_c { get; set; }
        public string col_d { get; set; }
        public string user_mobile { get; set; }
        public Nullable<int> uc_id { get; set; }
        public string lat8 { get; set; }
        public string lat7 { get; set; }
        public string lat4 { get; set; }
        public string lat3 { get; set; }
        public string data_sync_time { get; set; }
        public string lat6 { get; set; }
        public string is_concrete_pavers_crushing_strength_test { get; set; }
        public string lat5 { get; set; }
        public Nullable<int> uc_mc_type_id { get; set; }
        public string lat2 { get; set; }
        public string lat1 { get; set; }
        public string is_steel_material { get; set; }
        public string unique_code { get; set; }
        public string district_name { get; set; }
        public string is_mortar_material { get; set; }
        public string uc_mc_name { get; set; }
        public string tehsil_name { get; set; }
        public string is_plaster_material { get; set; }
        public string scheme_value { get; set; }
    
        public virtual Pm_Tbl_Main_Scheme Pm_Tbl_Main_Scheme { get; set; }
        public virtual tbl_UC_MC_Detail tbl_UC_MC_Detail { get; set; }
    }
}
