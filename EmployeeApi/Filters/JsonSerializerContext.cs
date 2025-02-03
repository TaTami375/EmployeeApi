namespace EmployeeApi.Filters
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    // Отметьте класс атрибутом [JsonSerializable]
    [JsonSerializable(typeof(ErrorResponse))]
    internal partial class ErrorResponseContext : JsonSerializerContext { }
}
