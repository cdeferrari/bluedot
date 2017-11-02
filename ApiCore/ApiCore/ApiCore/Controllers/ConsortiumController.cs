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
using ApiCore.Services.Contracts.Lists;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de consorcio
    /// </summary>
    [RoutePrefix("Consorcio")]
    public class ConsortiumController : ApiController
    {

        public IConsortiumService ConsortiumService { get; set; }
        public IListService ListService { get; set; }



        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("")]
        [ResponseType(typeof(List<ConsortiumResponse>))]
        public IHttpActionResult Get()
        {
            var completeConsortium = ConsortiumService.GetAll();

            if (completeConsortium == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);

            var dto = Mapper.Map<List<ConsortiumResponse>>(completeConsortium);

            return Ok(dto);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(ConsortiumResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeConsortium = ConsortiumService.GetById(id);

            if (completeConsortium == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);

            var dto = Mapper.Map<ConsortiumResponse>(completeConsortium);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un consorcio
        /// </summary>
        /// <param name="consortium">Consorcio a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ConsortiumRequest consortium)
        {
            var result = ConsortiumService.CreateConsortium(consortium);

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