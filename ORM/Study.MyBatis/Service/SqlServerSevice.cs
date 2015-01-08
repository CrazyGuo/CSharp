using MyBatis.DataMapper;
using MyBatis.DataMapper.Session;

namespace Study.MyBatis.Service
{
    public abstract class SqlServerSevice : BaseService
    {
        public IDataMapper DataMapper;
        private ISession Session;

        public SqlServerSevice()
        {
            DataMapper = MyBatisSingleton.GetDatabaseMapper("PTS");
            Session = MyBatisSingleton.GetTransaction("PTS");            
        }
    }
}
