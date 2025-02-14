namespace TaskManagerLib;

public class TaskManager : ITaskManager
{
    private readonly List<Task> _tasks = [];
    public List<Task> Tasks { get => _tasks; }
    public TaskManager()
    {
        Tasks.Add(new Task(""));
        Tasks.Add(new Task(""));
        Tasks.Add(new Task(""));
    }

    public void PrintTasks()
    {
        for (int i = 0; i < Tasks.Count; i++)
        {
            var taskName = string.IsNullOrEmpty(Tasks[i].Name) ? "Empty" : Tasks[i].Name;
            var taskStatus = Tasks[i].Status == TaskStatus.Unknown ? "Unknown" : Tasks[i].Status.ToString();

            Console.WriteLine($"{i + 1}. {taskName} - {taskStatus}");
        }
    }

    public TaskManagerActionResult AddTask(string taskName)
    {
        var task = Tasks?.FirstOrDefault(t => t.Name == "");

        if (task == null)
            return new TaskManagerActionResult(false, TaskManagerError.NotAbleToAddTask);

        task.SetName(taskName);
        task.SetStatus(TaskStatus.NotStarted);

        return new TaskManagerActionResult(true);
    }

    public TaskManagerActionResult UpdateTask(string taskName, TaskStatus status)
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

        task.SetName("");
        task.SetStatus(TaskStatus.Unknown);

        return new TaskManagerActionResult(true);
    }
}