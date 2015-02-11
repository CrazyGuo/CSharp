using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Study.ApplicationServices;
using Study.Entity;

namespace Study.BusinessService
{
    public interface IApplicationService : IServiceStudyBase<ApplicationDto, ApplicationQuery>
    {
    }
}
