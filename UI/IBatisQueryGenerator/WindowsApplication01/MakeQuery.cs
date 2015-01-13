using System;
using System.Collections.Generic;
using System.Text;

namespace IBatisQueryGenerator
{
    interface MakeQuery
    {
        string makeSelectQuery(string strColEng, string strColKor);
        string makeFromQuery(string strTableEng);
        string makeWhereQuery(string strColEng, string strColKor);
    }
}
