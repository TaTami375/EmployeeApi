using EmployeeApi;
using EmployeeApi.Data.Migrations;
using EmployeeApi.Repositoryes;
using EmployeeApi.Repositoryes.Interfaces;
using EmployeeApi.Services.Interfaces;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// ���������� �������� � ���������.
builder.Services.AddSingleton<DatabaseConnectionFactory>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    return new DatabaseConnectionFactory(connectionString);
});

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPassportRepository, PassportRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPassportService, PassportService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


MappingConfig.Configure();

// ���������� ������������
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// ���������� Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Employee API",
        Version = "v1",
        Description = "API ��� ���������� ������������"
    });
});

var app = builder.Build();

// ������������ HTTP-��������
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
// ��������� ��������
var migrationManager = new MigrationManager(builder.Configuration.GetConnectionString("DefaultConnection"));
migrationManager.ApplyMigrations();
app.Run();