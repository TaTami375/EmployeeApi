namespace EmployeeApi.Exceptions.Passport
{
    public class PassportIdAlreadyUsedException(int id): Exception($"Passport with id {id} is already used by another employee");
}
