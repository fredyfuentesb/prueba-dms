/*Capa de Datos
 *Esta Clase fue creada para administrar las operaciones hacia la base de datos de la tabla Tercero_Archivos
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 22:01:58</Fecha>
 *<Cambios>Indique su Nombre, la Fecha y el cambio realizado</Cambios>
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal;
using Transversal.Dtos;

namespace Datos.Repositorios
{
    public class Tercero_ArchivosRepositorio
    {
        public bool Save(Tercero_ArchivosDto dto, ref int id) => RepositorioGenerico<Tercero_ArchivosDto>.GenericOption(dto, "1", "dbo", "DefaultConnection", ref id);
        public bool Update(Tercero_ArchivosDto dto) => RepositorioGenerico<Tercero_ArchivosDto>.GenericOption(dto, "2", "dbo", "DefaultConnection");
        public bool Delete(Tercero_ArchivosDto dto) => RepositorioGenerico<Tercero_ArchivosDto>.GenericOption(dto, "3", "dbo", "DefaultConnection");
        public Tercero_ArchivosDto FindById(int id) => RepositorioGenerico<Tercero_ArchivosDto>.FindById("id", id.ToString(), "prueba", "dbo", "DefaultConnection");
        public List<Tercero_ArchivosDto> List() => RepositorioGenerico<Tercero_ArchivosDto>.List("prueba", "dbo", "DefaultConnection");

        public List<Tercero_ArchivosDto> ListWithUser(int id_tercero)
        {
            List<Tercero_ArchivosDto> listado = new List<Tercero_ArchivosDto>();

            try
            {
                string select = "id, id_tercero, nombre_archivo, ruta_archivo, es_foto";
                string where = $" WHERE id_tercero={id_tercero}";
                DataSet result = RepositorioGenerico<DataSet>.GenericQuery("DefaultConnection", select, 1, where, 0, "", " prueba.dbo.vw_tercero_archivos");
                listado = result.Tables[0].DataTableToList<Tercero_ArchivosDto>();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return listado;
        }

    }
}
