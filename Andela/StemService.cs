using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StemService.Controllers
{
    [Route("")]
    [ApiController]
    public class StemsController : Controller
    {
        private const string uri = "https://raw.githubusercontent.com/qualified/challenge-data/master/words_alpha.txt";
        
        public StemsController()
        {
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string stem)
        {
            var _httpClient = new HttpClient();
            var response = await _httpClient.GetAsync("https://raw.githubusercontent.com/qualified/challenge-data/master/words_alpha.txt");
          
          if (response == null || response.StatusCode != HttpStatusCode.OK)
          {
            return Ok("Something Not Right, Words URL is not working");
          }
          var json = await response.Content.ReadAsStringAsync();
          var words = System.Text.Json.JsonSerializer.Deserialize<HashSet<string>>(json);
          if (words != null)
          {
            var result = words.Where(y => y.StartsWith(stem)).ToList();
            if (result?.Any() != true)
              return NotFound();
            return Ok(result);
          }
          
          return Ok("Something Not Right, Words URL is not working");
        }
    }
}
