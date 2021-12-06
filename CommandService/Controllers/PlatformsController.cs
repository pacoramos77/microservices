using Microsoft.AspNetCore.Mvc;

namespace CommandService.controllers
{
  [Route("api/c/[controller]")]
  [ApiController]
  public class PlatformsController : ControllerBase
  {
    public PlatformsController()
    {

    }


    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      Console.WriteLine("---> TestInboundConnection");

      return Ok("ASD");
    }
  }
}