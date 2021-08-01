using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class SeguridadRepositorio
    {
        #region IniciarSesion
        /// <summary>
        /// Procedimiento almacenado para verificar el inicio de sesion de un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public DataSet IniciarSesion(string usuario, string clave)
        {
            var dtr = new DataSet();
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    var cmd = new SqlCommand("IniciarSesion", conn) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    conn.Open();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dtr);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                dtr = null;
            }
            return dtr;
        }
        #endregion
    }
}
