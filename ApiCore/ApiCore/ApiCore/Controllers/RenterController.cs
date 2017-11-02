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
using ApiCore.Services.Contracts.Renters;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("Renter")]
    public class RenterController : ApiController
    {

        public IRenterService RenterService { get; set; }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<RenterResponse>))]
        public IHttpActionResult Get()
        {
            var completeUserList = RenterService.GetAll();

            if (completeUserList == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<List<RenterResponse>>(completeUserList);

            return Ok(dto);
        }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("{id}")]        
        [ResponseType(typeof(RenterResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeRenter = RenterService.GetById(id);

            if (completeRenter == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<RenterResponse>(completeRenter);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un usuario
        /// </summary>
        /// <param name="Renter">Usuario a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(RenterRequest Renter)
        {
            var result = RenterService.CreateRenter(Renter);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="Renter">Usuario a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, RenterRequest Renter)
        {            
            var originalRenter = RenterService.GetById(id);
            
            var ret = RenterService.UpdateRenter(originalRenter, Renter);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">Usuario a eliminar</param>
        /// <returns></returns>
        /// 
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               RenterService.DeleteRenter(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}