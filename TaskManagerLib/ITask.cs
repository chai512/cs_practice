namespace TaskManagerLib
{
    public interface ITask
    {
        void SetName(string taskName);
        void SetStatus(TaskStatus taskStatus);
    }
}