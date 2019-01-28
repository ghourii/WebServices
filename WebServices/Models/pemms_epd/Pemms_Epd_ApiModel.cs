using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebServices.Models.pemms_epd
{
    public class Pemms_Epd_ApiModel
    {
        public void UpdateSolr()
        {
            //string host = "http://localhost:32671";
            string host = "http://172.20.81.19:8965";
            var client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.GetAsync($"/Api/pemms_epd/Sync");
        }
    }
}