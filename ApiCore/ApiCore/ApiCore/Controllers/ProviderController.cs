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
using ApiCore.Services.Contracts.Providers;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("Provider")]
    public class ProviderController : ApiController
    {

        public IProviderService ProviderService { get; set; }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<ProviderResponse>))]
        public IHttpActionResult Get()
        {
            var completeUserList = ProviderService.GetAll();

            if (completeUserList == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<List<ProviderResponse>>(completeUserList);

            return Ok(dto);
        }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("{id}")]        
        [ResponseType(typeof(ProviderResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeProvider = ProviderService.GetById(id);

            if (completeProvider == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            var dto = Mapper.Map<ProviderResponse>(completeProvider);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un usuario
        /// </summary>
        /// <param name="Provider">Usuario a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ProviderRequest Provider)
        {
            var result = ProviderService.CreateProvider(Provider);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="Provider">Usuario a modificar</param>
        /// <returns></returns>
        [Route("{id}")]
        public IHttpActionResult Put(int id, ProviderRequest Provider)
        {            
            var originalProvider = ProviderService.GetById(id);
            
            var ret = ProviderService.UpdateProvider(originalProvider, Provider);

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
               ProviderService.DeleteProvider(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}