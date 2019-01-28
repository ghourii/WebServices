using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

using System.IO;
using System.Text;
using System.Web.Hosting;
using System.Threading.Tasks;
using System.Net;

namespace WebServices.Utills
{
    public class WriteException
    {

        public Task Write(string Message)
        {
            return Task.Run(() => {
                try
                {
                    var filePath = HostingEnvironment.MapPath("~/Exception/ApiException.txt");
                    using (var writer = new StreamWriter(filePath, true))
                    {
                        string expt = Environment.NewLine + "Message: " + Message + Environment.NewLine + Environment.NewLine + "" +
                                      Environment.NewLine + Environment.NewLine +
                                      Environment.NewLine +
                                      "Date Time: " + DateTime.Now;
                        writer.WriteLine(expt);
                        writer.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");

                        writer.Dispose();
                        writer.Close();
                        //+Environment.NewLine);
                    }
                }
                catch (Exception)
                {

                }

            });
        }

        //static WriteException()
        //{
        //    var directory = HostingEnvironment.MapPath("~/Exception/");
        //    var dirExist = System.IO.Directory.Exists(directory);
        //    if (!dirExist)
        //    {
        //        System.IO.Directory.CreateDirectory(directory);
        //    }
        //    var filePath = HostingEnvironment.MapPath("~/Exception/ApiException.txt");
        //    var exist = System.IO.File.Exists(filePath);
        //    if (!exist)
        //    {
        //        using (FileStream fs = File.Create(filePath))
        //        {
        //            //System.IO.File.Create(filePath);
        //        }

        //    }
        //}

        /// <summary>
        ///     This method is used to keep record of Exceptions.
        /// </summary>
        /// <param name="Message">
        ///     Add Exception Message with ex.Exception as Parameter.
        /// </param>
        /// <param name="StackTrace">
        ///     Add StackTrace Message with ex.StackTrace as Parameter.
        /// </param>
        /// <param name="PageAndMethodName">
        ///     Add Page and Method in which the exception was generated.
        /// </param>
        public Task Exception(Exception ex, string pageandmethod)
        {
            return Task.Run( () => {
                try
                {
                    var filePath = HostingEnvironment.MapPath("~/Exception/ApiException.txt");
                    using (var writer = new StreamWriter(filePath, true))
                    {
                        string sInnerMsg = "", sMainMessage = "";
                        if (ex.InnerException != null)
                        {
                            if (ex.InnerException.InnerException != null)
                            {
                                sInnerMsg = ex.InnerException.InnerException.Message;
                            }
                            sMainMessage = ex.Message + ex.InnerException.Message + sInnerMsg;
                        }
                        else
                        {
                            sMainMessage = ex.Message;
                            //Ignore exception it is Thread abort exception
                            if (sMainMessage.Equals("Thread was being aborted."))
                            {
                                return;
                            }

                        }
                        string expt = Environment.NewLine + "Message: " + sMainMessage + Environment.NewLine + Environment.NewLine + "StackTrace: " + ex.StackTrace + "" +
                                      Environment.NewLine + Environment.NewLine + "Page & Method: " + pageandmethod + "" +
                                      Environment.NewLine +
                                      "Date Time: " + DateTime.Now;
                        writer.WriteLine(expt);
                        writer.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");

                        writer.Dispose();
                        writer.Close();
                        //+Environment.NewLine);
                    }
                }
                catch (Exception)
                {
                    
                }
                
            });
        }
        
        public static Task EntityValidationErrors(DbEntityValidationException exValidation, string pageandmethod)
        {
            return Task.Run(() =>
           {
               var strBuilder = new StringBuilder();
               foreach (var eve in exValidation.EntityValidationErrors)
               {
                   var EntityName = (eve.Entry.Entity.GetType()).BaseType.FullName;
                   //string EntityName =eve.Entry.Entity.GetType().
                   //strBuilder.AppendLine("Validation Error in " + EntityName + Environment.NewLine + "The type is in state : " + eve.Entry.State + ".");

                   //foreach (DbValidationError ve in eve.ValidationErrors)
                   //{                    
                   //    strBuilder.AppendLine("Propery Name: "+ ve.PropertyName  + 
                   //                          Environment.NewLine +  "Error Message: " + ve.ErrorMessage+  "." +
                   //                          Environment.NewLine + "Date :" + DateTime.Now); 
                   //}                              
                   var validationMessagss = exValidation.EntityValidationErrors.SelectMany(x => x.ValidationErrors)
                                                                               .Select(x => x.PropertyName + ".  " + x.ErrorMessage);

                   // Join the list messages to a single string.
                   var fullErrorMessage = string.Join("; ", validationMessagss);

                   // Combine the original ex.Message message with the detailed validation errors.
                   strBuilder.Append(exValidation.Message
                                     + Environment.NewLine + "The validation errors are: "
                                     + Environment.NewLine + fullErrorMessage
                                     + Environment.NewLine);

                   var filePath = HostingEnvironment.MapPath("~/Exception/Exception.txt");

                   using (var writer = new StreamWriter(filePath, true))
                   {
                       writer.WriteLine(strBuilder);
                       writer.WriteLine(
                           "---------------------------------------------------------------------------------------------------------------------------------------");
                   } //using              
               } //for each
           });
        }

        public static string Exception(Exception ex)
        {
            string sInnerMsg = "", sMainMessage = "";
            if (ex.InnerException != null) {
                if (ex.InnerException.InnerException != null) {
                    return ex.InnerException.InnerException.Message;
                }
                return ex.Message + ex.InnerException.Message + sInnerMsg;
            }
            else {
                sMainMessage = ex.Message;
                //Ignore exception it is Thread abort exception
                if (sMainMessage.Equals("Thread was being aborted.")) {
                    return "";
                }
            }
            return sMainMessage;
        }

        public static string Exception(DbEntityValidationException dbEx)
        {
            string sMainMessage = "";
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    sMainMessage += $"Property : { validationError.PropertyName} -- Error: {validationError.ErrorMessage} Error";
                }
            }
            return sMainMessage;
        }
        private static bool WriteErrorLog(string strPathName, Exception objException)
        {
            bool bReturn = false;
            using (StreamWriter sw = new StreamWriter(strPathName, true))
            {
                string strException = string.Empty;
                try
                {
                    //sw = new StreamWriter(strPathName, true);
                    sw.WriteLine("Source        : " +
                            objException.Source.ToString().Trim());
                    sw.WriteLine("Method        : " +
                            objException.TargetSite.Name.ToString());
                    sw.WriteLine("Date        : " +
                            DateTime.Now.ToLongTimeString());
                    sw.WriteLine("Time        : " +
                            DateTime.Now.ToShortDateString());
                    sw.WriteLine("Computer    : " +
                            Dns.GetHostName().ToString());
                    sw.WriteLine("Error        : " +
                            objException.Message.ToString().Trim());
                    sw.WriteLine("Stack Trace    : " +
                            objException.StackTrace.ToString().Trim());
                    sw.WriteLine(@"^^------------------------
                            ------------------------------------------ -^^ ");
    
                    sw.Flush();
                    sw.Close();
                    bReturn = true;
                }
                catch (Exception)
                {
                    bReturn = false;
                }
            }
                
            return bReturn;
        }
        public Task WriteHttpResponseErrorLogs(string errorMessage, string pageandmethod)
        {
            return Task.Run(() => {
                try
                {
                    var filePath = HostingEnvironment.MapPath("~/Exception/ApiException.txt");
                    using (var writer = new StreamWriter(filePath, true))
                    {
                        string expt = Environment.NewLine + 
                        "Message: " + errorMessage + Environment.NewLine + Environment.NewLine + 
                        "Page & Method: " + pageandmethod + "" + Environment.NewLine +
                        "Date Time: " + DateTime.Now;
                        writer.WriteLine(expt);
                        writer.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");

                        writer.Dispose();
                        writer.Close();
                        //+Environment.NewLine);
                    }
                }
                catch (Exception)
                {

                }

            });
        }
    }//class
}