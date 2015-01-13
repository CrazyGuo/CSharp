using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace IBatisQueryGenerator
{
    class EntConnection
    {
        private SqlConnection conn;
        private IEntQueryByProject query;

        public EntConnection(IEntQueryByProject query)
        {
            this.query = query;
            this.conn = new SqlConnection(query.getConnectionInfo().ToString());
        }


        public ArrayList getSchemaList()
        {
            return getList(this.query.getSchemaQuery());
        }

        public ArrayList getTableList(string schemaName)
        {
            return getList(this.query.getTableQuery(schemaName));
        }


        public DataTable getColumnList(string tableName, bool upperYn)
        {
            return getColumnList(this.query.getColumnQuery(tableName, upperYn));
        }


        private ArrayList getList(StringBuilder sbQuery)
        {
            SqlDataReader rdr = null;

            ArrayList list = new ArrayList();

            try
            {
                // Open the connection
                conn.Open();
                // 1. Instantiate a new command with a query and connection
                SqlCommand cmd = new SqlCommand(sbQuery.ToString(), conn);

                // 2. Call Execute reader to get query results
                rdr = cmd.ExecuteReader();

                // print the CategoryName of each record
                while (rdr.Read())
                {
                    //Console.WriteLine(rdr[0]);
                    list.Add(rdr[0]);
                }
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return list;
        }


        private DataTable getColumnList(StringBuilder sbQuery)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            DataColumn dc1 = dt.Columns.Add("col_eng");
            dc1.DataType = Type.GetType("System.String");

            DataColumn dc2 = dt.Columns.Add("col_kor");
            dc2.DataType = Type.GetType("System.String");

            DataColumn dc3 = dt.Columns.Add("pk");
            dc3.DataType = Type.GetType("System.Boolean");

            DataColumn dc4 = dt.Columns.Add("operationedFiled");
            dc4.DataType = Type.GetType("System.Boolean");
            dc4.DefaultValue = "True";

            DataRow dr;

            try
            {
                // Open the connection
                conn.Open();
                // 1. Instantiate a new command with a query and connection

                //Console.WriteLine(getColumnQuery(tableName));
                //Console.WriteLine(getColumnQuery(tableName, upperYn));
                SqlCommand cmd = new SqlCommand(sbQuery.ToString(), conn);

                // 2. Call Execute reader to get query results
                rdr = cmd.ExecuteReader();



                // print the CategoryName of each record
                while (rdr.Read())
                {
                    //Console.WriteLine(rdr[0]);
                    dr = dt.NewRow();
                    dr[0] = rdr[0];
                    dt.Rows.Add(dr);
                }
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return dt;
        }


        public void close()
        {
            Console.WriteLine("Close()");
            this.conn.Close();
        }




    }
}
