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
using ApiCore.Services.Contracts.Consortium;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de consorcio
    /// </summary>
    [RoutePrefix("Unidad")]
    public class UnitController : ApiController
    {

        public IUnitService UnitService { get; set; }
     

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(UnitResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeUnit = UnitService.GetById(id);

            if (completeUnit == null)
                throw new NotFoundException(ErrorMessages.UnidadNoEncontrada);

            var dto = Mapper.Map<UnitResponse>(completeUnit);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta una Unidad
        /// </summary>
        /// <param name="unit">Unidad a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ConsortiumRequest consortium)
        {
            var result = ConsortiumService.CreateConsortiumconsortium);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un consorcio
        /// </summary>
        /// <param name="consortium">Consorcio a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, ConsortiumRequest consortium)
        {            
            var originalConsortium = ConsortiumService.GetById(id);
            
            var ret = ConsortiumService.UpdateConsortium(originalConsortium, consortium);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un consorcio
        /// </summary>
        /// <param name="id">Consorcio a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               ConsortiumService.DeleteConsortium(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}