namespace QuizApp.ViewModels.Quiz;

public class QuizResultViewModel
{
    public string QuizTitle { get; set; } = string.Empty;
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public List<QuestionResultViewModel> Results { get; set; } = [];
}
