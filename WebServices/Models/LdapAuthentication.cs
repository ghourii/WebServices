using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Web;

namespace WebServices.Models
{
    public class LdapAuthentication
    {
        private string _path;
        private string _filterAttribute;

        public LdapAuthentication(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated1(string srvr, string usr, string pwd)
        {
            bool authenticated = false;

            try
            {
                DirectoryEntry entry = new DirectoryEntry(srvr, usr, pwd);
                object nativeObject = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + usr + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                //var rr = "CN=" + (String)result.Properties["cn"][0] + ",";
                // Update the new path to the user in the directory
                _path = result.Path;
                //_path = _path.Replace(rr, "");
                _filterAttribute = (String)result.Properties["cn"][0];
                authenticated = true;
            }
            catch (DirectoryServicesCOMException cex)
            {
                //not authenticated; reason why is in cex
            }
            catch (Exception ex)
            {
                //not authenticated due to some other exception [this is optional]
            }

            return authenticated;
        }
        public string GetGroups(string srvr, string usr, string pwd)
        {
            DirectoryEntry entry = new DirectoryEntry(srvr, usr, pwd);
            object nativeObject = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();
            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount;
                     propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1),
                                      (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return groupNames.ToString();
        }

        public string[] GetGroups(string username)
        {
            string[] output = null;

            using (var ctx = new PrincipalContext(ContextType.Domain))
            using (var user = UserPrincipal.FindByIdentity(ctx, username))
            {
                if (user != null)
                {
                    output = user.GetGroups() //this returns a collection of principal objects
                        .Select(x => x.SamAccountName) // select the name.  you may change this to choose the display name or whatever you want
                        .ToArray(); // convert to string array
                }
            }

            return output;
        }

    }
}