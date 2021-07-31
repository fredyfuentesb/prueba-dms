/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Variables_Notificacion
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 22:22:15</Fecha>
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
    public class Variables_NotificacionRepositorio
    {
        public bool Save(Variables_NotificacionDto dto) => RepositorioGenerico<Variables_NotificacionDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(Variables_NotificacionDto dto) => RepositorioGenerico<Variables_NotificacionDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(Variables_NotificacionDto dto) => RepositorioGenerico<Variables_NotificacionDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public Variables_NotificacionDto FindById(int id) => RepositorioGenerico<Variables_NotificacionDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<Variables_NotificacionDto> List() => RepositorioGenerico<Variables_NotificacionDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
