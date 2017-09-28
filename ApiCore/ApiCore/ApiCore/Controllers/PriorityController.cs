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
using ApiCore.Services.Contracts.Priorities;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("Priority")]
    public class PriorityController : ApiController
    {

        public IPriorityService PriorityService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<PriorityResponse>))]
        public IHttpActionResult Get()
        {
            var priorities = PriorityService.GetAll();

            if (priorities == null)
                throw new NotFoundException(ErrorMessages.TicketNoEncontrado);

            var dto = Mapper.Map<IList<PriorityResponse>>(priorities);
            
            return Ok(dto);
        }

        
    }
}