
using CM_Monitoring_Commette_DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebServices.Models.CM_Monitoring_Committe;

namespace WebServices.Controllers
{
    public class CM_Monitoring_CommetteController : ApiController
    {
        [HttpGet]
        public async Task SaveProgress()
        {
            var db = new CM_Monitorring_CommitteEntities();
            var raw = await db.tbl_progress_Commette_raw.Where(x => x.Added == null || x.Added == false).ToListAsync();
            foreach (var item in raw)
            {
                try
                {
                    var m = JsonConvert.DeserializeObject<ProgressJson>(item.data_json);
                    var ucStatus = (m.uc_available_status == "NA")? "0" : m.uc_available_status ;
                    var progress = new tbl_Progress_Commette_Gis
                    {
                        tehsilId = m.tehsilId,
                        auto_pk = m.auto_pk,
                        quality_of_work = m.quality_of_work,
                        DivisonId = m.DivisonId,
                        lng = m.lng,
                        remarks = m.remarks,
                        ucId = m.ucId,
                        serial_no_ADP = m.serial_no_ADP,
                        col_a = !string.IsNullOrEmpty(m.col_a) ? m.col_a : "NA",
                        col_b = !string.IsNullOrEmpty(m.col_b) ? m.col_b : "NA",
                        col_c = !string.IsNullOrEmpty(m.col_c) ? m.col_c : "NA",
                        col_d = !string.IsNullOrEmpty(m.col_d) ? m.col_d : "NA",
                        col_e = !string.IsNullOrEmpty(m.col_e) ? m.col_e : "NA",
                        username = m.username,
                        districtId = m.districtId,
                        CommitteId = m.CommitteId,
                        userId = m.userId,
                        lat = m.lat,
                        ProjectName = m.ProjectName,
                        imei = m.imei,
                        unique_code = m.unique_code,
                        uc_name = m.uc_name,
                        date_time_mobile = Convert.ToDateTime(m.date_time_mobile),
                        district_name = m.district_name,
                        uc_available_status = (Convert.ToInt16(ucStatus) == 1)? 1 : 0,
                        tehsil_name = m.tehsil_name,
                        data_captured_from_app_version = !string.IsNullOrEmpty(m.data_captured_from_app_version) ? m.data_captured_from_app_version : "NA",
                        tbl_progress_raw_id = item.id,
                        db_date_time = Convert.ToDateTime(item.db_date_time),
                        local_id = item.local_id,
                        img1 = item.img1,
                        img2 = item.img2,
                        img3 = item.img3,
                        img4 = item.img4,
                        geom = item.geom,
                        user_role = item.user_role,
                    };
                    db.tbl_Progress_Commette_Gis.Add(progress);
                    item.Added = true;
                    await db.SaveChangesAsync();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //var filePath = HostingEnvironment.MapPath("~/Exception/Exception.txt");
                    //var exist = System.IO.Directory.Exists(filePath);
                    //if (!exist)
                    //{
                    //    System.IO.Directory.CreateDirectory(filePath);
                    //}
                    //WriteException.Exception(ex, "Add Progress Api Method");
                }
            }
        }

    }
}
