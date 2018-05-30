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
using ApiCore.Services.Contracts.FireExtinguisherControls;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de FireExtinsguisherControls
    /// </summary>
    [RoutePrefix("FireExtinguisherControl")]
    public class FireExtinsguisherControlController : ApiController
    {

        public IFireExtinguisherControlService FireExtinguisherControlService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los FireExtinsguisherControls
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<FireExtinguisherControl>))]
        public IHttpActionResult Get()
        {
            var FireExtinsguisherControls = FireExtinguisherControlService.GetAll();

            if (FireExtinsguisherControls == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = FireExtinsguisherControls;

            return Ok(dto);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un gasto
        /// </summary>
        /// <param name="id">id del gasto</param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(FireExtinguisherControl))]
        public IHttpActionResult Get(int id)
        {
            var completeFireExtinsguisherControl = FireExtinguisherControlService.GetById(id);

            if (completeFireExtinsguisherControl == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            return Ok(completeFireExtinsguisherControl);
        }



        // POST api/<controller>
        /// <summary>
        /// Inserta un FireExtinsguisherControl
        /// </summary>
        /// <param name="FireExtinsguisherControl">FireExtinsguisherControl a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ControlRequest FireExtinsguisherControl)
        {
            var result = FireExtinguisherControlService.CreateFireExtinguisherControl(FireExtinsguisherControl);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un FireExtinsguisherControl
        /// </summary>
        /// <param name="FireExtinsguisherControl">FireExtinsguisherControl a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, ControlRequest FireExtinsguisherControl)
        {
            var originalFireExtinsguisherControl = FireExtinguisherControlService.GetById(id);

            var ret = FireExtinguisherControlService.UpdateFireExtinguisherControl(originalFireExtinsguisherControl, FireExtinsguisherControl);

            return Ok();

        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un FireExtinsguisherControl
        /// </summary>
        /// <param name="id">FireExtinsguisherControl a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();

            try
            {
                FireExtinguisherControlService.DeleteFireExtinguisherControl(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }

        }
    }
}