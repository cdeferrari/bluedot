using Administracion.Integration.Contracts;
using Administracion.Integration.Implementations;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.Autentication;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Services.Contracts.Ownerships;
using Administracion.Services.Contracts.Priorities;
using Administracion.Services.Contracts.Status;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Contracts.Users;
using Administracion.Services.Contracts.Workers;
using Administracion.Services.Implementations.Administrations;
using Administracion.Services.Implementations.Autentication;
using Administracion.Services.Implementations.Consortiums;
using Administracion.Services.Implementations.FunctionalUnits;
using Administracion.Services.Implementations.Ownerships;
using Administracion.Services.Implementations.Priorities;
using Administracion.Services.Implementations.Status;
using Administracion.Services.Implementations.Tickets;
using Administracion.Services.Implementations.Users;
using Administracion.Services.Implementations.Workers;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Mvc;

namespace Administracion.App_Start
{
    public class IocConfig
    {

        public static void ConfigureContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterTypes(builder);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterType<Asynchronic>().As<IAsync>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<Authentication>().As<IAuthentication>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<Synchronic>().As<ISync>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<TicketService>().As<ITicketService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<ConsortiumService>().As<IConsortiumService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<AdministrationService>().As<IAdministrationService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<OwnershipService>().As<IOwnershipService>().SingleInstance().PropertiesAutowired();

            builder.RegisterType<WorkerService>().As<IWorkerService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<PriorityService>().As<IPriorityService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<StatusService>().As<IStatusService>().SingleInstance().PropertiesAutowired();

            builder.RegisterType<FunctionalUnitService>().As<IFunctionalUnitService>().SingleInstance().PropertiesAutowired();





        }

    }
}