using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models.ADP
{
    public class SelectItemList
    {
        public bool Disabled { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class Select2Data
    {
        public string text { get; set; }
        public string id { get; set; }
    }
}