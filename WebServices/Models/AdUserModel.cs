using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models
{
    public class AdUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string Groups { get; set; }
    }
}