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
    
    public partial class tbl_raw_tpv
    {
        public int id { get; set; }
        public string data_json { get; set; }
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
    }
}
