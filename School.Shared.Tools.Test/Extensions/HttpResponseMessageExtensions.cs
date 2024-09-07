using System.Text.Json;
using System.Text.Json.Serialization;

namespace School.Shared.Tools.Test.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<TType?> ReadAsAsync<TType>(this HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();

        if (!(response.Content is not null & response.Content!.Headers.ContentType!.MediaType == "application/json"))
        {
            throw new("Http Response was invalid and cannot be deserialized!");
        }

        var contentStream = await response.Content.ReadAsStreamAsync();

        JsonSerializerOptions options;
        options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true
        };

        options.Converters.Add(new JsonStringEnumConverter());

        return await JsonSerializer.DeserializeAsync<TType>(contentStream, options);
    }
}