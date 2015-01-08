using MyBatis.DataMapper;
using Study.Entity.Common;
using Study.MyBatis.Service;
using System.Collections.Generic;

namespace Study.Business
{
    public class MenuService : SqlServerService
    {
        public IList<Menu> GetMenus(string Id)
        {
            return DataMapper.QueryForList<Menu>("qMenus", Id);          
        }
    }
}
