using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;


namespace DataLayer
{
    internal enum ConnectionHealthState
    { 
     OK = 0,
     DOUBTFUL = 1
    }
    public class SQL_CONNECTION : IDisposable
    {
        //Variables
        #region Variables
        private static DbProviderFactory _DBProviderFactory = null;
        private DbConnection _DbConnection;
        private DateTime _CreationTime;
        private bool _IsInUse;
        private DbTransaction _DbTransaction;
        private int _TransactionNestingCounter = 0;        
        private DateTime? _LastAllocationTime;
        private DateTime? _LastReleaseTime;
        private DateTime? _LastVerificationTime;

        private const string trasactionScopeMsg = "There is not trnasaction scope available";
        #endregion
        
        //Constructor
        #region Constructor
        public SQL_CONNECTION(DbProviderFactory dpf, String connectionStr)
        {
            this.DBConnection = dpf.CreateConnection();
            this._DbConnection.ConnectionString = connectionStr;
            DBProviderFactory = dpf;
            CreationTime = DateTime.Now;
            IsInUse = false;
        }
        #endregion

        //Properties
        #region Properties
        public static DbProviderFactory DBProviderFactory
        {
            //for static variables we donot use this.
            get { return _DBProviderFactory; }
            set { _DBProviderFactory = value; }
        }
        public DbConnection DBConnection
        {
            get { return this._DbConnection; }
            set { this._DbConnection = value; }
        }
        public DateTime CreationTime
        {
            get { return this._CreationTime; }
            set { this._CreationTime = value; }
        }
        public bool IsInUse
        {
            get { return this._IsInUse; }
            set {this._IsInUse = value ;}
        }
        public DbTransaction DBTransaction
        {
            get { return this._DbTransaction; }
            set { this._DbTransaction = value; }
        }
        public int TransactionNestingCounter
        {
            get { return this._TransactionNestingCounter; }
            set {this._TransactionNestingCounter = value ;}
        }
        public DateTime? LastVerifactionTime
        {
            get { return this._LastVerificationTime; }
            set { this._LastVerificationTime = value; }
        }
        #endregion

        //Methods
        #region Methods
        public void BeginTransaction()
        {
            if (this.DBTransaction == null)
            {
                this.DBTransaction = this.DBConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }
            else
            {
                this.TransactionNestingCounter++;
            }
        }

        public void CommitTransaction()
        {
            if (this.TransactionNestingCounter > 0)
            {
                this.TransactionNestingCounter--;
            }
            else if (this.DBTransaction != null)
            {
                this.DBTransaction.Commit();
                this.DBConnection.Close();
                this.DBTransaction = null;
                this.LastVerifactionTime = DateTime.Now;
            }
            else
            {
                throw new Exception(trasactionScopeMsg);
            }
        }
        public void RollbackTransaction()
        {
            if (this.DBTransaction == null)
            {
                throw new Exception(trasactionScopeMsg);
            }
            try
            {
                this.DBTransaction.Rollback();
            }
            catch(Exception ex)
            {
             this.DBConnection.Close();
            }   
            this.DBTransaction = null;
            this.TransactionNestingCounter = 0;
        }
        public void CloseConnection()
        {
            if (this.DBTransaction != null)
            {
                throw new Exception("An open transaction is available. A transaction Commit or Rollback is required."); 
            }
            if (this.DBConnection.State != ConnectionState.Closed)
            {
                this.DBConnection.Close();
            }
        }

        public void Dispose()
        {
            CloseConnection();
            this.DBConnection.Dispose();
        }
        #endregion

        //DBMethods With Transaction
        #region DBMethods With Transaction      
        public int Insert(string sql, bool getId)
        {
            using (DbCommand cmd = DBProviderFactory.CreateCommand())
            {
                cmd.Connection = this.DBConnection;
                cmd.CommandText = sql;
                if (this.DBTransaction != null)
                {
                    cmd.Transaction = this.DBTransaction;
                }
                cmd.ExecuteNonQuery();

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
                    cmd.CommandText = identitySelect;
                    id = int.Parse(cmd.ExecuteScalar().ToString());
                }
                return id;
            }
        }
        /// <summary>
        /// Executes Insert statements in the database. Optionally returns new identifier.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <param name="parameters">Sql parameter.</param>
        /// <param name="getId">Value indicating whether newly generated identity is returned.</param>
        /// <returns>Newly generated identity value (auto number value).</returns>
        public  int Insert(string sql, SQL_PARAMETER[] parameters, bool getId)
        {
           
                using (DbCommand command = DBProviderFactory.CreateCommand())
                {
                    command.Connection = this.DBConnection;
                    if (this.DBTransaction != null)
                    {
                        command.Transaction = this.DBTransaction;
                    }
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();

                    foreach (SQL_PARAMETER parameter in parameters)
                    {
                        DbParameter dbParameter = DBProviderFactory.CreateParameter();
                        dbParameter.DbType = parameter.Type;
                        dbParameter.ParameterName = parameter.Name;
                        dbParameter.Value = parameter.Value;
                        command.Parameters.Add(dbParameter);
                    }

                    
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
                        identitySelect = "SELECT @@IDENTITY";
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
                        //id = int.Parse(command.ExecuteScalar().ToString());
                    }

                    
                    return id;
                }
            
        }
        /// <summary>
        /// Executes Insert statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        public  int Insert(string sql)
        {
           return Insert(sql, false);
        }
        /// <summary>
        /// Executes Insert statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        public  void Insert(string sql, SQL_PARAMETER[] parameters)
        {
            Insert(sql, parameters, false);
        }
        public int Update(string sql)
        {
            using (DbCommand command = DBProviderFactory.CreateCommand())
            {
                command.Connection = this.DBConnection;
                command.CommandText = sql;
                if (this.DBTransaction != null)
                {
                    command.Transaction = this.DBTransaction;
                }
                int result = command.ExecuteNonQuery();
                return result;
            }
        }
        public int Update(string sql, SQL_PARAMETER[] parameters)
        {
            using (DbCommand command = DBProviderFactory.CreateCommand())
            {
                command.Connection = this.DBConnection;
                if (this.DBTransaction != null)
                {
                    command.Transaction = this.DBTransaction;
                }
                command.CommandText = sql;
                command.Parameters.Clear();
                foreach (SQL_PARAMETER parameter in parameters)
                {
                    DbParameter dbParameter = DBProviderFactory.CreateParameter();
                    dbParameter.DbType = parameter.Type;
                    dbParameter.ParameterName = parameter.Name;
                    dbParameter.Value = parameter.Value;
                    command.Parameters.Add(dbParameter);
                }
                int result = command.ExecuteNonQuery();
                return result;
            }
        }
        /// <summary>
        /// Executes Delete statements in the database.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>Number of rows affected.</returns>
        public int Delete(string sql)
        {
            return Update(sql);
        }

     
        public object GetScalar(string sql, SQL_PARAMETER[] parameters)
        {
            using (DbCommand command = DBProviderFactory.CreateCommand())
            {
                command.Connection = this.DBConnection;
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (SQL_PARAMETER parameter in parameters)
                    {
                        DbParameter dbParameter = DBProviderFactory.CreateParameter();
                        dbParameter.DbType = parameter.Type;
                        dbParameter.ParameterName = parameter.Name;
                        dbParameter.Value = parameter.Value;
                        command.Parameters.Add(dbParameter);
                    }
                }
                if (this.DBTransaction != null)
                {
                    command.Transaction = this.DBTransaction;
                }
                return command.ExecuteScalar();
            }
        }

           
        // with parameters 
        public  DataSet GetDataSet(string sql, SQL_PARAMETER[] parameters)
        {
               using (DbCommand command = DBProviderFactory.CreateCommand())
                {
                    command.Connection = this.DBConnection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = sql;
                    command.CommandTimeout = 3000;

                    if (parameters != null)
                    {
                        foreach (SQL_PARAMETER parameter in parameters)
                        {
                            DbParameter dbParameter = DBProviderFactory.CreateParameter();
                            dbParameter.DbType = parameter.Type;
                            dbParameter.ParameterName = parameter.Name;
                            dbParameter.Value = parameter.Value;
                            command.Parameters.Add(dbParameter);
                        }
                    }

                    using (DbDataAdapter adapter = DBProviderFactory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        DataSet ds = new DataSet();

                        adapter.Fill(ds);
                        return ds;
                    }
                }           
        }

        public  DataTable GetDataTable(string sql, SQL_PARAMETER[] parameters)
        {
            return GetDataSet(sql, parameters).Tables[0];
        }

        public  DataRow GetDataRow(string sql, SQL_PARAMETER[] parameters)
        {
            DataRow row = null;

            DataTable dt = GetDataTable(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }

            return row;
        }  
        
        #endregion
    }
}
