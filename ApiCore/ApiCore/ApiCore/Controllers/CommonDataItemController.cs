using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.CommonDataItems;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("CommonDataItem")]
    public class CommonDataItemController : ApiController
    {

        public ICommonDataItemsService CommonDataItemService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Item>))]
        public IHttpActionResult Get()
        {
            var CommonDataItemList = CommonDataItemService.GetAll();

            if (CommonDataItemList == null)
                throw new NotFoundException(ErrorMessages.NotFound);            
            
            return Ok(CommonDataItemList);
        }

        
    }
}