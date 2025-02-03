using EmployeeApi.Models;
using EmployeeApi.Requests;

namespace EmployeeApi.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<int> CreateDepartment(CreateDepartmentRequest request);
        Task<Department?> GetDepartmentById(int id);
        Task<IEnumerable<dynamic>> GetAllDepartments();
        Task DeleteDepartment(int id);
    }
}
