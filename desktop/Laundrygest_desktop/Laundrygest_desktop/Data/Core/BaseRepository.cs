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
        public static string urlApi = "https://localhost:7194/api/";
        private static HttpClient httpClient = new HttpClient { BaseAddress = new Uri(urlApi) };

        readonly string ErrorMessage = "Error en l'API.";
        readonly string contentType = "application/json";

        public static async Task<bool> ConnectAsync(string url)
        {
            try
            {   
                httpClient = new HttpClient { BaseAddress = new Uri(url)};
                HttpResponseMessage response = httpClient.GetAsync(url + "clients/").Result;

                return response.IsSuccessStatusCode;                
            }
            catch
            {
                return false;
            }
        }

        public async Task<T> MakeRequest<T>(string url, string method, object JSONcontent)
        ////  url: Url a partir de la base 
        ////  method: "GET"/"POST"/"PUT"/"DELETE"
        ////  JSONcontent: objecte que se li passa en el body 
        ////  responseType:  tipus d'objecte que torna el Web Service (=typeof(tipus))

        ////  contentType: "application/json" en els casos que el Web Service torni objectes
        {
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
