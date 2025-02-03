namespace EmployeeApi.Filters
{

    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class ErrorResponse
    {
        public string Error { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
