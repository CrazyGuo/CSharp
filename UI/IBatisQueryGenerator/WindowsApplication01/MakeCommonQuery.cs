using System;
using System.Collections.Generic;
using System.Text;

namespace IBatisQueryGenerator
{
    class MakeCommonQuery
    {
        private int nMaxColEngLength;
        private int nMaxColKorLength;
        private string strTableAlias;

        public MakeCommonQuery(string strTableAlias, int nMaxColEngLength, int nMaxColKorLength)
        {
            this.strTableAlias = strTableAlias;
            this.nMaxColEngLength = nMaxColEngLength;
            this.nMaxColKorLength = nMaxColKorLength;
        }

        public string makeQuery(string filedName, string filedComment, SqlOperationType sqlOperationType, bool checkedComment)
        {
            string strParam1 = null;
            string strParam2 = null;

            if (sqlOperationType == SqlOperationType.Select)
            {
                strParam1 = "AS";
                strParam2 = "\"";
            }

            else if (sqlOperationType == SqlOperationType.Where)
            {
                strParam1 = " =";
                strParam2 = "#";
            }

            if (filedName.Length >= nMaxColEngLength)
            {
                throw new MyException("컬럼길이(" + filedName.Length + ")보다 입력넓이가 작거나 같습니다.");
            }

            String strQuery = null;

            if (strTableAlias != null && strTableAlias.Length != 0)
            {
                strQuery += strTableAlias + ".";
            }

            String str1 = (strQuery + filedName).PadRight(nMaxColEngLength) + strParam1 + " " + strParam2 + filedName + strParam2;

            if (checkedComment)
            {
                return str1;
            }
            else
            {
                return makeComment(filedComment, str1);
            }            
        }

        public string makeComment(string strColKor, string str1)
        {
            String str2 = str1.PadRight(nMaxColEngLength * 2 + 3) + "<!-- " + strColKor;

            CalculateByteLength calculateByteLength = new CalculateByteLength();
            int nTotal = calculateByteLength.calculate(strColKor);

            String str3 = str2.PadRight(nMaxColEngLength * 2 + 3 + nMaxColKorLength - nTotal) + " -->";
            return str3;
        }
    }
}
