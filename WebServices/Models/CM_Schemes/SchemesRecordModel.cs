using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models.CM_Schemes
{
    public class SchemesRecordModel
    {
        public int ID { get; set; }
        public string SchemeName { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Na { get; set; }
        public string Pp { get; set; }
        public string Sector { get; set; }
        public string SubSector { get; set; }
        public string ChildSubSector { get; set; }
        public string ExecutingAgency { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Objectives { get; set; }
        public DateTime? AaDate { get; set; }
        public double? ApprovedCost { get; set; }
        public DateTime? TsDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public bool? ProcurementUnderProgress { get; set; }
        public DateTime? TenderDate { get; set; }
        public string ProjectId { get; set; }
        public double? ReleaseAmount { get; set; }
        public double? UtilizeAmount { get; set; }
        public int? Progress { get; set; }
        public double? EstimatedCost { get; set; }
        public string Status { get; set; }
        public int? SchemeInitiatedId { get; set; }

    }


    public class DistrictAllocationModel
    {
        public string DistrictName { get; set; }
        public double Allocation { get; set; }
    }


}