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
using ApiCore.Services.Contracts.Administrations;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de administracion
    /// </summary>
    [RoutePrefix("Administracion")]
    public class AdministrationController : ApiController
    {

        public IAdministrationService AdministrationService { get; set; }


        // GET api/<controller>/5
        /// <summary>
        /// Devuelve una Administracion
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(AdministrationResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeAdministration = AdministrationService.GetById(id);

            if (completeAdministration == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);

            var dto = Mapper.Map<AdministrationResponse>(completeAdministration);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta una Administracion
        /// </summary>
        /// <param name="Administration">Consorcio a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(AdministrationRequest Administration)
        {
            var result = AdministrationService.CreateAdministration(Administration);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica una Administracion
        /// </summary>
        /// <param name="Administration">Administracion a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, AdministrationRequest Administration)
        {            
            var originalAdministration = AdministrationService.GetById(id);
            
            var ret = AdministrationService.UpdateAdministration(originalAdministration, Administration);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un consorcio
        /// </summary>
        /// <param name="id">Administracion a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               AdministrationService.DeleteAdministration(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }


        // DELETE api/<controller>/5
        /// <summary>
        /// Devuelve todas las administraciones
        /// </summary>        
        /// <returns></returns>
        public IHttpActionResult GetAll()
        {

            var completeAdministrations = AdministrationService.GetAll();

            if (completeAdministrations == null)
                throw new NotFoundException(ErrorMessages.AdministracionNoEncontrado);

            var dto = Mapper.Map<List<AdministrationResponse>>(completeAdministrations);

            return Ok(dto);
        }

    }
}