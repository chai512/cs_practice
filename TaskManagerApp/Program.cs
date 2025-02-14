using TaskManager = TaskManagerLib.TaskManager;
using TaskStatus = TaskManagerLib.TaskStatus;
using TaskManagerActionResult = TaskManagerLib.TaskManagerActionResult;
using TaskManagerError = TaskManagerLib.TaskManagerError;
using Pastel;

internal class Program
{
    private static readonly string AppTitle = "Task Manager";
    private static readonly string AppExiting = "Exiting...";
    private static readonly string InvalidOption = "Invalid option.";
    private static readonly string TaskBag = "Task Bag:";
    private static readonly string SelectOption = "Select Option:";
    private static readonly string TaskNameInputPrompt = "Enter task name:";
    private static readonly string TaskNameCannotBeEmptyMessage = "Task name cannot be empty.";
    private const string OptionQuit = "Q";
    private static readonly string OptionQuitTitle = "Quit";
    private const string OptionAdd = "A";
    private static readonly string OptionAddTitle = "Add Task";
    private const string OptionStart = "S";
    private static readonly string OptionStartTitle = "Start Task";
    private const string OptionComplete = "C";
    private static readonly string OptionRemoveTitle = "Remove Task";
    private const string OptionRemove = "R";
    private static readonly string OptionCompleteTitle = "Complete Task";
    private static readonly string NotAbleToAddTaskMessage = "Not able to add task. Task bag is full.";
    private static readonly string NotAbleToRemoveTaskMessage = "Not able to remove task. Task not found.";
    private static readonly string NotAbleToSetTaskNoTaskFoundMessage = "Not able to set task. Task not found.";
    private static readonly string UnknownErrorMessage = "Unknown error.";

    static readonly TaskManager taskManager = new();

    private static void Main(string[] args)
    {
        Console.WriteLine($"********** {AppTitle} **********".Pastel("#FF1493"));

        var option = string.Empty;

        while (option != OptionQuit)
        {
            PrintTasks();
            option = TakeUserInput();
            TakeActionOnUserInput(option);
        }
    }

    static void PrintTasks()
    {
        Console.WriteLine(TaskBag);

        taskManager.PrintTasks();
    }

    static string TakeUserInput()
    {
        Console.WriteLine(SelectOption);

        Console.WriteLine($"{OptionAdd} - {OptionAddTitle} ");
        Console.WriteLine($"{OptionStart} - {OptionStartTitle} ");
        Console.WriteLine($"{OptionComplete} - {OptionCompleteTitle}");
        Console.WriteLine($"{OptionRemove} - {OptionRemoveTitle}");
        Console.WriteLine($"{OptionQuit} - {OptionQuitTitle}");

        var option = Console.ReadLine();

        return option ?? string.Empty;
    }

    static void TakeActionOnUserInput(string option)
    {
        string? taskName;

        switch (option)
        {
            case OptionAdd:
                taskName = TakeUserInputForTaskName();
                if (!string.IsNullOrEmpty(taskName))
                {
                    ProcessTaskManagerActionResult(taskManager.AddTask(taskName));
                }
                break;
            case OptionStart:
                taskName = TakeUserInputForTaskName();
                if (!string.IsNullOrEmpty(taskName))
                {
                    ProcessTaskManagerActionResult(taskManager.UpdateTask(taskName, TaskStatus.InProgress));
                }
                break;
            case OptionComplete:
                taskName = TakeUserInputForTaskName();
                if (!string.IsNullOrEmpty(taskName))
                {
                    ProcessTaskManagerActionResult(taskManager.UpdateTask(taskName, TaskStatus.Completed));
                }
                break;
            case OptionRemove:
                taskName = TakeUserInputForTaskName();
                if (!string.IsNullOrEmpty(taskName))
                {
                    ProcessTaskManagerActionResult(taskManager.RemoveTask(taskName));
                }
                break;
            case OptionQuit:
                Console.WriteLine(AppExiting);
                break;
            default:
                Console.WriteLine(InvalidOption);
                break;
        }
    }

    static string TakeUserInputForTaskName()
    {
        Console.WriteLine(TaskNameInputPrompt);

        var taskName = Console.ReadLine();

        if (string.IsNullOrEmpty(taskName))
        {
            Console.WriteLine(TaskNameCannotBeEmptyMessage);
            return string.Empty;
        }

        return taskName;
    }

    static void ProcessTaskManagerActionResult(TaskManagerActionResult result)
    {
        if (!result.IsSuccess)
        {
            Console.WriteLine(ErrorCodeToStringMapping(result.Error));
        }
    }

    static string ErrorCodeToStringMapping(TaskManagerError errorCode)
    {
        return errorCode switch
        {
            TaskManagerError.NotAbleToAddTask => NotAbleToAddTaskMessage,
            TaskManagerError.NotAbleToRemoveTask => NotAbleToRemoveTaskMessage,
            TaskManagerError.NotAbleToSetTaskNoTaskFound => NotAbleToSetTaskNoTaskFoundMessage,
            _ => UnknownErrorMessage
        };
    }
}