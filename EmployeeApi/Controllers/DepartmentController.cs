using EmployeeApi.Models;
using EmployeeApi.Repositories;
using EmployeeApi.Repositories.Interfaces;
using EmployeeApi.Requests;
using EmployeeApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IDepartmentService departmentService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] CreateDepartmentRequest department)
        {
            return Ok(await departmentService.CreateDepartment(department));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await departmentService.DeleteDepartment(id);

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {

            return Ok(await departmentService.GetDepartmentById(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            return Ok(await departmentService.GetAllDepartments());
        }
    }
}
