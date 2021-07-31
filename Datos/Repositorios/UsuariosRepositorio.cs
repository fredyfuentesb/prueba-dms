/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Usuarios
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 22:18:10</Fecha>
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
    public class UsuariosRepositorio
    {
        public bool Save(UsuariosDto dto) => RepositorioGenerico<UsuariosDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(UsuariosDto dto) => RepositorioGenerico<UsuariosDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(UsuariosDto dto) => RepositorioGenerico<UsuariosDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public UsuariosDto FindById(int id) => RepositorioGenerico<UsuariosDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<UsuariosDto> List() => RepositorioGenerico<UsuariosDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
