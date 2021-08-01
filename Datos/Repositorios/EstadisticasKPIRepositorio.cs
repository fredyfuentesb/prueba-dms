/* EstadisticasKPIRepositorio
 * Clase que permite consultar los procedimientos almacenados para las estadisticas
 * <autor>Fredy Fuentes</autor>
 * <Fecha>01/08/2021 12:03:18</Fecha>
 * <Cambios>Indique su Nombre, la Fecha y el cambio realizado</Cambios>
 */
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
    public class EstadisticasKPIRepositorio
    {
        /// <summary>
        /// Este procedimiento se usara para hacer las consultas para las estadisticas
        /// </summary>
        /// <returns></returns>
        public DataSet Kpi()
        {
            var dtr = new DataSet();
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    var cmd = new SqlCommand("kpi", conn) { CommandType = CommandType.StoredProcedure };
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
    }
}
