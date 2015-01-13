using System;
using System.Collections.Generic;
using System.Text;

namespace IBatisQueryGenerator
{
    class MakeSelectQuery : MakeQuery
    {
        private int nMaxColEngLength;
        private int nMaxColKorLength;
        private string strTableAlias;
        private bool checkedComment;

        public MakeSelectQuery(string strTableAlias, int nMaxColEngLength, int nMaxColKorLength, bool checkedComment)
        {
            this.strTableAlias = strTableAlias;
            this.nMaxColEngLength = nMaxColEngLength;
            this.nMaxColKorLength = nMaxColKorLength;
            this.checkedComment = checkedComment;
        }

        public string makeSelectQuery(string filedName, string filedComment)
        {
            return makeQuery(filedName, filedComment, SqlOperationType.Select);
        }

        public string makeFromQuery(string tableName)
        {
            return "  FROM  " + tableName + "  " + strTableAlias + "\n";
        }

        public string makeWhereQuery(string filedName, string filedComment)
        {
            return makeQuery(filedName, filedComment, SqlOperationType.Where);
        }

        private string makeQuery(string filedName, string filedComment, SqlOperationType sqlOperationType)
        {
            MakeCommonQuery comm = new MakeCommonQuery(strTableAlias, nMaxColEngLength, nMaxColKorLength);
            return comm.makeQuery(filedName, filedComment, sqlOperationType, checkedComment);
        }
    }
}
