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
using ApiCore.Services.Contracts.Consortiums;
using ApiCore.Services.Contracts.Unit;
using ApiCore.Services.Contracts.Owners;
using ApiCore.Services.Contracts.Renters;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de consorcio
    /// </summary>
    [RoutePrefix("Unidad")]
    public class UnitController : ApiController
    {

        public virtual IUnitService UnitService { get; set; }
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual IOwnerService OwnerService { get; set; }
        public virtual IRenterService RenterService { get; set; }


        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("")]
        [ResponseType(typeof(List<UnitResponse>))]
        public IHttpActionResult Get()
        {
            var completeUnit = UnitService.GetAll();

            if (completeUnit == null)
                throw new NotFoundException(ErrorMessages.UnidadNoEncontrada);

            var dto = Mapper.Map<List<UnitResponse>>(completeUnit);

            return Ok(dto);
        }



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
        public IHttpActionResult Post(FunctionalUnitRequest unit)
        {
            var result = UnitService.CreateUnit(unit);
            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un consorcio
        /// </summary>
        /// <param name="consortium">Consorcio a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, FunctionalUnitRequest unit)
        {            
            var originalUnit = UnitService.GetById(id);            
            var ret = UnitService.UpdateUnit(originalUnit, unit);
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
               UnitService.DeleteUnit(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}