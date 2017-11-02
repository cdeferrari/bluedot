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
using ApiCore.Services.Contracts.Owners;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("owner")]
    public class OwnerController : ApiController
    {

        public IOwnerService OwnerService { get; set; }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<OwnerResponse>))]
        public IHttpActionResult Get()
        {
            var completeUserList = OwnerService.GetAll();

            if (completeUserList == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<List<OwnerResponse>>(completeUserList);

            return Ok(dto);
        }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("{id}")]        
        [ResponseType(typeof(OwnerResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeOwner = OwnerService.GetById(id);

            if (completeOwner == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<OwnerResponse>(completeOwner);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un usuario
        /// </summary>
        /// <param name="Owner">Usuario a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(OwnerRequest Owner)
        {
            var result = OwnerService.CreateOwner(Owner);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="Owner">Usuario a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, OwnerRequest Owner)
        {            
            var originalOwner = OwnerService.GetById(id);
            
            var ret = OwnerService.UpdateOwner(originalOwner, Owner);

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
               OwnerService.DeleteOwner(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}