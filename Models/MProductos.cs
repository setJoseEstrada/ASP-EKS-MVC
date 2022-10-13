using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace EKS.Models
{
    public class MProductos
    {

        public List<productos> Consultar(string token)
        {
            List<productos> lista = new List<productos>();
            try
            {
                using ( var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> httpResponse = cliente.GetAsync("https://localhost:44308/api/Productos");

                    httpResponse.Wait();

                    HttpResponseMessage httpResponseMessage = httpResponse.Result;

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        Task<string> asincronoleerhttp = httpResponseMessage.Content.ReadAsStringAsync();
                        asincronoleerhttp.Wait();

                        string json = asincronoleerhttp.Result;

                        lista = JsonConvert.DeserializeObject<List<productos>>(json);
                    }
                    else
                    {

                        throw new Exception($"Web Api. Repondio con error.{httpResponseMessage.StatusCode}");
                    }


                }


            }
            catch (Exception ex)
            {
                throw new Exception($"Web Api. Repondio con error.{ex.Message}");
            }

            return lista;
        }

        public productos Crear(productos objproductos, string token)
        {
            productos obj = new productos();    
            try
            {   
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(objproductos), Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var postTask = cliente.PostAsync("https://localhost:44308/api/Productos", httpContent);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (postTask.IsCompleted)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string json = readTask.Result;

                        obj = JsonConvert.DeserializeObject<productos>(json);
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

        public productos Consultaruno(int id, string token)
        {
            productos productos = new productos();

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var responseTask = cliente.GetAsync("https://localhost:44308/api/Productos" + $"/{id}");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string json = readTask.Result;

                        productos = JsonConvert.DeserializeObject<productos>(json);

                    }
                    else
                    {

                        throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"WebAPI. Respondio con error.{ex.Message}");
            }
            return productos;
        }

        public void Eliminar(int id,string token)
        {
            using (var cliente = new HttpClient())
            {

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var respondeTask = cliente.DeleteAsync("https://localhost:44308/api/Productos" + $"/{id}");

                respondeTask.Wait();
                var resultado = respondeTask.Result;
                if (!resultado.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error: {resultado.StatusCode}");
                }

            }
        }
        public void Modificar(productos objproductos, string token)
        {
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(objproductos), Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var postTask = cliente.PutAsync("https://localhost:44308/api/Productos" + $"/{objproductos.id}", httpContent);

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