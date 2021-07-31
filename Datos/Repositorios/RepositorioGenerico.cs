/*RepositorioGenerico
 *Clase que permite reducir la cantidad de codigo en los repositorios para las operaciones basicas hacia una base de datos
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 21:48:18</Fecha>
 *<Cambios>Indique su Nombre, la Fecha y el cambio realizado</Cambios>
 */
using Transversal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public static class RepositorioGenerico<T> where T : class, new()
    {
        #region GenericOption
        /// <summary>
        /// Allows you to perform basic operations: 1 save, 2 update, 3 delete.
        /// </summary>
        /// <param name="dto">Object to which the operation is to be created</param>
        /// <param name="option">Option to make: 1 save, 2 update, 3 delete</param>
        /// <param name="scheme">Schematic of the database in which the operation will be carried out</param>
        /// <param name="connectionString">Connection string to be used</param>
        /// <returns>True or false depending on whether the operation was created</returns>
        public static bool GenericOption(T dto, string option, string scheme, string connectionString)
        {
            string objectName = dto.GetType().Name;
            objectName = objectName.Replace("Dto", "");
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand($"{scheme}.SP_Maestro_{objectName.ToLower()}", conn) { CommandType = CommandType.StoredProcedure };
                    foreach (MemberInfo prop in dto.GetType().GetProperties())
                    {
                        PropertyInfo propertyInfo = dto.GetType().GetProperty(prop.Name);
                        if (propertyInfo != null)
                        {
                            cmd.Parameters.AddWithValue($"@{prop.Name}", propertyInfo.GetValue(dto, null));
                        }
                    }
                    cmd.Parameters.AddWithValue("@accion", option);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
            return true;
        }
        #endregion

        #region GenericOption {id}
        /// <summary>
        /// Allows you to perform basic operations: 1 save, 2 update, 3 delete.
        /// </summary>
        /// <param name="dto">Allows you to perform basic operations: 1 save, 2 update, 3 delete.</param>
        /// <param name="option">Option to make: 1 save, 2 update, 3 delete</param>
        /// <param name="scheme">Schematic of the database in which the operation will be carried out</param>
        /// <param name="connectionString">Connection string to be used</param>
        /// <param name="id">identification created by the self-incrementing</param>
        /// <returns>True or false depending on whether the operation was created</returns>
        public static bool GenericOption(T dto, string option, string scheme, string connectionString, ref int id)
        {
            string objectName = dto.GetType().Name;
            objectName = objectName.Replace("Dto", "");
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand($"{scheme}.SP_Maestro_{objectName.ToLower()}", conn) { CommandType = CommandType.StoredProcedure };
                    foreach (MemberInfo prop in dto.GetType().GetProperties())
                    {
                        PropertyInfo propertyInfo = dto.GetType().GetProperty(prop.Name);
                        if (propertyInfo != null)
                        {
                            if (prop.Name.ToLower().Equals("id"))
                            {
                                SqlParameter idD = cmd.Parameters.AddWithValue($"@{prop.Name}", propertyInfo.GetValue(dto, null));
                                idD.Direction = ParameterDirection.InputOutput;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue($"@{prop.Name}", propertyInfo.GetValue(dto, null));
                            }
                        }
                    }
                    cmd.Parameters.AddWithValue("@accion", option);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(cmd.Parameters["@id"].Value);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
            return true;
        }
        #endregion

        #region FindById
        /// <summary>
        /// Allows to obtain by id an object in database
        /// </summary>
        /// <param name="field">name of the id field to be searched for</param>
        /// <param name="value">value to which it will be compared</param>
        /// <param name="db">database</param>
        /// <param name="scheme">database schema</param>
        /// <param name="connectionString">Connection string to be used</param>
        /// <returns>object in database</returns>
        public static T FindById(string field, string value, string db, string scheme, string connectionString)
        {
            T dto = new T();
            string objectName = dto.GetType().Name;
            objectName = objectName.Replace("Dto", "");
            try
            {
                string where = $" WHERE {field} = '{value}' ";
                DataSet result = GenericQuery(connectionString, "*", 1, where, 0, "", $"{db}.{scheme}.{objectName}");
                dto = result.Tables[0].Rows[0].ToObject<T>();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                dto = null;
            }
            return dto;
        }
        #endregion

        #region List {value}
        /// <summary>
        /// Allows you to list all items in a table that match a value.
        /// </summary>
        /// <param name="field">Name of the field to compare</param>
        /// <param name="value">Value of the field to be compared</param>
        /// <param name="db">database</param>
        /// <param name="scheme">database schema</param>
        /// <param name="connectionString">Connection string to be used</param>
        /// <returns>List of objects</returns>
        public static List<T> List(string field, string value, string db, string scheme, string connectionString)
        {
            List<T> list = new List<T>();
            T dto = new T();
            string objectName = dto.GetType().Name;
            objectName = objectName.Replace("Dto", "");
            try
            {
                string where = $" WHERE {field} = '{value}' ";
                DataSet result = GenericQuery(connectionString, "*", 1, where, 0, "", $"{db}.{scheme}.{objectName}");
                list = result.Tables[0].DataTableToList<T>();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return list;
        }
        #endregion

        #region List
        /// <summary>
        /// Allows you to list all items in a table
        /// </summary>
        /// <param name="db">database</param>
        /// <param name="scheme">database schema</param>
        /// <param name="connectionString">Connection string to be used</param>
        /// <returns>List of objects</returns>
        public static List<T> List(string db, string scheme, string connectionString)
        {
            List<T> list = new List<T>();
            T dto = new T();
            string objectName = dto.GetType().Name;
            objectName = objectName.Replace("Dto", "");
            try
            {
                DataSet result = GenericQuery(connectionString, "*", 0, "", 0, "", $"{db}.{scheme}.{objectName}");
                list = result.Tables[0].DataTableToList<T>();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return list;
        }
        #endregion

        #region ListDataSet
        /// <summary>
        /// Allows you to list all items in a table
        /// </summary>
        /// <param name="db">database</param>
        /// <param name="scheme">database schema</param>
        /// <param name="connectionString">Connection string to be used</param>
        /// <returns>List of objects</returns>
        public static DataSet ListDataSet(string db, string scheme, string connectionString)
        {
            T dto = new T();
            string objectName = dto.GetType().Name;
            objectName = objectName.Replace("Dto", "");
            DataSet result = new DataSet();
            try
            {
                result = GenericQuery(connectionString, "*", 0, "", 0, "", $"{db}.{scheme}.{objectName}");
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                result = null;
            }
            return result;
        }
        #endregion

        #region GenericQuery
        public static DataSet GenericQuery(string connectionString, string alcance, int apliDonde = 0, string donde = "", int apliGrupo = 0, string grupo = "", string tabla = "")
        {
            var dtr = new DataSet();
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
                {
                    var cmd = new SqlCommand("VerDatosTabla", conn) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("@Tabla", tabla);
                    cmd.Parameters.AddWithValue("@Alcance", alcance);
                    cmd.Parameters.AddWithValue("@NecFiltro", apliDonde);
                    cmd.Parameters.AddWithValue("@Filtro", donde);
                    cmd.Parameters.AddWithValue("@NecOrden", apliGrupo);
                    cmd.Parameters.AddWithValue("@Orden", grupo);
                    conn.Open();
                    var da = new SqlDataAdapter(cmd);

                    da.Fill(dtr);
                    return dtr;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return dtr;
        }
        #endregion
    }
}
