using MyBatis.DataMapper;
using MyBatis.DataMapper.Session;

namespace Study.MyBatis.Service
{
    public abstract class SqlServerService : BaseService
    {
        public IDataMapper DataMapper;
        private ISession Session;
        public SqlServerService()
        {
            DataMapper = MyBatisSingleton.GetDatabaseMapper("PTS");
            Session = MyBatisSingleton.GetTransaction("PTS");            
        }
    }
}
