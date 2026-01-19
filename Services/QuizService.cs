using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Models.Quiz;

namespace Services;

public class QuizService : IQuizService
{
    private readonly IHostEnvironment _env;

    public QuizService(IHostEnvironment env)
    {
        _env = env;
    }

    public Quiz? GetQuiz(int quizId)
    {
        try
        {
            var path = Path.Combine(_env.ContentRootPath, "Data", $"quiz{quizId}.json");
            var json = File.ReadAllText(path);

            var quiz = JsonSerializer.Deserialize<Quiz>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return quiz;

        }
        catch (Exception)
        {
            return null;
        }




    }
}
