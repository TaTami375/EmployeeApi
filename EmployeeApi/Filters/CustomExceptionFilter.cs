using EmployeeApi.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using EmployeeApi.Exceptions.Department;
using EmployeeApi.Exceptions.Employee;
using EmployeeApi.Exceptions.Passport;
using System.Text;
using System.Text.Json.Serialization;

namespace EmployeeApi.Filters
{
    public class CustomExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            var httpContext = context.HttpContext;

            httpContext.Response.StatusCode = exception switch
            {
                DepartmentNotFoundException => StatusCodes.Status404NotFound,
                EmployeeNotFoundException => StatusCodes.Status404NotFound,
                PassportNotFoundException => StatusCodes.Status404NotFound,
                DepartmentBadRequestException => StatusCodes.Status400BadRequest,
                PassportBadRequestException => StatusCodes.Status400BadRequest,
                PassportIdAlreadyUsedException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.WriteAsJsonAsync(new
            {
                ActionName = context.ActionDescriptor.DisplayName,
                exception.Message,
                exception.StackTrace
            });

            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }
    }
}