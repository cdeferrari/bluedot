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
using ApiCore.Services.Contracts.Owners;
using ApiCore.Services.Contracts.Renters;
using ApiCore.Services.Contracts.CommonDatas;

namespace ApiCore.Controllers
{
    /// <summary>
    /// Controlador de datos comunes
    /// </summary>
    [RoutePrefix("CommonData")]
    public class CommonDataController : ApiController
    {

        public virtual ICommonDataService CommonDataService { get; set; }        
        public virtual IOwnerService OwnerService { get; set; }
        

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("")]
        [ResponseType(typeof(List<CommonDataResponse>))]
        public IHttpActionResult Get()
        {
            var completeCommonData = CommonDataService.GetAll();

            if (completeCommonData == null)
                throw new NotFoundException(ErrorMessages.NotFound);

            var dto = Mapper.Map<List<CommonDataResponse>>(completeCommonData);

            return Ok(dto);
        }



        // GET api/<controller>/5
        /// <summary>
        /// Devuelve un consorcio
        /// </summary>
        /// <param name="consorcio">id del Consorcio</param>
        /// <returns></returns>

        [Route("{id}")]        
        [ResponseType(typeof(CommonDataResponse))]
        public IHttpActionResult Get(int id)
        {
            var completeCommonData = CommonDataService.GetById(id);

            if (completeCommonData == null)
                throw new NotFoundException(ErrorMessages.NotFound);

            var dto = Mapper.Map<CommonDataResponse>(completeCommonData);
            
            return Ok(dto);
        }

        // POST api/<controller>
        /// <summary>
        /// Inserta una CommonData
        /// </summary>
        /// <param name="CommonData">CommonData a insertar</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult Post(CommonDataRequest CommonData)
        {
            var result = CommonDataService.CreateCommonData(CommonData);
            return Created<Entidad>("", new Entidad { Id = result.Id });

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Modifica un consorcio
        /// </summary>
        /// <param name="consortium">Consorcio a modificar</param>
        /// <returns></returns>
        [Route("{id}")]
        public IHttpActionResult Put(int id, CommonDataRequest CommonData)
        {            
            var originalCommonData = CommonDataService.GetById(id);            
            var ret = CommonDataService.UpdateCommonData(originalCommonData, CommonData);
            return Ok();            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Elimina un consorcio
        /// </summary>
        /// <param name="id">Consorcio a eliminar</param>
        /// <returns></returns>
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest();
            
            try
            {
               CommonDataService.DeleteCommonData(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
            
        }
    }
}