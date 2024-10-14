using Bonsai.Models;
using Bonsai.Services;

namespace Bonsai;

public partial class App : Application
{
    public App(AppState appState, IFileService fileService, IRepository<ToDo> repository)
    {
        InitializeComponent();

        MainPage = new MainPage();

        appState.User = fileService.LoadUser();
        ToDoRepository = repository;
    }

    public static IRepository<ToDo> ToDoRepository { get; private set; }
}