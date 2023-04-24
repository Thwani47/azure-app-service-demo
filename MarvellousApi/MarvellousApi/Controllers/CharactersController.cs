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
    public async Task<ActionResult<List<Character>>> GetCharacters()
    {
        try
        {
            var characters = await _marvelService.GetCharacters();
            return characters is null ? Ok(new List<Character>()) : Ok(characters);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new { message = e.Message });
        }
    }

    [HttpGet("{characterId:int}")]
    public async Task<ActionResult<Character>> GetCharacter(int characterId)
    {
        try
        {
            var character = await _marvelService.GetCharacter(characterId);

            if (character is null)
            {
                return NotFound();
            }

            return Ok(character);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new { message = e.Message });
        }
    }

    [HttpGet("search/{characterName}")]
    public async Task<ActionResult<Character>> SearchCharacter(string characterName)
    {
        try
        {
            var character = await _marvelService.SearchCharacter(characterName);

            if (character is null)
            {
                return NotFound();
            }

            return Ok(character);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new { message = e.Message });
        }
    }
}