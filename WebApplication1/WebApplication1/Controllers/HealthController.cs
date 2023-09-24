

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapStore.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Endpoint Health
        /// </summary>
        /// <returns>Retorna se a aplicação está saudável ou não</returns>
        /// <response code="200">A aplicação está saudável</response>
        [HttpGet]
        [Route("health")]
        [AllowAnonymous]
        public string Health() => "Healthy!";
    }
}
