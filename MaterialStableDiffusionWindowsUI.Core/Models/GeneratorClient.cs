using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using MaterialStableDiffusionWindowsUI.Core.Poco;
using SixLabors.ImageSharp;

namespace MaterialStableDiffusionWindowsUI.Core.Models;
public class GeneratorClient
{
    private HttpClient Client
    {
        get;
    }

    public GeneratorClient()
    {
        Client = new();
    }

    public async Task<IEnumerable<Image>?> Generate(GenerationParameters parameters)
    {
        var response = await Client.PostAsJsonAsync(
            "http://localhost:5000/predictions",
            new RequestBody(parameters),
            new JsonSerializerOptions()
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString,
            }
        );

        if (response.StatusCode != HttpStatusCode.OK) { return null; }

        var responseBody = await response.Content.ReadFromJsonAsync<ResponseBody>();
        return responseBody.Output.Select(base64 => Convert.FromBase64String(base64[22..])).Select(bytes => Image.Load(bytes));
    }
}
