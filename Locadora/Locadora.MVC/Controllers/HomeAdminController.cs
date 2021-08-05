using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locadora.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Locadora.MVC.Services;
using Locadora.Comuns.Dtos;
using System.Net.Http;
using System.Text.Json;

namespace Locadora.MVC.Controllers
{
    public class HomeAdminController : Controller
    { 

        private readonly IHttpClientFactory _httpClientFactory;
        public HomeAdminController (IHttpClientFactory httpClient) {
            _httpClientFactory = httpClient;

        }
        public async Task<IActionResult> index() {

            var client = _httpClientFactory.CreateClient("api-cliente");

            var response = await client.GetAsync("posts/4");

            
             using var responseStream = await response.Content.ReadAsStreamAsync();
             var Cliente = await JsonSerializer.DeserializeAsync
                        <ClienteDto>(responseStream);
            
           
            return View(Cliente);

        }

        public async Task<IActionResult> cadastroCliente(string nome,
                                                         DateTime data,
                                                       string cpf,
                                                       string email,
                                                       string rua,
                                                       int numero,
                                                       string bairro,
                                                       string cep,
                                                       string cidade,
                                                       string estado
            ) {
        var cliente = new ClienteDto();
        cliente.Nome = nome;
        cliente.DataNascimento = data;
        cliente.Cpf = cpf;
        cliente.Email = email;
        cliente.Rua = rua;
        cliente.Numero = numero;
        cliente.Bairro = bairro;
        cliente.Cep = cep;
        cliente.Cidade = cidade;
        cliente.Estado = estado;

            return View();
        }

        public IActionResult cadastroItens() {
            return View();
        }
    }
}
