using Godot;
using System;

public partial class PauseMenu : Control
{
	// Called when the node enters the scene tree for the first time.CanvasLayer ui;
	string vpnIP = "";
	Control settingsmenu;
	Button resumeButton;
	public override  void _Ready()
	{
		settingsmenu = GetNode<Control>(".");
		resumeButton = GetNode<Button>("VBoxContainer/resume");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		if (Input.IsActionJustPressed("PauseGame"))
		{
			SwitchPauseMode();
		}
		

	}
	async void SwitchPauseMode()
	{
		//pause game
		if (!GetTree().Paused){
			pause();
			resumeButton.GrabFocus();
		}
		//unpause game
		else if (GetTree().Paused){
			unpause();
		}

	}

	private void ResumeButtonPressed()
	{
		unpause();
	}

	private void SettingsButtonPressed()
	{
		
	}

	private void LoadButtonPressed()
	{
		GlobalFunc.Instance.LoadGame();
	}

	private void SaveButtonPressed()
	{
		
	}

	private async void QuitButtonPressed()
	{
		// GD.Print("saving the game");
		// await GlobalFunc.Instance.SaveGameLocally();
		
		// GlobalFunc.Instance.SaveGameToServer();
		GetTree().Quit();
	}


	private async void unpause()
	{
		GetTree().Paused = false;
			settingsmenu.Hide();
			GD.Print("Game Unpaused");
			await GlobalFunc.Instance.WaitForSeconds(0.1f);
	}

	private async void pause()
	{
		GetTree().Paused = true;
			settingsmenu.Show();
			GD.Print("Game paused");
			await GlobalFunc.Instance.WaitForSeconds(0.1f);
	}
}
