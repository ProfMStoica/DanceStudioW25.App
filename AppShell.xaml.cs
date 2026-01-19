namespace DanceStudio;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute("dance-floor", typeof(DanceFloorPage));
    }
}//
