using System.Text.Json.Serialization;

namespace Weather.Api.Models;

public class MainWeatherInfo
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }

    [JsonPropertyName("temp_min")]
    public double TempMin { get; set; }

    [JsonPropertyName("temp_max")]
    public double TempMax { get; set; }

    [JsonPropertyName("pressure")]
    public long Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public long Humidity { get; set; }
}