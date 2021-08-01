using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Implementacion.Modelos
{
    [DataContract(Name = "TerceroArchivoModel"), Serializable]
    public class TerceroArchivoModel
    {
        [DataMember(Name = "Id")]
        public int id { get; set; }

        [DataMember(Name = "Id_Tercero"), Required]
        public int id_tercero { get; set; }

        [DataMember(Name = "Nombre_Archivo"), Required]
        public string nombre_archivo { get; set; }

        [DataMember(Name = "Ruta_Archivo"), Required]
        public string ruta_archivo { get; set; }

        [DataMember(Name = "Es_Foto"), Required]
        public bool es_foto { get; set; }
    }
}
