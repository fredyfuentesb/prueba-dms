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
    public class Config_NotificacionDto
    {
        private int Id;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        private int Id_Tipo_Notificacion;
        public int id_tipo_notificacion
        {
            get { return Id_Tipo_Notificacion; }
            set { Id_Tipo_Notificacion = value; }
        }
        private string Ruta;
        public string ruta
        {
            get { return Ruta; }
            set { Ruta = value; }
        }
        private bool Estado;
        public bool estado
        {
            get { return Estado; }
            set { Estado = value; }
        }
    }
}
