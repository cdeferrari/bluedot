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
using ApiCore.Services.Contracts.Tickets;
using ApiCore.Services.Contracts.Priorities;
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.Provinces;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("Country")]
    public class CountryController : ApiController
    {

        public IProvincesService ProvinceService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("{id}/Provinces")]
        [ResponseType(typeof(IList<Province>))]
        public IHttpActionResult Get(int id)
        {
            var provinces = ProvinceService.GetByCountryId(id);

            if (provinces == null)
                throw new NotFoundException(ErrorMessages.PropiedadNoEncontrada);

            var dto = provinces;
            
            return Ok(dto);
        }

        
    }
}