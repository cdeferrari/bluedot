using Administracion.DomainModel;
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
                cfg.CreateMap<ConsortiumViewModel, Consortium>();
                cfg.CreateMap<AddressViewModel, Address>();
                cfg.CreateMap<OwnershipViewModel, Ownership>();
                cfg.CreateMap<AdministrationViewModel, Administration>();
                cfg.CreateMap<DataContactViewModel, DataContact>();
                cfg.CreateMap<UserViewModel, User>();
                cfg.CreateMap<Account, AccountViewModel>();
            });

        }
    }
}