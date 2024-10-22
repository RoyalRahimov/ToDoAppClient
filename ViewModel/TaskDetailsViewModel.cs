using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MyMauiApp.Helpers;

namespace MyMauiApp.ViewModel;

public partial class TaskDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private int taskId = -1;

    [ObservableProperty]
    private string taskTitle;

    [ObservableProperty]
    private string taskDescription;

    [ObservableProperty]
    private PriorityLevel taskPriority;

    [ObservableProperty]
    private bool isTaskCompleted;

    [ObservableProperty]
    private DateTime dueDate = DateTime.Now;  // Default to current date

    [ObservableProperty]
    private TimeSpan dueTime = DateTime.Now.TimeOfDay;  // Default to current time

    public ObservableCollection<PriorityLevel> PriorityList { get; }

    public TaskDetailsViewModel()
    {
        PriorityList = new ObservableCollection<PriorityLevel>
            {
                new PriorityLevel { Name = Constants.LOW, Color = Constants.GREEN },
                new PriorityLevel { Name = Constants.MEDIUM, Color = Constants.YELLOW },
                new PriorityLevel { Name = Constants.HIGH, Color = Constants.RED },
                new PriorityLevel { Name = Constants.HIGHEST, Color = Constants.DARK_RED }
            };
    }
}

public class PriorityLevel
{
    public string Name { get; set; }
    public string Color { get; set; }
}
