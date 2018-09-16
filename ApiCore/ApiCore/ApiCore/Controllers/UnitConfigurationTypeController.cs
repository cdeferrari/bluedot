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
using ApiCore.Services.Contracts.UnitConfigurationTypes;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de UnitConfigurationTypes
    /// </summary>
    [RoutePrefix("UnitConfigurationTypes")]
    public class UnitConfigurationTypesController : ApiController
    {

        public IUnitConfigurationTypesService UnitConfigurationTypeService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los UnitConfigurationTypes
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<UnitConfigurationType>))]
        public IHttpActionResult Get()
        {
            var UnitConfigurationTypes = UnitConfigurationTypeService.GetAll();

            if (UnitConfigurationTypes == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = UnitConfigurationTypes;
            
            return Ok(dto);
        }
        
        
    }
}