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
        _btnAction.IsVisible = true;
    }

    private void OnToggleDancing(object sender, EventArgs e)
    {
        //Toggle the animation of the avatar image based on its current stage and update the action button
        if (_imgAvatar.IsAnimationPlaying)
        {
            //Stop the animation
            _imgAvatar.IsAnimationPlaying = false;
            
            //Allow the user to start the animation again
            _btnAction.Text = "Start Dancing";
        }
        else
        {
            //The animation is not playing. Start the animation
            _imgAvatar.IsAnimationPlaying = true;
            
            //Allow the user to stop the animation
            _btnAction.Text = "Stop Dancing";
        }
    }
}