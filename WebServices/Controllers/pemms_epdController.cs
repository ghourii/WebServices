using DynamixPostgreSQLHandler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebServices.Models.pemms_epd;
using WebServices.Utills;

namespace WebServices.Controllers
{
    public class pemms_epdController : ApiController
    {
        private string _connectionString { get; set; }
        private string _url { get; set; } = "http://172.20.82.57:8983/solr/EPD";
        public pemms_epdController()
        {
           
            _connectionString = GetConnection.GetConnectionString();
            
        }

        [HttpGet]
        public async Task<string> Sync()
        {
            try
            {
                SQLHandler sqlHandler = new SQLHandler(_connectionString);
                var search = sqlHandler.ExecuteAsListUsingQuery<epd_search_model>("Select * From epd_search ; ");
                foreach (var item in search)
                {
                    await FindRecord(item);
                    var add = await AddRecord(item);
                }
                await DeleteIrisFilesFromSolr();
                return "200";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
                // HANDLE YOUR EXCEPTION HERE
            }
        }

        private async Task FindRecord(epd_search_model model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{_url}/select?q=*:*&fq=uid:\"{model.uid}\"&fl=id,uid";
                    var result = await client.GetAsync(url);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    dynamic d = JsonConvert.DeserializeObject<dynamic>(resultContent);
                    var response = d.response;
                    var numFound = Numerics.GetInt(response.numFound);
                    if (numFound > 0)
                    {
                        var docs = Convert.ToString(response.docs);
                        epd_search_response_model[] res = JsonConvert.DeserializeObject<epd_search_response_model[]>(docs);
                        foreach (var item in res)
                        {
                            bool deleted = await DeleteRecord(item.id);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            
        }

        private async Task<bool> AddRecord(epd_search_model model)
        {
            try
            {
                var obj = new Dictionary<string, string>();
                obj.Add("uid", !string.IsNullOrEmpty(model.uid) ? model.uid : "null");

                obj.Add("track_proj_code", !string.IsNullOrEmpty(model.track_proj_code) ? model.track_proj_code : "null");
                obj.Add("project_title", !string.IsNullOrEmpty(model.project_title) ? model.project_title : "null");
                obj.Add("project_description", !string.IsNullOrEmpty(model.project_description) ? model.project_description : "null");
                obj.Add("application_type", !string.IsNullOrEmpty(model.application_type) ? model.application_type : "null");
                obj.Add("propo_name", !string.IsNullOrEmpty(model.propo_name) ? model.propo_name : "null");
                obj.Add("date_time", !string.IsNullOrEmpty(model.date_time) ? model.date_time : "null");
                obj.Add("project_sub_type", !string.IsNullOrEmpty(model.project_sub_type) ? model.project_sub_type : "null");
                obj.Add("project_type", !string.IsNullOrEmpty(model.project_type) ? model.project_type : "null");
                obj.Add("div_name", !string.IsNullOrEmpty(model.div_name) ? model.div_name : "null");
                obj.Add("dist_name", !string.IsNullOrEmpty(model.dist_name) ? model.dist_name : "null");
                obj.Add("tehsil_name", !string.IsNullOrEmpty(model.tehsil_name) ? model.tehsil_name : "null");
                obj.Add("status_internal", !string.IsNullOrEmpty(model.status_internal) ? model.status_internal : "null");
                obj.Add("status", !string.IsNullOrEmpty(model.status) ? model.status : "null");
                obj.Add("sir_track_status_internal", !string.IsNullOrEmpty(model.sir_track_status_internal) ? model.sir_track_status_internal : "null");
                obj.Add("sir_track__status", !string.IsNullOrEmpty(model.sir_track__status) ? model.sir_track__status : "null");
                obj.Add("remarks", !string.IsNullOrEmpty(model.remarks) ? model.remarks : "null");
                obj.Add("tehsil_code", !string.IsNullOrEmpty(model.tehsil_code) ? model.tehsil_code : "null");
                obj.Add("div_id", !string.IsNullOrEmpty(model.div_id) ? model.div_id : "null");
                obj.Add("dist_id", !string.IsNullOrEmpty(model.dist_id) ? model.dist_id : "null");

                var list = new List<Dictionary<string, string>> { obj };
                var json = JsonConvert.SerializeObject(list);
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var url = $"{_url}/update?commit=true&wt=json";
                    var result = await client.PostAsync(url, httpContent);
                    string resultContent = await result.Content.ReadAsStringAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> DeleteRecord(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var del = $"stream.body=<delete><query>id:{id}</query></delete>&commit=true";
                    var url = $"{_url}/update?wt=json&{del}";
                    var result = await client.GetAsync(url);
                    string resultContent = await result.Content.ReadAsStringAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
     
        private async Task<bool> DeleteIrisFilesFromSolr()
        {
            try
            {
                
                var url = $"{_url}/select?fl=id,uid&q=*:*";
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(url);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    dynamic d = JsonConvert.DeserializeObject<dynamic>(resultContent);
                    var response = d.response;
                    Int64 numFound = Numerics.GetInt(response.numFound);
                    if (numFound > 0)
                    {
                        using (var httpClient = new HttpClient())
                        {
                            url = url + $"&start=0&rows={numFound}";
                            var httpResult = await client.GetAsync(url);
                            string httpResultContent = await httpResult.Content.ReadAsStringAsync();
                            dynamic httpD = JsonConvert.DeserializeObject<dynamic>(httpResultContent);
                            var httpResponse = httpD.response;
                            string docs = Convert.ToString(httpResponse.docs);
                            List<epd_search_delete_model> solr = JsonConvert.DeserializeObject<List<epd_search_delete_model>>(docs);

                            SQLHandler sqlHandler = new SQLHandler(_connectionString);
                            var search = sqlHandler.ExecuteAsListUsingQuery<string>("Select uid From epd_search ; ");
                            solr.RemoveAll(x => search.Any(y => y == x.uid.FirstOrDefault()));
                            foreach (var item in solr)
                            {
                                var dl = await DeleteRecord(item.id);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Search
        [HttpGet]
        public async Task<string> Search(string q, int? take, int? skip)
        {
            if (string.IsNullOrEmpty(q))
            {
                q = "*:*";
            }
            take = take ?? 10;
            skip = skip ?? 0;
            var length = $"rows={take}&start={skip}";
            try
            {
                var search = await SearchFromSolr(length, q);
                return search;
                //var response = new ApiResponse
                //{
                //    Status = "200",
                //    Response = search
                //};
                ////return search;
                //return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                //await ew.Exception(ex, "Search EPD");
                //var response = new ApiResponse
                //{
                //    Status = "500",
                //    Response = ex.InnerException.Message
                //};
                //return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return "500";
            }
        }

        private async Task<string> SearchFromSolr(string length, string q)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = _url + $"/select?q={q}&{length}&hl=true&hl.fl=_copytext_&hl.fragsize=100";
                    var result = await client.GetAsync(url);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    return resultContent;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        [HttpGet]
        public async Task<HttpResponseMessage> SpellSearch(string q)
        {
            try
            {
                var search = await SpellCheckFromSolr(q);
                var response = new ApiResponse
                {
                    Status = "200",
                    Response = search
                };
                //return search;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse
                {
                    Status = "500",
                    Response = ex.Message
                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
           
        }

        private async Task<string> SpellCheckFromSolr(string q)
        {
            using (var client = new HttpClient())
            {
                var url = _url + $"/spell?spellcheck=on&spellcheck.q={q}";
                var result = await client.GetAsync(url);
                string resultContent = await result.Content.ReadAsStringAsync();
                return resultContent;
            }
        }
        #endregion



    }
    public class ApiResponse
    {
        public string Status { get; set; }
        public string Response { get; set; }
    }
}
