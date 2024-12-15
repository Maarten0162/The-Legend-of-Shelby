using Godot;
using System;

public partial class Block : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Access the CollisionShape2D node and create a PhysicsMaterial2D
		var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
		var material = new PhysicsMaterial();
		material.Friction = 0.5f;  // Adjust friction here (0 is no friction, 1 is maximum)



		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
