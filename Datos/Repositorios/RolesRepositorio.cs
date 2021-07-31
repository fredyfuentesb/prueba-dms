/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Roles
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 21:58:12</Fecha>
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
    public class RolesRepositorio
    {
        public bool Save(RolesDto dto) => RepositorioGenerico<RolesDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(RolesDto dto) => RepositorioGenerico<RolesDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(RolesDto dto) => RepositorioGenerico<RolesDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public RolesDto FindById(int id) => RepositorioGenerico<RolesDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<RolesDto> List() => RepositorioGenerico<RolesDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
