/* Capa Implementacion
 * Esta clase fue creada para hacer utilizacion de la api de estadisticas
 * <autor>Fredy Fuentes</autor>
 * <Fecha>01/08/2021 12:27:10</Fecha>
 * <Cambios>Indique su Nombre, la Fecha y el cambio realizado</Cambios>
 */
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Implementacion.Implementacion
{
    public class EstadisticaAplicacion
    {
        private readonly WebApiHelper _apiHelper = new WebApiHelper();
        private readonly string BASE = "api/Estadisticas";

        #region kpi
        /// <summary>
        /// Envia una peticion get a api/Estadisticas para traer las estadisticas de la aplicacion
        /// </summary>
        /// <returns></returns>
        public async Task<DataSet> Kpi(string token)
        {
            DataSet kpi = new DataSet();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await httpClient.GetAsync(BASE);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    kpi = JsonConvert.DeserializeObject<DataSet>(resultJson);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                kpi = null;
            }
            return kpi;
        }
        #endregion
    }
}
