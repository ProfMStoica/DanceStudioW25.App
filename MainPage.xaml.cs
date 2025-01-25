namespace DanceStudio;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnWelcomeToDanceStudio(object sender, EventArgs e)
    {
        //Display the image and the action buttons
        _imgAvatar.IsVisible = true;
        _btnStart.IsVisible = true;
        _btnStart.IsEnabled = true;
        _btnStop.IsVisible = true;
        _btnStop.IsEnabled = false;
    }

    private void OnToggleDancing(object sender, EventArgs e)
    {
        //Toggle the animation of the avatar image based on its current stage and update the action button
        //Determine  which button has triggered the event handler
        Button btnAction = (Button)sender;
        if (btnAction == _btnStop)
        {
            //Stop the animation
            _imgAvatar.IsAnimationPlaying = false;
            
            //Allow the user to start the animation again
            _btnStop.IsEnabled = false;
            _btnStart.IsEnabled = true;
        }
        else if (btnAction == _btnStart)
        {
            //The animation is not playing. Start the animation
            _imgAvatar.IsAnimationPlaying = true;
            
            //Allow the user to stop the animation
            _btnStop.IsEnabled = true;
            _btnStart.IsEnabled = false;
        }
    }
}