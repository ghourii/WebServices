using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataLayer
{
    public class SQL_PARAMETER
    {
        #region Member Variables
        private String _Name;
        private DbType _Type;
        private Object _Value;
        #endregion

        #region Constructor
        public SQL_PARAMETER()
        {
            this._Name = string.Empty;
            this._Type = DbType.String;
            this._Value = null;
        }

        public SQL_PARAMETER(string name, DbType type, object value)
        {
            this._Name = name;
            this._Type = type;
            this._Value = value;
        }
        #endregion

        #region Properties
        public String Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }
        public DbType Type
        {
            get { return this._Type; }
            set { this._Type = value; }
        }
        public Object Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }
        #endregion

    }
}
