using EmployeeApi.Models;
using Dapper;
using Npgsql;
using EmployeeApi.Repositoryes.Interfaces;
using EmployeeApi.Requests;

namespace EmployeeApi.Repositoryes
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public DepartmentRepository(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddDepartmentAsync(CreateDepartmentRequest department)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = @"
                    INSERT INTO Departments (Name, Phone) 
                    VALUES (@Name, @Phone)
                    RETURNING Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Name", department.Name);
                parameters.Add("@Phone", department.Phone);
                return await connection.ExecuteScalarAsync<int>(sql, parameters);
            }
        }

        public async Task DeleteDepartmentByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var deleteSql = "DELETE FROM departments WHERE Id = @Id";
                await connection.ExecuteAsync(deleteSql, parameters);
            }
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var getDepartmentSql = @"
                    SELECT 
                        Id, 
                        Name, 
                        Phone                       
                    FROM departments
                    WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var department = await connection.QueryFirstOrDefaultAsync<dynamic>(getDepartmentSql, parameters);
                if (department == null)
                {
                    return null; 
                }
                return new Department
                {
                    Id = id,
                    Name = department.name,
                    Phone = department.phone
                };
            }
        }

        public async Task<IEnumerable<dynamic>> GetAllDepartmentsAsync()
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = @"
                    SELECT 
                        Id, 
                        Name, 
                        Phone                       
                    FROM departments";

                var parameters = new DynamicParameters();


                var result = await connection.QueryAsync<dynamic>(sql, parameters);

                return result;
            }
        }

        public async Task<bool> IsDepartmentUsedByEmployeeAsync(int departmentId)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = "SELECT EXISTS(SELECT 1 FROM employees WHERE DepartmentId = @DepartmentId)";
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentId", departmentId);

                return await connection.ExecuteScalarAsync<bool>(sql, parameters);
            }
        }

    }
}
