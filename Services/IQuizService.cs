using Models.Quiz;

namespace Services;

public interface IQuizService
{
    Quiz? GetQuiz(int quizId);
}
