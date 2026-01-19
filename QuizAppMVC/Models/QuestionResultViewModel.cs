namespace QuizApp.ViewModels.Quiz;

public class QuestionResultViewModel
{
    public int DisplayOrder { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string? SelectedAnswer { get; set; }
    public string CorrectAnswer { get; set; } = string.Empty;
    public bool IsCorrect => SelectedAnswer == CorrectAnswer;
}
