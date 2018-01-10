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
using ApiCore.Services.Contracts.Cities;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("City")]
    public class CityController : ApiController
    {

        public ICitiesService CityService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Province>))]
        public IHttpActionResult Get()
        {
            var cities = CityService.GetAll();

            if (cities == null)
                throw new NotFoundException(ErrorMessages.CiudadNoEncontrada);

            var dto = cities;
            
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
        public IHttpActionResult Post(CityRequest city)
        {
            var result = CityService
                .CreateCity(city);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina una ciudad
        /// </summary>
        /// <param name="id">cuidad a eliminar</param>
        /// <returns></returns>

        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();

            try
            {
                CityService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }

        }
    }
}