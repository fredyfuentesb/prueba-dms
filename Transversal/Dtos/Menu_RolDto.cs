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
    public class Menu_RolDto
    {
        private int Id;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        private int Id_Menu;
        public int id_menu
        {
            get { return Id_Menu; }
            set { Id_Menu = value; }
        }
        private int Id_Rol;
        public int id_rol
        {
            get { return Id_Rol; }
            set { Id_Rol = value; }
        }
    }
}
