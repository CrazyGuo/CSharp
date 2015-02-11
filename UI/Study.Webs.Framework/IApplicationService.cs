using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.ApplicationServices;

namespace Study.Webs.Framework
{
    public interface IApplicationService : IServiceStudyBase<ApplicationDto, ApplicationQuery>
    {
    }
}
