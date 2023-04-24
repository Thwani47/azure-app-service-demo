using MarvellousApi.Models;

namespace MarvellousApi.Services;

public interface IMarvelService
{
    Task<List<Character?>?> GetCharacters();
    Task<Character?> GetCharacter(int characterId);
    
    Task<Character?> SearchCharacter(string characterName);
}