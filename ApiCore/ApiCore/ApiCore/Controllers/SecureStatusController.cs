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
using ApiCore.Services.Contracts.TaskResult;
using ApiCore.Services.Contracts.SecureStatus;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("SecureStatus")]
    public class SecureStatusController : ApiController
    {

        public ISecureStatusService SecureStatusService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<SecureStatus>))]
        public IHttpActionResult Get()
        {
            var SecureStatusList = this.SecureStatusService.GetAll();

            if (SecureStatusList == null)
                throw new NotFoundException(ErrorMessages.TicketNoEncontrado);            
            
            return Ok(SecureStatusList);
        }
        
    }
}