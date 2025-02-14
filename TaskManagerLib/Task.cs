namespace TaskManagerLib;

public class Task(string name) : ITask
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; protected set; } = name;
    public bool IsCompleted { get => Status == TaskStatus.Completed; }
    public TaskStatus Status { get; protected set; } = TaskStatus.Unknown;
    public void SetName(string name) => Name = name;
    public void SetStatus(TaskStatus status) => Status = status;
}