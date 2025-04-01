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
	/// <summary>
	/// Timer used to animate the image to simulate the avatar dancing
	/// </summary>
	private IDispatcherTimer _danceTimer;
	
	public DanceFloorPage()
	{
		InitializeComponent();
		
		//Create and configure the timer
		_danceTimer = Dispatcher.CreateTimer();
		_danceTimer.Interval = TimeSpan.FromMilliseconds(500);
		_danceTimer.Tick += OnBasicDanceStep;
	}

	private void OnBasicDanceStep(object sender, EventArgs e)
	{
		//On every beat, flip the image on the vertical axis
		_imgAvatar.ScaleX *= -1;
	}

	/// <summary>
	/// Switches the image avatar from proportional bounds to absolute bounds
	/// </summary>
	private void SwitchToAbsoluteLayoutBounds()
	{
		if (AbsoluteLayout.GetLayoutFlags(_imgAvatar) == AbsoluteLayoutFlags.PositionProportional)
		{
			//Calculate absolute position of the control
			Rect avatarBounds = AbsoluteLayout.GetLayoutBounds(_imgAvatar);

			//Switch to absolute positioning
			avatarBounds.X = avatarBounds.X * _alDanceFloor.Width - _imgAvatar.Width / 2;
			avatarBounds.Y = avatarBounds.Y * _alDanceFloor.Height - _imgAvatar.Height / 2;
			avatarBounds.Width = _imgAvatar.Width;
			avatarBounds.Height = _imgAvatar.Height;

			//Set the new absolute bounds
			AbsoluteLayout.SetLayoutFlags(_imgAvatar, AbsoluteLayoutFlags.None);
			AbsoluteLayout.SetLayoutBounds(_imgAvatar, avatarBounds);
		}
	}

	private void OnDanceMove(object sender, EventArgs e)
	{
		//The avatar is initially set to center using proportional position
		SwitchToAbsoluteLayoutBounds();

		//Determine the size of the step the avatar makes
		const int DANCE_STEP_SIZE = 10;
		Rect avatarBounds = AbsoluteLayout.GetLayoutBounds(_imgAvatar);

		//Check the direction of the movement and calculate the new position
		if (sender == _btnLeft)
		{
			//Move the avatar to the left
			avatarBounds.X -= DANCE_STEP_SIZE;
		}
		else if (sender == _btnRight)
		{
			//Move the avatar to the right
			avatarBounds.X += DANCE_STEP_SIZE;
		}
		else if (sender == _btnUp)
		{
			//Move the avatar up
			avatarBounds.Y -= DANCE_STEP_SIZE;
		}
		else if (sender == _btnDown)
		{
			//Move the avatar down
			avatarBounds.Y += DANCE_STEP_SIZE;
		}
		else
		{
			Debug.Assert(false, "Unknown dance move");
		}

		//Move the avatar to its new position
		AbsoluteLayout.SetLayoutBounds(_imgAvatar, avatarBounds);
	}

	private void OnDanceMoveRotate(object sender, EventArgs e)
	{
		//Define the rotation step
		const int  DANCE_ANGLE_SIZE = 10;
		
		//Apply the rotation depending on user request
		if (sender == _btnRotateLeft)
		{
			//Rotate the image left
			_imgAvatar.Rotation -= DANCE_ANGLE_SIZE;
		}
		else if (sender == _btnRotateRight)
		{
			//Rotate the image right
			_imgAvatar.Rotation += DANCE_ANGLE_SIZE;
		}
		else
		{
			Debug.Assert(false, "Unknown rotatation dance move");
		}

	}

	private void OnToggleDancing(object sender, EventArgs e)
	{
		//Toggle the timer and the caption of the button to Start / Stop Dancing
		if (_danceTimer.IsRunning)
		{
			//The avatar is dancing we need to stop it
			_danceTimer.Stop();
			
			//Allow the user to start the avatar dancing again
			_btnToggleDancing.Text = "Start Dancing";
		}
		else
		{
			//The avatar is not dancing, so we need to start 
			_danceTimer.Start();
			
			//Allow the user to stop the avatar from dancing
			_btnToggleDancing.Text = "Stop Dancing";
		}
	}
}

