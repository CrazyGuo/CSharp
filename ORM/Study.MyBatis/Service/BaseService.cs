using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.MyBatis.Service
{
    public abstract class BaseService
    {
        public BaseService()
        {
            string dbConfigure = AppDomain.CurrentDomain.BaseDirectory + "DatabaseFilter";
            MyBatisSingleton.GetInstance(dbConfigure);
        }
    }
}
