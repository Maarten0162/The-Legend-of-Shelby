using Godot;
using System;

public partial class DeathScreen : Node2D
{

	private PackedScene mainScene = (PackedScene)ResourceLoader.Load("res://scenes/main.tscn");
	private PackedScene mainMenuScene = (PackedScene)ResourceLoader.Load("res://scenes/Main Menu.tscn");

	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		GD.Print("in Deatchscreen");
		GD.Print(InputMap.HasAction("Yes")); // Should print "True"
		GD.Print(InputMap.HasAction("No"));  // Should print "True"
		SetProcessInput(true);


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
	}
	public override void _Input(InputEvent @event)
{
    if (@event.IsActionPressed("Yes"))
    {
        GD.Print("Unhandled Input: Yes action detected");
		GetTree().ChangeSceneToPacked(mainScene);
    }
    else if (@event.IsActionPressed("No"))
    {
        GD.Print("Unhandled Input: No action detected");
		GetTree().ChangeSceneToPacked(mainMenuScene);
    }
}

}
