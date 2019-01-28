using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDWPCommon.Models
{
    public class PDWPModel
    {
        public class Agenda
        {
            public string AgendaId { get; set; }
            public string Title { get; set; }
            public string AgendaNumber { get; set; }
            public DateTime? Date { get; set; }
            public int? FinancialYearId { get; set; }
            public string FinancialYearName { get; set; }
            public string AgendaAttachment { get; set; }
            public string AgendaAttachmentPath { get; set; }
            public Events[] Events { get; set; }
            public string MeetingStatus { get; set; }
            public string Status { get; set; }
            public string AttachmentStatus { get; set; }
        }
        public class Events
        {
            public string AdpNo { get; set; }
            public string Title { get; set; }
            public string Sector { get; set; }
            public string EstimatedCost { get; set; }
            public string AdpAllocation { get; set; }
            public EventDetail Detail { get; set; }
            public string MeetingStatus { get; set; }

        }
        public class EventDetail
        {
            public string SchemeCode { get; set; }
            public string SubSector { get; set; }
            public string SponsoringAgency { get; set; }
            public string ExecutingAgency { get; set; }
            public string RevisedCost { get; set; }
            public string Districts { get; set; }
            public Attachments[] Pc1 { get; set; }
            public Attachments[] Pc2 { get; set; }
            public Attachments[] Pc3 { get; set; }
            public Attachments[] Pc4 { get; set; }
            public Attachments[] Pc5 { get; set; }
            public MeetingsPrePDWP[] MeetingsPrePDWP { get; set; }
            public MeetingsPDWP[] MeetingsPDWP { get; set; }
            public MeetingsCDWP[] MeetingsCDWP { get; set; }
            public LiveMonitoringDash[] LiveMonitoringDash { get; set; }
            public MonitoringReports[] MonitoringReports { get; set; }

        }
        public class Attachments
        {
            public string Name { get; set; }
            public string Attachment { get; set; }
            public string Path { get; set; }
            public DateTime? Date { get; set; }
            public string AttachmentStatus { get; set; }
        }
        public class MeetingsPrePDWP
        {
            public string Pre_PDWP { get; set; }
            public DateTime? Date { get; set; }
            public string WorkingPaper { get; set; }
            public string Mom { get; set; }
            public string Presentation { get; set; }
            public string FileName { get; set; }
            public string AttachmentStatus { get; set; }
        }
        public class MeetingsPDWP
        {
            public string Pdwp { get; set; }
            public DateTime? Date { get; set; }
            public string WorkingPaper { get; set; }
            public string Mom { get; set; }
            public string Agenda { get; set; }
            public string Presentation { get; set; }
            public string FileName { get; set; }
            public string AttachmentStatus { get; set; }
        }
        public class MeetingsCDWP
        {
            public string Cdwp { get; set; }
            public DateTime? Date { get; set; }
            public string WorkingPaper { get; set; }
            public string Mom { get; set; }
            public string Presentation { get; set; }
            public string FileName { get; set; }
            public string AttachmentStatus { get; set; }
        }
        public class LiveMonitoringDash
        {
            public string Name { get; set; }
            public string File { get; set; }
        }
        public class MonitoringReports
        {
            public string Name { get; set; }
            public DateTime? Date { get; set; }
            public string Attachment { get; set; }
            public string Path { get; set; }
            public string AttachmentStatus { get; set; }
        }
    }
}
