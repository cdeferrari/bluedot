using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.Multimedias;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de multimedia
    /// </summary>
    [RoutePrefix("Multimedia")]
    public class MultimediaController : ApiController
    {

        public IMultimediaService MultimediaService { get; set; }



        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("")]
        [ResponseType(typeof(List<Multimedia>))]
        public IHttpActionResult Get()
        {
            var completeMultimedia = MultimediaService.GetAll();

            if (completeMultimedia == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);
                    
            return Ok(completeMultimedia);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(Multimedia))]
        public IHttpActionResult Get(int id)
        {
            var completeMultimedia = MultimediaService.GetById(id);

            if (completeMultimedia == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);

            
            return Ok(completeMultimedia);
        }
        
        // POST api/<controller>
        /// <summary>
        /// Inserta una multimedia
        /// </summary>
        /// <param name="Multimedia">Consorcio a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(MultimediaRequest Multimedia)
        {
            var result = MultimediaService.CreateMultimedia(Multimedia);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica una multimeida
        /// </summary>
        /// <param name="Multimedia">Consorcio a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, MultimediaRequest Multimedia)
        {            
            var originalMultimedia = MultimediaService.GetById(id);
            
            var ret = MultimediaService.UpdateMultimedia(originalMultimedia, Multimedia);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina una multimedia
        /// </summary>
        /// <param name="id">Consorcio a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               MultimediaService.DeleteMultimedia(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}