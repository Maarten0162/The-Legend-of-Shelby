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

	private async void SettingsButtonPressed()
	{
		await GlobalFunc.Instance.SaveGameLocally();
		GD.Print("make settings menu");
	}

	private void LoadButtonPressed()
	{
		unpause();
		GD.Print("loading save file");
		//unpause();
		//GlobalFunc.Instance.LoadGame();
		
	}

	private void SaveButtonPressed()
	{
		unpause();
		GD.Print("loading save file");
		// await GlobalFunc.Instance.SaveGameLocally();
		// await GlobalFunc.Instance.SaveGameToServer();
		// unpause();
		
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
