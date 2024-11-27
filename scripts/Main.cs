using Godot;
using System;

public partial class Main : Node2D
{

	Vector2 playerVector;
	int movementSpeed = 500;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	void playerMovement() 
	{
		playerVector = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
	}
	
}
