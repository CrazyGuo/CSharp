using Study.MyBatis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Business
{
   public class CfbSpindleTestRecordService : SqlServerService
    {
       public IList<double> GetRecords()
       {
           return DataMapper.QueryForList<double>("qRecord", null);
       }
    }
}
