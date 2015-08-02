using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyUnitTest.UnitTest
{
    public abstract class BaseService : IServiceBase
    {
       // [LoggingAspect]
        public virtual string GetDeleteSqlId()
        {
            return string.Empty;
        }
    }
}
