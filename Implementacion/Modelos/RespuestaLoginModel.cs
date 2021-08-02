using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Implementacion.Modelos
{
    [DataContract(Name = "RespuestaLoginModel"), Serializable]
    public class RespuestaLoginModel
    {
        [DataMember(Name = "Datos")]
        public DataSet datos { get; set; }
        [DataMember(Name = "Token")]
        public string token { get; set; }
    }
}
