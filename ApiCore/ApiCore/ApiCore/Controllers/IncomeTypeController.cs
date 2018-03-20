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
using ApiCore.Services.Contracts.IncomeTypes;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de IncomeTypes
    /// </summary>
    [RoutePrefix("IncomeTypes")]
    public class IncomeTypeController : ApiController
    {

        public IIncomeTypeService IncomeTypeService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los IncomeTypes
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<IncomeType>))]
        public IHttpActionResult Get()
        {
            var IncomeTypes = IncomeTypeService.GetAll();

            if (IncomeTypes == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = IncomeTypes;
            
            return Ok(dto);
        }


        
        // POST api/<controller>
        /// <summary>
        /// Inserta un IncomeType
        /// </summary>
        /// <param name="IncomeType">IncomeType a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(IncomeTypeRequest IncomeType)
        {
            var result = IncomeTypeService.CreateIncomeType(IncomeType);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un IncomeType
        /// </summary>
        /// <param name="IncomeType">IncomeType a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, IncomeTypeRequest IncomeType)
        {            
            var originalIncomeType = IncomeTypeService.GetById(id);
            
            var ret = IncomeTypeService.UpdateIncomeType(originalIncomeType, IncomeType);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un IncomeType
        /// </summary>
        /// <param name="id">IncomeType a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               IncomeTypeService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}