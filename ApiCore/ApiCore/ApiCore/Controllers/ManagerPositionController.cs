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
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.ManagerPositions;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de posicion de encargado
    /// </summary>
    [RoutePrefix("ManagerPosition")]
    public class ManagerPositionController : ApiController
    {

        public IManagerPositionService ManagerPositionService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todas las posiciones
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<ManagerPosition>))]
        public IHttpActionResult Get()
        {
            var ManagerPositionsList = ManagerPositionService.GetAll();

            if (ManagerPositionsList == null)
                throw new NotFoundException(ErrorMessages.NotFound);            
            
            return Ok(ManagerPositionsList);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta posicion
        /// </summary>
        /// <param name="ManagerPosition">posicion a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(DescriptionRequest ManagerPosition)
        {
            var result = ManagerPositionService.CreateManagerPosition(ManagerPosition);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina una posicion de manager
        /// </summary>
        /// <param name="id">Posicion a eliminar</param>
        /// <returns></returns>

        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();

            try
            {
                ManagerPositionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }

        }


    }
}