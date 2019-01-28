using PDWPCommon.Models;
using PDWPCommon.Utills;
using PDWPLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static PDWPCommon.Models.PDWPModel;

namespace PDWPCommon
{
    public class PDWPRepository
    {
        private PDWP_WorkingPaper_DBEntities db = new PDWP_WorkingPaper_DBEntities();
        public IEnumerable<DateTime?> GetCalendarDates()
        {
            var array = db.Agendas.Select(x => x.Date).ToList();
            return array;
        }
        public List<PDWPModel.Agenda> GetCalendarData(int month, int year, int province,string host,string controller)
        {
            
            var agenda = db.Iris_Get_Agendas(month, year, province).OrderBy(x => x.Date).ToList();
            var list = new List<PDWPModel.Agenda>();
            foreach (var item in agenda)
            {
                var obj = new PDWPModel.Agenda
                {
                    AgendaId = Encrypt.Encryption(item.AgendaId + "", "urbanunit"),
                    Title = item.Title,
                    AgendaNumber = item.AgendaNumber,
                    Date = item.Date,
                    FinancialYearId = item.FinancialYearId,
                    FinancialYearName = item.FinancialYearName,
                    AgendaAttachment = item.AgendaAttachment,
                    AgendaAttachmentPath = $"{host}/api/{controller}/AgendaAttachment/{Encrypt.Encryption(item.AgendaId + "", "urbanunit")}", //item.AgendaAttachmentPath?.Replace("~", "http://iris.urbanunit.gov.pk/"),
                    MeetingStatus = item.MeetingStatus,
                    Status = item.Status,
                    AttachmentStatus = item.AttachmentStatus
                };

                var wps = db.Iris_Get_Agenda_WorkingPapers(item.AgendaId);

                var events = wps.GroupBy(x => x.WorkingPaperId).Select(x => new Events
                {
                    AdpNo = x.FirstOrDefault()?.AdpSerial,
                    Title = x.FirstOrDefault()?.WorkingPaperTitle,
                    Sector = x.FirstOrDefault()?.SectorName,
                    EstimatedCost = x.FirstOrDefault()?.EstimatedCost + "",
                    AdpAllocation = x.FirstOrDefault()?.AdpAllocation + "",
                    MeetingStatus = x.FirstOrDefault()?.MeetingStatus,
                    Detail = new EventDetail
                    {
                        Districts = x.FirstOrDefault()?.DistName,
                        ExecutingAgency = x.FirstOrDefault()?.ExecAgencyName,
                        RevisedCost = x.FirstOrDefault()?.AdpCost + "",
                        SchemeCode = "",
                        SponsoringAgency = x.FirstOrDefault()?.SponAgencyName,
                        SubSector = x.FirstOrDefault()?.SubsectorName,
                        Pc1 = x.Where(y => y.AttachmentTypeId == 1).OrderByDescending(y => y.AttachmentDate).Select(pc => new Attachments
                        {
                            Attachment = pc.Attachment,
                            Name = pc.AttachmentName,
                            Path = $"{host}/api/{controller}/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}", //pc.AttachmentPath?.Replace("~", "http://iris.urbanunit.gov.pk/"),
                            Date = pc.AttachmentDate,
                            AttachmentStatus = item.AttachmentStatus
                        }).ToArray(),
                        Pc2 = x.Where(y => y.AttachmentTypeId == 2).OrderByDescending(y => y.AttachmentDate).Select(pc => new Attachments
                        {
                            Attachment = pc.Attachment,
                            Name = pc.AttachmentName,
                            Path = $"{host}/api/{controller}/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}", //pc.AttachmentPath?.Replace("~", "http://iris.urbanunit.gov.pk/"),
                            Date = pc.AttachmentDate,
                            AttachmentStatus = item.AttachmentStatus
                        }).ToArray(),
                        Pc3 = x.Where(y => y.AttachmentTypeId == 3).OrderByDescending(y => y.AttachmentDate).Select(pc => new Attachments
                        {
                            Attachment = pc.Attachment,
                            Name = pc.AttachmentName,
                            Path = $"{host}/api/{controller}/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}", //pc.AttachmentPath?.Replace("~", "http://iris.urbanunit.gov.pk/"),
                            Date = pc.AttachmentDate,
                            AttachmentStatus = item.AttachmentStatus
                        }).ToArray(),
                        Pc4 = x.Where(y => y.AttachmentTypeId == 4).OrderByDescending(y => y.AttachmentDate).Select(pc => new Attachments
                        {
                            Attachment = pc.Attachment,
                            Name = pc.AttachmentName,
                            Path = $"{host}/api/{controller}/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}", //pc.AttachmentPath?.Replace("~", "http://iris.urbanunit.gov.pk/"),
                            Date = pc.AttachmentDate,
                            AttachmentStatus = item.AttachmentStatus
                        }).ToArray(),
                        Pc5 = x.Where(y => y.AttachmentTypeId == 5).OrderByDescending(y => y.AttachmentDate).Select(pc => new Attachments
                        {
                            Attachment = pc.Attachment,
                            Name = pc.AttachmentName,
                            Path = $"{host}/api/{controller}/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}", //pc.AttachmentPath?.Replace("~", "http://iris.urbanunit.gov.pk/"),
                            Date = pc.AttachmentDate,
                            AttachmentStatus = item.AttachmentStatus
                        }).ToArray(),
                        MeetingsPrePDWP = x.Where(y => y.AttachmentTypeId == 15 || y.AttachmentTypeId == 9 || y.AttachmentTypeId == 10).OrderByDescending(y => y.AttachmentDate).Select(pc => new MeetingsPrePDWP
                        {
                            Presentation = pc.AttachmentTypeId == 10 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "", // pc.AttachmentPath?.Replace("~", "http://iris.urbanunit.gov.pk/") 
                            Mom = pc.AttachmentTypeId == 9 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "",
                            WorkingPaper = pc.AttachmentTypeId == 15 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "",
                            Pre_PDWP = pc.AttachmentName,
                            Date = pc.AttachmentDate,
                            FileName = pc.Attachment,
                            AttachmentStatus = item.AttachmentStatus

                        }).ToArray(),
                        MeetingsPDWP = x.Where(y => y.AttachmentTypeId == 6 || y.AttachmentTypeId == 7 || y.AttachmentTypeId == 8 || y.AttachmentTypeId == 17).OrderByDescending(y => y.AttachmentDate).Select(pc => new MeetingsPDWP
                        {
                            Presentation = pc.AttachmentTypeId == 7 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "",
                            Mom = pc.AttachmentTypeId == 8 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "",
                            WorkingPaper = pc.AttachmentTypeId == 6 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "",
                            Agenda = pc.AttachmentTypeId == 17 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "",
                            Pdwp = pc.AttachmentName,
                            Date = pc.AttachmentDate,
                            FileName = pc.Attachment,
                            AttachmentStatus = item.AttachmentStatus

                        }).ToArray(),
                        MeetingsCDWP = x.Where(y => y.AttachmentTypeId == 12 || y.AttachmentTypeId == 13 || y.AttachmentTypeId == 14).OrderByDescending(y => y.AttachmentDate).Select(pc => new MeetingsCDWP
                        {
                            WorkingPaper = pc.AttachmentTypeId == 12 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "",
                            Presentation = pc.AttachmentTypeId == 13 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "",
                            Mom = pc.AttachmentTypeId == 14 ? $"{host}/api/PDWP/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}" : "",
                            Cdwp = pc.AttachmentName,
                            Date = pc.AttachmentDate,
                            FileName = pc.Attachment,
                            AttachmentStatus = item.AttachmentStatus

                        }).ToArray(),
                        LiveMonitoringDash = x.Where(y => !string.IsNullOrEmpty(y.LiveMonitoringDash)).OrderByDescending(y => y.AttachmentDate).Select(pc => new LiveMonitoringDash
                        {
                            Name = pc.AttachmentName,
                            File = $"{host}/api/{controller}/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}"
                        }).ToArray(),
                        MonitoringReports = x.Where(y => y.AttachmentTypeId == 11).OrderByDescending(y => y.AttachmentDate).Select(pc => new MonitoringReports
                        {
                            Name = pc.AttachmentName,
                            Date = pc.AttachmentDate,
                            Attachment = pc.Attachment,
                            Path = $"{host}/api/{controller}/WorkingPaperAttachment/{Encrypt.Encryption(pc.AttachmentId + "", "urbanunit")}",
                            AttachmentStatus = item.AttachmentStatus
                        }).ToArray(),
                    }
                }).ToArray();
                obj.Events = events;
                list.Add(obj);
            }
            return list;
        }
        public void AgendaAttachment()
        {

        }
        //public class Agenda
        //{
        //    public string AgendaId { get; set; }
        //    public string Title { get; set; }
        //    public string AgendaNumber { get; set; }
        //    public DateTime? Date { get; set; }
        //    public int? FinancialYearId { get; set; }
        //    public string FinancialYearName { get; set; }
        //    public string AgendaAttachment { get; set; }
        //    public string AgendaAttachmentPath { get; set; }
        //    public Events[] Events { get; set; }
        //    public string MeetingStatus { get; set; }
        //    public string Status { get; set; }
        //    public string AttachmentStatus { get; set; }
        //}
    }
}
