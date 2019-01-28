using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models.pemms_epd
{
    public class epd_search_model
    {
        public string track_proj_code { get; set; }
        public string project_title { get; set; }
        public string project_description { get; set; }
        public string application_type { get; set; }
        public string propo_name { get; set; }
        public string date_time { get; set; }
        public string project_sub_type { get; set; }
        public string project_type { get; set; }
        public string div_name { get; set; }
        public string dist_name { get; set; }
        public string tehsil_name { get; set; }
        public string status_internal { get; set; }
        public string status { get; set; }
        public string sir_track_status_internal { get; set; }
        public string sir_track__status { get; set; }
        public string remarks { get; set; }
        public string uid { get; set; }
        public string tehsil_code { get; set; }
        public string div_id { get; set; }
        public string dist_id { get; set; }


    }
    public class epd_search_response_model
    {
        public string[] track_proj_code { get; set; }
        public string id { get; set; }
    }

    public class epd_search_delete_model
    {
        public string id { get; set; }
        public string[] uid { get; set; }
    }
}