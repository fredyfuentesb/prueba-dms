using Datos.Repositorios;
using PruebaApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transversal;
using Transversal.Dtos;
using Transversal.Helpers;

namespace PruebaApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        private readonly UsuariosRepositorio _usuariosRep = new UsuariosRepositorio();
        private readonly TercerosRepositorio _tercerosRep = new TercerosRepositorio();

        #region Save
        /// <summary>
        /// Permite crear un usuario en el sistema, pero tambien crea un tercero con los datos complementarios
        /// </summary>
        /// <param name="model">Usuario a crear</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Save([FromBody] UsuarioModel model)
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
                _tercerosRep.Save(terceroDto, ref idTerceroCreado);
                if (idTerceroCreado != 0)
                {
                    int idUsuario = 0;
                    UsuariosDto usuarioDto = new UsuariosDto();
                    usuarioDto = Mapper<UsuariosDto>.Map(model, usuarioDto);
                    usuarioDto.id_tercero = idTerceroCreado;
                    usuarioDto.clave = HashHelper.SHA1(usuarioDto.clave);
                    if (_usuariosRep.Save(usuarioDto, ref idUsuario))
                    {
                        statusCode = HttpStatusCode.Created;
                        model.id_tercero = idTerceroCreado;
                        model.id = idUsuario;
                        model.clave = usuarioDto.clave;
                        data = model;
                    }
                    else
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        data = new { message = "Se ha producido un error en la creacion del usuario, intente mas tarde" };
                    }
                }
                else
                {
                    statusCode = HttpStatusCode.BadRequest;
                    data = new { message = "Información no guardada, por favor verifique la información suministrada" };
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
        /// Permite actulizar el nombre del usuario y el estado
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage Update([FromBody] UsuarioModel model)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            if (ModelState.IsValid)
            {
                UsuariosDto usuarioDto = _usuariosRep.FindById(model.id);
                usuarioDto.usuario = model.usuario;
                usuarioDto.activo = model.activo;
                if (_usuariosRep.Update(usuarioDto))
                {
                    statusCode = HttpStatusCode.OK;
                    model.clave = string.Empty;
                    model.id_tercero = usuarioDto.id_tercero;
                    data = model;
                }
                else
                {
                    statusCode = HttpStatusCode.BadRequest;
                    data = new { message = "Información no guardada, por favor verifique la información suministrada" };
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
        /// informacion basica de los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage List()
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            DataSet usuarios = _usuariosRep.ListAllUsers();

            if(usuarios != null)
            {
                statusCode = HttpStatusCode.OK;
                data = usuarios;
            }
            else
            {
                statusCode = HttpStatusCode.NoContent;
                data = new { message = "No se encontraron registros guardados" };
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion

        #region CambiarClave
        /// <summary>
        /// Permite realizar el cambio de clave de los usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut, Route("CambiarClave")]
        public HttpResponseMessage CambiarClave([FromBody] UsuarioCambioClaveModel model)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            if (ModelState.IsValid)
            {
                UsuariosDto usuarioDto = _usuariosRep.FindById(model.id);
                usuarioDto.usuario = model.usuario;
                usuarioDto.clave = HashHelper.SHA1(model.clave);
                if (_usuariosRep.Update(usuarioDto))
                {
                    statusCode = HttpStatusCode.OK;
                    model.clave = string.Empty;
                    data = model;
                }
                else
                {
                    statusCode = HttpStatusCode.BadRequest;
                    data = new { message = "Información no guardada, por favor verifique la información suministrada" };
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
    }
}
