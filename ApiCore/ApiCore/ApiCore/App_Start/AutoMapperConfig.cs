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
                cfg.CreateMap<Priority, PriorityResponse>();
                cfg.CreateMap<TicketStatus, TicketStatusResponse>();
                cfg.CreateMap<BacklogUser, BacklogUserResponse>();
                cfg.CreateMap<User, UserResponse>();
                cfg.CreateMap<ContactData, ContactDataResponse>();
                cfg.CreateMap<Account, AccountResponse>();
                cfg.CreateMap<AccountType, AccountTypeResponse>();
                cfg.CreateMap<Worker, WorkerResponse>();
                cfg.CreateMap<Role, RoleResponse>();

            });

        }
    }
}