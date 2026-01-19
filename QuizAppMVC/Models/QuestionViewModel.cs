namespace QuizApp.ViewModels.Quiz;

public class QuestionViewModel
{
    public int Id { get; set; }
    public int DisplayOrder { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<string> Options { get; set; } = [];
}
