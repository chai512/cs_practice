namespace TaskManagerLib
{
    public class TaskManagerActionResult(bool isSuccess, TaskManagerError error = TaskManagerError.NoError)
    {
        public bool IsSuccess { get; set; } = isSuccess;
        public TaskManagerError Error { get; set; } = error;
    }
}