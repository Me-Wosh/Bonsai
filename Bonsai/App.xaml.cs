using Bonsai.Services;

namespace Bonsai;

public partial class App : Application
{
    public App(AppState appState, IFileService fileService)
    {
        appState.User = fileService.LoadUser();

        InitializeComponent();

        MainPage = new MainPage();
    }
}