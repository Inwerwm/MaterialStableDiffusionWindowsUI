using System.Text.Json.Serialization;

namespace MaterialStableDiffusionWindowsUI.Core.Poco;
internal class RequestBody
{
    [JsonPropertyName("input")]
    public GenerationParameters Input
    {
        get;
        init;
    }

    public RequestBody(GenerationParameters input)
    {
        Input = input;
    }
}
