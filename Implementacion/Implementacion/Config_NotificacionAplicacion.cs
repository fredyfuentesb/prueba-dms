using Implementacion.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Transversal.Dtos;

namespace Implementacion.Implementacion
{
    public class Config_NotificacionAplicacion
    {
        private readonly WebApiHelper _apiHelper = new WebApiHelper();
        private readonly string BASE = "api/Notificacion";

        #region Save
        /// <summary>
        /// Envia la peticion POST a api/Notificacion para configurar las notificaciones del sistema
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ConfigNotificacionModel> Save(ConfigNotificacionModel model, string token)
        {
            ConfigNotificacionModel configNotificacionModel = new ConfigNotificacionModel();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            string jsonData = JsonConvert.SerializeObject(model);
            StringContent content = _apiHelper.GetSerializedJson(jsonData);
            try
            {
                var response = await httpClient.PostAsync(BASE, content);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    configNotificacionModel = JsonConvert.DeserializeObject<ConfigNotificacionModel>(resultJson);
                }
                else
                {
                    configNotificacionModel = null;
                }
                
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                configNotificacionModel = null;
            }
            return configNotificacionModel;
        }
        #endregion

        #region tipo
        /// <summary>
        /// Lista todos los tipos de notificacion que se encuentran registrados
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tipo_NotificacionesDto>> Tipos(string token)
        {
            List<Tipo_NotificacionesDto> tipos = new List<Tipo_NotificacionesDto>();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response = await httpClient.GetAsync($"{BASE}/Tipos");
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    tipos = JsonConvert.DeserializeObject<List<Tipo_NotificacionesDto>>(resultJson);
                }
            }
            catch (Exception ex)
            {
                string mesg = ex.Message;                
            }
            return tipos;
        }
        #endregion

        #region variables
        public async Task<List<Variables_NotificacionDto>> Variables(int tipo, string token)
        {
            List<Variables_NotificacionDto> variables = new List<Variables_NotificacionDto>();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response = await httpClient.GetAsync($"{BASE}/Variables/?tipo={tipo}");
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    variables = JsonConvert.DeserializeObject<List<Variables_NotificacionDto>>(resultJson);
                }
            }
            catch (Exception ex)
            {
                string mesg = ex.Message;
            }
            return variables;
        }
        #endregion
    }
}
