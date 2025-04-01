namespace DanceStudio;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        //Declare custom routes that are used programmatically
        Routing.RegisterRoute("dance-floor", typeof(DanceFloorPage));
    }
}
