using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.CM_Schemes
{
    public class DAL_CM
    {
        public DataSet Get_Schemes_Record(int? id)
        {
            SQL_PARAMETER[] sql = new SQL_PARAMETER[1];
            
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                sql[0] = new SQL_PARAMETER("@id", DbType.Int32, id);
            }
            else
            {
                sql[0] = new SQL_PARAMETER("@id", DbType.Int32, DBNull.Value);
            }
            DataSet ds = CM_SERVER_DB.GetDataSet("Api_Get_Schemes_Record", sql);
            return ds;
        }


        public DataSet Get_District_Allocation()
        {
            DataSet ds = CM_SERVER_DB.GetDataSet("Api_Get_District_Allocation");
            return ds;
        }


    }
}
