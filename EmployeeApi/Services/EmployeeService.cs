using EmployeeApi.Exceptions.Department;
using EmployeeApi.Exceptions.Employee;
using EmployeeApi.Exceptions.Passport;
using EmployeeApi.Models;
using EmployeeApi.Repositories.Interfaces;
using EmployeeApi.Requests;
using EmployeeApi.Services.Interfaces;

namespace EmployeeApi.Services
{
    public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        public async Task<int> CreateEmployee(CreateEmployeeRequest request)
        {
            if(await employeeRepository.IsPassportIdAlreadyUsedAsync(request.PassportId, null))
            {
                throw new PassportIdAlreadyUsedException(request.PassportId);
            }
            if(!(await employeeRepository.IsDepartmentExistsAsync(request.DepartmentId)))
            {
                throw new DepartmentNotFoundException(request.DepartmentId);
            }
            return await employeeRepository.AddEmployeeAsync(request);
        }

        public async Task DeleteEmployee(int id)
        {
            if((await employeeRepository.GetEmployeeByIdAsync(id)) == null)
            {
                throw new EmployeeNotFoundException(id);
            }
            await employeeRepository.DeleteEmployeeByIdAsync(id);
        }

        public Task<IEnumerable<dynamic>> GetAllEmployees()
        {
            return employeeRepository.GetAllEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await employeeRepository.GetEmployeeByIdAsync(id)
                ?? throw new EmployeeNotFoundException(id);
        }

        public async Task<IEnumerable<dynamic>> GetEmployeesByCompany(int id)
        {
            return await employeeRepository.GetEmployeesByCompanyIdAsync(id);
        }

        public async Task<IEnumerable<dynamic>> GetEmployeesByDepartment(int companyId, int departmentId)
        {
            return await employeeRepository.GetEmployeesByDepartmentIdAsync(companyId, departmentId);
        }

        public async Task UpdeteEmployee(UpdateEmployeeRequest request)
        {
            if((await employeeRepository.GetEmployeeByIdAsync(request.Id)) == null)
            {
                throw new EmployeeNotFoundException(request.Id);
            }
            await employeeRepository.UpdateEmployeeAsync(request);
        }
    }
}
