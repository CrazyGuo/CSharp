using System;
using System.Collections.Generic;
using System.Text;

namespace IBatisQueryGenerator
{
    class MakeInsertQuery : MakeQuery
    {
        private int nMaxColEngLength;
        private int nMaxColKorLength;
        private bool checkedComment;

        public MakeInsertQuery(int nMaxColEngLength, int nMaxColKorLength, bool checkedComment)
        {
            this.nMaxColEngLength = nMaxColEngLength;
            this.nMaxColKorLength = nMaxColKorLength;
            this.checkedComment = checkedComment;
        }

        public string makeSelectQuery(string filedName, string filedComment)
        {
            return makeQuery(filedName, filedComment, string.Empty);
        }

        public string makeFromQuery(string tableName)
        {
            return "INSERT  INTO  " + tableName + " ( \n";
        }

        public string makeWhereQuery(string filedName, string filedComment)
        {
            return makeQuery(filedName, filedComment, "#");
        }


        private string makeQuery(string filedName, string filedComment, string strParam)
        {
            if (filedName.Length >= nMaxColEngLength)
            {
                throw new MyException("컬럼길이(" + filedName.Length + ")보다 입력넓이가 작거나 같습니다.");
            }

            String str1 = strParam + filedName + strParam;

            if (checkedComment)
            {
                return str1;
            }

            else
            {
                return makeComment(filedComment, str1);
            }
        }


        public string makeComment(string filedComment, string str1)
        {
            String str2 = (str1).PadRight(nMaxColEngLength) + "<!-- " + filedComment;

            CalculateByteLength calculateByteLength = new CalculateByteLength();
            int nTotal = calculateByteLength.calculate(filedComment);

            String str3 = str2.PadRight(nMaxColEngLength + nMaxColKorLength - nTotal) + " -->";

            return str3;
        }

    }
}
