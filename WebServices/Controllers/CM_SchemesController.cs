using DataLayer;
using DataLayer.CM_Schemes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using WebServices.Models.CM_Schemes;

namespace WebServices.Controllers
{
    public class CM_SchemesController : ApiController
    {
        [HttpGet]
        public List<SchemesRecordModel> Get(int? id)
        {
            var dal = new DAL_CM();
            var ds = dal.Get_Schemes_Record(id);
            DataTable dt = ds.Tables[0];

            var model = new List<SchemesRecordModel>();
            model = DataTableToList.ConvertDataTable<SchemesRecordModel>(dt);

            return model;
        }

        [HttpGet]
        public List<SchemesRecordModel> Get()
        {
            var dal = new DAL_CM();
            var ds = dal.Get_Schemes_Record(null);
            DataTable dt = ds.Tables[0];
            var model = new List<SchemesRecordModel>();
            model = DataTableToList.ConvertDataTable<SchemesRecordModel>(dt);
            return model;
        }

        [HttpGet]
        public List<DistrictAllocationModel> GetDistrictAllocation()
        {
            var dal = new DAL_CM();
            var ds = dal.Get_District_Allocation();
            DataTable dt = ds.Tables[0];
            var model = new List<DistrictAllocationModel>();
            model = DataTableToList.ConvertDataTable<DistrictAllocationModel>(dt);
            return model;
        }




    }
}
