using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class Playermove : CharacterBody2D
{
	Vector2 playerVelocity;
	int movementSpeed = 500;

	public override void _PhysicsProcess(double delta)
    {
        HandleInput();
        Velocity = playerVelocity;
        GD.Print(playerVelocity);
        MoveAndSlide();
    }

    private void HandleInput()
    {
        playerVelocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		playerVelocity = playerVelocity *= movementSpeed;
    }
}
