﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.AccountStatuss;
using System.Linq;
using ApiCore.Services.Contracts.Consortiums;
using ApiCore.Services.Contracts.Unit;
using ApiCore.Dtos.Response;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de AccountStatus
    /// </summary>
    [RoutePrefix("AccountStatus")]
    public class AccountStatusController : ApiController
    {

        public IAccountStatusService AccountStatusService { get; set; }
        public IConsortiumService ConsortiumService { get; set; }
        public IUnitService UnitService { get; set; }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve todos los AccountStatus
        /// </summary>        
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<AccountStatus>))]
        public IHttpActionResult Get()
        {
            var AccountStatus = AccountStatusService.GetAll();

            if (AccountStatus == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            var dto = AccountStatus;
            
            return Ok(dto);
        }

        [Route("{id}")]
        [ResponseType(typeof(AccountStatus))]
        public IHttpActionResult Get(int id)
        {
            var completeManager = AccountStatusService.GetById(id);

            if (completeManager == null)
                throw new NotFoundException(ErrorMessages.TrabajadorNoEncontrado);

            //var dto = Mapper.Map<AccountStatusRequest>(completeManager);

            return Ok(completeManager);
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
            if(AccountStatus.Id > 0)
            {
                var originalAccountStatus = AccountStatusService.GetById(AccountStatus.Id);

                var ret = AccountStatusService.UpdateAccountStatus(originalAccountStatus, AccountStatus);

                return Ok();
            }
            else
            {
                var unit = this.UnitService.GetById(AccountStatus.UnitId);
                var consortium = this.ConsortiumService.GetAll().Where(x => x.Ownership.Id == unit.Ownership.Id).FirstOrDefault();

                if (consortium != null)
                {
                    var result = AccountStatusService.CreateAccountStatus(AccountStatus);
                    return Created<Entidad>("", new Entidad { Id = result.Id });
                }

                return NotFound();
            }
            
            
        }

        
        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un AccountStatus
        /// </summary>
        /// <param name="AccountStatus">AccountStatus a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, AccountStatusRequest AccountStatus)
        {            
            var originalAccountStatus = AccountStatusService.GetById(id);
            
            var ret = AccountStatusService.UpdateAccountStatus(originalAccountStatus, AccountStatus);

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
               AccountStatusService.DeleteAccountStatus(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }

        private bool MonthClosed(Consortium consortium, int month)
        {

            var accountsStatus = this.AccountStatusService.GetByUnitId(consortium.Ownership.FunctionalUnits.FirstOrDefault().Id)
                .Where(x => x.StatusDate.Month == month && x.StatusDate.Year == DateTime.Now.Year);

            return accountsStatus.Any(x => !x.IsPayment());
        }
    }
}