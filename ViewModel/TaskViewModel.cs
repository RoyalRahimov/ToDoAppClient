using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Helpers;

namespace MyMauiApp.ViewModel;

public partial class TaskViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<ToDoTask> tasks;

    [ObservableProperty]
    ObservableCollection<ToDoTask> filteredTasks;

    [ObservableProperty]
    private string selectedFilter;

    private readonly HttpClient _httpClient;

    public TaskViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;

        Tasks = new ObservableCollection<ToDoTask>();
        FilteredTasks = new ObservableCollection<ToDoTask>();

        SelectedFilter = Constants.ALL;

        _ = GetAllTasksAsync();
    }

    private async Task GetAllTasksAsync()
    {
        try
        {
            // Make a GET request to the server to fetch all tasks
            var loadedTasks = await _httpClient.GetFromJsonAsync<ObservableCollection<ToDoTask>>(Constants.ApiBaseUrl);
            Console.WriteLine($"loaded tasks {loadedTasks.Count}");
            if (loadedTasks != null)
            {
                Tasks = loadedTasks;
                FilterTasks(); 
                Console.WriteLine($"filtered tasks {loadedTasks.Count}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Request error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching tasks: {ex.Message}");
        }
    }

    // This method is triggered when the SelectedFilter changes
    partial void OnSelectedFilterChanged(string value)
    {
        FilterTasks();
    }

    public async Task AddTask(ToDoTask task)
    {
        // Prepare the JSON data to be sent to the API
        var content = new StringContent(JsonSerializer.Serialize(task), Encoding.UTF8, "application/json");

        // Make the HTTP POST request to add the task to the server
        var response = await _httpClient.PostAsync(Constants.ApiBaseUrl, content);

        if (response.IsSuccessStatusCode)
        {
            // If the task is successfully created, refresh the task list by fetching all tasks
            await GetAllTasksAsync();  // Get updated list of tasks from the server
        }
        else
        {
            Debug.WriteLine("Failed to add task: " + response.StatusCode);
        }
    }

    [RelayCommand]
    public async Task DeleteTask(ToDoTask task)
    {
        if (task != null)
        {
            var response = await _httpClient.DeleteAsync($"{Constants.ApiBaseUrl}/{task.Id}");

            if (response.IsSuccessStatusCode)
            {
                // reload tasks to refresh page
                await GetAllTasksAsync();
            }
            else
            {
                Debug.WriteLine("Failed to delete task: " + response.StatusCode);
            }
        }
    }

    [RelayCommand]
    public async Task UpdateTask(ToDoTask task)
    {
        if (task != null)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(task), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{Constants.ApiBaseUrl}/{task.Id}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                await GetAllTasksAsync();
            }
            else
            {
                Debug.WriteLine("Failed to update task: " + response.StatusCode);
            }
        }
    }

    // Filtering function
    public void FilterTasks() 
    {
        FilteredTasks.Clear();

        switch (SelectedFilter)
        {
            case "Open":
                foreach (var task in Tasks.Where(t => !t.IsDone))
                    FilteredTasks.Add(task);
                break;

            case "Completed":
                foreach (var task in Tasks.Where(t => t.IsDone))
                    FilteredTasks.Add(task);
                break;

            default: 
                foreach (var task in Tasks)
                    FilteredTasks.Add(task);
                break;
        }
    }
}