namespace QuizApp.ViewModels.Quiz;


public class QuizViewModel
{
    public int QuizId { get; set; }
    public string QuizTitle { get; set; } = string.Empty;
    public List<QuestionViewModel> Questions { get; set; } = [];
}
