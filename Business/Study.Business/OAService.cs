using Study.Entity;
using Study.MyBatis.Service;
using System.Collections;
using System.Collections.Generic;
using MyBatis.DataMapper;
using Study.MyBatis;

namespace Study.Business
{
   public class OAService:MySqlService
    {
       public IList<Staff> GetStaffs(int page, int rows, string birthBegin, string birthEnd, ref int totalCount)
       {
           Hashtable hash = new Hashtable();
           hash.Add("birthBegin", birthBegin);
           hash.Add("birthEnd", birthEnd);
           hash.Add("STAFF_CARD_NO", "3");
           return DataMapper.QueryForListWithPage<Staff>("qStaff", hash, "STAFF_CARD_NO desc", page, rows, ref totalCount);
       }
    }
}
