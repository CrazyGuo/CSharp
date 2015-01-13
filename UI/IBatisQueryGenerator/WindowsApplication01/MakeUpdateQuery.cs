using System;
using System.Collections.Generic;
using System.Text;

namespace IBatisQueryGenerator
{
    class MakeUpdateQuery : MakeQuery
    {
        private int nMaxColEngLength;
        private int nMaxColKorLength;
        private bool checkedComment;

        public MakeUpdateQuery(int nMaxColEngLength, int nMaxColKorLength, bool checkedComment)
        {
            this.nMaxColEngLength = nMaxColEngLength;
            this.nMaxColKorLength = nMaxColKorLength;
            this.checkedComment = checkedComment;
        }

        public string makeSelectQuery(string filedName, string filedComment)
        {
            return makeQuery(filedName, filedComment);
        }

        public string makeFromQuery(string tableName)
        {
            string strQuery = "UPDATE  " + tableName + "\n";
            return strQuery;
        }

        public string makeWhereQuery(string filedName, string filedComment)
        {
            return makeQuery(filedName, filedComment);
        }



        public string makeQuery(string strColEng, string strColKor)
        {
            MakeCommonQuery comm = new MakeCommonQuery(string.Empty, nMaxColEngLength, nMaxColKorLength);
            return comm.makeQuery(strColEng, strColKor, SqlOperationType.Where, checkedComment);
        }

    }
}
