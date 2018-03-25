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
using ApiCore.Services.Implementations.TicketStatus;
using ApiCore.Services.Contracts.TicketStatus;
using ApiCore.Services.Contracts.Priorities;
using ApiCore.Services.Implementations.Priorities;
using ApiCore.Services.Implementations.Workers;
using ApiCore.Services.Contracts.Workers;
using ApiCore.Services.Contracts.Users;
using ApiCore.Services.Implementations.Users;
using ApiCore.Services.Contracts.Unit;
using ApiCore.Services.Contracts.Owners;
using ApiCore.Services.Implementations.Owners;
using ApiCore.Services.Contracts.Renters;
using ApiCore.Services.Implementations.Renters;
using ApiCore.Services.Implementations.Providers;
using ApiCore.Services.Contracts.Providers;
using ApiCore.Services.Implementations.Multimedias;
using ApiCore.Services.Contracts.Multimedias;
using ApiCore.Services.Implementations.Lists;
using ApiCore.Services.Contracts.Lists;
using ApiCore.Services.Contracts.Managers;
using ApiCore.Services.Implementations.Managers;
using ApiCore.Services.Implementations.LaboralUnion;
using ApiCore.Services.Contracts.LaboralUnion;
using ApiCore.Services.Implementations.TaskResult;
using ApiCore.Services.Contracts.TaskResult;
using ApiCore.Services.Contracts.PaymentTypes;
using ApiCore.Services.Implementations.SecureStatus;
using ApiCore.Services.Contracts.SecureStatus;
using ApiCore.Services.Implementations.ConsortiumSecures;
using ApiCore.Services.Contracts.ConsortiumSecures;
using ApiCore.Services.Implementations.CommonDataItems;
using ApiCore.Services.Contracts.CommonDataItems;
using ApiCore.Services.Implementations.CommonDatas;
using ApiCore.Services.Contracts.CommonDatas;
using ApiCore.Services.Contracts.Messages;
using ApiCore.Services.Implementations.Messages;
using ApiCore.Services.Implementations.Provinces;
using ApiCore.Services.Contracts.Provinces;
using ApiCore.Services.Contracts.Cities;
using ApiCore.Services.Implementations.Citys;
using ApiCore.Services.Implementations.SpendItems;
using ApiCore.Services.Contracts.SpendItems;
using ApiCore.Services.Implementations.SpendTypes;
using ApiCore.Services.Contracts.SpendTypes;
using ApiCore.Services.Implementations.Bills;
using ApiCore.Services.Contracts.Bills;
using ApiCore.Services.Implementations.Spends;
using ApiCore.Services.Contracts.Spends;
using ApiCore.Services.Implementations.PatrimonyStatuss;
using ApiCore.Services.Contracts.PatrimonyStatuss;
using ApiCore.Services.Implementations.Incomes;
using ApiCore.Services.Contracts.Incomes;
using ApiCore.Services.Contracts.Tasks;
using ApiCore.Services.Implementations.Tasks;

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

            builder.RegisterType<ConsortiumSecureService>().As<IConsortiumSecureService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<TicketStatusService>().As<ITicketStatusService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<PriorityService>().As<IPriorityService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<WorkerService>().As<IWorkerService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));


            builder.RegisterType<UserService>().As<IUserService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<OwnerService>().As<IOwnerService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<RenterService>().As<IRenterService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<ProviderService>().As<IProviderService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<ProvincesService>().As<IProvincesService>().PropertiesAutowired().InstancePerRequest()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<CitiesService>().As<ICitiesService>().PropertiesAutowired().InstancePerRequest()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<MultimediaService>().As<IMultimediaService>().PropertiesAutowired().InstancePerRequest()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ServicesInterceptor));


            builder.RegisterType<UnitService>().As<IUnitService>().PropertiesAutowired().InstancePerRequest()
              .EnableInterfaceInterceptors()
              .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<ListService>().As<IListService>().PropertiesAutowired().InstancePerRequest()
              .EnableInterfaceInterceptors()
              .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<TaskResultService>().As<ITaskResultService>().PropertiesAutowired().InstancePerRequest()
              .EnableInterfaceInterceptors()
              .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<SecureStatusService>().As<ISecureStatusService>().PropertiesAutowired().InstancePerRequest()
              .EnableInterfaceInterceptors()
              .InterceptedBy(typeof(ServicesInterceptor));


            builder.RegisterType<PaymentTypeService>().As<IPaymentTypeService>().PropertiesAutowired().InstancePerRequest()
              .EnableInterfaceInterceptors()
              .InterceptedBy(typeof(ServicesInterceptor));


            builder.RegisterType<ItemsService>().As<IItemsService>().PropertiesAutowired().InstancePerRequest()
              .EnableInterfaceInterceptors()
              .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<CommonDataItemsService>().As<ICommonDataItemsService>().PropertiesAutowired().InstancePerRequest()
              .EnableInterfaceInterceptors()
              .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<CommonDataService>().As<ICommonDataService>().PropertiesAutowired().InstancePerRequest()
              .EnableInterfaceInterceptors()
              .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<ManagerService>().As<IManagerService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<MessageService>().As<IMessageService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<LaboralUnionService>().As<ILaboralUnionService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<SpendItemService>().As<ISpendItemService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<SpendTypeService>().As<ISpendTypeService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<BillService>().As<IBillService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<SpendService>().As<ISpendService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<IncomeService>().As<IIncomeService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<PatrimonyStatusService>().As<IPatrimonyStatusService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<TicketService>().As<ITicketService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<TaskService>().As<ITaskService>().PropertiesAutowired().InstancePerRequest()
           .EnableInterfaceInterceptors()
           .InterceptedBy(typeof(ServicesInterceptor));

            builder.RegisterType<TicketRepository>().As<ITicketRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<MultimediaRepository>().As<IMultimediaRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ConsortiumRepository>().As<IConsortiumRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<FunctionalUnitRepository>().As<IFunctionalUnitRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<PriorityRepository>().As<IPriorityRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<BacklogUserRepository>().As<IBacklogUserRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<AdministrationRepository>().As<IAdministrationRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<OwnershipRepository>().As<IOwnershipRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<OwnerRepository>().As<IOwnerRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<WorkerRepository>().As<IWorkerRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<RenterRepository>().As<IRenterRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ProviderRepository>().As<IProviderRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<CityRepository>().As<ICityRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ProvinceRepository>().As<IProvinceRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<StatusRepository>().As<IStatusRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<SecureStatusRepository>().As<ISecureStatusRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ConsortiumSecureRepository>().As<IConsortiumSecureRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ContactDataRepository>().As<IContactDataRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ListRepository>().As<IListRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<TaskResultRepository>().As<ITaskResultRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ItemsRepository>().As<IItemsRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<CommonDataItemsRepository>().As<ICommonDataItemsRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<CommonDataRepository>().As<ICommonDataRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<LaboralUnionRepository>().As<ILaboralUnionRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ManagerRepository>().As<IManagerRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<MessageRepository>().As<IMessageRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<PaymentTypeRepository>().As<IPaymentTypeRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<SpendTypeRepository>().As<ISpendTypeRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<SpendItemRepository>().As<ISpendItemRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<BillRepository>().As<IBillRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<SpendRepository>().As<ISpendRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<IncomeRepository>().As<IIncomeRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<PatrimonyStatusRepository>().As<IPatrimonyStatusRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<TaskRepository>().As<ITaskRepository>().PropertiesAutowired().InstancePerRequest();

        }

    }
}