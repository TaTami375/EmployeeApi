
using Microsoft.AspNetCore.Mvc.Core;
namespace EmployeeApi.Exceptions.Department
{
    public class DepartmentNotFoundException (int id) : Exception($"Department with id {id} not found");
}
