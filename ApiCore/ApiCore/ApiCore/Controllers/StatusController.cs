using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using AutoMapper;
using ApiCore.Dtos.Response;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.Tickets;
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.TicketStatus;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("Status")]
    public class StatusController : ApiController
    {

        public ITicketStatusService TicketStatusService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<TicketStatus>))]
        public IHttpActionResult Get()
        {
            var statusList = TicketStatusService.GetAll();

            if (statusList == null)
                throw new NotFoundException(ErrorMessages.TicketNoEncontrado);            
            
            return Ok(statusList);
        }

        
    }
}