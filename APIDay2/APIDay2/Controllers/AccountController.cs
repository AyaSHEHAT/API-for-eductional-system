using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIDay2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

            [HttpGet]
            public ActionResult login(string username, string password)
            {
                if (username == "admin" && password == "123")
                {
                    List<Claim> userdata = new List<Claim>() 
                    { 
                        new Claim("username", "admin"),
                        new Claim(ClaimTypes.MobilePhone, "0123456789")
                    };
                    string key = "HERE IS THE SECRET KEY FOR AYA App";
                    var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                    var signingcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        claims: userdata,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: signingcer
                        );
                    var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                   return Ok(tokenstring);

                }
                else
                    return Unauthorized();
            }

            [HttpPost]
            [Authorize]
            public ActionResult add()
            {
                return Ok();
            }
        }

}

