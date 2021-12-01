using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVCAgenda.ApiHost.Models;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        #region Constructor

        public TokenController(AgendaContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region Fields

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AgendaContext _context;

        #endregion

        [HttpPost("getToken")]
        public async Task<ActionResult> GetToken([FromBody] LoginModel loginModel)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == loginModel.Email);
            if (user != null)
            {
                var singInResult = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

                if (singInResult.Succeeded)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(Constants.ApyKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, loginModel.Email)
                        }),
                        Expires = DateTime.Now.AddDays(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString });
                }
                else
                { return Unauthorized("Wrong, try again."); }
            }
            else
                return Unauthorized("Wrong, try again.");
        }

        public async Task<ActionResult> ValidateJwtToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.ApyKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = key  
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
