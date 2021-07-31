/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Tercero_Notificacion
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 22:05:45</Fecha>
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
    public class Tercero_NotificacionRepositorio
    {
        public bool Save(Tercero_NotificacionDto dto) => RepositorioGenerico<Tercero_NotificacionDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(Tercero_NotificacionDto dto) => RepositorioGenerico<Tercero_NotificacionDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(Tercero_NotificacionDto dto) => RepositorioGenerico<Tercero_NotificacionDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public Tercero_NotificacionDto FindById(int id) => RepositorioGenerico<Tercero_NotificacionDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<Tercero_NotificacionDto> List() => RepositorioGenerico<Tercero_NotificacionDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
