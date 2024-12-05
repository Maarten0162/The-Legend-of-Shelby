using Godot;
using System;

public partial class PauseMenu : Control
{
	// Called when the node enters the scene tree for the first time.CanvasLayer ui;
	Control settingsmenu;
	public override  void _Ready()
	{
		settingsmenu = GetNode<Control>(".");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		if (Input.IsActionJustReleased("PauseGame"))
		{
			pause();
		}
		

	}
	async void pause()
	{
		//pause game
		if (!GetTree().Paused){
			GetTree().Paused = true;
			settingsmenu.Show();
			GD.Print("Game paused");
			await GlobalFunc.Instance.WaitForSeconds(0.1f);
		}
		//unpause game
		else if (GetTree().Paused){
			GetTree().Paused = false;
			settingsmenu.Hide();
			GD.Print("Game Unpaused");
			await GlobalFunc.Instance.WaitForSeconds(0.1f);
		}

	}
}
