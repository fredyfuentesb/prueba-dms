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

namespace PruebaWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly SeguridadAplicacion _seguridad = new SeguridadAplicacion();
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
    }
}