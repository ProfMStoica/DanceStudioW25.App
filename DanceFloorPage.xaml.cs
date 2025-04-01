using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Layouts;

namespace DanceStudio;

public partial class DanceFloorPage : ContentPage
{
	private IDispatcherTimer _danceTimer;
	public DanceFloorPage()
	{
		InitializeComponent();
		
		//initialize the dance timer
		_danceTimer = Dispatcher.CreateTimer();
		_danceTimer.Interval = TimeSpan.FromMilliseconds(500);
		_danceTimer.Tick += OnBasicDanceStep;
	}

	private void OnBasicDanceStep(object sender, EventArgs e)
	{
		//Flip the avatar image on the Y axix
		_imgAvatar.ScaleX *= -1;
	}

	private void SwitchToAbsoluteLayoutBounds()
	{
		if (AbsoluteLayout.GetLayoutFlags(_imgAvatar) == AbsoluteLayoutFlags.PositionProportional)
		{
			//Switch the coordinates of the avatar from proportional to absolute values
			//so it can be moved on the dance floor
			Rect avatarBounds = AbsoluteLayout.GetLayoutBounds(_imgAvatar);
		
			//calculate absolute coordinates
			avatarBounds.X = avatarBounds.X * _alDanceFloor.Width - _imgAvatar.Width / 2;
			avatarBounds.Y = avatarBounds.Y * _alDanceFloor.Height - _imgAvatar.Height / 2;
			
			avatarBounds.Width = _imgAvatar.Width;
			avatarBounds.Height = _imgAvatar.Height;
		
			//switch to absolute coordinates and set them
			AbsoluteLayout.SetLayoutFlags(_imgAvatar, AbsoluteLayoutFlags.None);
			AbsoluteLayout.SetLayoutBounds(_imgAvatar, avatarBounds);			
		}
	}

	private void OnDanceMove(object sender, EventArgs e)
	{
		SwitchToAbsoluteLayoutBounds();
		
		//Check the sender to find out which button was clicked
		//Depending on the button used, move the avatar left/right/up/downw or rotate
		const int DANCE_STEP_SIZE = 10;
		Rect avatarBounds = AbsoluteLayout.GetLayoutBounds(_imgAvatar);
		if (sender == _btnLeft)
		{
			//Move the image to the left
			avatarBounds.X -= DANCE_STEP_SIZE;
		}
		else if (sender == _btnRight)
		{
			//Move the image to the right 
			avatarBounds.X += DANCE_STEP_SIZE;
		}
		else if (sender == _btnUp)
		{
			//Move the image up
			avatarBounds.Y -= DANCE_STEP_SIZE;
		}
		else if (sender == _btnDown)
		{
			//move the image down 
			avatarBounds.Y += DANCE_STEP_SIZE;
		}
		else
		{
			Debug.Assert(false, "Unknown dance move action");
		}
		
		//update the position of the avatar
		AbsoluteLayout.SetLayoutBounds(_imgAvatar, avatarBounds);
	}

	private void OnDanceMoveRotate(object sender, EventArgs e)
	{
		//Check the sender and rotate the 
		const int DANCE_ANGLE_SIZE = 10;
		if (sender == _btnRotateLeft)
		{
			//rotate the image left by 10 degrees
			_imgAvatar.Rotation -= DANCE_ANGLE_SIZE;

		}
		else if (sender == _btnRotateRight)
		{
			//rotate the image right by 10 degrees
			_imgAvatar.Rotation += DANCE_ANGLE_SIZE;
		}
		else
		{
			Debug.Assert(false, "Unknown dance move rotate action");
		}
	}

	private void OnToggleDancing(object sender, EventArgs e)
	{
		//Determine if the avatar is currently dancing based on whether the
		//timer is active
		if (_danceTimer.IsRunning)
		{
			//The avatar is dancing on a time so stop it
			_danceTimer.Stop();
			
			//Update the button label to allow the user to restart dancing
			_btnToggleDancing.Text = "Start Dancing";
		}
		else
		{
			//start the dancing timer
			_danceTimer.Start();
			
			//update the button label to allow the user to stop the avatar's dancing
			_btnToggleDancing.Text = "Stop Dancing";
			
		}
	}
}

