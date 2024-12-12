using Godot;
using System;
using System.Threading.Tasks;


public partial class Hydra : Enemy
{
	public Hydra()
	{
		
	}

	public Hydra    (Vector2 positon)
	{
		this.GlobalPosition = positon;
	}
	public override async void _Ready()
	{					
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedEnemy");
		left = new Vector2(-1 * Speed, 0);
		right = new Vector2(1 * Speed, 0);

		enemyVelocity = Vector2.Zero;
        
        
        await BossMovement();

	}

	public override void _PhysicsProcess(double delta)
	{

		CollisionCheck(delta);


	}



	
	


}
