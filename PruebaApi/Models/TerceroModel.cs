using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PruebaApi.Models
{
    [DataContract(Name = "UsuarioModel"), Serializable]
    public class TerceroModel
    {
        [DataMember(Name = "Id")]
        public int id { get; set; }

        [DataMember(Name = "Nombre"), Required]
        public string nombre { get; set; }

        [DataMember(Name = "Apellidos"), Required]
        public string apellidos { get; set; }

        [DataMember(Name = "Direccion")]
        public string direccion { get; set; }

        [DataMember(Name = "Email"), Required]
        public string email { get; set; }

        [DataMember(Name = "Telefono")]
        public Int64 telefono { get; set; }
    }
}