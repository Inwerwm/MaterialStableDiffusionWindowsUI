using System.Text.Json.Serialization;

namespace MaterialStableDiffusionWindowsUI.Core.Poco;
internal class ResponseBody
{
    [JsonPropertyName("status")]
    public string Status { get; init; }
    [JsonPropertyName("output")]
    public IEnumerable<string> Output { get; init; }
}
