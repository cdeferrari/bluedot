using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using ApiCore.Dtos.Response;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using AutoMapper;
using ApiCore.Services.Contracts.Workers;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("Worker")]
    public class WorkerController : ApiController
    {

        public IWorkerService WorkerService { get; set; }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<WorkerResponse>))]
        public IHttpActionResult Get()
        {
            var completeUserList = WorkerService.GetAll();

            if (completeUserList == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<List<WorkerResponse>>(completeUserList);

            return Ok(dto);
        }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("{id}")]        
        [ResponseType(typeof(WorkerResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeWorker = WorkerService.GetById(id);

            if (completeWorker == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<WorkerResponse>(completeWorker);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un usuario
        /// </summary>
        /// <param name="Worker">Usuario a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(WorkerRequest Worker)
        {
            var result = WorkerService.CreateWorker(Worker);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="Worker">Usuario a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, WorkerRequest Worker)
        {            
            var originalWorker = WorkerService.GetById(id);
            
            var ret = WorkerService.UpdateWorker(originalWorker, Worker);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">Usuario a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               WorkerService.DeleteWorker(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}