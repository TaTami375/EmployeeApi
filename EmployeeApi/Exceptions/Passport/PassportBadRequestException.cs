namespace EmployeeApi.Exceptions.Passport
{
    public class PassportBadRequestException(int id) : Exception($"Passport with id {id} is associated with an employee and cannot be deleted.");

}
