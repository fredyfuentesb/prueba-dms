using Implementacion.Implementacion;
using Implementacion.Modelos;
using PruebaWeb.Tags;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transversal.Dtos;

namespace PruebaWeb.Controllers
{
    [Autenticado]
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

        public async Task<JsonResult> Variables(int tipo)
        {
            List<Variables_NotificacionDto> variables = await _configNotificacionApp.Variables(tipo);
            return Json(variables, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Save()
        {
            bool guardo = false;
            object data;
            try
            {
                string idunico = Guid.NewGuid().ToString();
                var file = Request.Files[0];
                string fileName = file.FileName;
                Stream fileContent = file.InputStream;
                string directory = ConfigurationManager.AppSettings["RUTA_NOTIFICACIONES"];
                int tipo_notificacion = Convert.ToInt32(Request.Params.Get("id_tipo_notificacion"));

                string folderName = directory;
                Directory.CreateDirectory(folderName);
                file.SaveAs($"{folderName}{fileName}");
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (System.IO.File.Exists($"{folderName}{fileName}"))
                {
                    System.IO.File.Copy($"{folderName}{fileName}", $"{folderName}{idunico}{fileExtension}");
                    System.IO.File.Delete($"{folderName}{fileName}");
                }
                ConfigNotificacionModel configNotificacion = new ConfigNotificacionModel
                {
                    id_tipo_notificacion = tipo_notificacion,
                    ruta = $"{folderName}{idunico}{fileExtension}",
                    estado = true
                };
                ConfigNotificacionModel configCreada = await _configNotificacionApp.Save(configNotificacion);
                if(configCreada != null)
                {
                    guardo = true;
                    data = new { guardo, configCreada };
                }
                else
                {
                    data = new { guardo, message = "No se pudo guardar la informacion" };
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                data = new { guardo, message = "No se pudo guardar la informacion" };
            }
            return Json(data, JsonRequestBehavior.DenyGet);
        }
    }
}