using ApiCore.DomainModel;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using ApiCore.Dtos.Response;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore
{
    /// <summary>
    /// Clase con los mapeos de AutMapper
    /// </summary>
    public class AutoMapperConfig
    {
        public static void Configure()
        {

            Mapper.Initialize(cfg => {
                cfg.CreateMap<Ticket, TicketResponse>();
                cfg.CreateMap<Ticket, TicketListResponse>();
                cfg.CreateMap<Priority, PriorityResponse>();
                cfg.CreateMap<TicketStatus, TicketStatusResponse>();
                cfg.CreateMap<BacklogUser, BacklogUserResponse>();
                cfg.CreateMap<BacklogUser, BacklogUserResponseTicketList>();
                cfg.CreateMap<User, UserResponse>();
                cfg.CreateMap<ContactData, ContactDataResponse>();
                cfg.CreateMap<Account, AccountResponse>();
                cfg.CreateMap<AccountType, AccountTypeResponse>();
                cfg.CreateMap<Worker, WorkerResponse>();
                cfg.CreateMap<Renter, RenterResponse>();
                cfg.CreateMap<Renter, UnitRenterResponse>();

                cfg.CreateMap<Provider, ProviderResponse>();
                cfg.CreateMap<Role, RoleResponse>();
                cfg.CreateMap<Ownership, OwnershipResponse>();
                cfg.CreateMap<Ownership, OwnershipTicketListResponse>();
                cfg.CreateMap<Ownership, OwnershipUnitResponse>();
                cfg.CreateMap<Owner, OwnerResponse>()
                .ForMember(x => x.FunctionalUnitId , opt => opt.MapFrom(y => y.FunctionalUnits.Select(x => x.Id).ToList()));

                cfg.CreateMap<Owner, UnitOwnerResponse>();

                cfg.CreateMap<Administration, AdministrationResponse>();
                cfg.CreateMap<ConsortiumRequest, Consortium>()
                .ForMember(x => x.Administration, opt => opt.Ignore())
                .ForMember(x => x.Ownership, opt => opt.Ignore());

                cfg.CreateMap<ManagerRequest, Manager>()
                .ForMember(x => x.Consortium, opt => opt.Ignore())
                .ForMember(x => x.LaborUnion, opt => opt.Ignore());

                cfg.CreateMap<FunctionalUnitRequest, FunctionalUnit>();
                cfg.CreateMap<Consortium, ConsortiumResponse>();
                cfg.CreateMap<Consortium, ConsortiumTicketListResponse>();

                cfg.CreateMap<ConsortiumSecure, ConsortiumSecureResponse>();
                cfg.CreateMap<FunctionalUnit, UnitResponse>();
                cfg.CreateMap<FunctionalUnit, FunctionalUnitResponse>();
                cfg.CreateMap<FunctionalUnit, FunctionalUnitTicketList>();

                cfg.CreateMap<CommonData, CommonDataResponse>()
                .ForMember(x => x.OwnershipId, m => m.MapFrom(y => y.Ownership.Id));

                cfg.CreateMap<List, ListResponse>();
                cfg.CreateMap<Address,AddressResponse>();
                cfg.CreateMap<TaskList, TaskListResponse>();
                cfg.CreateMap<TaskHistory, TaskHistoryResponse>();
                cfg.CreateMap<TicketHistory, TicketHistoryResponse>();
                cfg.CreateMap<Manager, ManagerResponse>();
                cfg.CreateMap<Manager, ManagerResponseTicketList>();

                cfg.CreateMap<Message, MessageResponse>();

                cfg.CreateMap<ElevatorControl, ControlResponse>();
                cfg.CreateMap<FireExtinguisherControl, ControlResponse>();
                cfg.CreateMap<Task, TaskResponse>();
            });

        }
    }
}

