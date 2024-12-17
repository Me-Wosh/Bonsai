namespace Bonsai.Models;

public static class Files
{
    public const string Location = "location" + Extension;
    public const string User = "user" + Extension;
    public const string Weather = "weather" + Extension;
    public const string AddEditToDoPageContents = PageContentsBase + "AddEditToDo" + Extension;
    public const string BonsaiTreePageContents = PageContentsBase + "BonsaiTree" + Extension;
    public const string ProfilePageContents = PageContentsBase + "Profile" + Extension;
    public const string SettingsPageContents = PageContentsBase + "Settings" + Extension;
    public const string ToDosPageContents = PageContentsBase + "ToDos" + Extension;
    public const string IntensitySelectionModalPageContents = PageContentsBase + "IntensitySelectionModal" + Extension;
    private const string PageContentsBase = "PageContents/";
    private const string Extension = ".json";
}