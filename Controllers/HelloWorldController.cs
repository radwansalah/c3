using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using WordsBackend.Services;

namespace WordsBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    private readonly IReadFromWeb _readFromWeb;
    public HelloWorldController(IReadFromWeb readFromWeb)
    {
        // Constructor logic can be added here if needed
        _readFromWeb = readFromWeb;
    }
    // 
    // GET: /HelloWorld/
    [HttpGet]
    public IActionResult Index(string stem)
    {
        if (string.IsNullOrEmpty(stem))
        {
            return BadRequest("Stem parameter is required.");
        }

        var words = _readFromWeb.GetAllWordsAsync();
        if (words == null || !words.Any())
        {
            return NotFound("No words found.");
        }

        var result = words.Where(word => word.StartsWith(stem, StringComparison.OrdinalIgnoreCase)).ToList();
        if (!result.Any())
        {
            return NotFound($"No words found starting with '{stem}'.");
        }

        return Ok(new
        {
            data = result
        });
    }


    // 
    // GET: /HelloWorld/Welcome/ 
    [HttpGet("Welcome")]
    public string Welcome()
    {
        return "This is the Welcome action method...";
    }
}