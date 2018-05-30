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
using ApiCore.Services.Contracts.ElevatorControls;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de ElevatorControls
    /// </summary>
    [RoutePrefix("ElevatorControl")]
    public class ElevatorControlController : ApiController
    {

        public IElevatorControlService ElevatorControlService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los ElevatorControls
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<ElevatorControl>))]
        public IHttpActionResult Get()
        {
            var ElevatorControls = ElevatorControlService.GetAll();

            if (ElevatorControls == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = ElevatorControls;
            
            return Ok(dto);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un gasto
        /// </summary>
        /// <param name="id">id del gasto</param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(ElevatorControl))]
        public IHttpActionResult Get(int id)
        {
            var completeElevatorControl = ElevatorControlService.GetById(id);

            if (completeElevatorControl == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);            

            return Ok(completeElevatorControl);
        }



        // POST api/<controller>
        /// <summary>
        /// Inserta un ElevatorControl
        /// </summary>
        /// <param name="ElevatorControl">ElevatorControl a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ControlRequest ElevatorControl)
        {
            var result = ElevatorControlService.CreateElevatorControl(ElevatorControl);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un ElevatorControl
        /// </summary>
        /// <param name="ElevatorControl">ElevatorControl a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, ControlRequest ElevatorControl)
        {            
            var originalElevatorControl = ElevatorControlService.GetById(id);
            
            var ret = ElevatorControlService.UpdateElevatorControl(originalElevatorControl, ElevatorControl);

            return Ok();
            
        }
        
        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un ElevatorControl
        /// </summary>
        /// <param name="id">ElevatorControl a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               ElevatorControlService.DeleteElevatorControl(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}