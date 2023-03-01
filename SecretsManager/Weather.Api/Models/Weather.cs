using System.Text.Json.Serialization;

namespace Weather.Api.Models;

public class Weather
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("main")]
    public string Main { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }
}