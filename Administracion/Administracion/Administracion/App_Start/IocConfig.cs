using Administracion.Integration.Contracts;
using Administracion.Integration.Implementations;
using Administracion.Services.Contracts.Autentication;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Implementations.Autentication;
using Administracion.Services.Implementations.Consortiums;
using Administracion.Services.Implementations.Tickets;
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
            
        }

    }
}