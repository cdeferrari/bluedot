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
using ApiCore.Services.Contracts.Consortiums;
using ApiCore.Services.Contracts.Lists;
using ApiCore.Services.Contracts.Tickets;
using ApiCore.DomainModel;
using ApiCore.Services.Contracts.Incomes;
using ApiCore.Services.Contracts.PatrimonyStatuss;
using ApiCore.Services.Contracts.Spends;
using ApiCore.Services.Contracts.SpendTypes;
using ApiCore.Services.Contracts.ConsortiumConfigurations;
using ApiCore.Services.Contracts.AccountStatuss;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de consorcio
    /// </summary>
    [RoutePrefix("Consorcio")]
    public class ConsortiumController : ApiController
    {

        public IAccountStatusService AccountStatusService { get; set; }
        public IConsortiumService ConsortiumService { get; set; }
        public IListService ListService { get; set; }

        public ITicketService TicketService { get; set; }        
        public IIncomeService IncomeService { get; set; }
        public IConsortiumConfigurationsService ConsortiumConfigurationService { get; set; }
        public IPatrimonyStatusService PatrimonyStatusService { get; set; }
        public ISpendService SpendService { get; set; }
        

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("")]
        [ResponseType(typeof(List<ConsortiumResponse>))]
        public IHttpActionResult Get()
        {
            var completeConsortium = ConsortiumService.GetAll();

            if (completeConsortium == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);

            var dto = Mapper.Map<List<ConsortiumResponse>>(completeConsortium);

            return Ok(dto);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(ConsortiumResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeConsortium = ConsortiumService.GetById(id);

            if (completeConsortium == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);

            var dto = Mapper.Map<ConsortiumResponse>(completeConsortium);
            
            return Ok(dto);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Devuelve checklists del consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}/Checklists")]        
        [ResponseType(typeof(List<ListResponse>))]
        public IHttpActionResult GetChecklist(int id)
        {

            var completeTaskList = ListService.GetByConsortium(id);

            if (completeTaskList == null)
                throw new NotFoundException(ErrorMessages.ConsorcioNoEncontrado);

            var dto = Mapper.Map<List<ListResponse>>(completeTaskList);

            return Ok(dto);
        }


        // POST api/<controller>/5
        /// <summary>
        /// Cierra el mes
        /// </summary>
        /// <param name="consortiumId">Consorcio a cerrar</param>
        /// <returns></returns>
        [Route("{id}/CloseMonth")]
        public IHttpActionResult CloseMonth(int id)
        {
            var result = this.PatrimonyStatusService.RegisterMonth(id);

            return Ok();

        }

        // POST api/<controller>/5
        /// <summary>
        /// Cierra el mes
        /// </summary>
        /// <param name="id">Consorcio a cerrar</param>
        /// <param name="month">mes a cerrar</param>
        /// <returns></returns>
        [Route("{id}/RegisterUnitsMonthDebt/{month}")]
        public IHttpActionResult RegisterUnitsMonthDebt(int id, int month)
        {
            var consortium = this.ConsortiumService.GetById(id);

            if (!this.MonthClosed(consortium, month))
            {
                foreach (var unit in consortium.Ownership.FunctionalUnits)
                {
                    var result = this.AccountStatusService.RegisterMonth(unit.Id, month);
                }
            }
            
            return Ok();

        }


        private bool MonthClosed(Consortium consortium, int month)
        {

            var accountsStatus = this.AccountStatusService.GetByUnitId(consortium.Ownership.FunctionalUnits.FirstOrDefault().Id)
                .Where(x => x.StatusDate.Month == month && x.StatusDate.Year == DateTime.Now.Year);

            return accountsStatus.Any(x => !x.IsPayment());
        }


        // GET api/<controller>/5
        /// <summary>
        /// Devuelve tickets del consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}/Tickets")]
        [ResponseType(typeof(List<TicketResponse>))]
        public IHttpActionResult GetTickets(int id)
        {

            var completeTaskList = TicketService.GetByConsortiumId(id);

            if (completeTaskList == null)
                throw new NotFoundException(ErrorMessages.TicketNoEncontrado);

            var dto = Mapper.Map<List<TicketResponse>>(completeTaskList);

            return Ok(dto);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve ingresos del consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}/Incomes")]
        [ResponseType(typeof(List<Income>))]
        public IHttpActionResult GetIncomes(int id, string startDate, string endDate)
        {

            var dstartDate = DateTime.ParseExact(startDate, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            var dendDate = DateTime.ParseExact(startDate, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

            var completeIncomeList = IncomeService.GetByConsortiumId(id, dstartDate, dendDate);

            if (completeIncomeList == null)
                throw new NotFoundException(ErrorMessages.IngresoNoEncontrado);
            
            return Ok(completeIncomeList);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Devuelve ingresos del consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}/ConsortiumConfiguration")]
        [ResponseType(typeof(List<ConsortiumConfiguration>))]
        public IHttpActionResult GetConsortiumConfiguration(int id, string startDate, string endDate)
        {

            var dstartDate = DateTime.ParseExact(startDate, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            var dendDate = DateTime.ParseExact(endDate, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

            var completeConfigurationList = ConsortiumConfigurationService.GetByConsortiumId(id, dstartDate, dendDate);

            if (completeConfigurationList == null)
                throw new NotFoundException(ErrorMessages.IngresoNoEncontrado);

            return Ok(completeConfigurationList);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve el estado Patrimonial del consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}/PatrimonyStatus")]
        [ResponseType(typeof(List<PatrimonyStatus>))]
        public IHttpActionResult GetPatrimonyStatus(int id)
        {

            var completePatrimonyStatusList = PatrimonyStatusService.GetByConsortiumId(id);

            if (completePatrimonyStatusList == null)
                throw new NotFoundException(ErrorMessages.PatrimonioNoEncontrado);

            return Ok(completePatrimonyStatusList);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve los gastos del consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}/Spend")]
        [ResponseType(typeof(List<Spend>))]
        public IHttpActionResult GetSpend(int id, string startDate, string endDate)
        {
            
            var dstartDate = DateTime.ParseExact(startDate, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            var dendDate = DateTime.ParseExact(endDate, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

            var completeSpendList = SpendService.GetByConsortiumId(id, dstartDate, dendDate);

            if (completeSpendList == null)
                throw new NotFoundException(ErrorMessages.GastoNoEncontrado);

            return Ok(completeSpendList);
        }



        // GET api/<controller>/5
        /// <summary>
        /// Devuelve los gastos del consorcio
        /// </summary>
        /// <param name="id">id del Consorcio</param>
        /// <param name="month">mes del balanceo</param>
        /// <returns></returns>

        [Route("{id}/ConsortiumAccountStatusSummary/{month}")]
        [ResponseType(typeof(List<UnitAccountStatusSummary>))]
        public IHttpActionResult GetConsortiumAccountStatusSummary(int id, int month)
        {
            
            var completeResume = AccountStatusService.GetConsortiumSummary(id, month);

            if (completeResume == null)
                throw new NotFoundException(ErrorMessages.UnidadNoEncontrada);

            return Ok(completeResume);
        }


        // POST api/<controller>
        /// <summary>
        /// Inserta un consorcio
        /// </summary>
        /// <param name="consortium">Consorcio a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(ConsortiumRequest consortium)
        {
            var result = ConsortiumService.CreateConsortium(consortium);

            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un consorcio
        /// </summary>
        /// <param name="consortium">Consorcio a modificar</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, ConsortiumRequest consortium)
        {            
            var originalConsortium = ConsortiumService.GetById(id);
            
            var ret = ConsortiumService.UpdateConsortium(originalConsortium, consortium);

            return Ok();
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un consorcio
        /// </summary>
        /// <param name="id">Consorcio a eliminar</param>
        /// <returns></returns>

        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               ConsortiumService.DeleteConsortium(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}