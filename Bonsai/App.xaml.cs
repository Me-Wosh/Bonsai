using Bonsai.Models;
using Bonsai.Services;

namespace Bonsai;

public partial class App : Application
{
    public App(IRepository<ToDo> repository, IUserService userService)
    {
        InitializeComponent();

        MainPage = new MainPage();

        ToDoRepository = repository;

        userService.LoadUser();
    }

    public static IRepository<ToDo> ToDoRepository { get; private set; }
}