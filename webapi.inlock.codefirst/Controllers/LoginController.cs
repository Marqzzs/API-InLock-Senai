using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webapi.inlock.codefirst.Domains;
using webapi.inlock.codefirst.Interfaces;
using webapi.inlock.codefirst.Repositories;
using webapi.inlock.codefirst.ViewModels;

namespace webapi.inlock.codefirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel usuario)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                if(usuarioBuscado == null)
                {
                    return NotFound("Email ou senha invalidos");
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),

                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),

                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticacao-webapi-dev-codefirst"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                    (
                        //emissor do token
                        issuer: "webapi.inlock.codefirt",

                        //Destinatario do token
                        audience: "webapi.inlock.codefirt",

                        //Dados definidos nas Claims(informações)
                        claims: claims,

                        //Tempo de expiração do token
                        expires: DateTime.Now.AddMinutes(5),

                        //Credenciais do token
                        signingCredentials: creds
                    );
                return Ok(new
                {

                    token = new JwtSecurityTokenHandler().WriteToken(token)

                });
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
