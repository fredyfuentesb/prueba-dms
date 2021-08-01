using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PruebaApi.Models
{
    [DataContract(Name = "UsuarioLogin"), Serializable]
    public class UsuarioLogin
    {
        [DataMember(Name = "Usuario"), Required]
        public string usuario { get; set; }

        [DataMember(Name = "Clave"), Required]
        public string clave { get; set; }
    }
}