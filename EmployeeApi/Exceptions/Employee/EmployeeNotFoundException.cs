namespace EmployeeApi.Exceptions.Employee
{
    public class EmployeeNotFoundException(int id) : Exception($"Employee with id {id} not found");
}
