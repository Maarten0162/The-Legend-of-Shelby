using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void StartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/Room_01.tscn");

	}
	private void SettingsButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/settings.tscn");
	}
	
	private void ExitButtonPressed() 
	{
		GetTree().Quit();
	}
}
