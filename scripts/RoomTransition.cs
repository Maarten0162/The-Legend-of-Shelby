using Godot;
using System;

public partial class RoomTransition : Node
{
	[Export] public string Target; // Path to the next room/scene
	string exit;
	public override void _Ready()
	{
		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_body_entered(Node body)
	{
		


		CallDeferred(nameof(ChangeScene));
	}


	private void ChangeScene()
	{	exit = GetNode<RoomTransition>(".").Name;
	GD.Print(exit + " was the exit used");
		GetTree().ChangeSceneToFile($"res://scenes/rooms/{Target}.tscn"); // Change to the target scene
		
	}
}

