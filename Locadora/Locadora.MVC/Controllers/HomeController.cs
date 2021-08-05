using Locadora.MVC.Models;
using Locadora.Comuns.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace Locadora.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> IndexAsync()
        {

            var httpClient = _httpClientFactory.CreateClient("api");
            string bearer = "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlBlZHJvIiwiZW1haWwiOiJwZWRyb0BjdXJzby5jb20iLCJuYmYiOjE2MjgxOTA0NDQsImV4cCI6MTYyODE5NDA0NCwiaWF0IjoxNjI4MTkwNDQ0fQ.eML0OC6RSpnORt7NXEyUqFSLnY7Iy-XKOxg7CuI9USE";

            httpClient.DefaultRequestHeaders.Add("Authorization",
                "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlBlZHJvIiwiZW1haWwiOiJwZWRyb0BjdXJzby5jb20iLCJuYmYiOjE2MjgxOTA0NDQsImV4cCI6MTYyODE5NDA0NCwiaWF0IjoxNjI4MTkwNDQ0fQ.eML0OC6RSpnORt7NXEyUqFSLnY7Iy-XKOxg7CuI9USE");

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "Cliente");


            using var response = await httpClient.PostAsJsonAsync(
                "Cliente",
                new ClienteDto()
                {
                    Email = "mallu@curso.com",
                    Nome = "Mallu",
                    Senha = "999"
                });


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
