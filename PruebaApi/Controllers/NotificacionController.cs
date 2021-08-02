using Datos.Repositorios;
using PruebaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transversal;
using Transversal.Dtos;

namespace PruebaApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Notificacion")]
    public class NotificacionController : ApiController
    {
        private readonly Config_NotificacionRepositorio _configNotificacionRep = new Config_NotificacionRepositorio();
        private readonly Tipo_NotificacionesRepositorio _tipoNotificacionRep = new Tipo_NotificacionesRepositorio();
        private readonly Variables_NotificacionRepositorio _variableRep = new Variables_NotificacionRepositorio();

        #region Tipos
        [HttpGet, Route("Tipos")]
        public HttpResponseMessage Tipos()
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            List<Tipo_NotificacionesDto> tipos = _tipoNotificacionRep.List();
            if (tipos.Any())
            {
                statusCode = HttpStatusCode.OK;
                data = tipos;
            }
            else
            {
                statusCode = HttpStatusCode.NoContent;
                data = new { message = "No se encontraron registros guardados" };
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion

        #region Save
        [HttpPost]
        public HttpResponseMessage Save([FromBody] ConfigNotificacionModel model)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            if (ModelState.IsValid)
            {
                Config_NotificacionDto configNotificacionDto = _configNotificacionRep.FindByType(model.id_tipo_notificacion);
                if(configNotificacionDto != null)
                {
                    int id = configNotificacionDto.id;
                    configNotificacionDto = Mapper<Config_NotificacionDto>.Map(model, configNotificacionDto);
                    configNotificacionDto.id = id;
                    if (_configNotificacionRep.Update(configNotificacionDto))
                    {
                        statusCode = HttpStatusCode.OK;
                        data = configNotificacionDto;
                    }
                    else
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        data = new { message = "Información no guardada" };
                    }
                }
                else
                {
                    configNotificacionDto = new Config_NotificacionDto();
                    configNotificacionDto = Mapper<Config_NotificacionDto>.Map(model, configNotificacionDto);
                    if (_configNotificacionRep.Save(configNotificacionDto))
                    {
                        statusCode = HttpStatusCode.OK;
                        data = configNotificacionDto;
                    }
                    else
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        data = new { message = "Información no guardada" };
                    }
                }
            }
            else
            {
                List<string> errors = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)
                                        .ToList();
                statusCode = HttpStatusCode.BadRequest;
                return Request.CreateResponse(statusCode, errors, "application/json");
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion

        #region Variables
        [HttpGet, Route("Variables")]
        public HttpResponseMessage Variables(int tipo)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            List<Variables_NotificacionDto> variables = _variableRep.ListByType(tipo);

            if (variables.Any())
            {
                statusCode = HttpStatusCode.OK;
                data = variables;
            }
            else
            {
                statusCode = HttpStatusCode.NoContent;
                data = new { message = "No se encontraron registros" };
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion

    }
}
