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
using ApiCore.Services.Contracts.Area;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de Areass
    /// </summary>
    [RoutePrefix("Area")]
    public class AreaController : ApiController
    {

        public IAreaService AreaService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los Areass
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Area>))]
        public IHttpActionResult Get()
        {
            var Areass = AreaService.GetAll();

            if (Areass == null)
                throw new NotFoundException(ErrorMessages.AreaNoEncontrada);

            var dto = Areass;
            
            return Ok(dto);
        }
        
    }
}