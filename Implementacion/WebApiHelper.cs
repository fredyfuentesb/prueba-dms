using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Implementacion
{
    public class WebApiHelper
    {
        #region GenericHttpClient
        /// <summary>
        /// Este metodo devuelve un HttpClient serializado para header json
        /// </summary>
        /// <param name="webApiUrl"></param>
        /// <returns></returns>
        public HttpClient GenericHttpClient(string webApiUrl)
        {
            string baseUrl = ConfigurationManager.AppSettings[webApiUrl];
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            return httpClient;
        }
        #endregion

        #region GetSerializedJson
        /// <summary>
        /// Este metodo serializa un string en un json con utf-8
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public StringContent GetSerializedJson(string jsonData)
        {
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "UTF-8" };
            return content;
        }
        #endregion
    }
}
