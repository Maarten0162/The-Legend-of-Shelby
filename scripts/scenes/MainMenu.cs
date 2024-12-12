using Godot;
using System;

public partial class MainMenu : Control
{

	Button startButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startButton = GetNode<Button>("BoxContainer/StartButton");
		startButton.GrabFocus();
		SoundFx.instance.changeMusic("TitleScreen");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void StartButtonPressed()
	{
		GD.Print("switching scenes");
		GetTree().ChangeSceneToFile("res://scenes/rooms/Room_01.tscn");

	}
	private void SettingsButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menus/settings.tscn");
	}
	
	private void ExitButtonPressed() 
	{
		GetTree().Quit();
	}
}
