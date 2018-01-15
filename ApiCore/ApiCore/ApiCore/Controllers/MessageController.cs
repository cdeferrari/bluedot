using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using ApiCore.Dtos.Response;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using AutoMapper;
using ApiCore.Services.Contracts.Messages;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("message")]
    public class MessageController : ApiController
    {

        public IMessageService MessageService { get; set; }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<MessageResponse>))]
        public IHttpActionResult Get()
        {
            var completeMessageList = MessageService.GetAll();

            if (completeMessageList == null)
                throw new NotFoundException(ErrorMessages.MensajeNoEncontrado);

            var dto = Mapper.Map<List<MessageResponse>>(completeMessageList);

            return Ok(dto);
        }


        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("{id}")]        
        [ResponseType(typeof(MessageResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeMessage = MessageService.GetById(id);

            if (completeMessage == null)
                throw new NotFoundException(ErrorMessages.MensajeNoEncontrado);

            var dto = Mapper.Map<MessageResponse>(completeMessage);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un usuario
        /// </summary>
        /// <param name="Message">Usuario a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(MessageRequest Message)
        {
            var result = MessageService.CreateMessage(Message);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="Message">Usuario a modificar</param>
        /// <returns></returns>
        //[Route("{id}")]
        //public IHttpActionResult Put(int id, MessageRequest Message)
        //{            
        //    var originalMessage = MessageService.GetById(id);
            
        //    var ret = MessageService.UpdateMessage(originalMessage, Message);

        //    return Ok();
            
        //}

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">Usuario a eliminar</param>
        /// <returns></returns>        
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               MessageService.DeleteMessage(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}