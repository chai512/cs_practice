namespace TaskManagerTests;

using Task = TaskManagerLib.Task;
using TaskStatus = TaskManagerLib.TaskStatus;

public class TaskTest
{
    [Fact]
    public void Task_ShouldInitializeWithGivenName()
    {
        var taskName = "Test Task";
        var task = new Task(taskName);

        Assert.Equal(taskName, task.Name);
        Assert.Equal(TaskStatus.Unknown, task.Status);
        Assert.False(task.IsCompleted);
    }

    [Fact]
    public void SetName_ShouldUpdateTaskName()
    {
        var task = new Task("Initial Name");
        var newName = "Updated Name";

        task.SetName(newName);

        Assert.Equal(newName, task.Name);
    }

    [Fact]
    public void SetTaskStatus_ShouldUpdateStatus()
    {
        var task = new Task("Test Task");

        task.SetStatus(TaskStatus.InProgress);

        Assert.Equal(TaskStatus.InProgress, task.Status);
        Assert.False(task.IsCompleted);
    }

    [Fact]
    public void SetTaskStatus_ShouldMarkTaskAsCompleted()
    {
        var task = new Task("Test Task");

        task.SetStatus(TaskStatus.Completed);

        Assert.Equal(TaskStatus.Completed, task.Status);
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public void Task_ShouldGenerateUniqueId()
    {
        var task1 = new Task("Task 1");
        var task2 = new Task("Task 2");

        Assert.NotEqual(task1.Id, task2.Id);
    }

    [Fact]
    public void SetStatus_ShouldNotChangeNameIfNotCompleted()
    {
        var task = new Task("Test Task");

        task.SetStatus(TaskStatus.InProgress);

        Assert.Equal("Test Task", task.Name);
    }

    [Fact]
    public void SetName_ShouldNotAffectStatus()
    {
        var task = new Task("Test Task");

        task.SetStatus(TaskStatus.InProgress);
        task.SetName("New Task Name");

        Assert.Equal(TaskStatus.InProgress, task.Status);
    }
}
