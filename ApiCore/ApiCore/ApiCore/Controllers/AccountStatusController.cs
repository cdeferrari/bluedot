using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.AccountStatuss;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de AccountStatus
    /// </summary>
    [RoutePrefix("AccountStatus")]
    public class AccountStatusController : ApiController
    {

        public IAccountStatusService AccountStatuservice { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los AccountStatus
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<AccountStatus>))]
        public IHttpActionResult Get()
        {
            var AccountStatus = AccountStatuservice.GetAll();

            if (AccountStatus == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = AccountStatus;
            
            return Ok(dto);
        }


        
        // POST api/<controller>
        /// <summary>
        /// Inserta un AccountStatus
        /// </summary>
        /// <param name="AccountStatus">AccountStatus a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(AccountStatusRequest AccountStatus)
        {
            var result = AccountStatuservice.CreateAccountStatus(AccountStatus);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        
        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un AccountStatus
        /// </summary>
        /// <param name="AccountStatus">AccountStatus a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, AccountStatusRequest AccountStatus)
        {            
            var originalAccountStatus = AccountStatuservice.GetById(id);
            
            var ret = AccountStatuservice.UpdateAccountStatus(originalAccountStatus, AccountStatus);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un AccountStatus
        /// </summary>
        /// <param name="id">AccountStatus a eliminar</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               AccountStatuservice.DeleteAccountStatus(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}