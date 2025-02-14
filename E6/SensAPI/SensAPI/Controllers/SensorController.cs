using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SensAPI.Model;
using System.Text;

namespace SensAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase
    {

        [HttpGet]
        [Authorize] // Autenticación JWT
        public IActionResult ObtenerDatos([FromQuery] int pagina = 1, int tamanoPagina = 10)
        {
            var datos = Enumerable.Range(1, 5).Select(index => new Sensors
            {
                FechaHora = DateTime.Now.AddDays(index),
                Valor = Random.Shared.Next(-20, 55),
                Id = index//Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            var paginaResultados = datos.Skip((pagina - 1) * tamanoPagina).Take(tamanoPagina).ToList();
            return Ok(paginaResultados);
        }
    }
}
