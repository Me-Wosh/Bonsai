using Bonsai.Models;
using Bonsai.Services;

namespace Bonsai;

public partial class App : Application
{
    private readonly IUserService _userService;
    public App(IRepository<ToDo> repository, IUserService userService)
    {
        InitializeComponent();

        MainPage = new MainPage();

        ToDoRepository = repository;
        _userService = userService;
    }

    protected override async void OnStart()
    {
        await _userService.LoadUser();
    }

    public static IRepository<ToDo> ToDoRepository { get; private set; }
}