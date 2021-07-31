/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Menu_Rol
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 21:53:10</Fecha>
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
    public class Menu_RolRepositorio
    {
        public bool Save(Menu_RolDto dto) => RepositorioGenerico<Menu_RolDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(Menu_RolDto dto) => RepositorioGenerico<Menu_RolDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(Menu_RolDto dto) => RepositorioGenerico<Menu_RolDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public Menu_RolDto FindById(int id) => RepositorioGenerico<Menu_RolDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<Menu_RolDto> List() => RepositorioGenerico<Menu_RolDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
