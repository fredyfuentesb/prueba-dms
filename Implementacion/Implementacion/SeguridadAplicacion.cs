using Implementacion.Modelos;
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
    public class SeguridadAplicacion
    {
        private readonly WebApiHelper _apiHelper = new WebApiHelper();
        private readonly string BASE = "api/Seguridad";

        #region Login
        public async Task<DataSet> Login(UsuarioLogin model)
        {
            DataSet datos = new DataSet();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            string jsonData = JsonConvert.SerializeObject(model);
            StringContent content = _apiHelper.GetSerializedJson(jsonData);
            try
            {
                var response = await httpClient.PostAsync(BASE, content);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<DataSet>(resultJson);
                }
                else
                {
                    datos = null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                datos = null;
            }

            return datos;
        }
        #endregion
    }
}
