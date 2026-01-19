using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Services;
using QuizApp.ViewModels.Quiz;
using QuizAppMVC.Models;

namespace QuizApp.Controllers;

public static class EnumerableExtensions
{
    public static List<T> Shuffle<T>(this IEnumerable<T> source)
        => source.OrderBy(_ => Random.Shared.Next()).ToList();
}

public class QuizController : Controller
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }



    // GET
    public IActionResult Index(int quizId)
    {
        var quiz = _quizService.GetQuiz(quizId);
        if (quiz == null)
        {
            TempData["ErrorMessage"] = $"Quiz {quizId} not found.";
            return RedirectToAction("Error", "Home");
        }

        var randomizedQuestions = quiz.Questions
            .OrderBy(_ => Guid.NewGuid())
            .Select((q, index) => new QuestionViewModel
            {
                Id = q.Id,
                DisplayOrder = index + 1,
                QuestionText = q.QuestionText,
                Options = q.Options
                    .OrderBy(_ => Guid.NewGuid())
                    .ToList()
            })
            .ToList();

        var viewModel = new QuizViewModel
        {
            QuizId = quizId,
            QuizTitle = quiz.QuizTitle,
            Questions = randomizedQuestions
        };

        return View(viewModel);
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(
    Dictionary<int, string> answers,
    Dictionary<int, int> displayOrder,
    int quizId)
    {
        var quiz = _quizService.GetQuiz(quizId);
        if (quiz == null)
        {
            TempData["ErrorMessage"] = $"Quiz {quizId} not found.";
            return RedirectToAction("Error", "Home");
        }

        var resultViewModel = new QuizResultViewModel
        {
            QuizTitle = quiz.QuizTitle,
            TotalQuestions = quiz.Questions.Count
        };

        foreach (var question in quiz.Questions)
        {
            answers.TryGetValue(question.Id, out var selectedAnswer);
            displayOrder.TryGetValue(question.Id, out var order);

            resultViewModel.Results.Add(new QuestionResultViewModel
            {
                DisplayOrder = order,
                QuestionText = question.QuestionText,
                SelectedAnswer = selectedAnswer,
                CorrectAnswer = question.CorrectAnswer
            });
        }

        resultViewModel.Results = resultViewModel.Results
            .OrderBy(r => r.DisplayOrder)
            .ToList();

        resultViewModel.CorrectAnswers =
            resultViewModel.Results.Count(r => r.IsCorrect);

        return View("Result", resultViewModel);
    }
}
