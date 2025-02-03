using EmployeeApi.Exceptions.Department;
using EmployeeApi.Exceptions.Passport;
using EmployeeApi.Models;
using EmployeeApi.Repositories;
using EmployeeApi.Repositories.Interfaces;
using EmployeeApi.Requests;
using EmployeeApi.Services.Interfaces;

namespace EmployeeApi.Services
{
    public class PassportService(IPassportRepository passportRepository) : IPassportService
    {
        public async Task<int> CreatePassport(CreatePassportRequest request)
        {
            return await passportRepository.AddPassportAsync(request);
        }

        public async Task DeletePassport(int id)
        {
            if (await passportRepository.IsPassportUsedByEmployeeAsync(id))
            {
                throw new PassportBadRequestException(id);
            }
            if((await passportRepository.GetPassportByIdAsync(id)) == null)
            {
                throw new PassportNotFoundException(id);
            }
            await passportRepository.DeletePassportByIdAsync(id);

        }

        public Task<IEnumerable<dynamic>> GetAllPassport()
        {
            return passportRepository.GetAllPassportsAsync();
        }

        public async Task<Passport> GetPassportById(int id)
        {
            return await passportRepository.GetPassportByIdAsync(id)
                ?? throw new PassportNotFoundException(id);
        }
    }
}
