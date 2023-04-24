using MarvellousApi.Models;
using MarvellousApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarvellousApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class CharactersController : ControllerBase
{
    private readonly IMarvelService _marvelService;

    public CharactersController(IMarvelService marvelService)
    {
        _marvelService = marvelService;
    }

    [HttpGet]
    public async Task<List<Character>> GetCharacters()
    {
        var characters = await _marvelService.GetCharacters();

        return characters ?? new List<Character>();
    }

    [HttpGet("{characterId:int}")]
    public async Task<ActionResult<Character>> GetCharacter(int characterId)
    {
        var character = await _marvelService.GetCharacter(characterId);

        if (character is null)
        {
            return NotFound();
        }

        return Ok(character);
    }
    
    [HttpGet("search/{characterName}")]
    public async Task<ActionResult<Character>> SearchCharacter(string characterName)
    {
        var character = await _marvelService.SearchCharacter(characterName);

        if (character is null)
        {
            return NotFound();
        }

        return Ok(character);
    }
    
    
}