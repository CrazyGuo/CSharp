using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace IBatisQueryGenerator
{
    class CombineSelectQuery : CombineQuery
    {
        private MakeQuery makeQuery;
        private DataTable dataTable;
        private string tableName;

        public CombineSelectQuery(MakeQuery makeQuery, DataTable dataTable, string tableName )
        {
            this.makeQuery = makeQuery;
            this.dataTable = dataTable;
            this.tableName = tableName;
        }

        public string combine()
        {
            string strResult = combineSelectQuery();
            strResult += combineFromQuery();
            strResult += combineWhereQuery();
            return strResult;
        }

        private string combineSelectQuery()
        {
            String strResult = "SELECT \n";

            int i = 0;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                DataColumn filedColumn = dataTable.Columns[0];
                DataColumn commentColumn = dataTable.Columns[1];
                DataColumn whereFiledColumn = dataTable.Columns[2];
                DataColumn operationedColumn = dataTable.Columns[3];
                string seleted=dataRow[operationedColumn].ToString();
                if (seleted.Equals("true",StringComparison.OrdinalIgnoreCase)==false)
                {
                    continue;
                }
                if (dataRow[filedColumn].ToString() == null )
                {
                    continue;
                }

                if (dataRow[commentColumn].ToString() == null)
                {
                    dataRow[commentColumn] = "";
                }

                string strAdd = "        ";

                if (i != 0)
                {
                    strAdd = "     ,  ";
                }
                string filed = dataRow[filedColumn].ToString();
                string comment = dataRow[commentColumn].ToString();
                strResult += strAdd + makeQuery.makeSelectQuery( filed, comment );

                strResult += "\n";
                i++;
            }

            return strResult;
        }

        private string combineFromQuery()
        {
            return makeQuery.makeFromQuery(tableName);
        }

        private string combineWhereQuery()
        {
            String strResult = null;

            int nWhere = 0;


            foreach (DataRow dataRow in dataTable.Rows)
            {
                DataColumn filedColumn = dataTable.Columns[0];
                DataColumn commentColumn = dataTable.Columns[1];
                DataColumn whereFiledColumn = dataTable.Columns[2];

                if (dataRow[filedColumn].ToString() == null)
                {
                    continue;
                }

                if (dataRow[commentColumn].ToString() == null)
                {
                    dataRow[commentColumn] = "";

                }

                if (dataRow[whereFiledColumn].ToString() == null)
                {
                    continue;
                }

                if (!dataRow[whereFiledColumn].ToString().Equals("True"))
                {
                    continue;
                }

                nWhere++;

                string strAdd = " WHERE  ";

                if (nWhere > 1)
                {
                    strAdd = "   AND  ";
                }
                string filed = dataRow[filedColumn].ToString();
                string comment = dataRow[commentColumn].ToString();
                strResult += strAdd + makeQuery.makeWhereQuery(filed, comment);

                strResult += "\n";
            }
            
            return strResult;
        }
    }
}
