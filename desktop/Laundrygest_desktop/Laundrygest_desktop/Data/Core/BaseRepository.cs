using Laundrygest_desktop.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Data
{
    public class BaseRepository
    {
        public static string urlApi = ConfigController.GetSettings().ApiUrl;
        private static HttpClient httpClient = new HttpClient { BaseAddress = new Uri(ConfigController.GetSettings().ApiUrl) };

        readonly string ErrorMessage = "Error en l'API.";
        readonly string contentType = "application/json";

        public static async Task<bool> ConnectAsync(string url)
        {
            try
            {   
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, errors) => true;
                httpClient = new HttpClient(handler) { BaseAddress = new Uri(url) };
                HttpResponseMessage response = httpClient.GetAsync(url + "clients/").Result;

                return response.IsSuccessStatusCode;                
            }
            catch
            {
                return false;
            }
        }

        public async Task<T> MakeRequest<T>(string url, string method, object JSONcontent) {
            HttpResponseMessage response;
            if (method == "DELETE")
            {
                response = httpClient.DeleteAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return default;
                }
                else
                {
                    throw new Exception(ErrorMessage);
                }
            }
            else if (method == "GET")
            {
                response = httpClient.GetAsync(url).Result;
            }
            else if (method == "POST" || method == "PUT")
            {
                var objectJson = JsonConvert.SerializeObject(JSONcontent);
                var content = new StringContent(objectJson, Encoding.UTF8, contentType);
                if (method == "POST")
                {
                    response = httpClient.PostAsync(url, content).Result;
                }
                else
                {
                    response = httpClient.PutAsync(url, content).Result;
                }
            }
            else
            {
                return default;
            }
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(json, typeof(T));
                return (T)result;
            }
            else
            {
                throw new Exception(ErrorMessage);
            }
        }

    }
}
