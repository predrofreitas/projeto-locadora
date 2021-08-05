using Locadora.Comuns.Dtos;
using Locadora.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Locadora.MVC.Services
{
     public class ClienteService: IClienteService
    {
        static  HttpClient _httpClient;
        static  string _Uri;
        ClienteService(HttpClient httpClient) {

            _httpClient = httpClient;
            _Uri = "https://localhost:5002/cliente";
        }

        public async Task<ClienteDto> getClienteId(int id) {
            var response = await _httpClient.GetAsync(_Uri + id);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync
                <ClienteDto>(responseStream);
        }

        public async Task createCliente(ClienteDto cliente) { 
            var clientejson = new StringContent(
                JsonSerializer.Serialize(cliente),
                Encoding.UTF8,
                "application/json");
            using var httpResponse =
                await _httpClient.PostAsync(_Uri, clientejson);

            httpResponse.EnsureSuccessStatusCode();
        }

        public async Task deleteCliente(int id) {
            using  var response  = await _httpClient.DeleteAsync(_Uri + id);
             response.EnsureSuccessStatusCode();
        }
    }
}
