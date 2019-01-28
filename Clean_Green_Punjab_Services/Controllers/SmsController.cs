using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Clean_Green_Punjab_Services.Models;

namespace Clean_Green_Punjab_Services.Controllers
{
    public class SmsController : ApiController
    {
        [HttpPost]
        public async Task<string> GetCode(string mobile, string imei)
        {
            //var model = new ResponseModel();
            var res = "";
            try
            {
                using(var db = new clean_green_punjabEntities())
                {
                    var code = GenerateNewRandom();
                    var isSent = await SendSms(mobile, code);
                    if (isSent)
                    {
                        var obj = new android_user_sms_verification()
                        {
                            mobile = mobile,
                            imei = imei,
                            sms_code = code,
                            is_verified = false,
                            code_create_date_time = DateTime.Now
                        };
                        db.android_user_sms_verification.Add(obj);
                        await db.SaveChangesAsync();
                        res = "Sent";
                        //model.Status = "200";
                        //model.Message = "Sent";
                    }
                    else
                    {
                        //model.Status = "302";
                        //model.Message = "Not Sent";
                        res = "Not Sent";
                    }
                };
            }
            catch (Exception ex)
            {
                //model.Status = "500";
                //model.Message = ex.Message;
                res = ex.InnerException.Message;
            }
            return res;
            //return Request.CreateResponse(HttpStatusCode.OK, model);
        }
        private string GenerateNewRandom()
        {
            var generator = new Random();
            var r = generator.Next(10000, 90000).ToString("D5");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
        private async Task<bool> SendSms(string number, string message)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("http://172.20.81.32/gwasa/send.php?number=" + number + "&message=" + message + "").Result.Content.ReadAsStringAsync();
            var isSent = response.Contains("200");
            if (isSent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<string> Verification(string mobile, string code, string imei)
        {
            var res = "";
            //var model = new ResponseModel();
            try
            {
                using (var db = new clean_green_punjabEntities())
                {
                    var obj = await db.android_user_sms_verification.OrderByDescending(x => x.pk_id).FirstOrDefaultAsync(x => x.mobile.Contains(mobile) && x.imei.Contains(imei));
                    if (obj != null)
                    {
                        if (obj.sms_code == code)
                        {
                            obj.is_verified = true;
                            obj.code_verified_date_time = DateTime.Now;
                            await db.SaveChangesAsync();
                            res = "Verified";
                            //model.Status = "200";
                            //model.Message = "Verified";
                        }
                        else
                        {
                            res = "Not Verified";
                            //model.Status = "302";
                            //model.Message = "Not Verified";
                        }
                    }
                    else
                    {
                        res = "Record not found";
                        //model.Status = "404";
                        //model.Message = "Record not found";
                    }
                }
            }
            catch (Exception ex)
            {
                res = ex.Message;
                //model.Status = "500";
                //model.Message = ex.Message;
            }
            return res;
            //return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}
