using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace IBatisQueryGenerator
{
    class GridClass
    {
        //private DataGridView grid;

        public GridClass()
        {
            
        }

        public void setGridMultyRow(DataGridView grid, string strClipBoardText)
        {
            int x = grid.CurrentCellAddress.X;
            int y = grid.CurrentCellAddress.Y;

            if (x >= 2)
            {
                throw new MyException("현재셀에 붙여넣기가 불가능 합니다.");
            }

            char[] delimiterChars = { '\r', '\n' };

            string[] words = strClipBoardText.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            int nClcLength = y + words.Length + 1;

            if (grid.RowCount < nClcLength)
            {
                throw new MyException("현재셀에 붙여넣기가 불가능 합니다.");
            }

            foreach (string s in words)
            {
                grid[x, y++].Value = s;
            }
        }


        /**
         * 선택한셀에 공통된 정보를 채울때
         * 
         * 
         */
        public void setSelectedCellsValue(DataGridView grid, string value)
        {
            DataGridViewSelectedCellCollection cells = grid.SelectedCells;
            foreach(DataGridViewCell cell in cells)
            {
                if (cell.ColumnIndex == 2)
                {
                    if (value == null)
                    {
                        cell.Selected = false;
                    }
                    else if (value.Equals("1"))
                    {
                        cell.Selected = true;
                    }
                    else
                    {
                        cell.Selected = false;
                    }
                }
                else
                {
                    cell.Value = value;
                }
            }
        }

        public String getQueryText(DataTable dataTable, string tableName, string tableAlias
            , int nColEng, int nColKor, SqlOperationType strQueryKinds, bool checkComment)
        {
            ConvertGridToIBatisQuery convert = new ConvertGridToIBatisQuery();
            return convert.getIBatisQuery(dataTable, strQueryKinds, tableName
            , tableAlias, nColEng, nColKor, checkComment);

        }

        public String getQueryText2(DataGridView grid
            , string strTable, string strTableAlias
            , string nColEng, string nColKor, string strQueryKind)
        {
            
            try
            {
                String strResult = null;
                int nRow = grid.RowCount;

                if (strQueryKind == null)
                {
                    throw new MyException("쿼리구분을 선택하세요.");
                }

                if ("insert".Equals(strQueryKind))
                {
                    strResult += "insert into " + strTable + " ( \n";
                }
                else if ("update".Equals(strQueryKind))
                {
                    strResult += "update " + strTable + " " + strTableAlias + "\n";
                }
                else if ("delete".Equals(strQueryKind))
                {
                    strResult += "delete from " + strTable + "\n";
                }

                return strResult;

            }

            catch (MyException my)
            {
                throw my;
            }

        }

        public String getQueryMiddleText(DataGridView grid
            , string strTable, string strTableAlias
            , int nColEng, int nColKor
            , string strQueryKind)
        {
            try
            {
                String strResult = null;
                int nRow = grid.RowCount;

                for (int i = 0; i < nRow; i++)
                {
                    if (grid[0, i].Value == null)
                    {
                        continue;
                    }

                    if (grid[1, i].Value == null)
                    {
                        grid[1, i].Value = "";
                    }



                    string strAdd = "        ";

                    if (i != 0)
                    {
                        strAdd = "     ,  ";
                    }

                    strResult += strAdd + formatMiddleQuery(strTableAlias
                        , grid[0, i].Value.ToString()
                        , grid[1, i].Value.ToString()
                        , nColEng
                        , nColKor);

                    strResult += "\n";
                }


                return strResult;
            }

            catch (MyException my)
            {
                throw my;
            }

        }

        public String getQueryWhereText(DataGridView grid
            , string strTable, string strTableAlias
            , int nColEng, int nColKor
            , string strQueryKind)
        {
            try
            {
                String strResult = null;
                int nRow = grid.RowCount;

                int nWhere = 0;

                for (int i = 0; i < nRow; i++)
                {
                    if (grid[0, i].Value == null)
                    {
                        continue;
                    }

                    if (grid[1, i].Value == null)
                    {
                        grid[1, i].Value = "";
                    
                    }

                    if (grid[2, i].Value == null)
                    {
                        continue;
                    }

                    

                    //MessageBox.Show(grid[2, i].Value.ToString());

                    if (!grid[2, i].Value.ToString().Equals("True"))
                    {
                        continue;
                    }

                    nWhere++;

                    string strAdd = " WHERE  ";

                    if (nWhere > 1)
                    {
                        strAdd = "   AND  ";
                    }

                    strResult += strAdd + formatWhereQuery(strTableAlias
                        , grid[0, i].Value.ToString()
                        , grid[1, i].Value.ToString()
                        , nColEng
                        , nColKor);

                    strResult += "\n";
                }

                return strResult;

            }

            catch (MyException my)
            {
                throw my;
            }

        }

        private String formatMiddleQuery(string strTableAlias, string strColEng, string strColKor, int nColEng, int nColKor)
        {
            if (strColEng.Length >= nColEng)
            {
                throw new MyException("컬럼길이(" + strColEng.Length + ")보다 입력넓이가 작거나 같습니다.");
            }

            String strQuery = null;

            if (strTableAlias != null && strTableAlias.Length != 0)
            {
                strQuery += strTableAlias + ".";
            }

            String str1 = (strQuery + strColEng).PadRight(nColEng) + "AS \"" + strColEng + "\"";
            String str2 = str1.PadRight(nColEng*2+3) + "<!-- " + strColKor;

            int nCnt = Encoding.Default.GetBytes(strColKor).Length;
            char[] cList = strColKor.ToCharArray();

            int nTotal = 0;

            foreach (char c in cList)
            {
                char[] temp = { c };
                int nTemp = Encoding.Default.GetBytes(temp).Length;
                if (nTemp == 2)
                {
                    nTotal++;
                }
            }

            String str3 = str2.PadRight(nColEng * 2 + 3 + nColKor - nTotal) + " -->";
            return str3;
        }

        private String formatWhereQuery(string strTableAlias, string strColEng, string strColKor, int nColEng, int nColKor)
        {
            if (strColEng.Length >= nColEng)
            {
                throw new MyException("컬럼길이(" + strColEng.Length + ")보다 입력넓이가 작거나 같습니다.");
            }

            String strQuery = null;

            if (strTableAlias != null && strTableAlias.Length != 0)
            {
                strQuery += strTableAlias + ".";
            }

            String str1 = (strQuery + strColEng).PadRight(nColEng) + " = #" + strColEng + "#";
            String str2 = str1.PadRight(nColEng * 2 + 3) + "<!-- #" + strColKor + "#";

            int nCnt = Encoding.Default.GetBytes(strColKor).Length;
            char[] cList = strColKor.ToCharArray();

            int nTotal = 0;

            foreach (char c in cList)
            {
                char[] temp = { c };
                int nTemp = Encoding.Default.GetBytes(temp).Length;
                if (nTemp == 2)
                {
                    nTotal++;
                }
            }

            String str3 = str2.PadRight(nColEng * 2 + 3 + nColKor - nTotal) + " -->";
            return str3;
        }
    }
}
