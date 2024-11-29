using Godot;
using System;
using System.Threading.Tasks;


public partial class Moblin : Enemy
{
	[Export] public int Speed;
	[Export] public float Waittime;
	[Export] public int Health = 100;
	[Export] private int damage = 1;
	public int Damage
	{
		get
		{
			return damage;
		}
	}






	public override async void _Ready()
	{

		
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedEnemy");
		up = new Vector2(0, 1 * Speed);
		down = new Vector2(0, -1 * Speed);
		left = new Vector2(-1 * Speed, 0);
		right = new Vector2(1 * Speed, 0);
		while (1 == 1)
		{
			enemyVelocity = Vector2.Zero;
			await Movement();
		}



	}

	public override void _PhysicsProcess(double delta)
	{


		
		var collision = MoveAndCollide(Velocity * (float)delta);
		if(collision != null)
		{

		}

	}
	public override async Task Movement()
	{
		Random rnd = new Random();
		int whatway = rnd.Next(0, 4);

		switch (whatway)
		{
			case 0:
				enemyVelocity = up;
				break;
			case 1:
				enemyVelocity = down;
				break;
			case 2:
				enemyVelocity = left;
				break;
			case 3:
				enemyVelocity = right;
				break;
		}
		Sprite();
		Velocity = enemyVelocity;
		await GlobalFunc.Instance.WaitForSeconds(Waittime);
		Velocity = Vector2.Zero;
		animatedSprite2D.Pause();
		await GlobalFunc.Instance.WaitForSeconds(0.5f);

	}


	public override void Sprite()
	{
		if (enemyVelocity.X < 0)
		{
			animatedSprite2D.FlipH = true;
			animatedSprite2D.Play("walk_sideways");
		}
		else if (enemyVelocity.Y < 0)
		{
			animatedSprite2D.FlipH = false;
			animatedSprite2D.Play("walk_up");
		}
		else if (enemyVelocity.X > 0)
		{
			animatedSprite2D.FlipH = false;
			animatedSprite2D.Play("walk_sideways");
		}
		else if (enemyVelocity.Y > 0)
		{
			animatedSprite2D.FlipH = false;
			animatedSprite2D.Play("walk_down");
		}

	}
	

}
