using Implementacion.Implementacion;
using PruebaWeb.Tags;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PruebaWeb.Controllers
{
    [Autenticado]
    public class EstadisticaController : Controller
    {
        private readonly EstadisticaAplicacion _estadisticaApp = new EstadisticaAplicacion();
        // GET: Estadistica
        public async Task<ActionResult> Index()
        {
            DataSet kpi = await _estadisticaApp.Kpi();
            ViewBag.kpi = kpi;
            return View();
        }
    }
}