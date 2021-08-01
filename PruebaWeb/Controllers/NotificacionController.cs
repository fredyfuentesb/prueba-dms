using Implementacion.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transversal.Dtos;

namespace PruebaWeb.Controllers
{
    public class NotificacionController : Controller
    {
        private readonly Config_NotificacionAplicacion _configNotificacionApp = new Config_NotificacionAplicacion();
        // GET: Notificacion
        public async Task<ActionResult> Index()
        {
            List<Tipo_NotificacionesDto> tipos = await _configNotificacionApp.Tipos();
            ViewBag.tipos = tipos;
            return View();
        }
    }
}