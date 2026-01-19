namespace Models.Quiz;

public class Quiz
{
    public string QuizTitle { get; set; } = string.Empty;
    public List<Question> Questions { get; set; } = [];
}