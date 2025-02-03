using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeApi.Exceptions.Department
{
    public class DepartmentBadRequestException(int id): Exception($"Department with id {id} is associated with an employee and cannot be deleted.");
}
