using EmployeeApi.Models;
using EmployeeApi.Requests;
namespace EmployeeApi.Services.Interfaces
{
    public interface IPassportService
    {
        Task<int> CreatePassport(CreatePassportRequest request);
        Task<Passport?> GetPassportById(int id);
        Task<IEnumerable<dynamic>> GetAllPassport();
        Task DeletePassport(int id);
    }
}
