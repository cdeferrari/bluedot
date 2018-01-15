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
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.LaboralUnion;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("LaboralUnion")]
    public class LaboralUnionController : ApiController
    {

        public ILaboralUnionService LaboralUnionService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los tickets
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<LaboralUnion>))]
        public IHttpActionResult Get()
        {
            var statusList = LaboralUnionService.GetAll();

            if (statusList == null)
                throw new NotFoundException(ErrorMessages.TicketNoEncontrado);            
            
            return Ok(statusList);
        }


        // POST api/<controller>
        /// <summary>
        /// Inserta tipo de pago
        /// </summary>
        /// <param name="PaymentType">Tipo Pago a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(DescriptionRequest LaboralUnion)
        {
            var result = LaboralUnionService.CreateLaboralUnion(LaboralUnion);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un sindicato
        /// </summary>
        /// <param name="id">Sindicato a eliminar</param>
        /// <returns></returns>

        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();

            try
            {
                LaboralUnionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }

        }

    }
}