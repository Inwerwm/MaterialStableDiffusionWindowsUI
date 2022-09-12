using System.Text.Json.Serialization;

namespace MaterialStableDiffusionWindowsUI.Core.Poco;
public class GenerationParameters
{
    [JsonPropertyName("prompt")]
    public string Prompt
    {
        get;
        init;
    }

    [JsonPropertyName("width")]
    public int Width
    {
        get;
        init;
    }

    [JsonPropertyName("height")]
    public int Height
    {
        get;
        init;
    }

    [JsonPropertyName("num_outputs")]
    public int NumOutputs
    {
        get;
        init;
    }

    [JsonPropertyName("guidance_scale")]
    public float GuidanceScale
    {
        get;
        init;
    }
}
