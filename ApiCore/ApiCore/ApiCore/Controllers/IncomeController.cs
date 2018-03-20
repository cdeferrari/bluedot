using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.Incomes;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de Incomes
    /// </summary>
    [RoutePrefix("Incomes")]
    public class IncomeController : ApiController
    {

        public IIncomeService IncomeService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los Incomes
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Income>))]
        public IHttpActionResult Get()
        {
            var Incomes = IncomeService.GetAll();

            if (Incomes == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = Incomes;
            
            return Ok(dto);
        }


        
        // POST api/<controller>
        /// <summary>
        /// Inserta un Income
        /// </summary>
        /// <param name="Income">Income a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(IncomeRequest Income)
        {
            var result = IncomeService.CreateIncome(Income);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un Income
        /// </summary>
        /// <param name="Income">Income a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, IncomeRequest Income)
        {            
            var originalIncome = IncomeService.GetById(id);
            
            var ret = IncomeService.UpdateIncome(originalIncome, Income);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un Income
        /// </summary>
        /// <param name="id">Income a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               IncomeService.DeleteIncome(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}