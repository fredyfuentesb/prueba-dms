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
    public class TercerosAplicacion
    {
        private readonly WebApiHelper _apiHelper = new WebApiHelper();
        private readonly string BASE = "api/Tercero";

        #region Save
        /// <summary>
        /// Envia la peticion POST  a api/Tercero para guardar los datos
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TerceroModel> Save(TerceroModel model)
        {
            TerceroModel terceroCreado = new TerceroModel();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            string jsonData = JsonConvert.SerializeObject(model);
            StringContent content = _apiHelper.GetSerializedJson(jsonData);

            try
            {
                var response = await httpClient.PostAsync(BASE, content);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    terceroCreado = JsonConvert.DeserializeObject<TerceroModel>(resultJson);
                }
                else
                {
                    terceroCreado = null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                terceroCreado = null;
            }

            return terceroCreado;
        }
        #endregion

        #region Update
        /// <summary>
        /// Envia la peticion PUT  a api/Tercero para guardar los datos
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TerceroModel> Update(TerceroModel model)
        {
            TerceroModel terceroCreado = new TerceroModel();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            string jsonData = JsonConvert.SerializeObject(model);
            StringContent content = _apiHelper.GetSerializedJson(jsonData);

            try
            {
                var response = await httpClient.PutAsync(BASE, content);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    terceroCreado = JsonConvert.DeserializeObject<TerceroModel>(resultJson);
                }
                else
                {
                    terceroCreado = null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                terceroCreado = null;
            }

            return terceroCreado;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Envia la peticion DELETE  a api/Tercero para eliminar los datos
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TercerosDto> Delete(int id)
        {
            TercerosDto terceroEliminado = new TercerosDto();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");            

            try
            {
                var response = await httpClient.DeleteAsync($"{BASE}/?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    terceroEliminado = JsonConvert.DeserializeObject<TercerosDto>(resultJson);
                }
                else
                {
                    terceroEliminado = null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                terceroEliminado = null;
            }

            return terceroEliminado;
        }
        #endregion

        #region List
        /// <summary>
        /// Envia una peticion get a api/Tercero para obtener la lista de los terceros registrados
        /// </summary>
        /// <returns></returns>
        public async Task<List<TercerosDto>> List()
        {
            List<TercerosDto> terceros = new List<TercerosDto>();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");

            try
            {
                var response = await httpClient.GetAsync(BASE);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    terceros = JsonConvert.DeserializeObject<List<TercerosDto>>(resultJson);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return terceros;
        }
        #endregion
    }
}
