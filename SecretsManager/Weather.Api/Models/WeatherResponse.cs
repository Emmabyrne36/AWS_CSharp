using System.Text.Json.Serialization;

namespace Weather.Api.Models;

public class WeatherResponse
{

    [JsonPropertyName("weather")]
    public List<Weather> Weather { get; set; }

    [JsonPropertyName("main")]
    public MainWeatherInfo MainWeatherInfo { get; set; }

    [JsonPropertyName("visibility")]
    public long Visibility { get; set; }

    [JsonPropertyName("timezone")]
    public long Timezone { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}