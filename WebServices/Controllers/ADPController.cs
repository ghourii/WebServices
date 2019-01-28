using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AdpDataLayer;
using WebServices.Models.ADP;
using System.Web;
using WebServices.Utills;
using System.Data.Entity;

namespace WebServices.Controllers
{
    public class ADPController : ApiController
    {
        private ADP_PC1Entities db = new ADP_PC1Entities();

        [HttpGet]
        public IEnumerable<Select2Data> GetYears()
        {
            var years = db.tbl_adp.Select(x => new Select2Data
            {
                text = x.ADP_name,
                id = x.auto_ADP_id + ""
            });
            return years;
        }
        [HttpGet]
        public IEnumerable<Select2Data> GetDivisions()
        {
            var sectors = db.tbl_Division.OrderBy(x => x.Div_Name).Select(x => new Select2Data
            {
                text = x.Div_Name,
                id = x.Div_ID + ""
            });
            return sectors;
        }

        [HttpGet]
        public IEnumerable<Select2Data> GetDistricts(int? divid)
        {
            var sectors = db.tbl_Distt.AsQueryable();
            if (divid != null)
            {
                sectors = sectors.Where(x => x.Div_ID == divid);
            }
            var list = sectors.OrderBy(x => x.Dist_Name).Select(x => new Select2Data
            {
                text = x.Dist_Name,
                id = x.Distt_ID + ""
            });
            return list;
        }
        [HttpGet]
        public IEnumerable<Select2Data> GetSerials()
        {
            var years = db.tbl_ADP_Serial.Select(x => new Select2Data
            {
                text = x.serial_no_ADP,
                id = x.serial_no_ADP + ""
            });
            return years;
        }
        [HttpGet]
        public IEnumerable<Select2Data> GetSectors()
        {
            var sectors = db.tbl_Sectors.Select(x => new Select2Data
            {
                text = x.SectorName+ "",
                id = x.auto_Sector_id + ""
            });
            return sectors;
        }
        [HttpGet]
        public IEnumerable<Select2Data> GetSubSectors(int? sectorId)
        {
            var subsectors = db.tbl_SubSectors.AsQueryable();
            if (sectorId != null)
            {
                subsectors = subsectors.Where(x => x.auto_Sector_id == sectorId);
            }
            var sectors = subsectors.OrderBy(x => x.SubSectorName).Select(x => new Select2Data
            {
                text = x.SubSectorName,
                id = x.auto_SubSector_ID + ""
            });
            return sectors;
        }
        [HttpGet]
        public IEnumerable<Select2Data> GetDepartments()
        {
            var sectors = db.tbl_Department.Select(x => new Select2Data
            {
                id = x.auto_Dept_Id + "",
                text = x.DeptName + ""
            });
            return sectors;
        }
        [HttpGet]
        public Pc1Detail GetAdpDetail(int year, string serial, int? sector, int? subsector)
        {
            var model = new Pc1Detail();
            try
            {
                var obj = new tbl_ADP_Serial();
                var result = db.Rest_Get_Adp_Detail(serial, year, sector, subsector).ToList();
                if (result.Count > 0)
                {
                    var r = result.FirstOrDefault();
                    model.Serial = r.auto_adp_serial_id + "";
                    model.Name = r.ProjectName;
                    model.Cost = r.Allocation + "";
                    model.SectorId = r.auto_Sector_id + "";
                    model.SubsectorId = r.auto_SubSector_ID + "";
                    model.DistrictId = r.Distt_ID + "";
                    model.DivisionId = r.Div_ID + "";
                    model.DepartmentId = r.auto_Dept_Id + "";
                    model.yearly_total_allocation = r.yearly_total_allocation + "";
                    model.DistIds = result.Select(x => x.Districts).ToList();
                }


                //if (!string.IsNullOrEmpty(sector))
                //{
                //    if (!string.IsNullOrEmpty(subsector))
                //    {
                //        var subsectorId = Convert.ToInt32(subsector);
                //        var sectorId = Convert.ToInt32(sector);
                //        obj = db.tbl_ADP_Serial.FirstOrDefault(x => x.serial_no_ADP == serial && x.auto_ADP_id == year && x.auto_Sector_id == sectorId && x.auto_SubSector_ID == subsectorId);
                //    }
                //    else
                //    {
                //        var sectorId = Convert.ToInt32(sector);
                //        obj = db.tbl_ADP_Serial.FirstOrDefault(x => x.serial_no_ADP == serial && x.auto_ADP_id == year && x.auto_Sector_id == sectorId);
                //    }
                //}
                //else
                //{
                //    obj = db.tbl_ADP_Serial.FirstOrDefault(x => x.serial_no_ADP == serial && x.auto_ADP_id == year);
                //}

                //if (obj != null)
                //{
                    
                //    var distIds = db.tbl_AdpBook_multipleDist.Where(x => x.auto_adp_serial_id == obj.auto_adp_serial_id).ToList();
                //    List<int?> termsList = new List<int?>();
                //    foreach (var item in distIds)
                //    {
                //        termsList.Add(item.Distt_ID);
                //    }
                //    model.DistIds = termsList;
                //}
                model.RequestStatus = "Ok";
            }
            catch (Exception)
            {
                model.RequestStatus = "Error";
            }

            return model;
        }

        [HttpGet]
        public List<AutocompleteData> GetAdpProjectsByYear(int year)
        {
            var model = new List<AutocompleteData>();
            try
            {
                var obj = db.tbl_ADP_Serial.Where(x => x.auto_ADP_id == year).ToList();
                if (obj.Any())
                {
                    model = obj.Select(item => new AutocompleteData
                    {
                        id = item.auto_adp_serial_id + "",
                        value = item.ProjectName
                    }).ToList();

                }
            }
            catch (Exception)
            {

            }
            return model;

        }
        [HttpGet]
        public string GetShortCodeById(int id)
        {
            string code = "";
            var model = new List<AutocompleteData>();
            try
            {
                code = db.tbl_ADP_Serial.FirstOrDefault(x => x.auto_adp_serial_id == id).SchemeCode;
            }
            catch (Exception ex)
            {

            }
            return code;

        }
        [HttpGet]
        public AdpDetailByProject GetAdpDetailByProjectId(int project)
        {
            var model = new AdpDetailByProject();
            try
            {
                var result = db.Rest_Get_Adp_Detail_ByProject(project).ToList();
                if (result.Count > 0)
                {
                    var r = result.FirstOrDefault();
                    model.Serial = r.serial_no_ADP + "";
                    model.Name = r.ProjectName;
                    model.Cost = r.Allocation + "";
                    model.SectorId = r.auto_Sector_id + "";
                    model.SubsectorId = r.auto_SubSector_ID + "";
                    model.DistrictId = r.Distt_ID + "";
                    model.DivisionId = r.Div_ID + "";
                    model.DepartmentId = r.auto_Dept_Id + "";
                    model.DistIds = result.Select(x => x.Districts).ToList();
                    model.SectorName = r.SectorName;
                    model.SubSectorName = r.SubSectorName;
                    model.DistName = r.Dist_Name;
                    model.DivisionName = r.Div_Name;
                    model.DepName = r.DeptName;
                    model.SchemeCode = r.SchemeCode;
                    //model.yearly_total_allocation = r.yearly_total_allocation + "";
                }
               
                model.RequestStatus = "Ok";
            }
            catch (Exception ex)
            {
                model.RequestStatus = "Error";
            }

            return model;
        }
        [HttpGet]
        public Pc1Detail GetAdpDDL(int year, string serial, string sector, string subsector)
        {
            var model = new Pc1Detail();
            try
            {
                //var obj = new tbl_ADP_Serial();
                //if (!string.IsNullOrEmpty(sector))
                //{
                //    if (!string.IsNullOrEmpty(subsector))
                //    {
                //        var subsectorId = Convert.ToInt32(subsector);
                //        var sectorId = Convert.ToInt32(sector);
                //        obj = db.tbl_ADP_Serial.FirstOrDefault(x => x.serial_no_ADP == serial && x.auto_ADP_id == year && x.auto_Sector_id == sectorId && x.auto_SubSector_ID == subsectorId);
                //    }
                //    else
                //    {
                //        var sectorId = Convert.ToInt32(sector);
                //        obj = db.tbl_ADP_Serial.FirstOrDefault(x => x.serial_no_ADP == serial && x.auto_ADP_id == year && x.auto_Sector_id == sectorId);
                //    }

                //}
                //else
                //{
                //    obj = db.tbl_ADP_Serial.FirstOrDefault(x => x.serial_no_ADP == serial && x.auto_ADP_id == year);
                //}

                //if (obj != null)
                //{
                //    model.Serial = obj.auto_adp_serial_id + "";
                //    model.Name = obj.ProjectName;
                //    model.Cost = obj.Allocation + "";
                //    model.SectorId = obj.auto_Sector_id + "";
                //    model.SubsectorId = obj.auto_SubSector_ID + "";
                //    model.DistrictId = obj.Distt_ID + "";
                //    model.DivisionId = obj.Div_ID + "";
                //    model.DepartmentId = obj.auto_Dept_Id + "";
                //    var distIds = db.tbl_AdpBook_multipleDist.Where(x => x.auto_adp_serial_id == obj.auto_adp_serial_id).ToList();
                //    List<int?> termsList = new List<int?>();
                //    foreach (var item in distIds)
                //    {
                //        termsList.Add(item.Distt_ID);
                //    }
                //    model.DistIds = termsList;
                //}
                var obj = new tbl_ADP_Serial();
                if (!string.IsNullOrEmpty(sector))
                {
                    if (!string.IsNullOrEmpty(subsector))
                    {
                        var subsectorId = Convert.ToInt32(subsector);
                        var sectorId = Convert.ToInt32(sector);
                        obj = db.tbl_ADP_Serial.FirstOrDefault(x => x.serial_no_ADP == serial && x.auto_ADP_id == year && x.auto_Sector_id == sectorId && x.auto_SubSector_ID == subsectorId);
                    }
                    else
                    {
                        var sectorId = Convert.ToInt32(sector);
                        obj = db.tbl_ADP_Serial.FirstOrDefault(x => x.serial_no_ADP == serial && x.auto_ADP_id == year && x.auto_Sector_id == sectorId);
                    }

                }
                else
                {
                    obj = db.tbl_ADP_Serial.FirstOrDefault(x => x.serial_no_ADP == serial && x.auto_ADP_id == year);
                }

                if (obj != null)
                {
                    model.Serial = obj.auto_adp_serial_id + "";
                    model.Name = obj.ProjectName;
                    model.Cost = obj.Allocation + "";
                    model.SectorId = obj.auto_Sector_id + "";
                    model.SubsectorId = obj.auto_SubSector_ID + "";
                    model.DistrictId = obj.Distt_ID + "";
                    model.DivisionId = obj.Div_ID + "";
                    model.DepartmentId = obj.auto_Dept_Id + "";
                    var distIds = db.tbl_AdpBook_multipleDist.Where(x => x.auto_adp_serial_id == obj.auto_adp_serial_id).ToList();
                    List<int?> termsList = new List<int?>();
                    foreach (var item in distIds)
                    {
                        termsList.Add(item.Distt_ID);
                    }
                    model.DistIds = termsList;
                }
                model.RequestStatus = "Ok";
            }
            catch (Exception)
            {
                model.RequestStatus = "Error";
            }

            return model;
        }
        [HttpGet]
        public IEnumerable<Select2Data> GetSponcAgency()
        {
            var sectors = db.tbl_Sponsoring_Agency.Select(x => new Select2Data
            {
                id = x.auto_SponsorID + "",
                text = x.SponsoringAgencyName + ""
            });
            return sectors;
        }
        [HttpGet]
        public IEnumerable<Select2Data> GetExecAgency()
        {
            var sectors = db.tbl_Executing_Agency.Select(x => new Select2Data
            {
                id = x.Executing_Agency_ID + "",
                text = x.Executing_Agency_Name + ""
            });
            return sectors;
        }
        [HttpGet]
        public string GetPc1CountProvince()
        {
            try
            {
                var year_id = db.tbl_adp.OrderByDescending(x => x.auto_ADP_id).FirstOrDefault().auto_ADP_id;
                var count = db.tbl_ADP_Serial.Where(x => x.auto_ADP_id == year_id).Count();
                return count.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        [HttpGet]
        public string GetPc1CountDivision(string div_code)
        {
            try
            {
                
                var id = db.tbl_Division.FirstOrDefault(x => x.Div_Code == div_code).Div_ID;
                var year_id = db.tbl_adp.OrderByDescending(x => x.auto_ADP_id).FirstOrDefault().auto_ADP_id;
                var count = db.tbl_ADP_Serial.Where(x => x.Div_ID == id && x.auto_ADP_id == year_id).Count();
                return count.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [HttpGet]
        public string GetPc1CountDistrict(string dist_code)
        {
            try
            {
               
                var id = db.tbl_Distt.FirstOrDefault(x => x.Dist_Code == dist_code).Distt_ID;
                var year_id = db.tbl_adp.OrderByDescending(x => x.auto_ADP_id).FirstOrDefault().auto_ADP_id;
                var count = db.tbl_ADP_Serial.Where(x => x.Distt_ID == id && x.auto_ADP_id == year_id).Count();
                return count.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        //[HttpGet]
        //public List<AdpPC1> GetPc1Province()
        //{
        //    var length = Numerics.GetInt(Request["length"] + "");
        //    var draw = Request["draw"] + "";
        //    var start = Numerics.GetInt(Request["start"] + "");
        //    var orderIndex = Numerics.GetInt(Request["order[0][column]"] + "");
        //    var orderWise = Request["order[0][dir]"] + "";
        //    var search = Request["search[value]"] + "";
        //    var rows = 0;
        //    var dataTable = new DataTableModel();

        //    //var result = new List<AdpPC1>();
        //    //try
        //    //{
        //    //    var year_id = db.tbl_adp.OrderByDescending(x => x.auto_ADP_id).FirstOrDefault().auto_ADP_id;
        //    //    result = db.tbl_ADP_Serial.Where(x => x.auto_ADP_id == year_id && x.ProjectName != null && x.Allocation != null && x.serial_no_ADP != null).Select(x => new AdpPC1
        //    //    {
        //    //        Serial = x.serial_no_ADP,
        //    //        Name = x.ProjectName,
        //    //        Cost = x.Allocation + ""
        //    //    }).ToList();

        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //}
        //    return result;

        //}
        [HttpGet]
        public List<AdpPC1> GetPc1Division(string div_code)
        {
            var result = new List<AdpPC1>();
            try
            {
                var id = db.tbl_Division.FirstOrDefault(x => x.Div_Code == div_code).Div_ID;
                var year_id = db.tbl_adp.OrderByDescending(x => x.auto_ADP_id).FirstOrDefault().auto_ADP_id;
                result = db.tbl_ADP_Serial.Where(x => x.Div_ID == id && x.auto_ADP_id == year_id).Select(x => new AdpPC1
                {
                    Serial = x.serial_no_ADP,
                    Name = x.ProjectName,
                    Cost = x.Allocation + ""
                }).ToList();

            }
            catch (Exception ex)
            {

            }
            return result;

        }
        [HttpGet]
        public List<AdpPC1> GetPc1District(string dist_code)
        {
            var result = new List<AdpPC1>();
            try
            {
                var id = db.tbl_Distt.FirstOrDefault(x => x.Dist_Code == dist_code).Distt_ID;
                var year_id = db.tbl_adp.OrderByDescending(x => x.auto_ADP_id).FirstOrDefault().auto_ADP_id;
                result = db.tbl_ADP_Serial.Where(x => x.Distt_ID == id && x.auto_ADP_id == year_id).Select(x => new AdpPC1
                {
                    Serial = x.serial_no_ADP,
                    Name = x.ProjectName,
                    Cost = x.Allocation + ""
                }).ToList();

            }
            catch (Exception ex)
            {

            }
            return result;

        }


        [HttpGet]
        public IHttpActionResult GetPc1Province()
        {

            var request = HttpContext.Current.Request;
            var length = Numerics.GetInt(request["length"] + "");
            var draw = request["draw"] + "";
            var start = Numerics.GetInt(request["start"] + "");
            var orderIndex = Numerics.GetInt(request["order[0][column]"] + "");
            var orderWise = request["order[0][dir]"] + "";
            var search = request["search[value]"] + "";
            var rows = 0;
            var result = new List<AdpPC1>();

            var year_id = db.tbl_adp.OrderByDescending(x => x.auto_ADP_id).FirstOrDefault().auto_ADP_id;
            result = db.tbl_ADP_Serial.Where(x => x.auto_ADP_id == year_id && x.ProjectName != null && x.Allocation != null && x.serial_no_ADP != null).Select(x => new AdpPC1
            {
                Serial = x.serial_no_ADP,
                Name = x.ProjectName,
                Cost = x.Allocation + "",
                Year = x.auto_ADP_id + ""
            }).ToList();
            var tempList = result.Skip(start).Take(length).ToList();
            return Json(new
            {
                draw = draw,
                recordsFiltered = result.Count(),
                recordsTotal = result.Count(),
                data = tempList
            });
        }
        [HttpGet]
        public string GetPc1TotalCount()
        {
            try
            {
                var years = db.Get_All_ADP_Years().FirstOrDefault().ADP_name.Split('-')[0] + "-" + db.Get_All_ADP_Years().OrderByDescending(x=>x.auto_ADP_id).FirstOrDefault().ADP_name.Split('-')[1];
                var res = years + "," + db.tbl_ADP_Serial.ToList().Count();
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        [HttpGet]
        public List<AdpPC1New> GetAllProjects()
        {
            //var json = "";
            //try
            //{
            //    var list = db.GetAllAdps().ToList();
            //    json = JsonConvert.SerializeObject(list);
            //}
            //catch (Exception ex)
            //{


            //}
            var list = db.GetAllAdps().Select(x => new AdpPC1New
            {
                ProjectName = x.ProjectName,
                Allocation = x.Allocation,
                YearlyTotalAllocation = x.YearlyTotalAllocation,
                Approved = x.Approved,
                Sector = x.Sector,
                SubSector = x.SubSector,
                DepartmentName = x.DepartmentName,
                Division = x.Division,
                District = x.District,
                Tehsil = x.Tehsil,
                LocalCapital = x.LocalCapital,
                LocalRevenue = x.LocalRevenue,
                TotalRevenue = x.TotalRevenue,
                EstimatedCost = x.EstimatedCost,
                ExpenditureCost = x.ExpenditureCost,
                AdpSerial = x.AdpSerial,
                FinancialYear = x.FinancialYear

            }).ToList();
            return list;
        }
        [HttpGet]
        public IEnumerable<Select2Data> GetDistricts()
        {
            var dist = db.tbl_Distt.OrderBy(x => x.Dist_Name).Select(x => new Select2Data
            {
                text = x.Dist_Name,
                id = x.Div_ID + ""
            });
            return dist;
        }
        [HttpGet]
        public IEnumerable<Select2Data> GetTehsils()
        {
            var tehsils = db.tbl_tehsil.OrderBy(x => x.Tehsil_Name).Select(x => new Select2Data
            {
                text = x.Tehsil_Name,
                id = x.Tehsil_ID + ""
            });
            return tehsils;
        }
        [HttpGet]
        public List<string> GetAllAdpNumbers()
        {
            List<string> list = null;
            try
            {
                list = db.tbl_ADP_Serial.Select(x => x.serial_no_ADP).ToList();
            }
            catch (Exception ex)
            {
                
            }
            return list;
        }
        [HttpGet]
        public AdpDetailByProject GetAdpDetail(int id,int year)
        {
            var model = new AdpDetailByProject();
            try
            {
                var result = db.Rest_Get_Adp_Detail_Year_Wise(id,year).ToList();
                if (result.Count > 0)
                {
                    var r = result.FirstOrDefault();
                    model.Serial = r.serial_no_ADP + "";
                    model.Name = r.ProjectName;
                    model.Cost = r.Allocation + "";
                    model.SectorId = r.auto_Sector_id + "";
                    model.SubsectorId = r.auto_SubSector_ID + "";
                    model.DistrictId = r.Distt_ID + "";
                    model.DivisionId = r.Div_ID + "";
                    model.DepartmentId = r.auto_Dept_Id + "";
                    model.DistIds = result.Select(x => x.Districts).ToList();
                    model.SectorName = r.SectorName;
                    model.SubSectorName = r.SubSectorName;
                    model.DistName = r.Dist_Name;
                    model.DivisionName = r.Div_Name;
                    model.DepName = r.DeptName;
                    model.SchemeCode = r.SchemeCode;
                    model.Year = r.Year + "";
                    //model.yearly_total_allocation = r.yearly_total_allocation + "";
                }

                model.RequestStatus = "Ok";
            }
            catch (Exception ex)
            {
                model.RequestStatus = "Error";
            }

            return model;
        }

    }
}
