using EmployeeApi.Models;
using EmployeeApi.Requests;

namespace EmployeeApi.Repositories.Interfaces
{
    public interface IPassportRepository
    {
        Task<Passport?> GetPassportByIdAsync(int id);
        Task<IEnumerable<dynamic>> GetAllPassportsAsync();
        Task<int> AddPassportAsync(CreatePassportRequest passport);
        Task DeletePassportByIdAsync(int id);
        Task<bool> IsPassportUsedByEmployeeAsync(int passportId);
    }
}
