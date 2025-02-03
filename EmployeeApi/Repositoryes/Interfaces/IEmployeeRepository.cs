using EmployeeApi.Models;
using EmployeeApi.Requests;

namespace EmployeeApi.Repositoryes.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<int> AddEmployeeAsync(CreateEmployeeRequest employee);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<dynamic>> GetAllEmployeesAsync();
        Task<IEnumerable<dynamic>> GetEmployeesByCompanyIdAsync(int companyId);
        Task<IEnumerable<dynamic>> GetEmployeesByDepartmentIdAsync(int companyId, int departmentId);
        Task UpdateEmployeeAsync(UpdateEmployeeRequest request);
        Task<bool> IsPassportIdAlreadyUsedAsync(int? passportId, int? employeeId);
        Task DeleteEmployeeByIdAsync(int id);
        Task<bool> IsDepartmentExistsAsync(int id);
    }
}
