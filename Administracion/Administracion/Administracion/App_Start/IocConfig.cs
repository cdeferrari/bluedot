using Administracion.Integration.Contracts;
using Administracion.Integration.Implementations;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.Autentication;
using Administracion.Services.Contracts.Bills;
using Administracion.Services.Contracts.CommonData;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.ConsortiumSecure;
using Administracion.Services.Contracts.Countries;
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Services.Contracts.Incomes;
using Administracion.Services.Contracts.IncomeTypes;
using Administracion.Services.Contracts.LaboralUnion;
using Administracion.Services.Contracts.Lists;
using Administracion.Services.Contracts.Managers;
using Administracion.Services.Contracts.Messages;
using Administracion.Services.Contracts.Multimedias;
using Administracion.Services.Contracts.Owners;
using Administracion.Services.Contracts.Ownerships;
using Administracion.Services.Contracts.PatrimonyStatuss;
using Administracion.Services.Contracts.PaymentTypesService;
using Administracion.Services.Contracts.Priorities;
using Administracion.Services.Contracts.Providers;
using Administracion.Services.Contracts.Provinces;
using Administracion.Services.Contracts.Renters;
using Administracion.Services.Contracts.SecureStatus;
using Administracion.Services.Contracts.SpendItemsService;
using Administracion.Services.Contracts.Spends;
using Administracion.Services.Contracts.SpendTypes;
using Administracion.Services.Contracts.Status;
using Administracion.Services.Contracts.TaskResult;
using Administracion.Services.Contracts.Tasks;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Contracts.Users;
using Administracion.Services.Contracts.Workers;
using Administracion.Services.Implementations.Administrations;
using Administracion.Services.Implementations.Autentication;
using Administracion.Services.Implementations.Bills;
using Administracion.Services.Implementations.Checklists;
using Administracion.Services.Implementations.CommonData;
using Administracion.Services.Implementations.Consortiums;
using Administracion.Services.Implementations.ConsortiumSecure;
using Administracion.Services.Implementations.Countrys;
using Administracion.Services.Implementations.FunctionalUnits;
using Administracion.Services.Implementations.Incomes;
using Administracion.Services.Implementations.IncomeTypes;
using Administracion.Services.Implementations.LaboralUnion;
using Administracion.Services.Implementations.Managers;
using Administracion.Services.Implementations.Messages;
using Administracion.Services.Implementations.Multimedias;
using Administracion.Services.Implementations.Owners;
using Administracion.Services.Implementations.Ownerships;
using Administracion.Services.Implementations.PatrimonyStatuss;
using Administracion.Services.Implementations.Priorities;
using Administracion.Services.Implementations.Providers;
using Administracion.Services.Implementations.Province;
using Administracion.Services.Implementations.Renters;
using Administracion.Services.Implementations.SpendItems;
using Administracion.Services.Implementations.Spends;
using Administracion.Services.Implementations.SpendTypes;
using Administracion.Services.Implementations.Status;
using Administracion.Services.Implementations.TaskResult;
using Administracion.Services.Implementations.Tasks;
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
            builder.RegisterType<ConsortiumSecureService>().As<IConsortiumSecureService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<AdministrationService>().As<IAdministrationService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<MultimediaService>().As<IMultimediaService>().SingleInstance().PropertiesAutowired();

            builder.RegisterType<OwnershipService>().As<IOwnershipService>().SingleInstance().PropertiesAutowired();

            builder.RegisterType<WorkerService>().As<IWorkerService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<RenterService>().As<IRenterService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<ProviderService>().As<IProviderService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<ProvinceService>().As<IProvinceService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<CityService>().As<ICityService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<OwnerService>().As<IOwnerService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<PriorityService>().As<IPriorityService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<StatusService>().As<IStatusService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<CommonDataService>().As<ICommonDataService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<TaskResultService>().As<ITaskResultService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<LaboralUnionService>().As<ILaboralUnionService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<ManagerService>().As<IManagerService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<MessageService>().As<IMessageService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<FunctionalUnitService>().As<IFunctionalUnitService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<ChecklistService>().As<IChecklistService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<CountryService>().As<ICountryService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<PaymentTypesService>().As<IPaymentTypesService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<SecureStatusService>().As<ISecureStatusService>().SingleInstance().PropertiesAutowired();


            builder.RegisterType<BillService>().As<IBillService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<SpendTypeService>().As<ISpendTypeService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<SpendItemService>().As<ISpendItemsService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<SpendService>().As<ISpendService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<IncomeService>().As<IIncomeService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<TaskService>().As<ITaskService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<PatrimonyStatusService>().As<IPatrimonyStatusService>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<IncomeTypesService>().As<IIncomeTypeService>().SingleInstance().PropertiesAutowired();


        }

    }
}