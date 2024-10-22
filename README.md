Basic ToDo client application

P.S. Instructions given with the focus on Mac system. The code was generated on Visual Studio Code and ran on Android Studio Emulator! All setup for Dotnet libraries, Visual Studio Code and so on are considered to be installed beforehand. 

1. Download client app from Github: open the terminal and navigate to the desired directory (for example: "cd Downloads").
2. Run command: "git clone https://github.com/RoyalRahimov/ToDoAppClient.git"
3. Navigate to the app folder: "cd ToDoAppClient"
4. Open Android Studio and run any emulator (considering that Android Studio and emulators are installed!)
5. Run "dotnet clean"
6. Run "dotnet build -t:Run -f net8.0-android"

The client app will be installed directly on Android Studio Emulator! 

You will have the screen with tasks list and filtering option, create button, task details page which is used for filling out details to create a new task, and also reused for updating task details, delete button which appears on swipe the task items, etc.

See screenshots:

![tasks page](https://github.com/user-attachments/assets/6c3f58db-b005-41ac-9bd7-a68c1a76053c)

![task_details](https://github.com/user-attachments/assets/0204bfca-0988-42c1-81a6-e48835f2bf66)

![completed tasks](https://github.com/user-attachments/assets/07efad31-cbcd-4a0c-835d-b292eb374e68)

![delete](https://github.com/user-attachments/assets/da4bbe0f-8472-4a15-8388-0a331cda07b1)

Gif with use case is attached:

![use case](https://github.com/user-attachments/assets/2cda1eb8-9808-402c-8ec0-d893932772ed)
