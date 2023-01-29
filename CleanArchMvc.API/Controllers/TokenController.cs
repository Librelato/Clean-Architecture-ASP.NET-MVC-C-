using CleanArchMvc.API.DTOs;
using CleanArchMvc.Domain.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public TokenController(  IAuthenticate authentication
                               , IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(nameof(authentication));
            ArgumentNullException.ThrowIfNull(nameof(configuration));

            _authentication=authentication;
            _configuration=configuration;
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> CreateUser([FromBody] LoginDTO userInfo)
        {
            var result = await _authentication.RegisterUser(userInfo.Email, userInfo.Password);
            if (result)
            {
                return Ok($"Usuário {userInfo.Email} criado com sucesso!");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginDTO userInfo)
        {
            var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);
            if (result)
            {
                return GenerateToken(userInfo);
            } else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(LoginDTO userInfo)
        {
            //declarações do usuário (claims)
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuValor", "o que eu quiser"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) //Jti é o ID do token. JwtRegisteredClaimNames são estructure criadas
            };

            //gerar chave privada para criar o token
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //gerar a assinatura do token
            var credentials = new SigningCredentials(privateKey,SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração do token
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                    //emissor
                    issuer: _configuration["Jwt:Issuer"],
                    //audiencia
                    audience: _configuration["Jwt:Audience"],
                    //claims
                    claims: claims,
                    //expiration
                    expires: expiration,
                    //credential (assinatura digital)
                    signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
