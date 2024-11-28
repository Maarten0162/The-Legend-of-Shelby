using Godot;
using System;

public partial class Behaviour : CharacterBody2D
{
	[Export]
	private int Speed = 300;
	Vector2 velocity;
		Vector2 up = new Vector2 (0, 500);
		Vector2 down = new Vector2 (0, -500);
		Vector2 right = new Vector2 (500, 0);
		Vector2 left = new Vector2 (-500, 0);

	public override void _PhysicsProcess(double delta)
	{	

		
		//up, right, left , down, down, up
		Movement(up);
		Movement(right);
		Movement(le);
		Movement(up);
		Movement(up);
		Movement(up);
	

		
		

		Velocity = velocity;
		MoveAndSlide();
	}

	void Movement(Vector2 movement)
	{
		velocity = movement;

	}
}
