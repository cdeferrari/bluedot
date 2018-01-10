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
    [RoutePrefix("Province")]
    public class ProvinceController : ApiController
    {

        public IProvincesService ProvinceService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Province>))]
        public IHttpActionResult Get()
        {
            var provinces = ProvinceService.GetAll();

            if (provinces == null)
                throw new NotFoundException(ErrorMessages.ProvinciaNoEncontrada);

            var dto = provinces;
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta tipo de pago
        /// </summary>
        /// <param name="PaymentType">Tipo Pago a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ProvinceRequest province)
        {
            var result = ProvinceService                
                .CreateProvince(province);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina una provincia
        /// </summary>
        /// <param name="id">provincia a eliminar</param>
        /// <returns></returns>

        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();

            try
            {
                ProvinceService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }

        }

    }
}