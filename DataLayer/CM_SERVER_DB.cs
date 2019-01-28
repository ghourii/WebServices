using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;



namespace DataLayer
{
    public static class CM_SERVER_DB
    {

        #region Variables
        //private static readonly DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
        private static readonly DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
       // public static String ConStr = "Data Source=202.166.168.182\\SQLSTD2012;Initial Catalog=ADP_PC1;User ID=sa;password=xformanite22@fc";///////For local
        public static String ConStr = "Data Source=192.168.80.7\\SQLSERVERGMIS;Initial Catalog=CM_Schemes;User ID=sa;password=xformanite22@fc";
      // public static String ConStr = "Data Source=.;Initial Catalog=ADP_PC1;User ID=sa;password=Abc123";
        public static SQL_CONNECTION transConn = new SQL_CONNECTION(dbProviderFactory, ConStr);
        #endregion


        #region Data Update handlers

        /// <summary>
        /// Executes Update statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>Number of rows affected.</returns>
        public static int Update(string sql)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }

        /// <summary>
        /// Executes Update statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <param name="parameters">Sql parameters.</param>
        /// <returns>Number of rows affected.</returns>
        public static int Update(string sql, SQL_PARAMETER[] parameters)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.Parameters.Clear();

                    foreach (SQL_PARAMETER parameter in parameters)
                    {
                        DbParameter dbParameter = dbProviderFactory.CreateParameter();
                        dbParameter.DbType = parameter.Type;
                        dbParameter.ParameterName = parameter.Name;
                        dbParameter.Value = parameter.Value;
                        command.Parameters.Add(dbParameter);
                    }

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }
        /// <summary>
        /// Executes Update statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <param name="parameters">Sql parameters.</param>
        /// <returns>Number of rows affected.</returns>
        public static int UpdateUsingSp(string sql, SQL_PARAMETER[] parameters)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.Parameters.Clear();
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (SQL_PARAMETER parameter in parameters)
                    {
                        DbParameter dbParameter = dbProviderFactory.CreateParameter();
                        dbParameter.DbType = parameter.Type;
                        dbParameter.ParameterName = parameter.Name;
                        dbParameter.Value = parameter.Value;
                        command.Parameters.Add(dbParameter);
                    }

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }
        /// <summary>
        /// Executes Delete statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>Number of rows affected.</returns>
        public static int Delete(string sql)
        {
            return Update(sql);
        }
        public static int UpdateSp(string sql, SQL_PARAMETER[] parameters)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = sql;
                    command.Parameters.Clear();

                    foreach (SQL_PARAMETER parameter in parameters)
                    {
                        DbParameter dbParameter = dbProviderFactory.CreateParameter();
                        dbParameter.DbType = parameter.Type;
                        dbParameter.ParameterName = parameter.Name;
                        dbParameter.Value = parameter.Value;
                        command.Parameters.Add(dbParameter);
                    }

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }
        /// <summary>
        /// Executes Insert statements in the database. Optionally returns new identifier.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <param name="getId">Value indicating whether newly generated identity is returned.</param>
        /// <returns>Newly generated identity value (auto number value).</returns>
        public static int Insert(string sql, bool getId)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;

                    connection.Open();
                    command.ExecuteNonQuery();

                    int id = -1;

                    // Check if new identity is needed.
                    if (getId)
                    {
                        // Execute RpmServerDb specific auto number or identity retrieval code
                        // SELECT SCOPE_IDENTITY() -- for SQL Server
                        // SELECT @@IDENTITY -- for MS Access/Odbc
                        string identitySelect;
                        //switch (Consts.DataProvider)
                        //{
                        //    // Odbc
                        //    case "System.Data.Odbc":
                        //        identitySelect = "SELECT @@IDENTITY";
                        //        break;
                        //    // Sql Server
                        //    case "System.Data.SqlClient":
                        identitySelect = "SELECT SCOPE_IDENTITY()";
                        //        break;
                        //    default:
                        //        identitySelect = "SELECT @@IDENTITY";
                        //        break;
                        //}
                        command.CommandText = identitySelect;
                        id = int.Parse(command.ExecuteScalar().ToString());
                    }

                    connection.Close();
                    return id;
                }
            }
        }

        /// <summary>
        /// Executes Insert statements in the database. Optionally returns new identifier.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <param name="parameters">Sql parameter.</param>
        /// <param name="getId">Value indicating whether newly generated identity is returned.</param>
        /// <returns>Newly generated identity value (auto number value).</returns>
        public static int Insert(string sql, SQL_PARAMETER[] parameters, bool getId)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();

                    foreach (SQL_PARAMETER parameter in parameters)
                    {
                        DbParameter dbParameter = dbProviderFactory.CreateParameter();
                        dbParameter.DbType = parameter.Type;
                        dbParameter.ParameterName = parameter.Name;
                        dbParameter.Value = parameter.Value;
                        command.Parameters.Add(dbParameter);
                    }

                    connection.Open();
                    command.ExecuteNonQuery();

                    int id = -1;

                    // Check if new identity is needed.
                    if (getId)
                    {
                        // Execute RpmServerDb specific auto number or identity retrieval code
                        // SELECT SCOPE_IDENTITY() -- for SQL Server
                        // SELECT @@IDENTITY -- for MS Access/Odbc
                        string identitySelect;
                        //switch (Consts.DataProvider)
                        //{
                        // Odbc
                        //case "System.Data.Odbc":
                        identitySelect = "SELECT LAST_INSERT_ID()";
                        //break;
                        // Sql Server
                        //case "System.Data.SqlClient":
                        //   identitySelect = "SELECT SCOPE_IDENTITY()";
                        //   break;
                        //default:
                        //   identitySelect = "SELECT @@IDENTITY";
                        //   break;
                        // }
                        command.CommandText = identitySelect;
                        id = int.Parse(command.ExecuteScalar().ToString());
                    }

                    connection.Close();
                    return id;
                }
            }
        }

        /// <summary>
        /// Executes Insert statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        public static void Insert(string sql)
        {
            Insert(sql, false);
        }

        /// <summary>
        /// Executes Insert statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        public static void Insert(string sql, SQL_PARAMETER[] parameters)
        {
            Insert(sql, parameters, false);
        }

        #endregion

        #region Data Retrieve Handlers

        public static DataSet GetDataSet(string sql)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        DataSet ds = new DataSet();

                        adapter.Fill(ds);


                        return ds;
                    }
                }
            }
        }

        public static DataSet GetDataSetUsingQuery(string sql)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        DataSet ds = new DataSet();

                        adapter.Fill(ds);


                        return ds;
                    }
                }
            }
        }

        public static DataTable GetDataTable(string sql)
        {
            return GetDataSet(sql).Tables[0];
        }
        public static DataTable GetDataTableUsingQuery(string sql)
        {
            return GetDataSetUsingQuery(sql).Tables[0];
        }

        public static DataRow GetDataRow(string sql)
        {
            DataRow row = null;

            DataTable dt = GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }

            return row;
        }

        public static object GetScalar(string sql)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;

                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }



        // with parameters 

        public static DataSet GetDataSet(string sql, SQL_PARAMETER[] parameters)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    if (parameters != null)
                    {
                        foreach (SQL_PARAMETER parameter in parameters)
                        {
                            DbParameter dbParameter = dbProviderFactory.CreateParameter();
                            dbParameter.DbType = parameter.Type;
                            dbParameter.ParameterName = parameter.Name;
                            dbParameter.Value = parameter.Value;
                            command.Parameters.Add(dbParameter);
                        }
                    }

                    using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        DataSet ds = new DataSet();

                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public static DataTable GetDataTable(string sql, SQL_PARAMETER[] parameters)
        {
            return GetDataSet(sql, parameters).Tables[0];
        }

        public static DataRow GetDataRow(string sql, SQL_PARAMETER[] parameters)
        {
            DataRow row = null;

            DataTable dt = GetDataTable(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }

            return row;
        }

        public static object GetScalar(string sql, SQL_PARAMETER[] parameters)
        {
            using (DbConnection connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConStr;//Properties.Settings.Default.ConnectionString;

                using (DbCommand command = dbProviderFactory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (SQL_PARAMETER parameter in parameters)
                    {
                        DbParameter dbParameter = dbProviderFactory.CreateParameter();
                        dbParameter.DbType = parameter.Type;
                        dbParameter.ParameterName = parameter.Name;
                        dbParameter.Value = parameter.Value;
                        command.Parameters.Add(dbParameter);
                    }
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        #endregion

        #region Utility methods

        /// <summary>
        /// Escapes an input string for database processing, that is, 
        /// surround it with quotes and change any quote in the string to 
        /// two adjacent quotes (i.e. escape it). 
        /// If input string is null or empty a NULL string is returned.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <returns>Escaped output string.</returns>
        public static string Escape(string s)
        {
            if (String.IsNullOrEmpty(s))
                return "NULL";
            else
                return "'" + s.Trim().Replace("'", "''") + "'";
        }

        /// <summary>
        /// Escapes an input string for database processing, that is, 
        /// surround it with quotes and change any quote in the string to 
        /// two adjacent quotes (i.e. escape it). 
        /// Also trims string at a given maximum length.
        /// If input string is null or empty a NULL string is returned.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <param name="maxLength">Maximum length of output string.</param>
        /// <returns>Escaped output string.</returns>
        public static string Escape(string s, int maxLength)
        {
            if (String.IsNullOrEmpty(s))
                return "NULL";
            else
            {
                s = s.Trim();
                if (s.Length > maxLength) s = s.Substring(0, maxLength - 1);
                return "'" + s.Trim().Replace("'", "''") + "'";
            }
        }

        #endregion

    }
}
