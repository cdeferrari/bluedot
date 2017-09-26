using System;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using AutoMapper;
using ApiCore.Dtos.Response;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.Ownerships;
using System.Collections.Generic;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de propiedad
    /// </summary>
    [RoutePrefix("Propiedad")]
    public class OwnershipController : ApiController
    {

        public IOwnershipService OwnershipService { get; set; }
     

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(OwnershipResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeOwnership = OwnershipService.GetById(id);

            if (completeOwnership == null)
                throw new NotFoundException(ErrorMessages.PropiedadNoEncontrada);

            var dto = Mapper.Map<OwnershipResponse>(completeOwnership);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta una propiedad
        /// </summary>
        /// <param name="Ownership">Propiedad a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(OwnershipRequest Ownership)
        {
            var result = OwnershipService.CreateOwnership(Ownership);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica una propiedad
        /// </summary>
        /// <param name="Ownership">Propiedad a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, OwnershipRequest Ownership)
        {            
            var originalOwnership = OwnershipService.GetById(id);
            
            var ret = OwnershipService.UpdateOwnership(originalOwnership, Ownership);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina una propiedad
        /// </summary>
        /// <param name="id">Propiedad a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               OwnershipService.DeleteOwnership(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }

        // GET api/<controller>/
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>
        
        [ResponseType(typeof(IList<OwnershipResponse>))]
        public IHttpActionResult Get()
        {
            var completeOwnerships = OwnershipService.GetAll();

            if (completeOwnerships == null)
                throw new NotFoundException(ErrorMessages.PropiedadNoEncontrada);

            var dto = Mapper.Map<List<OwnershipResponse>>(completeOwnerships);

            return Ok(dto);
        }
    }
}