using Datos.Repositorios;
using PruebaApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transversal.Helpers;

namespace PruebaApi.Controllers
{
    [RoutePrefix("api/Seguridad")]
    public class SeguridadController : ApiController
    {
        private readonly SeguridadRepositorio _seguridadRep = new SeguridadRepositorio();

        #region Login
        [HttpPost]
        public HttpResponseMessage Login([FromBody] UsuarioLogin model)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;
            model.clave = HashHelper.SHA1(model.clave);
            DataSet datos = _seguridadRep.IniciarSesion(model.usuario, model.clave);
            if(datos != null && datos.Tables.Count > 0 && datos.Tables[0].Rows.Count > 0)
            {
                statusCode = HttpStatusCode.OK;
                data = datos;
            }
            else
            {
                statusCode = HttpStatusCode.BadRequest;
                data = new { message = "Imposible iniciar sesion" };
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion
    }
}
