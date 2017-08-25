using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Core;
using System.Reflection;

namespace ApiCore.App_Start
{
    public class IocConfig
    {

        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            RegisterTypes(builder, Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        public static void RegisterTypes(ContainerBuilder builder, Assembly assembly)
        {
            RegisterControllers(builder, assembly);
            RegisterCustomTypes(builder);
        }

        private static void RegisterControllers(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterApiControllers(assembly).PropertiesAutowired();
        }
    }
}