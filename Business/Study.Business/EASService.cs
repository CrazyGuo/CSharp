using Study.Entity;
using Study.MyBatis.Service;
using System.Collections;
using System.Collections.Generic;
using MyBatis.DataMapper;
using Study.MyBatis;

namespace Study.Business
{
    public class EASService : OracleService
    {
        public int TestOracle()
        {
            return DataMapper.QueryForObject<int>("qStaff", null);
        }
    }
}
