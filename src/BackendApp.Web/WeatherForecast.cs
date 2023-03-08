using System.Diagnostics.CodeAnalysis;

namespace BackendApp.Web;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record WeatherForecast(DateTime Date, int TemperatureC, string? Summary);
