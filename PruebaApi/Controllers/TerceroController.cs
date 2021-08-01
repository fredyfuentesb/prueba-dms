using Datos.Repositorios;
using PruebaApi.Helpers;
using PruebaApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using Transversal;
using Transversal.Dtos;

namespace PruebaApi.Controllers
{
    [RoutePrefix("api/Tercero")]
    public class TerceroController : ApiController
    {
        private readonly TercerosRepositorio _tercerosRep = new TercerosRepositorio();
        private readonly Config_NotificacionRepositorio _configNotificacionRep = new Config_NotificacionRepositorio();
        private readonly Variables_NotificacionRepositorio _variablesRep = new Variables_NotificacionRepositorio();

        #region Save
        /// <summary>
        /// Permite crear un tercero
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Save([FromBody] TerceroModel model)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            if (ModelState.IsValid)
            {
                TercerosDto terceroDto = new TercerosDto();
                terceroDto = Mapper<TercerosDto>.Map(model, terceroDto);
                terceroDto.fecha_creacion = DateTime.Now;
                terceroDto.fecha_ultima_modificacion = DateTime.Now;
                int idTerceroCreado = 0;
                if(_tercerosRep.Save(terceroDto, ref idTerceroCreado))
                {
                    try
                    {
                        Config_NotificacionDto notificacion = _configNotificacionRep.FindByType(1);
                        string cuerpo = System.IO.File.ReadAllText(notificacion.ruta);
                        Dictionary<string, string> valores = terceroDto.ObtenerDatos();

                        List<Variables_NotificacionDto> variables = _variablesRep.ListByType(1);
                        foreach(Variables_NotificacionDto variable in variables)
                        {
                            if (variable.destino.Contains(","))
                            {
                                string valor = string.Empty;
                                string[] variablesInternas = variable.destino.Split(',');
                                foreach (string variableInterna in variablesInternas)
                                {
                                    valor += $"{valores[variableInterna]} ";
                                }
                                cuerpo = cuerpo.Replace(variable.origen, valor.Trim());
                            }
                            else
                            {
                                cuerpo = cuerpo.Replace(variable.origen, valores[variable.destino]);
                            }                            
                        }
                        NotificadorSMTP notificadorSmtp = new NotificadorSMTP();

                        List<string> destinatario = new List<string>();
                        destinatario.Add(terceroDto.email);
                        MailMessage mensaje = notificadorSmtp.CrearMessage(destinatario, "Contacto creado en prueba-dms", cuerpo, true, ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["email"]);
                        notificadorSmtp.EnviarMensajeCorreo(mensaje, 1, ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["clave_email"], "smtp.gmail.com", 587, true);
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                    }
                    model.id = idTerceroCreado;
                    statusCode = HttpStatusCode.OK;
                    data = new { tercero = model, message = "Información guardada correctamente" };
                }
                else
                {
                    statusCode = HttpStatusCode.BadRequest;
                    data = new { message = "Información no guardada" };
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

        #region Update
        /// <summary>
        /// Permite actualizar los datos de un tercero
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage Update([FromBody] TerceroModel model)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            if (ModelState.IsValid)
            {
                TercerosDto terceroOriginal = _tercerosRep.FindById(model.id);
                TercerosDto terceroDto = new TercerosDto();
                terceroDto = Mapper<TercerosDto>.Map(model, terceroDto);
                terceroDto.fecha_creacion = terceroOriginal.fecha_creacion;
                terceroDto.fecha_ultima_modificacion = DateTime.Now;
                if (_tercerosRep.Update(terceroDto))
                {
                    try
                    {
                        Config_NotificacionDto notificacion = _configNotificacionRep.FindByType(2);
                        string cuerpo = System.IO.File.ReadAllText(notificacion.ruta);
                        //Se obtienen todos cambios realizados
                        List<Variacion> variaciones = terceroOriginal.ComparacionDetallada(terceroDto);
                        Dictionary<string, string> valores = terceroDto.ObtenerDatos();

                        List<Variables_NotificacionDto> variables = _variablesRep.ListByType(2);
                        foreach (Variables_NotificacionDto variable in variables)
                        {
                            if (!variable.origen.Equals("{{variantes}}"))
                            {
                                if (variable.destino.Contains(","))
                                {
                                    string valor = string.Empty;
                                    string[] variablesInternas = variable.destino.Split(',');
                                    foreach (string variableInterna in variablesInternas)
                                    {
                                        valor += $"{valores[variableInterna]} ";
                                    }
                                    cuerpo = cuerpo.Replace(variable.origen, valor.Trim());
                                }
                                else
                                {
                                    cuerpo = cuerpo.Replace(variable.origen, valores[variable.destino]);
                                }
                            }                            
                        }

                        string variacionesTextos = string.Empty;
                        foreach(Variacion variacion in variaciones)
                        {
                            variacionesTextos += $"<ul><li>Cambio: <b>{variacion.propiedad}</b></li><li>Valor anterior: <b>{variacion.valorA}</b></li><li>Nuevo Valor <b>{variacion.valorB}</b></li></ul>";
                        }
                        cuerpo = cuerpo.Replace("{{variantes}}", variacionesTextos);

                        NotificadorSMTP notificadorSmtp = new NotificadorSMTP();

                        List<string> destinatario = new List<string>();
                        destinatario.Add(terceroDto.email);
                        MailMessage mensaje = notificadorSmtp.CrearMessage(destinatario, "Actualizacion de Contacto en prueba-dms", cuerpo, true, ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["email"]);
                        notificadorSmtp.EnviarMensajeCorreo(mensaje, 1, ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["clave_email"], "smtp.gmail.com", 587, true);
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                    }                    
                    statusCode = HttpStatusCode.OK;
                    data = new { tercero = terceroDto, message = "Información guardada correctamente" };
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

        #region List
        /// <summary>
        /// Lista todos los terceros guardados en base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage List()
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            List<TercerosDto> terceros = _tercerosRep.List();
            if (terceros.Any())
            {
                statusCode = HttpStatusCode.OK;
                data = terceros;
            }
            else
            {
                statusCode = HttpStatusCode.NoContent;
                data = new { message = "No se encontraron registros guardados" };
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion

        #region FindById
        /// <summary>
        /// devuelve la informacion del tercero especificado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage FindById(int id)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            TercerosDto tercero = _tercerosRep.FindById(id);
            if (tercero != null)
            {
                statusCode = HttpStatusCode.OK;
                data = tercero;
            }
            else
            {
                statusCode = HttpStatusCode.OK;
                data = new { message = "No se encontraron registros guardados" };
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion

        #region Delete
        /// <summary>
        /// Permite eliminar los datos de un tercero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            TercerosDto tercero = _tercerosRep.FindById(id);
            if (tercero != null)
            {
                if (_tercerosRep.Delete(tercero))
                {
                    statusCode = HttpStatusCode.OK;
                    data = tercero;
                }
                else
                {
                    statusCode = HttpStatusCode.BadRequest;
                    data = new { message = "No se encontraron registros guardados" };
                }
            }
            else
            {
                statusCode = HttpStatusCode.BadRequest;
                data = new { message = "No se encontraron registros guardados" };
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion
    }
}
