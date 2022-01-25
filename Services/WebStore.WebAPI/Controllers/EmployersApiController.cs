using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/employers")]
    [ApiController]
    public class EmployersApiController : ControllerBase
    {
        private readonly IEmployerData _employerData;

        public EmployersApiController(IEmployerData employerData) => _employerData = employerData;

        [HttpGet]
        public IActionResult Get()
        {
            var employers = _employerData.GetAll();
            return Ok(employers);
        }
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var employer = _employerData.GetById(ID);
            if (employer is null)
                return NotFound();
            return Ok(employer);
        }
        [HttpPost]
        public IActionResult Add(Employer employer)
        {
            var id = _employerData.Add(employer);
            return CreatedAtAction(nameof(GetByID), new { id = id }, employer);
        }
        [HttpPut]
        public IActionResult Update(Employer employer)
        {
            var success = _employerData.Edit(employer);
            return Ok(success);
        }
        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var result =_employerData.Delete(ID);
            return result ? Ok(true) : NotFound(false);
        }
    }
}
