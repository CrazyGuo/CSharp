using System;
using System.Collections.Generic;
using System.Text;

namespace IBatisQueryGenerator
{
    class CalculateByteLength
    {
        public int calculate(string str)
        {
            char[] cList = str.ToCharArray();

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
            return nTotal;
        }

    }
}
