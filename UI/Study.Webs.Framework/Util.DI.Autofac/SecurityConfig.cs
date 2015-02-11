using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Study.Webs.Framework.Util.DI.Autofac
{
    public class SecurityConfig : ConfigBase
    {
        /// <summary>
        /// 加载配置
        /// </summary>
        protected override void Load(ContainerBuilder builder)
        {
            LoadApplicationServices(builder);
        }

        private void LoadApplicationServices(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationService>().As<IApplicationService>().InstancePerLifetimeScope();
        }
    }
}
