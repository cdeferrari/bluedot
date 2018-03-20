using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.PatrimonyStatuss;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de PatrimonyStatus
    /// </summary>
    [RoutePrefix("PatrimonyStatus")]
    public class PatrimonyStatusController : ApiController
    {

        public IPatrimonyStatusService PatrimonyStatuservice { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los PatrimonyStatus
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<PatrimonyStatus>))]
        public IHttpActionResult Get()
        {
            var PatrimonyStatus = PatrimonyStatuservice.GetAll();

            if (PatrimonyStatus == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = PatrimonyStatus;
            
            return Ok(dto);
        }


        
        // POST api/<controller>
        /// <summary>
        /// Inserta un PatrimonyStatus
        /// </summary>
        /// <param name="PatrimonyStatus">PatrimonyStatus a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(PatrimonyStatusRequest PatrimonyStatus)
        {
            var result = PatrimonyStatuservice.CreatePatrimonyStatus(PatrimonyStatus);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        
        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un PatrimonyStatus
        /// </summary>
        /// <param name="PatrimonyStatus">PatrimonyStatus a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, PatrimonyStatusRequest PatrimonyStatus)
        {            
            var originalPatrimonyStatus = PatrimonyStatuservice.GetById(id);
            
            var ret = PatrimonyStatuservice.UpdatePatrimonyStatus(originalPatrimonyStatus, PatrimonyStatus);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un PatrimonyStatus
        /// </summary>
        /// <param name="id">PatrimonyStatus a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               PatrimonyStatuservice.DeletePatrimonyStatus(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}