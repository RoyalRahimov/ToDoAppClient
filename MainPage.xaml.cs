using MyMauiApp.CreateTask;
using MyMauiApp.ViewModel;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    public MainPage(TaskViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        // Navigate to the NewTaskPage to create a new task
        await Navigation.PushAsync(new TaskDetailsPage(BindingContext as TaskViewModel));
    }

    private async void OnUpdateTaskClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is ToDoTask task)
        {
            // Navigate to the TaskDetailsPage to update the selected task
            var taskDetailsPage = new TaskDetailsPage(BindingContext as TaskViewModel, task);
            await Navigation.PushAsync(taskDetailsPage);
        }
    }

    // Event handler for when the checkbox is clicked
    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var task = (sender as CheckBox)?.BindingContext as ToDoTask;

        if (task != null)
        {
            if (task.IsDone != e.Value)
            {
                // Update the task's IsDone (IsCompleted) property based on the checkbox value
                task.IsDone = e.Value;

                // Call the UpdateTaskCommand from the ViewModel
                (BindingContext as TaskViewModel)?.UpdateTaskCommand.Execute(task);
            }
        }
    }

    private void OnFilterChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // Only respond to checked event
        {
            var radioButton = sender as RadioButton;
            var selectedFilter = radioButton?.Content.ToString();

            // Set the SelectedFilter in the ViewModel
            (BindingContext as TaskViewModel).SelectedFilter = selectedFilter;
        }
    }
}