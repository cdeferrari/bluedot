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
using ApiCore.Services.Contracts.TaskHistory;
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.Tasks;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de TaskHistorys
    /// </summary>
    [RoutePrefix("TaskHistory")]
    public class TaskHistoryController : ApiController
    {

        public ITaskHistoryService TaskHistoryService { get; set; }
        
        

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un TaskHistory
        /// </summary>
        /// <param name="TaskHistory">id del TaskHistory</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(TaskHistoryResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeTaskHistory = TaskHistoryService.GetById(id);

            if (completeTaskHistory == null)
                throw new NotFoundException(ErrorMessages.TaskHistoryNoEncontrado);

            var dto = Mapper.Map<TaskHistoryResponse>(completeTaskHistory);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un TaskHistory
        /// </summary>
        /// <param name="TaskHistory">TaskHistory a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(TaskHistoryRequest TaskHistory)
        {
            var result = TaskHistoryService.CreateTaskHistory(TaskHistory);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un TaskHistory
        /// </summary>
        /// <param name="TaskHistory">TaskHistory a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, TaskHistoryRequest TaskHistory)
        {            
            var originalTaskHistory = TaskHistoryService.GetById(id);
            
            var ret = TaskHistoryService.UpdateTaskHistory(originalTaskHistory, TaskHistory);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un TaskHistory
        /// </summary>
        /// <param name="id">TaskHistory a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               TaskHistoryService.DeleteTaskHistory(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
        

    }
}