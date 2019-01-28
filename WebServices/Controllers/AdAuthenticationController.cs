using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebServices.Models;

namespace WebServices.Controllers
{
    public class AdAuthenticationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Authenticate(string username, string password)
        {
            var obj = new ResponseModel();
            string adPath = "LDAP://172.20.82.57,DC=urbanunit,DC=gov,DC=pk"; // "basitkhan", "Abc!2345"
            LdapAuthentication adAuth = new LdapAuthentication(adPath);
            try
            {
                if (true == adAuth.IsAuthenticated1("LDAP://172.20.82.57/OU=UrbanUnit,DC=urbanunit,DC=gov,DC=pk", username, password))
                {
                    //// Retrieve the user's groups
                    string groups = adAuth.GetGroups("LDAP://172.20.82.57/OU=UrbanUnit,DC=urbanunit,DC=gov,DC=pk", username, password);
                    var aduser = GetActiveDirectoryUserInfo(username, password);
                    aduser.Groups = groups;
                    obj.status = "200";
                    obj.message = "Login successfully";
                    obj.data = aduser;
                }
                else
                {
                    //ViewBag.Error = "Authentication failed, check username and password.";
                    obj.status = "400";
                    obj.message = "Authentication failed, check username and password.";
                }
            }
            catch (Exception ex)
            {
                //ViewBag.Error = "Error authenticating. " + ex.Message;
                obj.status = "500";
                obj.message = "Error authenticating. " + ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        private AdUserModel GetActiveDirectoryUserInfo(string username, string password)
        {
            var obj = new AdUserModel();
            string adPath = "LDAP://172.20.82.57";
            
            LdapAuthentication adAuth = new LdapAuthentication(adPath);
            try
            {
                DirectoryEntry de = new DirectoryEntry(adPath, username, password);
                DirectorySearcher ds = new DirectorySearcher(de);

                ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + username + "))";

                obj.UserName = username;
                obj.Password = password;

                ds.SearchScope = SearchScope.Subtree;
                SearchResult rs = ds.FindOne();

                if (rs.GetDirectoryEntry().Properties["givenName"].Value != null)
                {
                    obj.FirstName = rs.GetDirectoryEntry().Properties["givenName"].Value.ToString();
                }
                if (rs.GetDirectoryEntry().Properties["sn"].Value != null)
                {
                    obj.LastName = rs.GetDirectoryEntry().Properties["sn"].Value.ToString(); ;
                }

                if (rs.GetDirectoryEntry().Properties["mail"].Value != null)
                {
                    obj.Email = rs.GetDirectoryEntry().Properties["mail"].Value.ToString();
                }
                if (rs.GetDirectoryEntry().Properties["description"].Value != null)
                {
                    obj.Description = rs.GetDirectoryEntry().Properties["description"].Value.ToString();
                }
                if (rs.GetDirectoryEntry().Properties["st"].Value != null)
                {
                    obj.State = rs.GetDirectoryEntry().Properties["st"].Value.ToString();
                }
                if (rs.GetDirectoryEntry().Properties["telephoneNumber"].Value != null)
                {
                    obj.Phone = rs.GetDirectoryEntry().Properties["telephoneNumber"].Value.ToString();
                }
                if (rs.GetDirectoryEntry().Properties["title"].Value != null)
                {
                    obj.JobTitle = rs.GetDirectoryEntry().Properties["title"].Value.ToString();
                }
                if (rs.GetDirectoryEntry().Properties["co"].Value != null)
                {
                    obj.Country = rs.GetDirectoryEntry().Properties["co"].Value.ToString();
                }
                if (rs.GetDirectoryEntry().Properties["department"].Value != null)
                {
                    obj.Department = rs.GetDirectoryEntry().Properties["department"].Value.ToString();
                }
                if (rs.GetDirectoryEntry().Properties["company"].Value != null)
                {
                    obj.Company = rs.GetDirectoryEntry().Properties["company"].Value.ToString();
                }
                if (rs.GetDirectoryEntry().Properties["mobile"].Value != null)
                {
                    obj.Mobile = rs.GetDirectoryEntry().Properties["mobile"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            return obj;
        }
    }
}
