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

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("TaskResult")]
    public class TaskResultController : ApiController
    {

        public ITaskResultService TaskResultService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<TaskResult>))]
        public IHttpActionResult Get()
        {
            var TaskResultList = TaskResultService.GetAll();

            if (TaskResultList == null)
                throw new NotFoundException(ErrorMessages.TicketNoEncontrado);            
            
            return Ok(TaskResultList);
        }
        
    }
}