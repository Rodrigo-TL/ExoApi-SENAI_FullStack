using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ExoApi.Contexts;
using ExoApi.Models;

namespace ExoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ExoContext _context;

        public LoginController()
        {
            _context = new ExoContext();
        }

        [HttpPost]
        public IActionResult Login(Usuario login)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuarios.FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);

                if (usuarioBuscado == null)
                {
                    return Unauthorized(new { mensagem = "E-mail ou senha invalidos." });
                }

                // Define os dados (Claims) que irão dentro do Token
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString())
                };

                // Chave secreta para assinar o Token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("exoapi-chave-autenticacao-senai"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Gera o Token com validade de 30 minutos
                var token = new JwtSecurityToken(
                    issuer: "exoapi.webapi",
                    audience: "exoapi.webapi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao realizar login.", detalhes = ex.Message });
            }
        }
    }
}
