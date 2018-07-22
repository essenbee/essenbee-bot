namespace Essenbee.Bot.Core.Interfaces
{
    public interface IAnswerSearchEngine
    {
        string GetAnswer(string question);
        void SetApiKey(string apiKey);
    }
}
