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
using ApiCore.Services.Contracts.Lists;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de consorcio
    /// </summary>
    [RoutePrefix("Lista")]
    public class TaskListController : ApiController
    {

        public IListService ListService { get; set; }



        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="tarea">id de la tarea</param>
        /// <returns></returns>

        [Route("")]
        [ResponseType(typeof(List<ListResponse>))]
        public IHttpActionResult Get()
        {
            var completeTaskList = ListService.GetAll();

            if (completeTaskList == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);

            var dto = Mapper.Map<List<ListResponse>>(completeTaskList);

            return Ok(dto);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="tarea">id de la tarea</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(ListResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeTaskList = ListService.GetById(id);

            if (completeTaskList == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);

            var dto = Mapper.Map<ListResponse>(completeTaskList);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un consorcio
        /// </summary>
        /// <param name="TaskList">Consorcio a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ListRequest TaskList)
        {
            var result = ListService.CreateList(TaskList);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un consorcio
        /// </summary>
        /// <param name="TaskList">Consorcio a modificar</param>
        /// <returns></returns>
        [Route("{id}")]
        public IHttpActionResult Put(int id, ListRequest TaskList)
        {            
            var originalTaskList = ListService.GetById(id);
            
            var ret = ListService.UpdateList(originalTaskList, TaskList);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un consorcio
        /// </summary>
        /// <param name="id">Consorcio a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               ListService.DeleteList(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}