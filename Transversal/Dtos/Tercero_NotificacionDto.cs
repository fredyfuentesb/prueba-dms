/*Data Transfer objetcs
 *Esta Clase fue generada por medio de una herramienta de generacion automatica de codigo, si usted edita la funcionalidad de la misma por favor indiquelo en los comentarios.
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 20:29:20</Fecha>
 *<Cambios>Indique su Nombre, la Fecha y el cambio realizado</Cambios>
 */

using System;
using System.Collections.Generic;
using System.Web;
using System.Threading.Tasks;
using System.Text;

namespace Transversal.Dtos
{
    [Serializable]
    public class Tercero_NotificacionDto
    {
        private int Id;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        private int Id_Tercero;
        public int id_tercero
        {
            get { return Id_Tercero; }
            set { Id_Tercero = value; }
        }
        private int Id_Config_Notificacion;
        public int id_config_notificacion
        {
            get { return Id_Config_Notificacion; }
            set { Id_Config_Notificacion = value; }
        }
        private DateTime Fecha;
        public DateTime fecha
        {
            get { return Fecha; }
            set { Fecha = value; }
        }
    }
}
