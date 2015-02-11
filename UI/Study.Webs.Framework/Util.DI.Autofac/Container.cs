using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Study.Webs.Framework.Util.DI.Autofac 
{
    /// <summary>
    /// Autofac对象容器
    /// </summary>
    public class Container 
    {
        /// <summary>
        /// 初始化Autofac对象容器
        /// </summary>
        /// <param name="modules">配置模块</param>
        public Container( params IModule[] modules ) 
        {
            var builder = CreateBuilder( modules );
            _container = builder.Build();
        }

        /// <summary>
        /// 创建容器生成器
        /// </summary>
        public static ContainerBuilder CreateBuilder( params IModule[] modules ) 
        {
            var builder = new ContainerBuilder();
            foreach ( var module in modules )
                builder.RegisterModule( module );
            return builder;
        }

        /// <summary>
        /// 容器
        /// </summary>
        private readonly IContainer _container;

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        public T Create<T>() 
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        public object Create( Type type ) 
        {
            return _container.Resolve( type );
        }

        /// <summary>
        /// 为Mvc注册依赖
        /// </summary>
        /// <param name="modules">依赖配置</param>
        public static void RegisterMvc( params IModule[] modules ) 
        {
            RegisterMvc( Assembly.GetCallingAssembly(),modules );
        }

        /// <summary>
        /// 为Mvc注册依赖
        /// </summary>
        /// <param name="mvcAssembly">mvc项目所在的程序集</param>
        /// <param name="modules">依赖配置</param>
        public static void RegisterMvc( Assembly mvcAssembly, params IModule[] modules ) 
        {
            var builder = CreateBuilder( modules );
            builder.RegisterControllers( mvcAssembly );
            builder.RegisterApiControllers( mvcAssembly );
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver( container );
            DependencyResolver.SetResolver( new AutofacDependencyResolver( container ) );
        }
    }
}
