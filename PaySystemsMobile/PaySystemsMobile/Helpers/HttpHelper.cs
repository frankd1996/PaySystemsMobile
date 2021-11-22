using Newtonsoft.Json;
using PaySystemsMobile.Views;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Forms.UI;

namespace PaySystemsMobile.Helpers
{
    public class HttpHelper<T>
    {
        //public string baseAdress = "https://192.168.1.110:45455";//Dominio del uri 
        public string baseAdress = "http://paysystemsapi-001-site1.itempurl.com";
        public async Task<T> GetRestServiceDataAsync(string endPoint, string token)
        {
            var client = new HttpClient(new HttpClientHandler());//Con new HttpClientHandler() solucionamos error de certificacion de SSL en Android (URL remota con Conveyor)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Obtiene o establece el intervalo de tiempo de espera antes de que
            //se agote el tiempo de espera de la solicitud
            client.Timeout = TimeSpan.FromSeconds(40);

            //Dominio del uri
            client.BaseAddress = new Uri(baseAdress);

            //endPoint --> resto del uri
            var response = await client.GetAsync(endPoint);

            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }
        public async Task<HttpResponseMessage> PostRestServiceDataAsync(string endPoint, object Objeto)
        {
            var client = new HttpClient(new HttpClientHandler());

            client.BaseAddress = new Uri(baseAdress);

            //Obtiene o establece el intervalo de tiempo de espera antes de que
            //se agote el tiempo de espera de la solicitud
            client.Timeout = TimeSpan.FromSeconds(40);

            string jsonData = JsonConvert.SerializeObject(Objeto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endPoint, content);            
            return response;
        }
    }
}
