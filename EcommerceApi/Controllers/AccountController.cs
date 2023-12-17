using EcommerceApi.Data.Models;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager , IConfiguration configuration)
        {
           _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(RegisterDTO data)
        {

            if (ModelState.IsValid)
            {
                User user = new User { 
                    UserName = data.username,
                    Email = data.email,
                };
                IdentityResult result = await _userManager.CreateAsync(user,data.password);
                if (result.Succeeded)
                {
                    return Ok(user);
                }
                else
                {
                    foreach ( var error in result.Errors )
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginCreds)
        {
            if (ModelState.IsValid)
            {
                User? user =await _userManager.FindByNameAsync(loginCreds.username);
                if (user != null)
                {
                    if(await _userManager.CheckPasswordAsync(user , loginCreds.password))
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, loginCreds.username));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach(var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                        var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            expires: DateTime.Now.AddDays(3),
                            signingCredentials: signingCreds
                            );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                        };
                        return Ok(_token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong creadintials !");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
