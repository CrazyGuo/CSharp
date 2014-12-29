using MyBatis.DataMapper;
using MyBatis.DataMapper.Session;

namespace Study.MyBatis.Service
{
    public abstract class MainDBService : BaseService
    {
        public IDataMapper DataMapper;
        private ISession Session;
        public MainDBService()
        {
            DataMapper = MyBatisSingleton.GetDatabaseMapper("PTS");
            Session = MyBatisSingleton.GetTransaction("PTS");            
        }
    }
}
