using MarvellousApi.Helpers;
using MarvellousApi.Models;
using MarvellousApi.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MarvellousApi.Services;

public class MarvelService : IMarvelService
{
    private readonly MarvelOptions _options;
    public MarvelService(IOptions<MarvelOptions> options)
    {
        _options = options.Value;
    }
    
    public async Task<List<Character?>?> GetCharacters()
    {
        var timestamp = DateTime.UtcNow.Ticks.ToString();
        var hash = ApiUtils.ComputeKeyHash(timestamp, _options.PrivateKey, _options.PublicKey);

        using var client = new HttpClient();
        var url = $"{_options.BaseUrl}/characters?ts={timestamp}&apikey={_options.PublicKey}&hash={hash}&limit=100";
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            content = content.Replace("\r", "").Replace("\n", "");
            // Console.WriteLine(content);
            var json = JsonConvert.DeserializeObject<ApiResponse<List<Character>>>(content);
            var characters = json?.Data.Results;
            return characters;
        }

        Console.WriteLine($"HTTP error {response.StatusCode}");
        throw new Exception($"Failed to get characters: {response.StatusCode}");
    }

    public async Task<Character?> GetCharacter(int characterId)
    {
        var timestamp = DateTime.UtcNow.Ticks.ToString();
        var hash = ApiUtils.ComputeKeyHash(timestamp, _options.PrivateKey, _options.PublicKey);

        using var client = new HttpClient();
        var url = $"{_options.BaseUrl}/characters/{characterId}?ts={timestamp}&apikey={_options.PublicKey}&hash={hash}&limit=100";
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            content = content.Replace("\r", "").Replace("\n", "");
            // Console.WriteLine(content);
            var json = JsonConvert.DeserializeObject<ApiResponse<List<Character>>>(content);
            var characters = json?.Data.Results;
            return characters?[0];
        }

        Console.WriteLine($"HTTP error {response.StatusCode}");
        throw new Exception($"Failed to get character: {response.StatusCode}");
    }

    public async Task<Character?> SearchCharacter(string characterName)
    {
        var timestamp = DateTime.UtcNow.Ticks.ToString();
        var hash = ApiUtils.ComputeKeyHash(timestamp, _options.PrivateKey, _options.PublicKey);

        using var client = new HttpClient();
        var url = $"{_options.BaseUrl}/characters?nameStartsWith={characterName.ToLower()}&ts={timestamp}&apikey={_options.PublicKey}&hash={hash}&limit=100";
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            content = content.Replace("\r", "").Replace("\n", "");
            // Console.WriteLine(content);
            var json = JsonConvert.DeserializeObject<ApiResponse<List<Character>>>(content);
            var characters = json?.Data.Results?[0];
            return characters;
        }

        Console.WriteLine($"HTTP error {response.StatusCode}");
        throw new Exception($"Failed to search for character: {response.StatusCode}");
    }
}