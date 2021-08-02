using Implementacion.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Transversal.Dtos;

namespace Implementacion.Implementacion
{
    public class Tercero_ArchivosAplicacion
    {
        private readonly WebApiHelper _apiHelper = new WebApiHelper();
        private readonly string BASE = "api/TerceroArchivo";

        #region Save
        /// <summary>
        /// Envia la peticion POST a api/TerceroArchivo para guardar los archivos de los terceros
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TerceroArchivoModel> Save(TerceroArchivoModel model, string token)
        {
            TerceroArchivoModel terceroArchivoModel = new TerceroArchivoModel();
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
                    terceroArchivoModel = JsonConvert.DeserializeObject<TerceroArchivoModel>(resultJson);
                }
                else
                {
                    terceroArchivoModel = null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                terceroArchivoModel = null;
            }

            return terceroArchivoModel;
        }
        #endregion

        #region List
        /// <summary>
        /// Envia una peticion GET a api/TercerAplicacion/{id}/List para mostrar los archivos asociados a un tercero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Tercero_ArchivosDto>> List(int id, string token)
        {
            List<Tercero_ArchivosDto> terceroArchivos = new List<Tercero_ArchivosDto>();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response = await httpClient.GetAsync($"{BASE}/{id}/List");
                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    terceroArchivos = JsonConvert.DeserializeObject<List<Tercero_ArchivosDto>>(resultJson);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return terceroArchivos;
        }
        #endregion

        #region FindById
        public async Task<Tercero_ArchivosDto> FindById(int id, string token)
        {
            Tercero_ArchivosDto terceroArchivo = new Tercero_ArchivosDto();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await httpClient.GetAsync($"{BASE}/?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    terceroArchivo = JsonConvert.DeserializeObject<Tercero_ArchivosDto>(resultJson);
                }
                else
                {
                    terceroArchivo = null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                terceroArchivo = null;
            }

            return terceroArchivo;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Envia una peticion DELETE a api/TercerArchivo para eliminar logicamente un archivo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tercero_ArchivosDto> Delete(int id, string token)
        {
            Tercero_ArchivosDto terceroArchivoEliminado = new Tercero_ArchivosDto();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await httpClient.DeleteAsync($"{BASE}/?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    terceroArchivoEliminado = JsonConvert.DeserializeObject<Tercero_ArchivosDto>(resultJson);
                }
                else
                {
                    terceroArchivoEliminado = null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                terceroArchivoEliminado = null;
            }

            return terceroArchivoEliminado;
        }
        #endregion
    }
}
