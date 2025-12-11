using Microsoft.AspNetCore.Mvc;
using Task = TaskApi.Models.Task;

// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private static readonly string[] Tasks =
        {
            "Cook breakfast", "Just chill", "Study psychology", "Learn how to log", "Buy some food"
        };

        private readonly ILogger<TasksController> _logger;
        public TasksController(ILogger<TasksController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public void Get()
        {
            var task = LoadTask();
            _logger.LogInformation($"Log request with no structured logging at: {DateTimeOffset.Now} for task: {task.Message}",
               DateTimeOffset.Now, task);
            _logger.LogInformation("Log request with structured logging at: {At} for task: {@Task}",
                DateTimeOffset.Now, task);
        }

        private static Task LoadTask()
        {
            return new Task
            {
                Date = DateOnly.MinValue,
                Message = Tasks[Random.Shared.Next(0, Tasks.Length - 1)]
            };
        }
    }
}