/*Data Transfer objetcs
 *Esta Clase fue generada por medio de una herramienta de generacion automatica de codigo, si usted edita la funcionalidad de la misma por favor indiquelo en los comentarios.
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 20:29:19</Fecha>
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
    public class TercerosDto
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
        private string Apellidos;
        public string apellidos
        {
            get { return Apellidos; }
            set { Apellidos = value; }
        }
        private string Direccion;
        public string direccion
        {
            get { return Direccion; }
            set { Direccion = value; }
        }
        private string Email;
        public string email
        {
            get { return Email; }
            set { Email = value; }
        }
        private Int64 Telefono;
        public Int64 telefono
        {
            get { return Telefono; }
            set { Telefono = value; }
        }
        private DateTime Fecha_Creacion;
        public DateTime fecha_creacion
        {
            get { return Fecha_Creacion; }
            set { Fecha_Creacion = value; }
        }
        private DateTime Fecha_Ultima_Modificacion;
        public DateTime fecha_ultima_modificacion
        {
            get { return Fecha_Ultima_Modificacion; }
            set { Fecha_Ultima_Modificacion = value; }
        }
    }
}
