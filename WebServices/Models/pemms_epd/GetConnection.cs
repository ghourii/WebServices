using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models.pemms_epd
{
    public static class GetConnection
    {
        public static string GetConnectionString()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["pemm_epd"].ConnectionString;
            return connection;
        }
    }
}