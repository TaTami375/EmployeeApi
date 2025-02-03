using EmployeeApi.Models;
using EmployeeApi.Requests;
namespace EmployeeApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployee(CreateEmployeeRequest request);
        Task<Employee?> GetEmployeeById(int id);
        Task<IEnumerable<dynamic>> GetAllEmployees();
        Task DeleteEmployee(int id);
        Task UpdeteEmployee(UpdateEmployeeRequest request);
        Task<IEnumerable<dynamic>> GetEmployeesByCompany(int id);
        Task<IEnumerable<dynamic>> GetEmployeesByDepartment(int companyId, int departmentId);
    }
}
