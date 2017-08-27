using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;

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
        [Route("{id}")]        
        [ResponseType(typeof(UserResponse))]
        public string Get(int id)
        {
            var completeUser = UserService.GetById(id);

            if (completeUser == null)
                throw new NotFoundException(ErrorMessages.UserNotFound);

            var dto = Mapper.Map<UsuarioResponse>(completeUser);
            
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
        public void Post(UserRequest user)
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
        public void Put(int id, UserRequest user)
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
        public void Delete(int id)
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