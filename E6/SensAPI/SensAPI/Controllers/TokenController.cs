using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SensAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        [HttpGet]
        public string Token()
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Su32car4ct3rK3yF0rHs256SCu5FunV4uL"));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            return new JsonWebTokenHandler()
                .CreateToken(new SecurityTokenDescriptor
                {
                    SigningCredentials = Credentials,

                });
        }
    }
}
