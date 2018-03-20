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
using ApiCore.Services.Contracts.Bills;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de Bills
    /// </summary>
    [RoutePrefix("Bills")]
    public class BillController : ApiController
    {

        public IBillService BillService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los Bills
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Bill>))]
        public IHttpActionResult Get()
        {
            var Bills = BillService.GetAll();

            if (Bills == null)
                throw new NotFoundException(ErrorMessages.FacturaNoEncontrada);

            var dto = Bills;
            
            return Ok(dto);
        }


        
        // POST api/<controller>
        /// <summary>
        /// Inserta un Bill
        /// </summary>
        /// <param name="Bill">Bill a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(BillRequest Bill)
        {
            var result = BillService.CreateBill(Bill);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un Bill
        /// </summary>
        /// <param name="Bill">Bill a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, BillRequest Bill)
        {            
            var originalBill = BillService.GetById(id);
            
            var ret = BillService.UpdateBill(originalBill, Bill);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un Bill
        /// </summary>
        /// <param name="id">Bill a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               BillService.DeleteBill(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}