using CommunityToolkit.Mvvm.ComponentModel;
using MyMauiApp.Helpers;

namespace MyMauiApp;

public partial class ToDoTask : ObservableObject
{
    [ObservableProperty]
    public int id;

    [ObservableProperty]
    public string title;

    [ObservableProperty]
    public string description;

    [ObservableProperty]
    public bool isDone = false;

    [ObservableProperty]
    public TaskPriorityLevel taskPriorityLevel = new TaskPriorityLevel {
        PriorityName = Constants.LOW,
        PriorityColor = Constants.GREEN
    };

    [ObservableProperty]
    public DateTime dueDate;

    [ObservableProperty]
    public TimeSpan dueTime;
}

public partial class TaskPriorityLevel : ObservableObject
{
    [ObservableProperty]
    public string priorityName;

    [ObservableProperty]
    public string priorityColor;
}