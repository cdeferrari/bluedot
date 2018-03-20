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
using ApiCore.Services.Contracts.SpendTypes;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de SpendTypes
    /// </summary>
    [RoutePrefix("SpendTypes")]
    public class SpendTypeController : ApiController
    {

        public ISpendTypeService SpendTypeService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los SpendTypes
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<SpendType>))]
        public IHttpActionResult Get()
        {
            var SpendTypes = SpendTypeService.GetAll();

            if (SpendTypes == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = SpendTypes;
            
            return Ok(dto);
        }


        
        // POST api/<controller>
        /// <summary>
        /// Inserta un SpendType
        /// </summary>
        /// <param name="SpendType">SpendType a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(SpendTypeRequest SpendType)
        {
            var result = SpendTypeService.CreateSpendType(SpendType);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un SpendType
        /// </summary>
        /// <param name="SpendType">SpendType a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, SpendTypeRequest SpendType)
        {            
            var originalSpendType = SpendTypeService.GetById(id);
            
            var ret = SpendTypeService.UpdateSpendType(originalSpendType, SpendType);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un SpendType
        /// </summary>
        /// <param name="id">SpendType a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               SpendTypeService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}