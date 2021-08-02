using Implementacion.Implementacion;
using Implementacion.Modelos;
using PruebaWeb.Tags;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transversal;

namespace PruebaWeb.Controllers
{
    [Autenticado]
    public class UsuarioController : Controller
    {
        private readonly UsuariosAplicacion _usuarioApp = new UsuariosAplicacion();
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        #region List
        /// <summary>
        /// Lista la informacion de los usuarios que se encuetran registrados
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> List()
        {
            bool vieneInformacion = false;
            object data;
            DataSet usuariosDS = await _usuarioApp.List(Session["token"].ToString());
            if(usuariosDS != null)
            {
                List<UsuarioModel> usuarios = usuariosDS.Tables[0].DataTableToList<UsuarioModel>();
                vieneInformacion = true;
                data = new { vieneInformacion, usuarios };
            }
            else
            {
                data = new { vieneInformacion, message = "No hay informacion guardada" };
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Save
        /// <summary>
        /// Envia la informacion del usuario que se va a guardar a la capa de negocio
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Save(UsuarioModel model)
        {
            bool guardo = false;
            object data;

            if (ModelState.IsValid)
            {
                UsuarioModel usuarioCreado = await _usuarioApp.Save(model, Session["token"].ToString());
                if(usuarioCreado != null)
                {
                    guardo = true;
                    data = new { guardo, usuarioCreado };
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
        /// <summary>
        /// Envia la informacion del usuario que se va a actualizar a la capa de negocio
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Update(UsuarioModel model)
        {
            bool guardo = false;
            object data;

            if (ModelState.IsValid)
            {
                UsuarioModel usuarioCreado = await _usuarioApp.Update(model, Session["token"].ToString());
                if (usuarioCreado != null)
                {
                    guardo = true;
                    data = new { guardo, usuarioCreado };
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

        #region CambiarClave
        /// <summary>
        /// Envia la informacion para el cambio de clave a la capa de negocio
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CambiarClave(UsuarioCambioClaveModel model)
        {
            bool guardo = false;
            object data;

            if (ModelState.IsValid)
            {
                UsuarioCambioClaveModel cambio = await _usuarioApp.CambiarClave(model);
                if(cambio != null)
                {
                    guardo = true;
                    data = new { guardo, cambio };
                }
                else
                {
                    guardo = false;
                    data = new { guardo, message = "No se pudo realizar el cambio de clave" };
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
    }
}