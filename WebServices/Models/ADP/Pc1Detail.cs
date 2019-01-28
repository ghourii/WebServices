using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models.ADP
{
    public class Pc1Detail
    {
        public string PcId { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string ApprovedCost { get; set; }
        public string yearly_total_allocation { get; set; }
        public string Serial { get; set; }
        public string RequestStatus { get; set; }
        public string SectorId { get; set; }
        public string SubsectorId { get; set; }
        public string DivisionId { get; set; }
        public string DistrictId { get; set; }
        public List<int?> DistIds { get; set; }
        public string DepartmentId { get; set; }
    }

    public class AdpDetailByProject
    {

        public string Name { get; set; }
        public string Cost { get; set; }
        public string Serial { get; set; }
        public string RequestStatus { get; set; }
        public string SectorId { get; set; }
        public string SubsectorId { get; set; }
        public string DivisionId { get; set; }
        public string DistrictId { get; set; }
        public List<int?> DistIds { get; set; }
        public string DepartmentId { get; set; }
        public string SectorName { get;  set; }
        public string SubSectorName { get;  set; }
        public string DivisionName { get;  set; }
        public string DistName { get;  set; }
        public string DepName { get;  set; }
        public string yearly_total_allocation { get; set; }
        public string SchemeCode { get; set; }
        public string Year { get; set; }

    }
    public class AutocompleteData
    {
        public string value { get; set; }
        public string id { get; set; }
    }
    public class AdpPC1
    {
        public string Name { get; set; }
        public string Cost { get; set; }
        public string Serial { get; set; }
        public string Year { get; set; }
    }
    public class AdpPC1New
    {
        public string ProjectName { get; set; }
        public string Allocation { get; set; }
        public string YearlyTotalAllocation { get; set; }
        public string Approved { get; set; }
        public string Sector { get; set; }
        public string SubSector { get; set; }
        public string DepartmentName { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string LocalCapital { get; set; }
        public string LocalRevenue { get; set; }
        public string TotalRevenue { get; set; }
        public string EstimatedCost { get; set; }
        public string ExpenditureCost { get; set; }
        public string AdpSerial { get; set; }
        public string FinancialYear { get; set; }
    }


}