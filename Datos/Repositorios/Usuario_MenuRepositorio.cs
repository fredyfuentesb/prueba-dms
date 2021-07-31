/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Usuario_Menu
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 22:13:25</Fecha>
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
    public class Usuario_MenuRepositorio
    {
        public bool Save(Usuario_MenuDto dto) => RepositorioGenerico<Usuario_MenuDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(Usuario_MenuDto dto) => RepositorioGenerico<Usuario_MenuDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(Usuario_MenuDto dto) => RepositorioGenerico<Usuario_MenuDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public Usuario_MenuDto FindById(int id) => RepositorioGenerico<Usuario_MenuDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<Usuario_MenuDto> List() => RepositorioGenerico<Usuario_MenuDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
