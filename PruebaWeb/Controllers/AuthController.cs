using Implementacion.Implementacion;
using Implementacion.Modelos;
using PruebaWeb.Helpers;
using PruebaWeb.Tags;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transversal.Helpers;

namespace PruebaWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly SeguridadAplicacion _seguridad = new SeguridadAplicacion();
        private readonly UsuariosAplicacion _usuarioApp = new UsuariosAplicacion();
        // GET: Auth
        [NoLogin]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UsuarioLogin model)
        {
            DataSet datos = await _seguridad.Login(model);
            if(datos != null)
            {
                string usuario = datos.Tables[0].Rows[0]["usuario"].ToString();
                string nombre_completo = $"{datos.Tables[0].Rows[0]["nombre"].ToString()} {datos.Tables[0].Rows[0]["apellidos"].ToString()}";
                string email = datos.Tables[0].Rows[0]["email"].ToString();
                SessionHelper.AddUserToSession(usuario);
                Session["usuario"] = usuario;
                Session["nombre_completo"] = nombre_completo;
                Session["email"] = email;

                return this.RedirectToAction("Index", "Estadistica");

            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            SessionHelper.DestroyUserSession();
            return this.RedirectToAction("Login", "Auth");
        }

        public ActionResult OlvidoClave()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmoOlvidoClave()
        {
            bool envio = await _seguridad.OlvidoClave(Request.Params.Get("usuario"));
            return this.RedirectToAction("Login", "Auth");
        }

        public ActionResult Olvido(string id)
        {
            string decodificado = HashHelper.Base64Decode(id);
            Dictionary<string, string> datos = StringHelper.ObtenerDiccionario(decodificado);
            ViewBag.datos = datos;
            DateTime fechaLimite = Convert.ToDateTime(datos["fecha"]);
            DateTime fechaActual = DateTime.Now;
            if(fechaActual > fechaLimite)
            {
                return this.RedirectToAction("TiempoAgotado", "Auth");
            }
            return View();
        }

        public async Task<ActionResult> CambiarClave(UsuarioCambioClaveModel model)
        {
            UsuarioCambioClaveModel cambio = await _usuarioApp.CambiarClave(model);
            return this.RedirectToAction("Login", "Auth");
        }

        public ActionResult TiempoAgotado()
        {
            return View();
        }
    }
}