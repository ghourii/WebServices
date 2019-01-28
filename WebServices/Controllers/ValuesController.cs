using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace WebServices.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        public String calcularRota(String latitude, String longitude)
        {
            try
            {
                string url = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", latitude.ToString(), longitude.ToString());
                XElement xml = XElement.Load(url);
                if (xml.Element("status").Value == "OK")
                {
                    return xml.Element("result").Element("formatted_address").Value.ToString();
                }
                else
                {
                    return String.Concat(xml.Element("status").Value);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
