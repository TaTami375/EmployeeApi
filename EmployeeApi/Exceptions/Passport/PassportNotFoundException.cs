namespace EmployeeApi.Exceptions.Passport
{
    public class PassportNotFoundException(int id) : Exception($"Passport with id {id} not found");
}
