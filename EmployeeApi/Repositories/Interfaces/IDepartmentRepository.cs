using EmployeeApi.Models;
using EmployeeApi.Requests;

namespace EmployeeApi.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task<IEnumerable<dynamic>> GetAllDepartmentsAsync();
        Task<int> AddDepartmentAsync(CreateDepartmentRequest department);
        Task DeleteDepartmentByIdAsync(int id);
        Task<bool> IsDepartmentUsedByEmployeeAsync(int departmentId);
    }
}
