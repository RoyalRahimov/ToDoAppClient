using MyMauiApp.Helpers;
using MyMauiApp.ViewModel;

namespace MyMauiApp.CreateTask;

public partial class TaskDetailsPage : ContentPage
{

	private readonly TaskViewModel _taskViewModel;
	private readonly TaskDetailsViewModel _taskDetailsViewModel;

	public TaskDetailsPage(TaskViewModel taskViewModel)
	{
		InitializeComponent();

		_taskViewModel = taskViewModel;
		BindingContext = new TaskDetailsViewModel();

		_taskDetailsViewModel = BindingContext as TaskDetailsViewModel;

		// default taskPriority value setup during creating task operation
		if (_taskDetailsViewModel.TaskPriority == null)
		{
			var taskPriority = new PriorityLevel
			{
				Color = Constants.GREEN,
				Name = Constants.LOW
			};
			_taskDetailsViewModel.TaskPriority = taskPriority;
		}
	}

	public TaskDetailsPage(TaskViewModel taskViewModel, ToDoTask task) : this(taskViewModel)
	{
		_taskDetailsViewModel = BindingContext as TaskDetailsViewModel;
		_taskDetailsViewModel.TaskId = task.Id;
		_taskDetailsViewModel.TaskTitle = task.Title;
		_taskDetailsViewModel.TaskDescription = task.Description;
		_taskDetailsViewModel.DueDate = task.DueDate;
		_taskDetailsViewModel.DueTime = task.DueTime;
		_taskDetailsViewModel.IsTaskCompleted = task.IsDone;

		var taskPriority = new PriorityLevel();
		taskPriority.Color = task.TaskPriorityLevel.PriorityColor;
		taskPriority.Name = task.TaskPriorityLevel.PriorityName;
		_taskDetailsViewModel.TaskPriority = taskPriority;
	}

	private async void OnSaveTaskClicked(object sender, EventArgs e)
	{
		if (IsNotValidated())
		{
			// Show a message box if validation fails
			await Application.Current.MainPage.DisplayAlert("Validation Error", "Please fill all data", "OK");
			return;
		}

		if (_taskDetailsViewModel.TaskId == -1)
		{
			// Create a new ToDoTask based on the input
			var newTask = GetTask();
	
			// Add the new task to the TaskViewModel's Tasks collection
			await _taskViewModel.AddTask(newTask);
		}
		else
		{
			// update process
			var updatedTask = GetTask();
			updatedTask.Id = _taskDetailsViewModel.TaskId;

			await _taskViewModel.UpdateTask(updatedTask);
		}

		// Go back to the previous page (MainPage)
		await Navigation.PopAsync();
	}

	private ToDoTask GetTask()
	{
		return new ToDoTask
		{
			Title = _taskDetailsViewModel.TaskTitle,
			Description = _taskDetailsViewModel.TaskDescription,
			TaskPriorityLevel = new TaskPriorityLevel
			{
				PriorityName = _taskDetailsViewModel.TaskPriority.Name,
				PriorityColor = _taskDetailsViewModel.TaskPriority.Color
			},
			IsDone = _taskDetailsViewModel.IsTaskCompleted,
			DueDate = _taskDetailsViewModel.DueDate,
			DueTime = _taskDetailsViewModel.DueTime
		};
	}

	private bool IsNotValidated()
	{
		return string.IsNullOrEmpty(_taskDetailsViewModel.TaskTitle)
			|| string.IsNullOrEmpty(_taskDetailsViewModel.TaskDescription)
			|| string.IsNullOrWhiteSpace(_taskDetailsViewModel.TaskPriority.Name)
			|| string.IsNullOrWhiteSpace(_taskDetailsViewModel.TaskPriority.Color);
	}

	// Handle the label tap and open the Picker
	private void OnPriorityLabelTapped(object sender, EventArgs e)
	{
		// Make the Picker visible, open it, and hide it after interaction
		priorityPicker.IsVisible = true;
		priorityPicker.Focus();
	}

	// Handle the priority selection change in the Picker
	private void OnPrioritySelected(object sender, EventArgs e)
	{
		if (priorityPicker.SelectedItem != null)
		{
			// The selected priority is already bound to the ViewModel
			// After selection, hide the picker
			priorityPicker.IsVisible = false;
		}
	}
}