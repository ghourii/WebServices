using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models.PDWP
{
    public class AgendaDetailModel
    {
        public Agenda AgendaDetail { get; set; }
        public List<Events> EventList { get; set; }
    }
    public class Agenda
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string AgendaNumber { get; set; }
        public string FinancialYearName { get; set; }
        public string Attachment { get; set; }
        public string ProvinceId { get; set; }
    }
    public class Events
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Cost { get; set; }
        public string Objectives { get; set; }
        public string Justification { get; set; }
        public string AdpSerial { get; set; }
        public string SectorName { get; set; }
        public string AdpCost { get; set; }
        public string DivName { get; set; }
        public string DistName { get; set; }
        public string DepartmentName { get; set; }
        public string ExecAgencyName { get; set; }
    }
}