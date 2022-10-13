using Antlr.Runtime.Misc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EKS.Models
{
    public class MMarcas
    {
        public List<marca> Consultar(string token)
        {
            List<marca> list = new List<marca>();

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token);
                    Task<HttpResponseMessage> httpResponseMessage = cliente.GetAsync("https://localhost:44308/api/Marcas");
                    httpResponseMessage.Wait();
                    var httpresponde = httpResponseMessage.Result;
                    if(httpresponde.IsSuccessStatusCode)
                    {
                        Task<string> asincornoleerhttp = httpresponde.Content.ReadAsStringAsync();
                        asincornoleerhttp.Wait();
                        string json = asincornoleerhttp.Result;
                        list = JsonConvert.DeserializeObject<List<marca>>(json);

                    }
                    else
                    {
                        throw new Exception($"Web Api. Repondio con error.{httpresponde.StatusCode}");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Api no respondio{ex.Message}");
            }

            return list;

        }
        public marca Crear(marca objmarca, string token)
        {
            marca obj = new marca();
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(objmarca), Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var postTask = cliente.PostAsync("https://localhost:44308/api/Marcas", httpContent);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (postTask.IsCompleted)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string json = readTask.Result;

                        obj = JsonConvert.DeserializeObject<marca>(json);
                    }
                    else
                    {
                        throw new Exception($"{result.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
            return obj;
        }

        public marca Consultaruno(int id, string token)
        {
            marca productos = new marca();

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var responseTask = cliente.GetAsync("https://localhost:44308/api/Marcas" + $"/{id}");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string json = readTask.Result;

                        productos = JsonConvert.DeserializeObject<marca>(json);

                    }
                    else
                    {

                        throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebAPI. Respondio con error.{ex.Message}");
            }
            return productos;
        }

        public void Eliminar(int id, string token)
        {
            using (var cliente = new HttpClient())
            {

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var respondeTask = cliente.DeleteAsync("https://localhost:44308/api/Marcas" + $"/{id}");

                respondeTask.Wait();
                var resultado = respondeTask.Result;
                if (!resultado.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error: {resultado.StatusCode}");
                }

            }
        }
        public void Modificar(marca objproductos, string token)
        {
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(objproductos), Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var postTask = cliente.PutAsync("https://localhost:44308/api/Marcas" + $"/{objproductos.id}", httpContent);

                postTask.Wait();
                var result = postTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }
            }
        }
    }
}