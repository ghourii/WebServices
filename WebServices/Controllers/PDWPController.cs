using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;
using PDWPLayer;
using WebServices.Models;
using WebServices.Utills;
using System.Security.Claims;
using PDWPCommon;

namespace WebServices.Controllers
{
    public class PDWPController : ApiController
    {
        private PDWP_WorkingPaper_DBEntities db = new PDWP_WorkingPaper_DBEntities();
        private string _host;

        public PDWPController()
        {
            _host = "http://" + HttpContext.Current.Request.Url.Authority;
        }

        [HttpGet]
        public IEnumerable<DateTime?> GetCalendarDates()
        {
            return new PDWPRepository().GetCalendarDates();
        }

        [HttpPost]
        public HttpResponseMessage GetCalendarData(int month, int year, int province)
        {
            var model = new ResponseModel();

            if (User.Identity.IsAuthenticated && User.IsInRole("PDWP"))
            {
                var stamp = ((ClaimsIdentity)User.Identity).FindFirst("SecurityStamp").Value;
                var isValid = User.Identity.ValidateSecurityStamp(stamp);
                if (isValid)
                {
                    var list = new PDWPRepository().GetCalendarData(month, year, province, _host,"PDWP");
                    model.status = "200";
                    model.message = "Success";
                    model.data = list;
                }
                else
                {
                    model.status = "600";
                    model.message = "Your password has been changed.";
                }
            }
            else
            {
                model.status = "500";
                model.message = "User is not authorized.";
                model.data = null;
            }
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpGet]
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> AgendaAttachment(string id)
        {
            var model = new ResponseModel();
            if (User.Identity.IsAuthenticated && User.IsInRole("PDWP"))
            {
                var stamp = ((ClaimsIdentity)User.Identity).FindFirst("SecurityStamp").Value;
                var isValid = User.Identity.ValidateSecurityStamp(stamp);
                if (isValid)
                {
                    var agendaId = Numerics.GetInt(Encrypt.Decryption(id, "urbanunit"));
                    var agenda = await db.Agendas.FindAsync(agendaId);
                    if (agenda != null)
                    {
                        if (agenda.FileName != null)
                        {
                            var url = agenda.FilePath.Replace("~", "http://172.20.82.51/");
                            var uri = new Uri(url);
                            var name = Path.GetFileName(uri.AbsolutePath);

                            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                            WebClient client = new WebClient();
                            Stream stream = client.OpenRead(url);
                            result.Content = new StreamContent(stream);
                            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                            {
                                FileName = name
                            };
                            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            return result;
                        }
                        else
                        {
                            var obj = agenda?.SchemeAttachments?.FirstOrDefault(x => x.AttachmentTypeId == 17);
                            if (CheckFileDownload(obj?.IsPublic))
                            {
                                var attachment = obj?.Path;
                                if (!string.IsNullOrEmpty(attachment))
                                {
                                    var url = attachment.Replace("~", "http://172.20.82.51/");
                                    var uri = new Uri(url);
                                    var name = Path.GetFileName(uri.AbsolutePath);
                                    try
                                    {
                                        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                                        WebClient client = new WebClient();
                                        Stream stream = client.OpenRead(url);
                                        result.Content = new StreamContent(stream);
                                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                                        {
                                            FileName = name
                                        };
                                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                                        return result;
                                    }
                                    catch (Exception ex)
                                    {
                                        model.status = "404";
                                        model.message = ex.Message;
                                        return Request.CreateResponse(HttpStatusCode.OK, model);
                                    }

                                }
                            }
                        }
                        model.status = "500";
                        model.message = "File is not available.";
                        return Request.CreateResponse(HttpStatusCode.OK, model);
                    }
                    model.status = "500";
                    model.message = "Agenda not found.";
                }
                else
                {
                    model.status = "600";
                    model.message = "Your password has been changed.";
                }
            }
            else
            {
                model.status = "500";
                model.message = "User is not authorized to view this file.";
            }
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> WorkingPaperAttachment(string id)
        {
            var model = new ResponseModel();
            if (User.Identity.IsAuthenticated && User.IsInRole("PDWP"))
            {
                var stamp = ((ClaimsIdentity)User.Identity).FindFirst("SecurityStamp").Value;
                var isValid = User.Identity.ValidateSecurityStamp(stamp);
                if (isValid)
                {
                    var attachmentId = Numerics.GetInt(Encrypt.Decryption(id, "urbanunit"));
                    var attachment = await db.SchemeAttachments.FindAsync(attachmentId);
                    if (attachment != null)
                    {
                        if (!string.IsNullOrEmpty(attachment.Path))
                        {
                            var url = attachment.Path.Replace("~", "http://172.20.82.51/");
                            var uri = new Uri(url);
                            var name = Path.GetFileName(uri.AbsolutePath);

                            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                            WebClient client = new WebClient();
                            Stream stream = client.OpenRead(url);
                            result.Content = new StreamContent(stream);
                            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                            {
                                FileName = name
                            };
                            result.Content.Headers.ContentType =
                                new MediaTypeHeaderValue("application/octet-stream");
                            return result;
                        }
                        else
                        {
                            model.status = "500";
                            model.message = "File is not available.";
                            return Request.CreateResponse(HttpStatusCode.OK, model);
                        }
                    }
                    else
                    {
                        model.status = "500";
                        model.message = "File is not available.";
                        return Request.CreateResponse(HttpStatusCode.OK, model);
                    }

                }
                else
                {
                    model.status = "600";
                    model.message = "Your password has been changed.";
                    return Request.CreateResponse(HttpStatusCode.OK, model);
                }
            }
            else
            {
                model.status = "500";
                model.message = "User is not authorized to view this file.";
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }

        }

        private bool CheckFileDownload(bool? isPublic)
        {
            var showAttachment = false;
            if (isPublic == true || isPublic == null)
            {
                showAttachment = true;
            }
            else
            {
                if (User.Identity.IsAuthenticated && User.IsInRole("PDWP"))
                {
                    showAttachment = true;
                }
            }
            return showAttachment;
        }

    }
}