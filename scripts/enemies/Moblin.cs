using Godot;
using System;
using System.Threading.Tasks;


public partial class Moblin : Enemy
{
	public override async void _Ready()
	{					
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedEnemy");
		up = new Vector2(0, 1 * Speed);
		down = new Vector2(0, -1 * Speed);
		left = new Vector2(-1 * Speed, 0);
		right = new Vector2(1 * Speed, 0);
		enemyVelocity = Vector2.Zero;

		await Movement();
	}

	public override void _PhysicsProcess(double delta)
	{

		CollisionCheck(delta);


	}


	
	


}
