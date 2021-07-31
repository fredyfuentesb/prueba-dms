/* NotificadorSMTP
 * Esta clase permite enviar correos electronicos
 * <autor>Fredy Fuentes</autor>
 * <Fecha>31/07/2021 10:53:19</Fecha>
 * <Cambios>Indique su Nombre, la Fecha y el cambio realizado</Cambios>
 * */
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace PruebaApi.Helpers
{
    public class NotificadorSMTP
    {
        #region EnviarCorreo
        /// <summary>
        /// Permite enviar un correo electornico a un correo especificado
        /// </summary>
        /// <param name="destinatario">correo electronico del destinatario</param>
        /// <param name="asunto">Asunto que sera mostrado en el correo electronico</param>
        /// <param name="cuerpoMensaje">Cuerpo que tendra el mensaje enviado</param>
        /// <param name="cuerpoEsHtml">Booleano para determinar si cuerpoMensaje es html</param>
        /// <param name="usuario">Usuario del correo que utilizara para enviar las notificaciones</param>
        /// <param name="clave">Contraseña del correo electronico que se utilizara para enviar las notificaciones</param>
        /// <param name="nombreUsuarioMostrado">Nombre de usuario que aparecera visible en el correo enviado</param>
        /// <param name="hostSmtp">Host del servicio SMTP</param>
        /// <param name="portSmtp">Puerto que utilizara el SMTP</param>
        /// <param name="enableSslSmtp">Booleando para saber si se utilizara ssl</param>
        /// <returns></returns>
        public bool EnviarCorreo(string destinatario, string asunto, string cuerpoMensaje, bool cuerpoEsHtml, string usuario, string clave, string nombreUsuarioMostrado, string hostSmtp, int portSmtp, bool enableSslSmtp)
        {
            bool res = false;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            //CONFIGURACIÓN DEL SMTP
            smtp.Host = hostSmtp;
            smtp.Port = portSmtp;
            smtp.EnableSsl = enableSslSmtp;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(usuario, clave);

            //CONFIGURACIÓN DEL MENSAJE
            //Message.[To].Add[Destinatario] //Cuenta del correo al que se le quiere envial el e-mail
            message.Bcc.Add(destinatario);
            message.From = new MailAddress(usuario, nombreUsuarioMostrado, System.Text.Encoding.UTF8); //Quien lo envia
            message.Subject = asunto; //Asunto del e-mail
            message.SubjectEncoding = System.Text.Encoding.UTF8; //Codificacion
            message.Body = cuerpoMensaje; //Contenido del e-mail
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Priority = System.Net.Mail.MailPriority.Normal;
            message.IsBodyHtml = cuerpoEsHtml;

            //ENVIO
            try
            {
                smtp.Send(message);
                Console.WriteLine("Mensaje enviado correctamente");
                res = true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
            }


            return res;
        }
        #endregion

        #region EnviarCorreoVariosDestinos
        /// <summary>
        /// Permite enviar un correo electronico a varios destinatarios
        /// </summary>
        /// <param name="destinatarios">Lista de destinatarios del correo electronico</param>
        /// <param name="asunto">Asunto que sera mostrado en el correo electronico</param>
        /// <param name="cuerpoMensaje">Cuerpo que tendra el mensaje enviado</param>
        /// <param name="cuerpoEsHtml">Booleano para determinar si cuerpoMensaje es html</param>
        /// <param name="limiteMaximoRemitentes">Permite limitar el numero maximo de remitentes para evitar que el correo sea marcado como spam</param>
        /// <param name="usuario">Usuario del correo que utilizara para enviar las notificaciones</param>
        /// <param name="clave">Contraseña del correo electronico que se utilizara para enviar las notificaciones</param>
        /// <param name="nombreUsuarioMostrado">Nombre de usuario que aparecera visible en el correo enviado</param>
        /// <param name="hostSmtp">Host del servicio SMTP</param>
        /// <param name="portSmtp">Puerto que utilizara el SMTP</param>
        /// <param name="enableSslSmtp">Booleando para saber si se utilizara ssl</param>
        public void EnviarCorreoVariosDestinos(List<string> destinatarios, string asunto, string cuerpoMensaje, bool cuerpoEsHtml, int limiteMaximoRemitentes, string usuario, string clave, string nombreUsuarioMostrado, string hostSmtp, int portSmtp, bool enableSslSmtp)
        {
            if (destinatarios.Count > limiteMaximoRemitentes)
            {
                throw new Exception("Se ha excedido el número maximo de remitentes");
            }

            SmtpClient smtp = new SmtpClient();

            //CONFIGURACIÓN DEL SMTP
            smtp.Host = hostSmtp;
            smtp.Port = portSmtp;
            smtp.EnableSsl = enableSslSmtp;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(usuario, clave);
            smtp.Timeout = 20000;

            //CONFIGURACIÓN DEL MENSAJE
            foreach (string destinatario in destinatarios)
            {
                MailMessage message = new MailMessage();
                message.Bcc.Add(destinatario);
                message.From = new MailAddress(usuario, nombreUsuarioMostrado, System.Text.Encoding.UTF8); //Quien lo envia
                message.Subject = asunto; //Asunto del e-mail
                message.SubjectEncoding = System.Text.Encoding.UTF8; //Codificacion
                message.Body = cuerpoMensaje; //Contenido del e-mail
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Priority = System.Net.Mail.MailPriority.Normal;
                message.IsBodyHtml = cuerpoEsHtml;

                //ENVIO
                try
                {
                    smtp.Send(message);
                    Console.WriteLine("Mensaje enviado correctamente");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion

        #region EnviarCorreoVariosDestinosConAdjuntos
        /// <summary>
        /// Permite enviar un correo a diferentes destinatarios con archivos adjuntos
        /// </summary>
        /// <param name="destinatarios">Destinatarios del correo</param>
        /// <param name="asunto">Asunto que sera mostrado en el correo electronico</param>
        /// <param name="cuerpoMensaje">Cuerpo que tendra el mensaje enviado</param>
        /// <param name="cuerpoEsHtml">Booleano para determinar si cuerpoMensaje es html</param>
        /// <param name="limiteMaximoRemitentes">Cantidad de remitentes maximos para evitar que el correo sea marcado como spam</param>
        /// <param name="usuario">Usuario del correo que utilizara para enviar las notificaciones</param>
        /// <param name="clave">Contraseña del correo electronico que se utilizara para enviar las notificaciones</param>
        /// <param name="nombreUsuarioMostrado">Nombre de usuario que aparecera visible en el correo enviado</param>
        /// <param name="hostSmtp">Host del servicio SMTP</param>
        /// <param name="portSmtp">Puerto que utilizara el SMTP</param>
        /// <param name="enableSslSmtp">Booleando para saber si se utilizara ssl</param>
        /// <param name="adjuntos">Lista de archivos que seran adjuntados</param>
        public void EnviarCorreoVariosDestinosConAdjuntos(List<string> destinatarios, string asunto, string cuerpoMensaje, bool cuerpoEsHtml, int limiteMaximoRemitentes, string usuario, string clave, string nombreUsuarioMostrado, string hostSmtp, int portSmtp, bool enableSslSmtp, List<string> adjuntos)
        {

            Attachment attachment;

            if (destinatarios.Count > limiteMaximoRemitentes)
            {
                throw new Exception("Se ha excedido el número maximo de remitentes");
            }

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            //CONFIGURACIÓN DEL SMTP
            smtp.Host = hostSmtp;
            smtp.Port = portSmtp;
            smtp.EnableSsl = enableSslSmtp;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(usuario, clave);
            smtp.Timeout = 20000;

            //CONFIGURACIÓN DEL MENSAJE
            foreach (string destinatario in destinatarios)
            {
                message.Bcc.Add(destinatario);
                message.From = new MailAddress(usuario, nombreUsuarioMostrado, System.Text.Encoding.UTF8); //Quien lo envia
                message.Subject = asunto; //Asunto del e-mail
                message.SubjectEncoding = System.Text.Encoding.UTF8; //Codificacion
                message.Body = cuerpoMensaje; //Contenido del e-mail
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Priority = System.Net.Mail.MailPriority.Normal;
                message.IsBodyHtml = cuerpoEsHtml;

                if (adjuntos.Count > 0)
                {
                    foreach (string adjunto in adjuntos)
                    {
                        if (FileExists(adjunto))
                        {
                            attachment = new Attachment(adjunto);
                            message.Attachments.Add(attachment);
                        }
                    }
                }

                //ENVIO
                try
                {
                    smtp.Send(message);
                    Console.WriteLine("Mensaje enviado correctamente");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        #endregion

        #region CrearMessage
        /// <summary>
        /// Permite crear la cabecera de un correo electronico
        /// </summary>
        /// <param name="destinatarios">Destinatarios del correo electronico</param>
        /// <param name="asunto">Asunto que sera mostrado en el correo electronico</param>
        /// <param name="cuerpoMensaje">Cuerpo que tendra el mensaje enviado</param>
        /// <param name="cuerpoEsHtml">Booleano para determinar si cuerpoMensaje es html</param>
        /// <param name="usuario">Usuario del correo que utilizara para enviar las notificaciones</param>
        /// <param name="nombreUsuarioMostrado">Contraseña del correo electronico que se utilizara para enviar las notificaciones</param>
        /// <returns></returns>
        public MailMessage CrearMessage(List<string> destinatarios, string asunto, string cuerpoMensaje, bool cuerpoEsHtml, string usuario, string nombreUsuarioMostrado)
        {
            MailMessage message = new MailMessage();

            //CONFIGURACIÓN DEL MENSAJE
            foreach (string destinatario in destinatarios)
            {
                message.CC.Add(destinatario);
            }

            message.From = new MailAddress(usuario, nombreUsuarioMostrado, System.Text.Encoding.UTF8); //Quien lo envia
            message.Subject = asunto; //Asunto del e-mail
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Body = cuerpoMensaje; //Contenido del e-mail
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Priority = System.Net.Mail.MailPriority.Normal;
            message.IsBodyHtml = cuerpoEsHtml;

            return message;
        }
        #endregion

        #region AdjuntarArchivosAMensaje
        /// <summary>
        /// Permite adjuntar archivos a un correo configurado
        /// </summary>
        /// <param name="message">Mensaje configurado al que se van a adjuntar los archivos</param>
        /// <param name="adjuntos">Lista de archivos que seran adjuntados</param>
        /// <returns></returns>
        public MailMessage AdjuntarArchivosAMensaje(MailMessage message, List<string> adjuntos)
        {
            if (message != null)
            {
                Attachment attachment;

                if (adjuntos.Count > 0)
                {
                    foreach (string adjunto in adjuntos)
                    {
                        if (FileExists(adjunto))
                        {
                            attachment = new Attachment(adjunto);
                            message.Attachments.Add(attachment);
                        }
                    }
                }
            }
            return message;
        }
        #endregion

        #region AdjuntarArchivosAMensaje
        /// <summary>
        /// Permite adjuntar archivos a un correo configurado
        /// </summary>
        /// <param name="message">Mensaje configurado al que se van a adjuntar los archivos</param>
        /// <param name="adjuntos">Lista de archivos que seran adjuntados</param>
        /// <returns></returns>
        public MailMessage AdjuntarArchivosAMensaje(MailMessage message, List<Tuple<string, System.IO.Stream>> adjuntos)
        {
            //Tuple<string, System.IO.Stream>
            //Item1 -> Nombre
            //Item2 -> IO

            if (message != null)
            {
                Attachment attachment;

                if (adjuntos.Count > 0)
                {
                    foreach (Tuple<string, Stream> adjunto in adjuntos)
                    {
                        attachment = new Attachment(adjunto.Item2, adjunto.Item1);
                        message.Attachments.Add(attachment);
                    }
                }
            }

            return message;
        }
        #endregion

        #region EnviarMensajeCorreo
        /// <summary>
        /// Permite enviar un correo electronico
        /// </summary>
        /// <param name="message">Correo electronico configurado para ser enviado</param>
        /// <param name="limiteMaximoRemitentes">Limitar la cantidad de remitentes para que el correo no se marcado como spam</param>
        /// <param name="usuario">Usuario del correo que utilizara para enviar las notificaciones</param>
        /// <param name="clave">Contraseña del correo electronico que se utilizara para enviar las notificaciones</param>
        /// <param name="hostSmtp">Host del servicio SMTP</param>
        /// <param name="portSmtp">Puerto que utilizara el SMTP</param>
        /// <param name="enableSslSmtp">Booleando para saber si se utilizara ssl</param>
        /// <returns></returns>
        public bool EnviarMensajeCorreo(MailMessage message, int limiteMaximoRemitentes, string usuario, string clave, string hostSmtp, int portSmtp, bool enableSslSmtp)
        {
            bool res = false;

            if (message.To.Count > limiteMaximoRemitentes || message.CC.Count > limiteMaximoRemitentes || message.Bcc.Count > limiteMaximoRemitentes)
            {
                throw new Exception("Se ha excedido el número maximo de remitentes");
            }

            SmtpClient smtp = new SmtpClient();

            //CONFIGURACIÓN DEL SMTP
            smtp.Host = hostSmtp;
            smtp.Port = portSmtp;
            smtp.EnableSsl = enableSslSmtp;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(usuario, clave);
            smtp.Timeout = 20000;

            //ENVIO
            try
            {
                smtp.Send(message);
                res = true;
                Console.WriteLine("Mensaje enviado correctamente");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return res;
        }
        #endregion

        #region FileExists
        /// <summary>
        /// Verifica que un archivo exista en la ruta que se especifico
        /// </summary>
        /// <param name="rutaCompleta">Ruta del archivo</param>
        /// <returns></returns>
        public bool FileExists(string rutaCompleta)
        {
            bool res = false;
            if (!rutaCompleta.Trim().Equals(""))
            {
                FileInfo f = new FileInfo(rutaCompleta);
                res = f.Exists;
            }
            return res;
        }
        #endregion
    }
}