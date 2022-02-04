using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers.API
{
    [Route("api/console")]
    [ApiController]
    public class ConsoleApiController : ControllerBase
    {
        public void Clear() => Console.Clear();
        public void ConsoleWrite(string message) => Console.WriteLine(message);
    }
}
