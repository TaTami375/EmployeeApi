using EmployeeApi.Models;
using Npgsql;
using Dapper;
using EmployeeApi.Repositoryes.Interfaces;
using EmployeeApi.Requests;

namespace EmployeeApi.Repositoryes
{
    public class PassportRepository : IPassportRepository
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public PassportRepository(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddPassportAsync(CreatePassportRequest passport)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = @"
                    INSERT INTO Passports (Type, Number) 
                    VALUES (@Type, @Number)
                    RETURNING Id";

                var parameters = new DynamicParameters();
                parameters.Add("@Type", passport.Type);
                parameters.Add("@Number", passport.Number);

                return await connection.ExecuteScalarAsync<int>(sql, parameters);
            }
        }

        public async Task DeletePassportByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var deleteSql = "DELETE FROM passports WHERE Id = @Id";
                await connection.ExecuteAsync(deleteSql, parameters);
            }
        }

        public async Task<Passport> GetPassportByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var getPassportSql = @"
                    SELECT 
                        Id, 
                        Type, 
                        Number                       
                    FROM passports
                    WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var passport = await connection.QueryFirstOrDefaultAsync<dynamic>(getPassportSql, parameters);
                if(passport == null)
                {
                    return null;
                }
                return new Passport
                {
                    Id = id,
                    Type = passport.type,
                    Number = passport.number
                };
            }
        }

        public async Task<IEnumerable<dynamic>> GetAllPassportsAsync()
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = @"
                     SELECT 
                        Id, 
                        Type, 
                        Number                       
                    FROM passports";

                var parameters = new DynamicParameters();


                var result = await connection.QueryAsync(sql, parameters);

                return result;
            }
        }

        public async Task<bool> IsPassportUsedByEmployeeAsync(int passportId)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = "SELECT EXISTS(SELECT 1 FROM employees WHERE PassportId = @PassportId)";
                var parameters = new DynamicParameters();
                parameters.Add("@PassportId", passportId);

                return await connection.ExecuteScalarAsync<bool>(sql, parameters);
            }
        }
    }
}
