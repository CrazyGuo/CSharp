using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace IBatisQueryGenerator
{
    class CombineDeleteQuery : CombineQuery
    {
        private MakeQuery makeQuery;
        private DataTable dataTable;
        private string tableName;

        public CombineDeleteQuery(MakeQuery makeQuery, DataTable dataTable, string tableName)
        {
            this.makeQuery = makeQuery;
            this.dataTable = dataTable;
            this.tableName = tableName;
        }

        public string combine()
        {
            string strResult = combineFromQuery();
            strResult += combineWhereQuery();
            return strResult;
        }

        private string combineSelectQuery()
        {
            return null;
        }

        private string combineFromQuery()
        {
            return makeQuery.makeFromQuery(tableName);
        }

        private string combineWhereQuery()
        {
            String strResult = null;
            int nRow = dataTable.Rows.Count;

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
