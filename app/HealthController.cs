using System.Web;
using Microsoft.AspNetCore.Mvc;

public class HealthController : Controller
{
    [HttpGet]
    [Route("/healthz/alive")]
    public IActionResult Alive()
    {
      return StatusCode(500);
    }
}