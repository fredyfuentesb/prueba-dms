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
    public class TerceroController : Controller
    {
        private readonly TercerosAplicacion _tercero = new TercerosAplicacion();
        private readonly Tercero_ArchivosAplicacion _terceroArchivo = new Tercero_ArchivosAplicacion();
        // GET: Tercero
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> List()
        {
            List<TercerosDto> terceros = await _tercero.List();
            return Json(terceros, JsonRequestBehavior.AllowGet);
        }

        #region Save
        [HttpPost]
        public async Task<JsonResult> Save(TerceroModel model)
        {
            bool guardo = false;
            object data;

            if (ModelState.IsValid)
            {
                TerceroModel terceroCreado = await _tercero.Save(model);
                if(terceroCreado != null)
                {
                    guardo = true;
                    data = new { guardo, terceroCreado };
                }
                else
                {
                    guardo = false;
                    data = new { guardo, message = "No se logro guardar" };
                }
            }
            else
            {
                guardo = false;
                List<string> errors = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)
                                        .ToList();
                data = new { guardo, errors };
            }

            return Json(data, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region Update
        [HttpPost]
        public async Task<JsonResult> Update(TerceroModel model)
        {
            bool guardo = false;
            object data;

            if (ModelState.IsValid)
            {
                TerceroModel terceroCreado = await _tercero.Update(model);
                if (terceroCreado != null)
                {
                    guardo = true;
                    data = new { guardo, terceroCreado };
                }
                else
                {
                    guardo = false;
                    data = new { guardo, message = "No se logro guardar" };
                }
            }
            else
            {
                guardo = false;
                List<string> errors = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)
                                        .ToList();
                data = new { guardo, errors };
            }

            return Json(data, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region Delete
        public async Task<JsonResult> Delete(int id)
        {
            bool elimino = false;
            object data;

            TercerosDto terceroEliminado = await _tercero.Delete(id);
            if(terceroEliminado != null)
            {
                elimino = true;
                data = new { elimino, message = "Se elimino correctamente" };
            }
            else
            {
                elimino = false;
                data = new { elimino, message = "No se elimino correctamente" };
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Archivos
        public async Task<ActionResult> Archivos(int id)
        {
            TercerosDto tercero = await _tercero.FindById(id);
            ViewBag.tercero = tercero;
            return View();
        }

        #region SaveFile
        [HttpPost]
        public async Task<JsonResult> SaveFile()
        {
            bool guardo = false;
            object data;

            try
            {
                string idunico = Guid.NewGuid().ToString();
                var file = Request.Files[0];
                string fileName = file.FileName;
                Stream fileContent = file.InputStream;
                string directory = ConfigurationManager.AppSettings["RUTA_ARCHIVOS"];
                bool es_foto = Convert.ToBoolean(Request.Params.Get("es_foto"));

                string folderName = directory;
                Directory.CreateDirectory(folderName);
                file.SaveAs($"{folderName}{fileName}");
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (System.IO.File.Exists($"{folderName}{fileName}"))
                {
                    System.IO.File.Copy($"{folderName}{fileName}", $"{folderName}{idunico}{fileExtension}");
                    System.IO.File.Delete($"{folderName}{fileName}");
                }
                TerceroArchivoModel model = new TerceroArchivoModel()
                {
                    id_tercero = Convert.ToInt32(Request.Params.Get("id_tercero")),
                    nombre_archivo = fileName,
                    ruta_archivo = $"{folderName}{idunico}{fileExtension}",
                    es_foto = es_foto
                };
                TerceroArchivoModel archivoCreado = await _terceroArchivo.Save(model);
                if(archivoCreado != null)
                {
                    guardo = true;
                    data = new { guardo, archivoCreado };
                }
                else
                {
                    data = new { guardo, message = "No se pudo guardar la informacion" };
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                data = new { guardo, message = "No se pudo guardar la información" };
            }

            return Json(data, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region ListFiles
        public async Task<JsonResult> ListFiles(int id)
        {
            List<Tercero_ArchivosDto> terceroArchivos = await _terceroArchivo.List(id);
            return Json(terceroArchivos, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DeleteFile
        public async Task<JsonResult> DeleteFile(int id)
        {
            bool elimino = false;
            object data;

            Tercero_ArchivosDto terceroArchivoEliminado = await _terceroArchivo.Delete(id);
            if(terceroArchivoEliminado != null)
            {
                if (System.IO.File.Exists(terceroArchivoEliminado.ruta_archivo))
                {
                    System.IO.File.Delete(terceroArchivoEliminado.ruta_archivo);
                }
                elimino = true;
                data = new { elimino, message = "Se elimino correctamente" };
            }
            else
            {
                elimino = false;
                data = new { elimino, message = "No se elimino correctamente" };
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DownloadFile
        public async Task<ActionResult> DownloadFile(int id)
        {
            Tercero_ArchivosDto terceroArchivo = await _terceroArchivo.FindById(id);
            string rutaPdf = $"{terceroArchivo.ruta_archivo}";
            return new DownloadResult { VirtualPath = rutaPdf, FileDownloadName = $"{terceroArchivo.nombre_archivo}" };
        }
        #endregion
        #endregion
    }
}