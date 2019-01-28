using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebServices.Models;
using WebServices.Models.CM_Schemes;

namespace WebServices.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            //var userManager = new ApplicationUserManager(store);

            //await userManager.UpdateSecurityStampAsync(User.Identity.GetUserId());
            

            return View();
        }

        public async Task<ActionResult> GetValues(int? id)
        {
            string values = "";
            string host = Request.Url.Authority;
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://" + host);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (id != null)
            {
                values = await client.GetAsync("/Api/CM_Schemes/Get/" + id).Result.Content.ReadAsStringAsync();
            }
            else
            {
                values = await client.GetAsync("/Api/CM_Schemes/Get").Result.Content.ReadAsStringAsync();
            }
            var model = JsonConvert.DeserializeObject<List<SchemesRecordModel>>(values);
            return View(model);
        }

        public async Task<ActionResult> GetDistrictAllocation()
        {
            string values = "";
            string host = Request.Url.Authority;
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://" + host);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            values = await client.GetAsync("/Api/CM_Schemes/GetDistrictAllocation").Result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<List<DistrictAllocationModel>>(values);
            return View(model);
        }


    }
}
