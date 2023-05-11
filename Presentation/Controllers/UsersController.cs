using Domain.Entities;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Presentation.Model;
using Presentation.Token;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            if(string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }

            var resultado = await
                _signInManager.PasswordSignInAsync(login.email, login.senha, false, lockoutOnFailure: false);
            if(resultado.Succeeded)
            {
                var user = new ApplicationUser
                {
                    UserName = login.email,
                    Email = login.email,

                };
                var userCurrent = await _userManager.FindByEmailAsync(login.email);
                var idUsuario = userCurrent.Id;

                var token = new TokenJwtBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Security_Key-12345678"))
                    .AddSubject("Empresa - GisStartup")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaims("idUsuario", idUsuario)
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.Value);
            }
            else
            {
                return Unauthorized();
            }
        }

        public async Task<IActionResult> AdicionarUsuarioIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Ok("Email e/ou Senha invalidos");

            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                CPF = login.cpf,
                Tipo = TipoUsuario.Comum

            };
            var resultado = await _userManager.CreateAsync(user, login.senha);
            if(resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if(result.Succeeded)
            {
                return Ok("Usuario adicionado com sucesso");
            }
            else
            {
                return Ok("Erro ao confirmar usuario");
            }
        }

    }
}
