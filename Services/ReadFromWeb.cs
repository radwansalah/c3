using System.Net.Http;
using System.Threading.Tasks;

namespace WordsBackend.Services
{
    public class ReadFromWeb : IReadFromWeb
    {
        private readonly HttpClient _httpClient;
        private readonly HashSet<string> Words;
        public ReadFromWeb(HttpClient httpClient)
        {
            _httpClient = httpClient;

            var response = _httpClient.GetAsync(@"https://raw.githubusercontent.com/qualified/challenge-data/master/words_alpha.txt").Result;
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;
            var words = json.Split('\n').Select(line => line.Trim()).Where(line => !string.IsNullOrEmpty(line)).ToHashSet();
            if (words == null)
            {
                throw new System.Exception("Failed to deserialize words from the response.");
            }
            
            Words = words;
        }

        public HashSet<string> GetAllWordsAsync()
        {
            return Words;
        }
    }
}