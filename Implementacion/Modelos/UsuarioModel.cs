using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Implementacion.Modelos
{
    [DataContract(Name = "UsuarioModel"), Serializable]
    public class UsuarioModel
    {
        [DataMember(Name = "Id")]
        public int id { get; set; }

        [DataMember(Name = "Id_Tercero")]
        public int id_tercero { get; set; }

        [DataMember(Name = "Nombre"), Required]
        public string nombre { get; set; }

        [DataMember(Name = "Apellidos"), Required]
        public string apellidos { get; set; }

        [DataMember(Name = "Email"), Required]
        public string email { get; set; }

        [DataMember(Name = "Usuario"), Required]
        public string usuario { get; set; }

        [DataMember(Name = "Clave"), Required]
        public string clave { get; set; }

        [DataMember(Name = "Activo")]
        public bool activo { get; set; }
    }
}
