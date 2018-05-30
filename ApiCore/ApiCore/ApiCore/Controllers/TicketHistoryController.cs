using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using AutoMapper;
using ApiCore.Dtos.Response;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.TicketHistory;
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.Tasks;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de TicketHistorys
    /// </summary>
    [RoutePrefix("TicketHistory")]
    public class TicketHistoryController : ApiController
    {

        public ITicketHistoryService TicketHistoryService { get; set; }
        
        

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un TicketHistory
        /// </summary>
        /// <param name="TicketHistory">id del TicketHistory</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(TicketHistoryResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeTicketHistory = TicketHistoryService.GetById(id);

            if (completeTicketHistory == null)
                throw new NotFoundException(ErrorMessages.TicketHistoryNoEncontrado);

            var dto = Mapper.Map<TicketHistoryResponse>(completeTicketHistory);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un TicketHistory
        /// </summary>
        /// <param name="TicketHistory">TicketHistory a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(TicketHistoryRequest TicketHistory)
        {
            var result = TicketHistoryService.CreateTicketHistory(TicketHistory);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un TicketHistory
        /// </summary>
        /// <param name="TicketHistory">TicketHistory a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, TicketHistoryRequest TicketHistory)
        {            
            var originalTicketHistory = TicketHistoryService.GetById(id);
            
            var ret = TicketHistoryService.UpdateTicketHistory(originalTicketHistory, TicketHistory);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un TicketHistory
        /// </summary>
        /// <param name="id">TicketHistory a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               TicketHistoryService.DeleteTicketHistory(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
        

    }
}