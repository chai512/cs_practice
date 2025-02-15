namespace TaskManagerTests;

using TaskManagerLib;
using Xunit;

public class TaskManagerTest
{
    [Fact]
    public void TaskManager_InitialTasksCount_ShouldBeZero()
    {
        var taskManager = new TaskManager();

        var tasksCount = taskManager.Tasks.Count;

        Assert.Equal(0, tasksCount);
    }

    [Fact]
    public void AddTask_ShouldAddTaskWithName()
    {
        var taskManager = new TaskManager();

        var taskName = "New Task";

        taskManager.AddTask(taskName);
        var task = taskManager.Tasks.FirstOrDefault(t => t.Name == taskName);

        Assert.NotNull(task);
        Assert.Equal(taskName, task.Name);
    }

    [Fact]
    public void AddTask_ShouldNotAddTaskWhenNoEmptySlot()
    {
        var taskManager = new TaskManager();

        taskManager.AddTask("Task 1");
        taskManager.AddTask("Task 2");
        taskManager.AddTask("Task 3");
        taskManager.AddTask("Task 4");
        taskManager.AddTask("Task 5");
        taskManager.AddTask("Task 6");
        taskManager.AddTask("Task 7");
        taskManager.AddTask("Task 8");
        taskManager.AddTask("Task 9");
        taskManager.AddTask("Task 10");

        var result = taskManager.AddTask("Task 11");

        Assert.False(result.IsSuccess);
        Assert.Equal(TaskManagerError.NotAbleToAddTask, result.Error);
    }

    [Fact]
    public void RemoveTask_ShouldRemoveTaskWithName()
    {
        var taskManager = new TaskManager();

        var taskName = "Task to Remove";

        taskManager.AddTask(taskName);

        taskManager.RemoveTask(taskName);
        var task = taskManager.Tasks.FirstOrDefault(t => t.Name == taskName);

        Assert.Null(task);
    }

    [Fact]
    public void RemoveTask_ShouldNotRemoveNonExistentTask()
    {
        var taskManager = new TaskManager();

        var result = taskManager.RemoveTask("Non Existent Task");

        Assert.False(result.IsSuccess);
        Assert.Equal(TaskManagerError.NotAbleToRemoveTask, result.Error);
    }

    [Fact]
    public void UpdateTask_ShouldUpdateTaskStatus()
    {
        var taskManager = new TaskManager();

        var taskName = "Task to Update";
        taskManager.AddTask(taskName);
        var newStatus = TaskStatus.Completed;

        var task = taskManager.Tasks.FirstOrDefault(t => t.Name == taskName);
        taskManager.SetTaskStatus(taskName, newStatus);

        Assert.NotNull(task);
        Assert.Equal(newStatus, task.Status);
    }

    [Fact]
    public void UpdateTask_ShouldNotUpdateNonExistentTask()
    {
        var taskManager = new TaskManager();

        var taskName = "Non Existent Task";
        var newStatus = TaskStatus.Completed;

        var result = taskManager.SetTaskStatus(taskName, newStatus);

        Assert.False(result.IsSuccess);
        Assert.Equal(TaskManagerError.NotAbleToSetTaskNoTaskFound, result.Error);
    }

    [Fact]
    public void PrintTasks_ShouldPrintAllTasks()
    {
        var taskManager = new TaskManager();

        taskManager.AddTask("Task 1");
        taskManager.AddTask("Task 2");

        using var sw = new StringWriter();
        Console.SetOut(sw);
        taskManager.PrintTasks();

        var output = sw.ToString().Trim();
        Assert.Contains("1. Task 1 - NotStarted", output);
        Assert.Contains("2. Task 2 - NotStarted", output);
    }
}
