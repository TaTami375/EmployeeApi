using System.Data;
using Npgsql;
using System.IO;

namespace EmployeeApi.Data.Migrations
{
    public class MigrationManager
    {
        private readonly string _connectionString;

        public MigrationManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ApplyMigrations()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            try
            {
                using (var createTableCommand = new NpgsqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Migrations (
                        Id SERIAL PRIMARY KEY,
                        Name TEXT NOT NULL,
                        AppliedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                    );", connection))
                {
                    createTableCommand.ExecuteNonQuery();
                }

                List<string> appliedMigrations = new List<string>();
                using (var selectCommand = new NpgsqlCommand("SELECT Name FROM Migrations", connection))
                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appliedMigrations.Add(reader.GetString(0));
                    }
                }

                var migrationFiles = Directory.GetFiles("Data/Migrations", "*.sql");

                foreach (var file in migrationFiles)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);

                    if (appliedMigrations.Contains(fileName))
                    {
                        Console.WriteLine($"Миграция {fileName} уже применена.");
                        continue;
                    }

                    Console.WriteLine($"Применение миграции {fileName}...");

                    var sql = File.ReadAllText(file);

                    var commands = sql.Split(new[] { ";\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var commandText in commands)
                    {
                        using (var executeCommand = new NpgsqlCommand(commandText.Trim(), connection))
                        {
                            executeCommand.ExecuteNonQuery();
                        }
                    }

                    using (var insertCommand = new NpgsqlCommand("INSERT INTO Migrations (Name) VALUES (@Name)", connection))
                    {
                        insertCommand.Parameters.AddWithValue("Name", fileName);
                        insertCommand.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Миграции успешно применены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при применении миграций: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}