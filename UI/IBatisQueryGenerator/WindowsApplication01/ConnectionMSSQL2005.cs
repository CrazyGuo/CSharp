using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace IBatisQueryGenerator
{
    class ConnectionMSSQL2005 : IEntQueryByProject
    {
        private StringBuilder sbConnection;
        private string schemaName;

        public ConnectionMSSQL2005(string ip, string id, string pw)
        {
            //ConnectionString.com
            this.sbConnection = new StringBuilder(64);
            this.sbConnection.Append("Data Source=");
            this.sbConnection.Append(ip);
            this.sbConnection.Append(";Initial Catalog=sample;User Id=");
            this.sbConnection.Append(id);
            this.sbConnection.Append(";Password=");
            this.sbConnection.Append(pw);
            this.sbConnection.Append(";");
        }

        public StringBuilder getConnectionInfo()
        {
            return this.sbConnection;
        }



        //스키마 정보 알아오기.
        public StringBuilder getSchemaQuery()
        {
            StringBuilder sbQuery = new StringBuilder(128);
            sbQuery.Append("select * from master.dbo.sysdatabases ");
            sbQuery.Append("where filename like 'D:\\db\\%' ");
            sbQuery.Append("order by dbid");
            return sbQuery;
        }


        public StringBuilder getTableQuery(string schemaName)
        {
            this.schemaName = schemaName;
            StringBuilder sbQuery = new StringBuilder(128);
            sbQuery.Append("select name from ");
            sbQuery.Append(schemaName);
            sbQuery.Append(".sys.all_objects where type_desc = 'USER_TABLE' order by name ");
            return sbQuery;
        }



        public StringBuilder getColumnQuery(string tableName, bool upperYn)
        {
            StringBuilder sbQuery = new StringBuilder(128);

            if (upperYn)
            {
                sbQuery.Append("select UPPER(Y.name) as name from ");
            }
            else
            {
                sbQuery.Append("select Y.name from ");
            }
            sbQuery.Append(schemaName);
            sbQuery.Append(".sys.all_objects X inner join ");
            sbQuery.Append(schemaName);
            sbQuery.Append(".sys.all_columns Y ");
            sbQuery.Append("ON (X.object_id = Y.object_id AND X.type_desc = 'USER_TABLE') WHERE X.name = '");
            sbQuery.Append(tableName);
            sbQuery.Append("'");

            return sbQuery;
        }

    }
}
