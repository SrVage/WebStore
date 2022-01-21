using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly Dictionary<int, string> _values = Enumerable.Range(1, 10)
            .Select(i => (Id: i, Value: $"Value-{i}"))
            .ToDictionary(v => v.Id, v => v.Value);
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get() => Ok(_values.Values);
        [HttpGet("{ID}")]
        public IActionResult GetById(int ID)
        {
            if (!_values.ContainsKey(ID))
                return NotFound();
            return Ok(_values[ID]);
        }
        [HttpGet("count")]
        public IActionResult Count() => Ok(_values.Count);
        [HttpPost]
        [HttpPost("add")]
        public IActionResult Add([FromBody]string value)
        {
            var id = _values.Count == 0 ? 1 : _values.Keys.Max() + 1;
            _values[id] = value;
            return CreatedAtAction(nameof(GetById), new { id });
        }
        [HttpPut("{ID}")]
        public IActionResult Replace(int ID, [FromBody] string value)
        {
            if (!_values.ContainsKey(ID))
                return NotFound();
            _values[ID] = value;
            return Ok();
        }
        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            if (!_values.ContainsKey(ID))
                return NotFound();
            _values.Remove(ID);
            return Ok();
        }
    }
}
