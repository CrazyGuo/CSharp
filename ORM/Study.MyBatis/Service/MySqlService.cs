using MyBatis.DataMapper;
using MyBatis.DataMapper.Session;

namespace Study.MyBatis.Service
{
    public abstract class MySqlService : BaseService
    {
        public IDataMapper DataMapper;
        private ISession Session;

        public MySqlService()
        {
            DataMapper = MyBatisSingleton.GetDatabaseMapper("OA");
            Session = MyBatisSingleton.GetTransaction("OA");            
        }
    }
}
