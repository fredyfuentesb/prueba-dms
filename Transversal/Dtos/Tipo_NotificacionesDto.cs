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
    public class Tipo_NotificacionesDto
    {
        private int Id;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        private string Nombre;
        public string nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }
        private bool Estado;
        public bool estado
        {
            get { return Estado; }
            set { Estado = value; }
        }
    }
}
