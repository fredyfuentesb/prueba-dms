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
    public class Tercero_ArchivosDto
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
        private string Nombre_Archivo;
        public string nombre_archivo
        {
            get { return Nombre_Archivo; }
            set { Nombre_Archivo = value; }
        }
        private string Ruta_Archivo;
        public string ruta_archivo
        {
            get { return Ruta_Archivo; }
            set { Ruta_Archivo = value; }
        }
        private bool Es_Foto;
        public bool es_foto
        {
            get { return Es_Foto; }
            set { Es_Foto = value; }
        }
    }
}
