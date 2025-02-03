using EmployeeApi.Models;
using EmployeeApi.Exceptions.Department;
using EmployeeApi.Repositories;
using EmployeeApi.Repositories.Interfaces;
using EmployeeApi.Requests;
using EmployeeApi.Services.Interfaces;
using EmployeeApi.Requests;

namespace EmployeeApi.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        public async Task<int> CreateDepartment(CreateDepartmentRequest request)
        {
            return await departmentRepository.AddDepartmentAsync(request);
        }

        public async Task DeleteDepartment(int id)
        {
            if (await departmentRepository.IsDepartmentUsedByEmployeeAsync(id))
            {
                throw new DepartmentBadRequestException(id);
            }
            if((await departmentRepository.GetDepartmentByIdAsync(id)) == null)
            {
                throw new DepartmentNotFoundException(id);
            }
            await departmentRepository.DeleteDepartmentByIdAsync(id);
                
        }

        public Task<IEnumerable<dynamic>> GetAllDepartments()
        {
            return departmentRepository.GetAllDepartmentsAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await departmentRepository.GetDepartmentByIdAsync(id)
                ?? throw new DepartmentNotFoundException(id);
        }
    }
}
