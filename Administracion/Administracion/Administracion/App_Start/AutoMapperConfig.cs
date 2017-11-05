using Administracion.DomainModel;
using Administracion.Dto.Consortium;
using Administracion.Dto.Ticket;
using Administracion.Dto.Unit;
using Administracion.Dto.Worker;
using Administracion.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {

            Mapper.Initialize(cfg => {
                cfg.CreateMap<Ticket, TicketViewModel>();
                cfg.CreateMap<TicketViewModel, Ticket>();
                cfg.CreateMap<TicketViewModel, TicketRequest>()
                .ForMember(x => x.PriorityId, o=> o.MapFrom(y=>y.Priority.Id))
                .ForMember(x => x.StatusId, o => o.MapFrom(y => y.Status.Id));

                cfg.CreateMap<Consortium, ConsortiumViewModel>()
                .ForMember(x => x.OwnershipId, o => o.MapFrom(y => y.Ownership.Id))
                .ForMember(x => x.AdministrationId, o => o.MapFrom(y => y.Administration.Id));
                
                cfg.CreateMap<Consortium, ConsortiumDetailsViewModel>()                                
                .ForMember(x => x.AdministrationId, o => o.MapFrom(y => y.Administration.Id));


                cfg.CreateMap<ConsortiumViewModel, Consortium>();
                cfg.CreateMap<ConsortiumViewModel, ConsortiumRequest>();
                cfg.CreateMap<AddressViewModel, Address>();
                cfg.CreateMap<Address, AddressViewModel>();
                cfg.CreateMap<OwnershipViewModel, Ownership>();
                cfg.CreateMap<Ownership, OwnershipViewModel>();

                cfg.CreateMap<FunctionalUnit, FunctionalUnitViewModel>();
                cfg.CreateMap<FunctionalUnit, FunctionalUnitRequest>()
                .ForMember(x => x.OwnershipId, o=> o.MapFrom(y => y.Ownership.Id));

                cfg.CreateMap<FunctionalUnitViewModel, FunctionalUnit>()
                .ForMember(x => x.Ownership, o=> o.Ignore());

                cfg.CreateMap<AdministrationViewModel, Administration>();
                cfg.CreateMap<Administration, AdministrationViewModel>();

                cfg.CreateMap<ContactDataViewModel, ContactData>();
                cfg.CreateMap<ContactData, ContactDataViewModel>();
                cfg.CreateMap<UserViewModel, User>();                
                

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Account, AccountViewModel>();

                cfg.CreateMap<ProviderViewModel, Provider>();
                cfg.CreateMap<Provider, ProviderViewModel>();

                cfg.CreateMap<List, CheckListViewModel>();
                cfg.CreateMap<TaskList, TaskListViewModel>();

                cfg.CreateMap<ManagerViewModel, ManagerRequest>()
                .ForMember(x => x.OwnershipId, o=> o.MapFrom(y => y.Ownership.Id));


            });

        }
    }
}