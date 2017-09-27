using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Core;
using System.Reflection;
using Autofac.Integration.WebApi;
using System.Web.Http;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using ApiCore.Repository;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Interceptors;
using ApiCore.Services.Contracts.Tickets;
using ApiCore.Services.Implementations.Tickets;
using ApiCore.Repository.Implementatios;
using Autofac.Extras.DynamicProxy2;
using ApiCore.Services.Implementations.BacklogUser;
using ApiCore.Services.Implementations.BacklogUsers;
using ApiCore.Services.Contracts.Administrations;
using ApiCore.Services.Implementations.Administrations;
using ApiCore.Services.Contracts.Ownerships;
using ApiCore.Services.Implementations.Ownerships;
using ApiCore.Services.Implementations.Consortiums;
using ApiCore.Services.Contracts.Consortiums;

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

        private static void RegisterCustomTypes(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>().As<IDbContext>().PropertiesAutowired().InstancePerRequest();

            builder.RegisterType<ServicesInterceptor>().AsSelf().PropertiesAutowired().InstancePerRequest();

            
            builder.RegisterType<TicketService>().As<ITicketService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<BacklogUserService>().As<IBacklogUserService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<AdministrationService>().As<IAdministrationService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<OwnershipService>().As<IOwnershipService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<ConsortiumService>().As<IConsortiumService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            

            builder.RegisterType<TicketRepository>().As<ITicketRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ConsortiumRepository>().As<IConsortiumRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<FunctionalUnitRepository>().As<IFunctionalUnitRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<StatusRepository>().As<IStatusRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<PriorityRepository>().As<IPriorityRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<BacklogUserRepository>().As<IBacklogUserRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<AdministrationRepository>().As<IAdministrationRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<OwnershipRepository>().As<IOwnershipRepository>().PropertiesAutowired().InstancePerRequest();

        }

    }
}