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
using ApiCore.Services.Contracts.Users;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("Users")]
    public class UserController : ApiController
    {

        public IUserService UserService { get; set; }



        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<UserResponse>))]
        public IHttpActionResult Get()
        {
            var completeUserList = UserService.GetAll();

            if (completeUserList == null)
                throw new NotFoundException(ErrorMessages.UserNoEncontrado);

            var dto = Mapper.Map<List<UserResponse>>(completeUserList);

            return Ok(dto);
        }



        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("{id}")]        
        [ResponseType(typeof(UserResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeUser = UserService.GetById(id);

            if (completeUser == null)
                throw new NotFoundException(ErrorMessages.UserNoEncontrado);

            var dto = Mapper.Map<UserResponse>(completeUser);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un usuario
        /// </summary>
        /// <param name="user">Usuario a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(UserRequest user)
        {
            var result = UserService.CreateUser(user);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="user">Usuario a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, UserRequest user)
        {            
            var originalUser = UserService.GetById(id);
            
            var ret = UserService.UpdateUser(originalUser, user);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">Usuario a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               UserService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}