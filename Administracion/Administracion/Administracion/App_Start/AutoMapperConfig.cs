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
                
            });

        }
    }
}