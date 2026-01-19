namespace Models.Quiz;

public class Question
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<string> Options { get; set; } = [];
    public string CorrectAnswer { get; set; } = string.Empty;
}
