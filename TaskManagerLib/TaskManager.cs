namespace TaskManagerLib;

public class TaskManager : ITaskManager
{
    private static readonly int MaxTasks = 10;
    private readonly List<Task> _tasks = [];
    public List<Task> Tasks { get => _tasks; }

    public void PrintTasks()
    {
        for (int i = 0; i < Tasks.Count; i++)
        {
            var taskName = string.IsNullOrEmpty(Tasks[i].Name) ? "Empty" : Tasks[i].Name;
            var taskStatus = Tasks[i].Status.ToString();

            Console.WriteLine($"{i + 1}. {taskName} - {taskStatus}");
        }
    }

    public TaskManagerActionResult AddTask(string taskName)
    {
        if (Tasks.Count == MaxTasks)
            return new TaskManagerActionResult(false, TaskManagerError.NotAbleToAddTask);

        var task = new Task(taskName);
        task.SetStatus(TaskStatus.NotStarted);

        Tasks.Add(task);

        return new TaskManagerActionResult(true);
    }

    public TaskManagerActionResult SetTaskStatus(string taskName, TaskStatus status)
    {
        var task = Tasks?.FirstOrDefault(t => t.Name == taskName);

        if (task == null)
            return new TaskManagerActionResult(false, TaskManagerError.NotAbleToSetTaskNoTaskFound);

        task.SetStatus(status);

        return new TaskManagerActionResult(true);
    }

    public TaskManagerActionResult RemoveTask(string taskName)
    {
        var task = Tasks?.FirstOrDefault(t => t.Name == taskName);

        if (task == null || Tasks == null)
            return new TaskManagerActionResult(false, TaskManagerError.NotAbleToRemoveTask);

        Tasks.Remove(task);

        return new TaskManagerActionResult(true);
    }
}