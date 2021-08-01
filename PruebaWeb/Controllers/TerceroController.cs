using Implementacion.Implementacion;
using Implementacion.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transversal.Dtos;

namespace PruebaWeb.Controllers
{
    public class TerceroController : Controller
    {
        private readonly TercerosAplicacion _tercero = new TercerosAplicacion();
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
    }
}