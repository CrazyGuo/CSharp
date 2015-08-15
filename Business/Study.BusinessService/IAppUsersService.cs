using Study.BusinessService.Application;
using Study.Entity;
using System.Collections.Generic;

namespace Study.BusinessService
{
    public interface IAppUsersService :IServiceStudyBase <AppUsersDto, AppUsersQuery>
    {
         //Here add your service code
        IList<AppUsersDto> FetchLogonUser(string userName, string pwd);
    }
}
