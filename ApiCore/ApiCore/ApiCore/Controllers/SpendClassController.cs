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
using ApiCore.Services.Contracts.SpendClasss;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de SpendClasss
    /// </summary>
    [RoutePrefix("SpendClass")]
    public class SpendClassController : ApiController
    {

        public ISpendClassService SpendClassService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los SpendClasss
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<SpendClass>))]
        public IHttpActionResult Get()
        {
            var SpendClasss = SpendClassService.GetAll();

            if (SpendClasss == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = SpendClasss;
            
            return Ok(dto);
        }
        
    }
}