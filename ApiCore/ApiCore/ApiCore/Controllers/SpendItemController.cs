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
using ApiCore.Services.Contracts.SpendItems;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("SpendItem")]
    public class SpendItemController : ApiController
    {

        public ISpendItemService SpendItemService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<SpendItem>))]
        public IHttpActionResult Get()
        {
            var items = SpendItemService.GetAll();

            if (items == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);
                        
            return Ok(items);
        }

        
    }
}