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
using ApiCore.Services.Contracts.BankAccounts;
using ApiCore.DomainModel;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de tickets
    /// </summary>
    [RoutePrefix("BankAccounts")]
    public class BankAccountController : ApiController
    {

        public IBankAccountService BankAccountService { get; set; }



        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<BankAccountResponse>))]
        public IHttpActionResult Get()
        {
            var completeBankAccountList = BankAccountService.GetAll();

            if (completeBankAccountList == null)
                throw new NotFoundException(ErrorMessages.BankAccountNoEncontrado);

            var dto = Mapper.Map<List<BankAccountResponse>>(completeBankAccountList);

            return Ok(dto);
        }



        // GET api/<controller>/5        
        /// <summary>
        /// Devuelve el usuario del id
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns></returns>
        [Route("{id}")]        
        [ResponseType(typeof(BankAccountResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeBankAccount = BankAccountService.GetById(id);

            if (completeBankAccount == null)
                throw new NotFoundException(ErrorMessages.BankAccountNoEncontrado);

            var dto = Mapper.Map<BankAccountResponse>(completeBankAccount);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta un usuario
        /// </summary>
        /// <param name="BankAccount">Usuario a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(BankAccount))]
        public IHttpActionResult Post(BankAccountRequest BankAccount)
        {
            var result = BankAccountService.CreateBankAccount(BankAccount);

            return Created<BankAccount>("", result);

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="BankAccount">Usuario a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, BankAccountRequest BankAccount)
        {            
            var originalBankAccount = BankAccountService.GetById(id);
            
            var ret = BankAccountService.UpdateBankAccount(originalBankAccount, BankAccount);

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
               BankAccountService.DeleteBankAccount(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}