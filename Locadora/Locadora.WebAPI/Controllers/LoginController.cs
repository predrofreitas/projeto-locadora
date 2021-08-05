using System;
using Microsoft.AspNetCore.Mvc;
using Locadora.Dominio.Entidades;
using Locadora.Comuns.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Locadora.Dominio.Interfaces;
using Microsoft.Extensions.Logging;
using Locadora.Dados;
using Locadora.WebAPI.Handlers;
using Locadora.Comuns;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Locadora.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly LocadoraContext _locadoraContext;

        public LoginController(ILogger<LoginController> logger,
            IRepositorioCliente repositorioCliente,
            LocadoraContext locadoraContext)
        {
            _logger = logger;
            _locadoraContext = locadoraContext;
            _repositorioCliente = repositorioCliente;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Autenticar(LoginDto loginDto)
        {

            var cadastrarCliente = new CadastrarClienteHandler(_locadoraContext, _repositorioCliente);
            var clienteDto = cadastrarCliente.BuscarPorNome(loginDto.Nome);

            try
            {
                if (clienteDto.Email == loginDto.Email && clienteDto.Senha == loginDto.Senha)
                {
                    var token = GerarToken(clienteDto);

                    return Ok(new TokenDto() { Email = clienteDto.Email, Token = token });
                }
                else
                {
                    return NotFound("Usuário e/ou senha inválida!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Requisição fora do padrão");
            }
        }

        private static string GerarToken(ClienteDto clienteDto)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Settings.Secret);
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, clienteDto.Nome),
                    new Claim(ClaimTypes.Email, clienteDto.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave),
                                                                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtSecurityTokenHandler.CreateToken(descriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
