using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDWPAuth.Models
{
    public class ResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}