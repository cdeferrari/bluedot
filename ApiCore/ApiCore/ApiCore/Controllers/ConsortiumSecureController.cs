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
using ApiCore.Services.Contracts.ConsortiumSecures;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("ConsortiumSecure")]
    public class ConsortiumSecureController : ApiController
    {

        public IConsortiumSecureService ConsortiumSecureService { get; set; }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<ConsortiumSecureResponse>))]
        public IHttpActionResult Get()
        {
            var completeUserList = ConsortiumSecureService.GetAll();

            if (completeUserList == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<List<ConsortiumSecureResponse>>(completeUserList);

            return Ok(dto);
        }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("{id}")]        
        [ResponseType(typeof(ConsortiumSecureResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeConsortiumSecure = ConsortiumSecureService.GetById(id);

            if (completeConsortiumSecure == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<ConsortiumSecureResponse>(completeConsortiumSecure);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un usuario
        /// </summary>
        /// <param name="ConsortiumSecure">Usuario a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ConsortiumSecureRequest ConsortiumSecure)
        {
            var result = ConsortiumSecureService.CreateConsortiumSecure(ConsortiumSecure);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="ConsortiumSecure">Usuario a modificar</param>
        /// <returns></returns>
        [Route("{id}")]
        public IHttpActionResult Put(int id, ConsortiumSecureRequest ConsortiumSecure)
        {            
            var originalConsortiumSecure = ConsortiumSecureService.GetById(id);
            
            var ret = ConsortiumSecureService.UpdateConsortiumSecure(originalConsortiumSecure, ConsortiumSecure);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">Usuario a eliminar</param>
        /// <returns></returns>        
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               ConsortiumSecureService.DeleteConsortiumSecure(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}