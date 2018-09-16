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
using ApiCore.Services.Contracts.ConsortiumConfigurationTypes;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de ConsortiumConfigurationTypes
    /// </summary>
    [RoutePrefix("ConsortiumConfigurationTypes")]
    public class ConsortiumConfigurationTypesController : ApiController
    {

        public IConsortiumConfigurationTypesService ConsortiumConfigurationTypeService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los ConsortiumConfigurationTypes
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<ConsortiumConfigurationType>))]
        public IHttpActionResult Get()
        {
            var ConsortiumConfigurationTypes = ConsortiumConfigurationTypeService.GetAll();

            if (ConsortiumConfigurationTypes == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = ConsortiumConfigurationTypes;
            
            return Ok(dto);
        }
        
        
    }
}