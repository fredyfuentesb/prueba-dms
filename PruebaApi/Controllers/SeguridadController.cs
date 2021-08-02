using Datos.Repositorios;
using PruebaApi.Helpers;
using PruebaApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using Transversal.Helpers;

namespace PruebaApi.Controllers
{
    [RoutePrefix("api/Seguridad")]
    public class SeguridadController : ApiController
    {
        private readonly SeguridadRepositorio _seguridadRep = new SeguridadRepositorio();
        private readonly UsuariosRepositorio _usuariosRep = new UsuariosRepositorio();

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
                var token = TokenGenerator.GenerateTokenJwt(model.usuario);
                statusCode = HttpStatusCode.OK;
                data = new { datos, token };
            }
            else
            {
                statusCode = HttpStatusCode.BadRequest;
                data = new { message = "Imposible iniciar sesion" };
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion

        #region OlvidoClave
        [HttpGet]
        public HttpResponseMessage OlvidoClave(string usuario)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            object data = null;

            DataSet datosUsuario = _usuariosRep.FindByUsuario(usuario);
            if(datosUsuario != null)
            {
                try
                {
                    string emailDestino = datosUsuario.Tables[0].Rows[0]["email"].ToString();
                    string nombreCompleto = $"{datosUsuario.Tables[0].Rows[0]["nombre"].ToString()} {datosUsuario.Tables[0].Rows[0]["apellidos"].ToString()}";
                    string usuarioRegistrado = datosUsuario.Tables[0].Rows[0]["usuario"].ToString();
                    string id = datosUsuario.Tables[0].Rows[0]["id"].ToString();
                    DateTime fecha = DateTime.Now;
                    string idunico = Guid.NewGuid().ToString();
                    string datos = $"id={id}&nombre={nombreCompleto}&usuario={usuarioRegistrado}&token={idunico}&fecha={fecha}";
                    datos = HashHelper.Base64Encode(datos);
                    string url = $"{ConfigurationManager.AppSettings["url_web"]}Auth/Olvido/{datos}";
                    string cuerpo = $"<h3>Hola, {nombreCompleto}</h3><p>Recientemente recibimos una solicitud tuya para restablecer la contraseña de tu usuario ({usuarioRegistrado}) en nuestro software.</p>";
                    cuerpo += $"<p>Si aun tienes problemas para acceder a tu cuenta por favor </p><a href='{url}'>Haz click aca</a>";
                    cuerpo += $"<p>El anterior enlace solo es valido hasta {fecha.AddMinutes(15).ToString("G")}.</p>";

                    NotificadorSMTP notificadorSmtp = new NotificadorSMTP();
                    List<string> destinatario = new List<string>();
                    destinatario.Add(emailDestino);
                    MailMessage mensaje = notificadorSmtp.CrearMessage(destinatario, "Restablecimiento de contraseña en prueba-dms", cuerpo, true, ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["email"]);
                    notificadorSmtp.EnviarMensajeCorreo(mensaje, 1, ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["clave_email"], "smtp.gmail.com", 587, true);
                    statusCode = HttpStatusCode.OK;
                    data = new { message = "Email enviado" };
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    statusCode = HttpStatusCode.BadRequest;
                    data = new { message = "Usuario No encontrado" };
                }
            }

            return Request.CreateResponse(statusCode, data, "application/json");
        }
        #endregion
    }
}
