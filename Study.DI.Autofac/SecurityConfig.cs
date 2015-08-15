using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Study.MongoDB.Core;
using Study.Entity;
using Study.BusinessService;

namespace Study.DI.Autofac
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
            builder.RegisterType<MoneyOutService>().As<IMoneyOutService>().InstancePerLifetimeScope();
            builder.RegisterType<MoneyKindService>().As<IMoneyKindService>().InstancePerLifetimeScope();
            builder.RegisterType<MongoDbRemoteRepository<OpcModel>>().As<MongoDbRemoteRepository<OpcModel>>().InstancePerLifetimeScope();
            builder.RegisterType<SportRecordService>().As<ISportRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<SportKindService>().As<ISportKindService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUsersService>().As<IAppUsersService>().InstancePerLifetimeScope();
        }
    }
}
