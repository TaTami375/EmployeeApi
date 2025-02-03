using EmployeeApi.Models;
using EmployeeApi.Repositoryes;
using EmployeeApi.Repositoryes.Interfaces;
using EmployeeApi.Requests;
using EmployeeApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportController(IPassportService passportService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddPassport([FromBody] CreatePassportRequest passport)
        {
            return Ok(await passportService.CreatePassport(passport));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassport(int id)
        {
            await passportService.DeletePassport(id);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPassportById(int id)
        {
            return Ok(await passportService.GetPassportById(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPassports()
        {
            return Ok(await passportService.GetAllPassport());
        }
    }
}
