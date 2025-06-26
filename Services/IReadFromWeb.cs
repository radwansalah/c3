namespace WordsBackend.Services
{
    public interface IReadFromWeb
    {
       HashSet<string> GetAllWordsAsync();
    }
}