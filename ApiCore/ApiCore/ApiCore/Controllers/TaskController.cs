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
using ApiCore.Services.Contracts.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de Tasks
    /// </summary>
    [RoutePrefix("Tasks")]
    public class TaskController : ApiController
    {

        public ITaskService TaskService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todas las tareas
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Task>))]
        public IHttpActionResult Get()
        {
            var Tasks = TaskService.GetAll();

            if (Tasks == null)
                throw new NotFoundException(ErrorMessages.TareaNoEncontrada);
            
            return Ok(Tasks);
        }



        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un Task
        /// </summary>
        /// <param name="Task">id del Task</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(Task))]
        public IHttpActionResult Get(int id)
        {
            var completeTask = TaskService.GetById(id);

            if (completeTask == null)
                throw new NotFoundException(ErrorMessages.TareaNoEncontrada);
                        
            return Ok(completeTask);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un Task
        /// </summary>
        /// <param name="Task">Task a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(TaskRequest Task)
        {
            var result = TaskService.CreateTask(Task);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un Task
        /// </summary>
        /// <param name="Task">Task a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, TaskRequest Task)
        {            
            var originalTask = TaskService.GetById(id);
            
            var ret = TaskService.UpdateTask(originalTask, Task);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un Task
        /// </summary>
        /// <param name="id">Task a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               TaskService.DeleteTask(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}