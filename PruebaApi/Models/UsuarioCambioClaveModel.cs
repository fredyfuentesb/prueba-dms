using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PruebaApi.Models
{
    [DataContract(Name = "UsuarioModel"), Serializable]
    public class UsuarioCambioClaveModel
    {
        [DataMember(Name = "Id")]
        public int id { get; set; }

        [DataMember(Name = "Usuario"), Required]
        public string usuario { get; set; }

        [DataMember(Name = "Clave"), Required]
        public string clave { get; set; }
    }
}