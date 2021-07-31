/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Terceros
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 22:07:14</Fecha>
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
    public class TercerosRepositorio
    {
        public bool Save(TercerosDto dto) => RepositorioGenerico<TercerosDto>.GenericOption(dto, "1", "dbo", "DefaultConnection");
        public bool Update(TercerosDto dto) => RepositorioGenerico<TercerosDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(TercerosDto dto) => RepositorioGenerico<TercerosDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public TercerosDto FindById(int id) => RepositorioGenerico<TercerosDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<TercerosDto> List() => RepositorioGenerico<TercerosDto>.List("prueba", "dbo", "DefaultConnection");
    }
}
