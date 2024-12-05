using Godot;
using System;

public partial class RoomTransition : Node
{
	[Export] public string TargetScenePath = ""; // Path to the next room/scene
	public override void _Ready()
	{
		GD.Print("room transition ready");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	void _on_body_entered(Node body)
	{

		CallDeferred(nameof(ChangeScene));
	}


	private void ChangeScene()
	{
		GetTree().ChangeSceneToFile(TargetScenePath); // Change to the target scene
	}
}

