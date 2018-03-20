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
using ApiCore.Services.Contracts.Spends;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de Spends
    /// </summary>
    [RoutePrefix("Spends")]
    public class SpendController : ApiController
    {

        public ISpendService SpendService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los Spends
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Spend>))]
        public IHttpActionResult Get()
        {
            var Spends = SpendService.GetAll();

            if (Spends == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = Spends;
            
            return Ok(dto);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un gasto
        /// </summary>
        /// <param name="id">id del gasto</param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(Spend))]
        public IHttpActionResult Get(int id)
        {
            var completeSpend = SpendService.GetById(id);

            if (completeSpend == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);            

            return Ok(completeSpend);
        }



        // POST api/<controller>
        /// <summary>
        /// Inserta un Spend
        /// </summary>
        /// <param name="Spend">Spend a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(SpendRequest Spend)
        {
            var result = SpendService.CreateSpend(Spend);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un Spend
        /// </summary>
        /// <param name="Spend">Spend a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, SpendRequest Spend)
        {            
            var originalSpend = SpendService.GetById(id);
            
            var ret = SpendService.UpdateSpend(originalSpend, Spend);

            return Ok();
            
        }
        
        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un Spend
        /// </summary>
        /// <param name="id">Spend a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               SpendService.DeleteSpend(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}