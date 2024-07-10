using Foundation;

namespace AtHome.CrossPlatform;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override Microsoft.Maui.Hosting.MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
