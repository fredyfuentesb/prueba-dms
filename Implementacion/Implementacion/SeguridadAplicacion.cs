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
        public async Task<RespuestaLoginModel> Login(UsuarioLogin model)
        {
            RespuestaLoginModel datos = new RespuestaLoginModel();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            string jsonData = JsonConvert.SerializeObject(model);
            StringContent content = _apiHelper.GetSerializedJson(jsonData);
            try
            {
                var response = await httpClient.PostAsync(BASE, content);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<RespuestaLoginModel>(resultJson);
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

        #region OlvidoClave
        public async Task<bool> OlvidoClave(string usuario)
        {
            bool envio = false;
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");

            try
            {
                var response = await httpClient.GetAsync($"{BASE}/?usuario={usuario}");
                if (response.IsSuccessStatusCode)
                {
                    envio = true;
                }
                else
                {
                    envio = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                envio = false;
            }
            return envio;
        }
        #endregion
    }
}
