using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using UC_SchemesDataLayer;
using WebServices.Models.UC_Schemes;
using WebServices.Utills;

namespace WebServices.Controllers
{
    public class UC_SchemesController : ApiController
    {
        [HttpGet]
        public async Task SaveProgress()
        {
            var db = new UC_SchemesEntities();
            var raw = await db.tbl_progress_raw.Where(x => x.Added == null || x.Added == false).ToListAsync();
            foreach (var item in raw)
            {
                try
                {
                    var m = JsonConvert.DeserializeObject<ProgressJson>(item.data_json);
                    var schemeId = Numerics.GetInt(m.scheme_id);
                    var scheme = db.Pm_Tbl_Main_Scheme.Find(schemeId);
                    if (m.uc_id != null && m.uc_id != 0)
                    {
                        var uc = await db.tbl_UC_MC_Detail.FindAsync(m.uc_id);
                        if (scheme != null && uc != null)
                        {
                            int? divId = null;
                            int? distId = null;
                            divId = scheme.Div_ID;
                            distId = scheme.Distt_ID;
                            if (m.status_id == 5)
                            {
                                if (item.qc_status == true)
                                {
                                    var progress = new tbl_Progress_Scheme_GIS
                                    {
                                        auto_Pm_Scheme_id = schemeId,
                                        geom = item.geom,
                                        file_path = item.img1,
                                        appversion = item.app_version,
                                        bit_for_dd = false,
                                        db_datetime = item.db_date_time,
                                        DesignationId = m.DesignationId,
                                        imei = item.imei,
                                        local_id = item.local_id,
                                        mobile_datetime = Convert.ToDateTime(m.date_time_mobile),
                                        picture_four = item.img2,
                                        picture_three = item.img3,
                                        picture_two = item.img4,
                                        progress = 1,
                                        remarks = m.remarks,
                                        Role = !string.IsNullOrEmpty(m.user_role) ? m.user_role : m.Role,
                                        status_id = m.status_id,
                                        Uc_Id = m.uc_id,
                                        User_name = "User",
                                        unique_code = m.unique_code,
                                        progress_raw_id = item.id,
                                        Div_ID = divId,
                                        Distt_ID = distId,
                                        col_a = !string.IsNullOrEmpty(m.col_a) ? m.col_a : "NA",
                                        col_b = !string.IsNullOrEmpty(m.col_b) ? m.col_b : "NA",
                                        col_c = !string.IsNullOrEmpty(m.col_c) ? m.col_c : "NA",
                                        col_d = !string.IsNullOrEmpty(m.col_d) ? m.col_d : "NA",
                                        col_e = !string.IsNullOrEmpty(m.col_e) ? m.col_e : "NA",
                                        designation_double_check = !string.IsNullOrEmpty(m.designation_double_check) ? m.designation_double_check : "NA",
                                        data_captured_from_app_version = !string.IsNullOrEmpty(m.data_captured_from_app_version) ? m.data_captured_from_app_version : "NA",
                                        need_designation_correction = !string.IsNullOrEmpty(m.need_designation_correction) ? m.need_designation_correction : "NA"
                                    };
                                    db.tbl_Progress_Scheme_GIS.Add(progress);
                                    item.Added = true;
                                    item.auto_Pm_Scheme_id = schemeId;
                                    await db.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                var progress = new tbl_Progress_Scheme_GIS
                                {
                                    auto_Pm_Scheme_id = schemeId,
                                    geom = item.geom,
                                    file_path = item.img1,
                                    appversion = item.app_version,
                                    bit_for_dd = false,
                                    db_datetime = item.db_date_time,
                                    DesignationId = m.DesignationId,
                                    imei = item.imei,
                                    local_id = item.local_id,
                                    mobile_datetime = Convert.ToDateTime(m.date_time_mobile),
                                    picture_four = item.img2,
                                    picture_three = item.img3,
                                    picture_two = item.img4,
                                    progress = 1,
                                    remarks = m.remarks,
                                    Role = !string.IsNullOrEmpty(m.user_role) ? m.user_role : m.Role,
                                    status_id = m.status_id,
                                    Uc_Id = m.uc_id,
                                    User_name = "User",
                                    unique_code = m.unique_code,
                                    progress_raw_id = item.id,
                                    Div_ID = divId,
                                    Distt_ID = distId,
                                    col_a = !string.IsNullOrEmpty(m.col_a) ? m.col_a : "NA",
                                    col_b = !string.IsNullOrEmpty(m.col_b) ? m.col_b : "NA",
                                    col_c = !string.IsNullOrEmpty(m.col_c) ? m.col_c : "NA",
                                    col_d = !string.IsNullOrEmpty(m.col_d) ? m.col_d : "NA",
                                    col_e = !string.IsNullOrEmpty(m.col_e) ? m.col_e : "NA",
                                    designation_double_check = !string.IsNullOrEmpty(m.designation_double_check) ? m.designation_double_check : "NA",
                                    data_captured_from_app_version = !string.IsNullOrEmpty(m.data_captured_from_app_version) ? m.data_captured_from_app_version : "NA",
                                    need_designation_correction = !string.IsNullOrEmpty(m.need_designation_correction) ? m.need_designation_correction : "NA"
                                };
                                db.tbl_Progress_Scheme_GIS.Add(progress);
                                item.Added = true;
                                item.auto_Pm_Scheme_id = schemeId;
                                await db.SaveChangesAsync();
                            }

                        }
                    }
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

        [HttpGet]
        public async Task SaveProgressMonitoring()
        {
            var db = new UC_SchemesEntities();
            var raw = await db.tbl_progress_raw_monitoring.Where(x => x.Added == null || x.Added == false).ToListAsync();
            foreach (var item in raw)
            {
                try
                {
                    var m = JsonConvert.DeserializeObject<ProgressJsonMonitoring>(item.data_json);
                    var schemeId = Numerics.GetInt(m.scheme_id);
                    var scheme = db.Pm_Tbl_Main_Scheme.Find(schemeId);

                    if (scheme != null)
                    {
                        int? divId = null;
                        int? distId = null;

                        divId = scheme.Div_ID;
                        distId = scheme.Distt_ID;

                        var progress = new tbl_Progress_Scheme_GIS
                        {
                            auto_Pm_Scheme_id = schemeId,
                            geom = item.geom,
                            file_path = item.img1,
                            appversion = item.app_version,
                            bit_for_dd = false,
                            db_datetime = item.db_date_time,
                            DesignationId = m.DesignationId,
                            imei = item.imei,
                            local_id = item.local_id,
                            mobile_datetime = Convert.ToDateTime(m.date_time_mobile),
                            picture_four = item.img2,
                            picture_three = item.img3,
                            picture_two = item.img4,
                            progress = 1,
                            remarks = m.remarks,
                            Role = m.user_role,
                            status_id = m.status_id,
                            Uc_Id = m.uc_id,
                            User_name = "User",
                            unique_code = m.unique_code,
                            Consultant_Issue = m.consultant_issue,
                            Contractor_Issue = m.contractors_issue,
                            Drawing_Issue = m.drawing_issue,
                            Land_Issue = m.land_issue,
                            Other_Issue = m.other_issue,
                            Payment_Issue = m.payment_issue,
                            Quality_issue = m.quality_issue,
                            Time_delay_Issue = m.time_delay_issue,
                            progress_raw_monitoring_id = item.id,
                            Div_ID = divId,
                            Distt_ID = distId
                        };
                        db.tbl_Progress_Scheme_GIS.Add(progress);
                        item.Added = true;
                        await db.SaveChangesAsync();
                    }

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
        [HttpGet]
        public async Task<bool> SendMessage()
        {
            try
            {
                var db = new UC_SchemesEntities();
                var message = "For Rural Development Program Mobile App \n";
                message += "Kindly note: \n";
                message += "Location of (before, progress and completed) pictures must be taken from same point in order to be calculated otherwise pictures will be rejected automatically. Your progress will not be considered and will be not be reflected on the Dashboard. \n";

                var number = "923058500011";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("http://192.168.1.22/gwasa/send.php?number=" + number + "&message=" + message + "").Result.Content.ReadAsStringAsync();

                var collection = db.tbl_SendSMS.ToList();
                foreach (var item in collection)
                {
                    number = item.Mobile;

                    //var length = message.Length;
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await client.GetAsync("http://192.168.1.22/gwasa/send.php?number=" + number + "&message=" + message + "").Result.Content.ReadAsStringAsync();
                    //response = await client.GetAsync("http://202.166.167.115/gwasa/send.php?number=" + number + "&message=" + message + "").Result.Content.ReadAsStringAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public async Task<bool> SendMessages(string message)
        {
            try
            {
                var db = new UC_SchemesEntities();
                var collection = db.tbl_SendSMS.ToList();
                foreach (var item in collection)
                {
                    var number = item.Mobile;
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("http://192.168.1.22/gwasa/send.php?number=" + number + "&message=" + message + "").Result.Content.ReadAsStringAsync();
                  //  var response = await client.GetAsync("http://202.166.167.115/gwasa/send.php?number=" + number + "&message=" + message + "").Result.Content.ReadAsStringAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public async Task SaveProgressTpv()
        {
            var db = new UC_SchemesEntities();
            var raw = await db.tbl_raw_tpv.Where(x => x.Added == null || x.Added == false).ToListAsync();
            foreach (var item in raw)
            {
                try
                {
                    var m = JsonConvert.DeserializeObject<ProgressJsonTpv>(item.data_json);
                    var schemeId = Numerics.GetInt(m.scheme_id);
                    var scheme = await db.Pm_Tbl_Main_Scheme.FindAsync(schemeId);
                    if (m.uc_id != null && m.uc_id != 0 && scheme != null)
                    {
                        var uc = await db.tbl_UC_MC_Detail.FindAsync(m.uc_id);
                        if (scheme != null && uc != null)
                        {
                            var progress = new tpv_progress
                            {
                                Added = true,
                                app_version = item.app_version,
                                col_a = m.col_a,
                                col_b = m.col_b,
                                col_c = m.col_c,
                                col_d = m.col_d,
                                col_e = m.col_e,
                                data_captured_from_app_version = m.data_captured_from_app_version,
                                data_sync_time = m.data_sync_time,
                                date_time_mobile = m.date_time_mobile,
                                db_date_time = DateTime.Now,
                                district_id = m.district_id,
                                district_name = m.district_name,
                                geom_bricks = item.geom_bricks,
                                geom_concrete = item.geom_concrete,
                                geom_mortor = item.geom_mortor,
                                geom_pcc = item.geom_pcc,
                                geom_plaster = item.geom_plaster,
                                geom_scheme_1 = item.geom_scheme_1,
                                geom_scheme_2 = item.geom_scheme_2,
                                geom_steel = item.geom_steel,
                                imei = item.imei,
                                imei_surveyed_mobile = m.imei_surveyed_mobile,
                                img_bricks = item.img_bricks,
                                img_concrete = item.img_concrete,
                                img_mortor = item.img_mortor,
                                img_pcc = item.img_pcc,
                                img_plaster = item.img_plaster,
                                img_scheme_1 = item.img_scheme_1,
                                img_scheme_2 = item.img_scheme_2,
                                img_steel = item.img_steel,
                                is_bricks_absorption_test = m.is_bricks_absorption_test,
                                is_bricks_crushing_strength_test = m.is_bricks_crushing_strength_test,
                                is_bricks_material = m.is_bricks_material,
                                is_bricks_measurements_test = m.is_bricks_measurements_test,
                                is_concrete_pavers_crushing_strength_test = m.is_concrete_pavers_crushing_strength_test,
                                is_concrete_pavers_material = m.is_concrete_pavers_material,
                                is_concrete_pavers_size_test = m.is_concrete_pavers_size_test,
                                is_mortar_material = m.is_mortar_material,
                                is_mortar_ratio_cement_sand_test = m.is_mortar_ratio_cement_sand_test,
                                is_pcc_crushing_strength_test = m.is_pcc_crushing_strength_test,
                                is_pcc_material = m.is_pcc_material,
                                is_pcc_ratio_concrete_test = m.is_pcc_ratio_concrete_test,
                                is_plaster_material = m.is_plaster_material,
                                is_plaster_thickness_test = m.is_plaster_thickness_test,
                                is_steel_material = m.is_steel_material,
                                is_steel_tensile_and_yield_strength_test = m.is_steel_tensile_and_yield_strength_test,
                                lat1 = m.lat1,
                                lat2 = m.lat2,
                                lat3 = m.lat3,
                                lat4 = m.lat4,
                                lat5 = m.lat5,
                                lat6 = m.lat6,
                                lat7 = m.lat7,
                                lat8 = m.lat8,
                                lng1 = m.lng1,
                                lng2 = m.lng2,
                                lng3 = m.lng3,
                                lng4 = m.lng4,
                                lng5 = m.lng5,
                                lng6 = m.lng6,
                                lng7 = m.lng7,
                                lng8 = m.lng8,
                                local_id = item.local_id,
                                mobile_no = item.mobile_no,
                                previous_scheme_condition = m.previous_scheme_condition,
                                quality_of_work = m.quality_of_work,
                                remarks = m.remarks,
                                scheme_exits = Numerics.GetBool(m.scheme_exits),
                                scheme_id = m.scheme_id,
                                scheme_measurement = Numerics.GetBool(m.scheme_measurement),
                                scheme_value = m.scheme_value,
                                status_id = m.status_id,
                                tehsil_id = m.tehsil_id,
                                tehsil_name = m.tehsil_name,
                                uc_id = m.uc_id,
                                uc_mc_name = m.uc_mc_name,
                                uc_mc_type_id = m.uc_mc_type_id,
                                unique_code = m.unique_code,
                                user_mobile = m.user_mobile,
                                user_name = m.user_name,
                                user_role = item.user_role
                            };
                            db.tpv_progress.Add(progress);
                            item.Added = true;
                            await db.SaveChangesAsync();
                        }
                    }
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
        [HttpGet]
        public async Task<bool> SendMessageToNumber(string number, string message)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("http://192.168.1.22/gwasa/send.php?number=" + number + "&message=" + message + "").Result.Content.ReadAsStringAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
