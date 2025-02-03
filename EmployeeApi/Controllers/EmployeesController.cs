using EmployeeApi.Models;
using EmployeeApi.Repositoryes.Interfaces;
using EmployeeApi.Requests;
using EmployeeApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeRequest employee)
        {
            return Ok(await employeeService.CreateEmployee(employee));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await employeeService.DeleteEmployee(id);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            return Ok(await employeeService.GetEmployeeById(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await employeeService.GetAllEmployees());
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetEmployeesByCompanyId(int companyId)
        {

            return Ok(await employeeService.GetEmployeesByCompany(companyId));
        }

        [HttpGet("company/{companyId}/department/{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartmentId(int companyId, int departmentId)
        {
            return Ok(await employeeService.GetEmployeesByDepartment(companyId, departmentId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest request)
        {
            await employeeService.UpdeteEmployee(request);

            return Ok(request.Id);
        }
    }
}