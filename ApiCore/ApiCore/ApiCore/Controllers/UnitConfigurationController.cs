using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.UnitConfigurations;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de UnitConfigurations
    /// </summary>
    [RoutePrefix("UnitConfigurations")]
    public class UnitConfigurationController : ApiController
    {

        public IUnitConfigurationsService UnitConfigurationService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los UnitConfigurations
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<UnitConfiguration>))]
        public IHttpActionResult Get()
        {
            var UnitConfigurations = UnitConfigurationService.GetAll();

            if (UnitConfigurations == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = UnitConfigurations;
            
            return Ok(dto);
        }


        
        // POST api/<controller>
        /// <summary>
        /// Inserta un UnitConfiguration
        /// </summary>
        /// <param name="UnitConfiguration">UnitConfiguration a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(UnitConfigurationRequest UnitConfiguration)
        {
            var result = UnitConfigurationService.CreateUnitConfiguration(UnitConfiguration);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un UnitConfiguration
        /// </summary>
        /// <param name="UnitConfiguration">UnitConfiguration a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, UnitConfigurationRequest UnitConfiguration)
        {            
            var originalUnitConfiguration = UnitConfigurationService.GetById(id);
            
            var ret = UnitConfigurationService.UpdateUnitConfiguration(originalUnitConfiguration, UnitConfiguration);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un UnitConfiguration
        /// </summary>
        /// <param name="id">UnitConfiguration a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               UnitConfigurationService.DeleteUnitConfiguration(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}