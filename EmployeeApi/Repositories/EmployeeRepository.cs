using Dapper;
using Npgsql;
using EmployeeApi.Models;
using System.Data.SqlClient;
using System.ComponentModel.Design;
using EmployeeApi.Repositories.Interfaces;
using EmployeeApi.Requests;
using System.Data;
namespace EmployeeApi.Repositories
{

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public EmployeeRepository(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddEmployeeAsync(CreateEmployeeRequest employee)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = @"
                    INSERT INTO employees (name, surname, phone, companyid, departmentid, passportid) 
                    VALUES (@Name, @Surname, @Phone, @CompanyId, @DepartmentId, @PassportId) 
                    RETURNING Id";

                var parameters = new DynamicParameters();
                parameters.Add("@Name", employee.Name);
                parameters.Add("@Surname", employee.Surname);
                parameters.Add("@Phone", employee.Phone);
                parameters.Add("@CompanyId", employee.CompanyId);
                parameters.Add("@PassportId", employee.PassportId);
                parameters.Add("@DepartmentId", employee.DepartmentId);

                return await connection.ExecuteScalarAsync<int>(sql, parameters);
            }
        }

        public async Task<IEnumerable<dynamic>> GetAllEmployeesAsync()
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = @"
                    SELECT 
                        e.Id AS EmployeeId, 
                        e.Name AS EmployeeName, 
                        e.Surname AS EmployeeSurname, 
                        e.Phone AS EmployeePhone, 
                        e.CompanyId AS EmployeeCompanyId, 
                        e.DepartmentId AS EmployeeDepartmentId, 
                        e.PassportId AS EmployeePassportId, 
                        p.Type AS PassportType, 
                        p.Number AS PassportNumber, 
                        d.Name AS DepartmentName, 
                        d.Phone AS DepartmentPhone
                    FROM employees e
                    LEFT JOIN passports p ON e.PassportId = p.Id
                    LEFT JOIN departments d ON e.DepartmentId = d.Id";

                var parameters = new DynamicParameters();
                

                var result = await connection.QueryAsync(sql, parameters);

                return result;
            }
        }

        public async Task DeleteEmployeeByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var deleteSql = "DELETE FROM employees WHERE Id = @Id";
                await connection.ExecuteAsync(deleteSql, parameters);

            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var getEmployeeSql = @"
                    SELECT 
                        e.Id AS EmployeeId, 
                        e.Name AS EmployeeName, 
                        e.Surname AS EmployeeSurname, 
                        e.Phone AS EmployeePhone, 
                        e.CompanyId AS EmployeeCompanyId, 
                        e.DepartmentId AS EmployeeDepartmentId, 
                        e.PassportId AS EmployeePassportId, 
                        p.Type AS PassportType, 
                        p.Number AS PassportNumber, 
                        d.Name AS DepartmentName, 
                        d.Phone AS DepartmentPhone
                    FROM employees e
                    LEFT JOIN passports p ON e.PassportId = p.Id
                    LEFT JOIN departments d ON e.DepartmentId = d.Id
                    WHERE e.Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var employee = await connection.QueryFirstOrDefaultAsync<dynamic>(getEmployeeSql, parameters);
                if (employee == null)
                {
                    return null; // Возвращаем null, если сотрудник не найден
                }
                return new Employee
                {
                    Id = id,
                    Name = employee.employeename,
                    Surname = employee.employeesurname,
                    Phone = employee.employeephone,
                    CompanyId = employee.employeecompanyid,
                    DepartmentId = employee.employeedepartmentid,
                    PassportId = employee.employeepassportid
                };
            }
        }

        public async Task<IEnumerable<dynamic>> GetEmployeesByCompanyIdAsync(int companyId)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = @"
                    SELECT 
                        e.Id AS EmployeeId, 
                        e.Name AS EmployeeName, 
                        e.Surname AS EmployeeSurname, 
                        e.Phone AS EmployeePhone, 
                        e.CompanyId AS EmployeeCompanyId, 
                        e.DepartmentId AS EmployeeDepartmentId, 
                        e.PassportId AS EmployeePassportId, 
                        p.Type AS PassportType, 
                        p.Number AS PassportNumber, 
                        d.Name AS DepartmentName, 
                        d.Phone AS DepartmentPhone
                    FROM employees e
                    LEFT JOIN passports p ON e.PassportId = p.Id
                    LEFT JOIN departments d ON e.DepartmentId = d.Id
                    WHERE e.CompanyId = @CompanyId";

                var parameters = new DynamicParameters();
                parameters.Add("@CompanyId", companyId);

                var result = await connection.QueryAsync(sql, parameters);

                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> GetEmployeesByDepartmentIdAsync(int companyId, int departmentId)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = @"
                    SELECT 
                        e.Id AS EmployeeId, 
                        e.Name AS EmployeeName, 
                        e.Surname AS EmployeeSurname, 
                        e.Phone AS EmployeePhone, 
                        e.CompanyId AS EmployeeCompanyId, 
                        e.DepartmentId AS EmployeeDepartmentId, 
                        e.PassportId AS EmployeePassportId, 
                        p.Type AS PassportType, 
                        p.Number AS PassportNumber, 
                        d.Name AS DepartmentName, 
                        d.Phone AS DepartmentPhone
                    FROM employees e
                    LEFT JOIN passports p ON e.passportid = p.id
                    LEFT JOIN departments d ON e.departmentid = d.id
                    WHERE e.departmentid = @departmentId AND e.CompanyId = @CompanyId";

                var parameters = new DynamicParameters();
                parameters.Add("@departmentId", departmentId);
                parameters.Add("@CompanyId", companyId);

                var result = await connection.QueryAsync(sql, parameters);

                return result;
            }
        }

        public async Task<bool> IsEmployeeExistsByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = "SELECT EXISTS(SELECT 1 FROM employees WHERE Id = @Id)";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                return await connection.ExecuteScalarAsync<bool>(sql, parameters);
            }
        }

        public async Task<bool> IsPassportIdAlreadyUsedAsync(int? passportId, int? employeeId)
        {
            if (!passportId.HasValue) return false;

            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = "SELECT EXISTS(SELECT 1 FROM employees WHERE PassportId = @PassportId";

                if (employeeId.HasValue)
                {
                    sql += " AND Id != @EmployeeId";
                }
                sql += ")";

                var parameters = new DynamicParameters();
                parameters.Add("@PassportId", passportId);

                if (employeeId.HasValue)
                {
                    parameters.Add("@EmployeeId", employeeId);
                }

                return await connection.ExecuteScalarAsync<bool>(sql, parameters);
            }
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = @"UPDATE employees SET 
                    Name = COALESCE(@Name, Name),
	                Surname = COALESCE(@Surname, Surname),
	                Phone = COALESCE(@Phone, Phone),
	                CompanyId = COALESCE(@CompanyId, CompanyId),
	                PassportId = COALESCE(@PassportId, PassportId),
	                DepartmentId = COALESCE(@DepartmentId, DepartmentId)
                WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", request.Id, DbType.Int32);

                if (!string.IsNullOrEmpty(request.Name))
                    parameters.Add("@Name", request.Name, DbType.String);
                else
                    parameters.Add("@Name", null, DbType.String);

                if (!string.IsNullOrEmpty(request.Surname))
                    parameters.Add("@Surname", request.Surname, DbType.String);
                else
                    parameters.Add("@Surname", null, DbType.String);

                if (!string.IsNullOrEmpty(request.Phone))
                    parameters.Add("@Phone", request.Phone, DbType.String);
                else
                    parameters.Add("@Phone", null, DbType.String);

                if (request.CompanyId.HasValue)
                    parameters.Add("@CompanyId", request.CompanyId.Value, DbType.Int32);
                else
                    parameters.Add("@CompanyId", null, DbType.Int32);

                if (request.PassportId.HasValue)
                    parameters.Add("@PassportId", request.PassportId.Value, DbType.Int32);
                else
                    parameters.Add("@PassportId", null, DbType.Int32);

                if (request.DepartmentId.HasValue)
                    parameters.Add("@DepartmentId", request.DepartmentId.Value, DbType.Int32);
                else
                    parameters.Add("@DepartmentId", null, DbType.Int32);

                var rowsAffected = await connection.ExecuteAsync(sql, parameters);

            }
        }

        public async Task<bool> IsDepartmentExistsAsync(int departmentId)
        {
            if (!(departmentId>0)) return true; 

            using (var connection = _connectionFactory.GetConnection())
            {
                var sql = "SELECT EXISTS(SELECT 1 FROM departments WHERE Id = @DepartmentId)";
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentId", departmentId);

                return await connection.ExecuteScalarAsync<bool>(sql, parameters);
            }
        }
    }
}
