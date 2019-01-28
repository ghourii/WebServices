using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clean_Green_Punjab_Services.Models
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}