using System;
using System.Collections.Generic;
using System.Text;

namespace IBatisQueryGenerator
{
    class MakeDeleteQuery : MakeQuery
    {
        private int nMaxColEngLength;
        private int nMaxColKorLength;
        private bool checkedComment;

        public MakeDeleteQuery(int nMaxColEngLength, int nMaxColKorLength, bool checkedComment)
        {
            this.nMaxColEngLength = nMaxColEngLength;
            this.nMaxColKorLength = nMaxColKorLength;
            this.checkedComment = checkedComment;
        }

        public string makeSelectQuery(string filedName, string filedComment)
        {
            return null;
        }

        public string makeFromQuery(string tableName )
        {
            return "DELETE  FROM  " + tableName + "\n";
        }

        public string makeWhereQuery(string filedName, string filedComment)
        {
            MakeCommonQuery comm = new MakeCommonQuery(string.Empty, nMaxColEngLength, nMaxColKorLength);
            return comm.makeQuery(filedName, filedComment, SqlOperationType.Where, checkedComment);
        }

    }
}
