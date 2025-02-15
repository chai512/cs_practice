namespace TaskManagerLib;

public interface ITaskManager
{
    public TaskManagerActionResult AddTask(string taskName);
    public TaskManagerActionResult RemoveTask(string taskName);
    public TaskManagerActionResult SetTaskStatus(string taskName, TaskStatus status);
    public void PrintTasks();
}
