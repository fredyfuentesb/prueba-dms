/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Menus
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 21:55:41</Fecha>
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
    public class MenusRepositorio
    {
        public bool Save(MenusDto dto) => RepositorioGenerico<MenusDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(MenusDto dto) => RepositorioGenerico<MenusDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(MenusDto dto) => RepositorioGenerico<MenusDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public MenusDto FindById(int id) => RepositorioGenerico<MenusDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<MenusDto> List() => RepositorioGenerico<MenusDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
