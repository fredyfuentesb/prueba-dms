/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Config_Notificacion
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 21:42:15</Fecha>
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
    public class Config_NotificacionRepositorio
    {
        public bool Save(Config_NotificacionDto dto) => RepositorioGenerico<Config_NotificacionDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(Config_NotificacionDto dto) => RepositorioGenerico<Config_NotificacionDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(Config_NotificacionDto dto) => RepositorioGenerico<Config_NotificacionDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public Config_NotificacionDto FindById(int id) => RepositorioGenerico<Config_NotificacionDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<Config_NotificacionDto> List() => RepositorioGenerico<Config_NotificacionDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
