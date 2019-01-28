using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CM_Monitoring_Commette_DataLayer;
using System.Data.Entity;
using Newtonsoft.Json;
using WebServices.Models.CM_Monitoring_Committe;
using WebServices.Utills;

namespace WebServices.Controllers
{
    public class CM_Monitoring_CommetteController : Controller
    {
        // GET: CM_Monitoring_Commette
        //[HttpGet]
        //public async Task SaveProgress()
        //{
        //    var db = new CM_Monitorring_CommitteEntities();
        //    var raw = await db.tbl_progress_raw.Where(x => x.Added == null || x.Added == false).ToListAsync();
        //    foreach (var item in raw)
        //    {
        //        try
        //        {
        //            var m = JsonConvert.DeserializeObject<ProgressJson>(item.data_json);
        //            var schemeId = Numerics.GetInt(m.scheme_id);
        //            //var scheme = db.Pm_Tbl_Main_Scheme.Find(schemeId);
        //            if (m.uc_id != null && m.uc_id != 0)
        //            {
        //                var uc = await db.tbl_UC_MC_Detail.FindAsync(m.uc_id);
        //                if (scheme != null && uc != null)
        //                {
        //                    int? divId = null;
        //                    int? distId = null;
        //                    divId = scheme.Div_ID;
        //                    distId = scheme.Distt_ID;
        //                    if (m.status_id == 5)
        //                    {
        //                        if (item.qc_status == true)
        //                        {
        //                            var progress = new tbl_Progress_Scheme_GIS
        //                            {
        //                                auto_Pm_Scheme_id = schemeId,
        //                                geom = item.geom,
        //                                file_path = item.img1,
        //                                appversion = item.app_version,
        //                                bit_for_dd = false,
        //                                db_datetime = item.db_date_time,
        //                                DesignationId = m.DesignationId,
        //                                imei = item.imei,
        //                                local_id = item.local_id,
        //                                mobile_datetime = Convert.ToDateTime(m.date_time_mobile),
        //                                picture_four = item.img2,
        //                                picture_three = item.img3,
        //                                picture_two = item.img4,
        //                                progress = 1,
        //                                remarks = m.remarks,
        //                                Role = !string.IsNullOrEmpty(m.user_role) ? m.user_role : m.Role,
        //                                status_id = m.status_id,
        //                                Uc_Id = m.uc_id,
        //                                User_name = "User",
        //                                unique_code = m.unique_code,
        //                                progress_raw_id = item.id,
        //                                Div_ID = divId,
        //                                Distt_ID = distId,
        //                                col_a = !string.IsNullOrEmpty(m.col_a) ? m.col_a : "NA",
        //                                col_b = !string.IsNullOrEmpty(m.col_b) ? m.col_b : "NA",
        //                                col_c = !string.IsNullOrEmpty(m.col_c) ? m.col_c : "NA",
        //                                col_d = !string.IsNullOrEmpty(m.col_d) ? m.col_d : "NA",
        //                                col_e = !string.IsNullOrEmpty(m.col_e) ? m.col_e : "NA",
        //                                designation_double_check = !string.IsNullOrEmpty(m.designation_double_check) ? m.designation_double_check : "NA",
        //                                data_captured_from_app_version = !string.IsNullOrEmpty(m.data_captured_from_app_version) ? m.data_captured_from_app_version : "NA",
        //                                need_designation_correction = !string.IsNullOrEmpty(m.need_designation_correction) ? m.need_designation_correction : "NA"
        //                            };
        //                            db.tbl_Progress_Scheme_GIS.Add(progress);
        //                            item.Added = true;
        //                            await db.SaveChangesAsync();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var progress = new tbl_Progress_Scheme_GIS
        //                        {
        //                            auto_Pm_Scheme_id = schemeId,
        //                            geom = item.geom,
        //                            file_path = item.img1,
        //                            appversion = item.app_version,
        //                            bit_for_dd = false,
        //                            db_datetime = item.db_date_time,
        //                            DesignationId = m.DesignationId,
        //                            imei = item.imei,
        //                            local_id = item.local_id,
        //                            mobile_datetime = Convert.ToDateTime(m.date_time_mobile),
        //                            picture_four = item.img2,
        //                            picture_three = item.img3,
        //                            picture_two = item.img4,
        //                            progress = 1,
        //                            remarks = m.remarks,
        //                            Role = !string.IsNullOrEmpty(m.user_role) ? m.user_role : m.Role,
        //                            status_id = m.status_id,
        //                            Uc_Id = m.uc_id,
        //                            User_name = "User",
        //                            unique_code = m.unique_code,
        //                            progress_raw_id = item.id,
        //                            Div_ID = divId,
        //                            Distt_ID = distId,
        //                            col_a = !string.IsNullOrEmpty(m.col_a) ? m.col_a : "NA",
        //                            col_b = !string.IsNullOrEmpty(m.col_b) ? m.col_b : "NA",
        //                            col_c = !string.IsNullOrEmpty(m.col_c) ? m.col_c : "NA",
        //                            col_d = !string.IsNullOrEmpty(m.col_d) ? m.col_d : "NA",
        //                            col_e = !string.IsNullOrEmpty(m.col_e) ? m.col_e : "NA",
        //                            designation_double_check = !string.IsNullOrEmpty(m.designation_double_check) ? m.designation_double_check : "NA",
        //                            data_captured_from_app_version = !string.IsNullOrEmpty(m.data_captured_from_app_version) ? m.data_captured_from_app_version : "NA",
        //                            need_designation_correction = !string.IsNullOrEmpty(m.need_designation_correction) ? m.need_designation_correction : "NA"
        //                        };
        //                        db.tbl_Progress_Scheme_GIS.Add(progress);
        //                        item.Added = true;
        //                        await db.SaveChangesAsync();
        //                    }

        //                }
        //            }
        //        }
        //        catch (DbEntityValidationException dbEx)
        //        {
        //            foreach (var validationErrors in dbEx.EntityValidationErrors)
        //            {
        //                foreach (var validationError in validationErrors.ValidationErrors)
        //                {
        //                    Trace.TraceInformation("Property: {0} Error: {1}",
        //                                            validationError.PropertyName,
        //                                            validationError.ErrorMessage);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //var filePath = HostingEnvironment.MapPath("~/Exception/Exception.txt");
        //            //var exist = System.IO.Directory.Exists(filePath);
        //            //if (!exist)
        //            //{
        //            //    System.IO.Directory.CreateDirectory(filePath);
        //            //}
        //            //WriteException.Exception(ex, "Add Progress Api Method");
        //        }
        //    }
        //}

        //[HttpGet]
        //public async Task SaveProgressMonitoring()
        //{
        //    var db = new UC_SchemesEntities();
        //    var raw = await db.tbl_progress_raw_monitoring.Where(x => x.Added == null || x.Added == false).ToListAsync();
        //    foreach (var item in raw)
        //    {
        //        try
        //        {
        //            var m = JsonConvert.DeserializeObject<ProgressJsonMonitoring>(item.data_json);
        //            var schemeId = Numerics.GetInt(m.scheme_id);
        //            var scheme = db.Pm_Tbl_Main_Scheme.Find(schemeId);

        //            if (scheme != null)
        //            {
        //                int? divId = null;
        //                int? distId = null;

        //                divId = scheme.Div_ID;
        //                distId = scheme.Distt_ID;

        //                var progress = new tbl_Progress_Scheme_GIS
        //                {
        //                    auto_Pm_Scheme_id = schemeId,
        //                    geom = item.geom,
        //                    file_path = item.img1,
        //                    appversion = item.app_version,
        //                    bit_for_dd = false,
        //                    db_datetime = item.db_date_time,
        //                    DesignationId = m.DesignationId,
        //                    imei = item.imei,
        //                    local_id = item.local_id,
        //                    mobile_datetime = Convert.ToDateTime(m.date_time_mobile),
        //                    picture_four = item.img2,
        //                    picture_three = item.img3,
        //                    picture_two = item.img4,
        //                    progress = 1,
        //                    remarks = m.remarks,
        //                    Role = m.user_role,
        //                    status_id = m.status_id,
        //                    Uc_Id = m.uc_id,
        //                    User_name = "User",
        //                    unique_code = m.unique_code,
        //                    Consultant_Issue = m.consultant_issue,
        //                    Contractor_Issue = m.contractors_issue,
        //                    Drawing_Issue = m.drawing_issue,
        //                    Land_Issue = m.land_issue,
        //                    Other_Issue = m.other_issue,
        //                    Payment_Issue = m.payment_issue,
        //                    Quality_issue = m.quality_issue,
        //                    Time_delay_Issue = m.time_delay_issue,
        //                    progress_raw_monitoring_id = item.id,
        //                    Div_ID = divId,
        //                    Distt_ID = distId
        //                };
        //                db.tbl_Progress_Scheme_GIS.Add(progress);
        //                item.Added = true;
        //                await db.SaveChangesAsync();
        //            }

        //        }
        //        catch (DbEntityValidationException dbEx)
        //        {
        //            foreach (var validationErrors in dbEx.EntityValidationErrors)
        //            {
        //                foreach (var validationError in validationErrors.ValidationErrors)
        //                {
        //                    Trace.TraceInformation("Property: {0} Error: {1}",
        //                                            validationError.PropertyName,
        //                                            validationError.ErrorMessage);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //var filePath = HostingEnvironment.MapPath("~/Exception/Exception.txt");
        //            //var exist = System.IO.Directory.Exists(filePath);
        //            //if (!exist)
        //            //{
        //            //    System.IO.Directory.CreateDirectory(filePath);
        //            //}
        //            //WriteException.Exception(ex, "Add Progress Api Method");
        //        }
        //    }
        //}
    }
}