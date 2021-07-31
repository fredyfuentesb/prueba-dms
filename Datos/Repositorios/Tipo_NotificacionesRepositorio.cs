/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Tipo_Notificaciones
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 22:10:36</Fecha>
 *<Cambios>Indique su Nombre, la Fecha y el cambio realizado</Cambios>
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Dtos;

namespace Datos.Repositorios
{
    public class Tipo_NotificacionesRepositorio
    {
        public bool Save(Tipo_NotificacionesDto dto) => RepositorioGenerico<Tipo_NotificacionesDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(Tipo_NotificacionesDto dto) => RepositorioGenerico<Tipo_NotificacionesDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(Tipo_NotificacionesDto dto) => RepositorioGenerico<Tipo_NotificacionesDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public Tipo_NotificacionesDto FindById(int id) => RepositorioGenerico<Tipo_NotificacionesDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<Tipo_NotificacionesDto> List() => RepositorioGenerico<Tipo_NotificacionesDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
