using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Implementacion.Modelos
{
    [DataContract(Name = "ConfigNotificacionModel"), Serializable]
    public class ConfigNotificacionModel
    {
        [DataMember(Name = "Id")]
        public int id { get; set; }

        [DataMember(Name = "Id_Tipo_Notificacion"), Required]
        public int id_tipo_notificacion { get; set; }

        [DataMember(Name = "Ruta"), Required]
        public string ruta { get; set; }

        [DataMember(Name = "Estado"), Required]
        public bool estado { get; set; }
    }
}
