using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace IBatisQueryGenerator
{
    class ConvertGridToIBatisQuery
    {
         
        public string getIBatisQuery(DataTable dataTable
            , SqlOperationType queryKinds
            , string tableName
            , string tableAlias
            , int nMaxColEngLength
            , int nMaxColKorLength
            , bool checkedComment)
        {
            string strResult = null;

            MakeQuery msq = null;
            CombineQuery csq = null;
            
            if(queryKinds == SqlOperationType.Select)
            {
                msq = new MakeSelectQuery(tableAlias, nMaxColEngLength, nMaxColKorLength, checkedComment);
                csq = new CombineSelectQuery(msq, dataTable, tableName);
            }
            else if(queryKinds == SqlOperationType.Insert)
            {
                msq = new MakeInsertQuery(nMaxColEngLength, nMaxColKorLength, checkedComment);
                csq = new CombineInsertQuery(msq, dataTable, tableName);
            }
            else if (queryKinds == SqlOperationType.Update)
            {
                msq = new MakeUpdateQuery(nMaxColEngLength, nMaxColKorLength, checkedComment);
                csq = new CombineUpdateQuery(msq, dataTable, tableName);
            }
            else if (queryKinds == SqlOperationType.Delete)
            {
                msq = new MakeDeleteQuery(nMaxColEngLength, nMaxColKorLength, checkedComment);
                csq = new CombineDeleteQuery(msq, dataTable, tableName);
            }

            IBatisQueryFactory factory = new IBatisQueryFactory(msq, csq);
            strResult = factory.getQuery();

            return strResult;
        }
    
    }
}
