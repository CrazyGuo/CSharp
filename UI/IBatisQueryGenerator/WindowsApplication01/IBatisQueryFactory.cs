using System;
using System.Collections.Generic;
using System.Text;

namespace IBatisQueryGenerator
{
    class IBatisQueryFactory
    {
        private MakeQuery makeQuery;
        private CombineQuery combineQuery;

        public IBatisQueryFactory(MakeQuery makeQuery, CombineQuery combineQuery)
        {
            this.makeQuery = makeQuery;
            this.combineQuery = combineQuery;
        }

        public void setMakeQuery(MakeQuery makeQuery)
        {
            this.makeQuery = makeQuery;
        }

        public void setCombineQuery(CombineQuery combineQuery)
        {
            this.combineQuery = combineQuery;
        }

        public string getQuery()
        { 
            String strResult = combineQuery.combine();
            return strResult;
        }
    }
}
