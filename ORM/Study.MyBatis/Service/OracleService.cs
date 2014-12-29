using MyBatis.DataMapper;
using MyBatis.DataMapper.Session;

namespace Study.MyBatis.Service
{
    public abstract class OracleService : BaseService
    {
        public IDataMapper DataMapper;
        private ISession Session;
        public OracleService()
        {
            DataMapper = MyBatisSingleton.GetDatabaseMapper("EAS");
            Session = MyBatisSingleton.GetTransaction("EAS");            
        }
    }
}
