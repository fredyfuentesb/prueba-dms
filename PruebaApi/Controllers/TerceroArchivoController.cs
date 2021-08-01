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
    [RoutePrefix("api/TerceroArchivo")]
    public class TerceroArchivoController : ApiController
    {
        private readonly Tercero_ArchivosRepositorio _terceroArchivoRep = new Tercero_ArchivosRepositorio();

        #region Save
        /// <summary>
        /// Permite guardar un archivo y asociarlo a un tercero
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Save([FromBody] TerceroArchivoModel model)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            if (ModelState.IsValid)
            {
                Tercero_ArchivosDto terceroArchivoDto = new Tercero_ArchivosDto();
                terceroArchivoDto = Mapper<Tercero_ArchivosDto>.Map(model, terceroArchivoDto);
                int idTerceroArchivo = 0;
                if(_terceroArchivoRep.Save(terceroArchivoDto, ref idTerceroArchivo))
                {
                    model.id = idTerceroArchivo;
                    statusCode = HttpStatusCode.OK;
                    data = model;
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

        #region List
        /// <summary>
        /// Lista todos los archivos que tenga asociado un tercero
        /// </summary>
        /// <param name="id_tercero"></param>
        /// <returns></returns>
        [HttpGet, Route("{id_tercero}/List")]
        public HttpResponseMessage List(int id_tercero)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            List<Tercero_ArchivosDto> archivos = _terceroArchivoRep.ListWithUser(id_tercero);

            if (archivos.Any())
            {
                statusCode = HttpStatusCode.OK;
                data = archivos;
            }
            else
            {
                statusCode = HttpStatusCode.NoContent;
                data = new { message = "No se encontraron registros guardados" };
            }


            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion

        #region Delete
        /// <summary>
        /// Elimina logicamente (Pero no fisicamente) un archivo asociado a un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete(int id) 
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            Tercero_ArchivosDto terceroArchivo = _terceroArchivoRep.FindById(id);

            if(terceroArchivo != null)
            {
                if (_terceroArchivoRep.Delete(terceroArchivo))
                {
                    statusCode = HttpStatusCode.OK;
                    data = terceroArchivo;
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
