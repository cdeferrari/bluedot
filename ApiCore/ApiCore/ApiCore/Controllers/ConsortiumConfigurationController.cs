using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.ConsortiumConfigurations;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de ConsortiumConfigurations
    /// </summary>
    [RoutePrefix("ConsortiumConfigurations")]
    public class ConsortiumConfigurationController : ApiController
    {

        public IConsortiumConfigurationsService ConsortiumConfigurationService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los ConsortiumConfigurations
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<ConsortiumConfiguration>))]
        public IHttpActionResult Get()
        {
            var ConsortiumConfigurations = ConsortiumConfigurationService.GetAll();

            if (ConsortiumConfigurations == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = ConsortiumConfigurations;
            
            return Ok(dto);
        }


        
        // POST api/<controller>
        /// <summary>
        /// Inserta un ConsortiumConfiguration
        /// </summary>
        /// <param name="ConsortiumConfiguration">ConsortiumConfiguration a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ConsortiumConfigurationRequest ConsortiumConfiguration)
        {
            var result = ConsortiumConfigurationService.CreateConsortiumConfiguration(ConsortiumConfiguration);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un ConsortiumConfiguration
        /// </summary>
        /// <param name="ConsortiumConfiguration">ConsortiumConfiguration a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, ConsortiumConfigurationRequest ConsortiumConfiguration)
        {            
            var originalConsortiumConfiguration = ConsortiumConfigurationService.GetById(id);
            
            var ret = ConsortiumConfigurationService.UpdateConsortiumConfiguration(originalConsortiumConfiguration, ConsortiumConfiguration);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un ConsortiumConfiguration
        /// </summary>
        /// <param name="id">ConsortiumConfiguration a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               ConsortiumConfigurationService.DeleteConsortiumConfiguration(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}