using Study.ApplicationServices;
using Study.Entity;

namespace Study.BusinessService
{
    public interface IApplicationService : IServiceStudyBase<ApplicationDto, ApplicationQuery>
    {
    }
}
