using Datos.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PruebaApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Estadisticas")]
    public class EstadisticasController : ApiController
    {
        private readonly EstadisticasKPIRepositorio _estadisticasRep = new EstadisticasKPIRepositorio();

        #region kpi
        /// <summary>
        /// Accion de la api para las estadisticas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Kpi()
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            DataSet kpi = _estadisticasRep.Kpi();
            if(kpi != null)
            {
                statusCode = HttpStatusCode.OK;
                data = kpi;
            }
            else
            {
                statusCode = HttpStatusCode.BadRequest;
                data = new { message = "No se logro consultar las estadisticas" };
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion

    }
}
