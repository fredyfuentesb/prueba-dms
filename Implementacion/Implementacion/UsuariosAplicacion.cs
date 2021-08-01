/* Capa Implementacion
 * Esta clase fue creada para hacer utilizacion de la api de usuarios
 * <autor>Fredy Fuentes</autor>
 * <Fecha>31/07/2021 16:43:10</Fecha>
 * <Cambios>Indique su Nombre, la Fecha y el cambio realizado</Cambios>
 */
using Implementacion.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Transversal.Dtos;

namespace Implementacion.Implementacion
{
    public class UsuariosAplicacion
    {
        private readonly WebApiHelper _apiHelper = new WebApiHelper();
        private readonly string BASE = "api/Usuario/";
        #region Save
        /// <summary>
        /// Envia la peticion POST a api/Usuario para crear un usuario nuevo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UsuarioModel> Save(UsuarioModel model)
        {
            UsuarioModel usuarioCreado = new UsuarioModel();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            string jsonData = JsonConvert.SerializeObject(model);
            StringContent content = _apiHelper.GetSerializedJson(jsonData);
            try
            {
                var response = await httpClient.PostAsync(BASE, content);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    usuarioCreado = JsonConvert.DeserializeObject<UsuarioModel>(resultJson);
                }
                else
                {
                    usuarioCreado = null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                usuarioCreado = null;
            }
            return usuarioCreado;
        }
        #endregion

        #region Update
        /// <summary>
        /// Actualiza la informacion de un usuario enviado la solicitud put a api/Usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UsuarioModel> Update(UsuarioModel model)
        {
            UsuarioModel usuarioModificado = new UsuarioModel();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            string jsonData = JsonConvert.SerializeObject(model);
            StringContent content = _apiHelper.GetSerializedJson(jsonData);
            try
            {
                var response = await httpClient.PutAsync(BASE, content);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    usuarioModificado = JsonConvert.DeserializeObject<UsuarioModel>(resultJson);
                }
                else
                {
                    usuarioModificado = null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                usuarioModificado = null;
            }
            return usuarioModificado;
        }
        #endregion

        #region List
        public async Task<DataSet> List()
        {
            DataSet usuarios = new DataSet();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");

            try
            {
                var response = await httpClient.GetAsync(BASE);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    usuarios = JsonConvert.DeserializeObject<DataSet>(resultJson);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                usuarios = null;
            }

            return usuarios;
        }
        #endregion

        #region CambiarClave
        public async Task<UsuarioCambioClaveModel> CambiarClave(UsuarioCambioClaveModel model)
        {
            UsuarioCambioClaveModel usuarioCambioClave = new UsuarioCambioClaveModel();
            HttpClient httpClient = _apiHelper.GenericHttpClient("base_url");
            string jsonData = JsonConvert.SerializeObject(model);
            StringContent content = _apiHelper.GetSerializedJson(jsonData);
            try
            {
                var response = await httpClient.PutAsync($"{BASE}CambiarClave", content);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    usuarioCambioClave = JsonConvert.DeserializeObject<UsuarioCambioClaveModel>(resultJson);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                usuarioCambioClave = null;
            }

            return usuarioCambioClave;
        }
        #endregion
    }
}
