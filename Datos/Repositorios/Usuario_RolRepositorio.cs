/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Usuario_Rol
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 22:15:08</Fecha>
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
    public class Usuario_RolRepositorio
    {
        public bool Save(Usuario_RolDto dto) => RepositorioGenerico<Usuario_RolDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(Usuario_RolDto dto) => RepositorioGenerico<Usuario_RolDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(Usuario_RolDto dto) => RepositorioGenerico<Usuario_RolDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public Usuario_RolDto FindById(int id) => RepositorioGenerico<Usuario_RolDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<Usuario_RolDto> List() => RepositorioGenerico<Usuario_RolDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
