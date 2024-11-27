using Godot;
using System;
using System.Threading.Tasks;

public partial class Behaviour : CharacterBody2D
{
	[Export] private int Speed;
	[Export] private float Waittime = 2f;	
	AnimatedSprite2D animatedSprite2D;

	Vector2 up;
	Vector2 down;
	Vector2 left;
	Vector2 right;
	Vector2 enemyVelocity;
		public override async void _Ready(){

			animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");		
			up = new Vector2(0, 1 * Speed);
			down = new Vector2(0, -1 * Speed);
			left = new Vector2(-1 * Speed ,0);
			right = new Vector2(1 * Speed ,0);
			while(1==1){
				enemyVelocity = Vector2.Zero;
				await Movement();
			}

			
		}

	public override void _PhysicsProcess(double delta)
	{	
		Velocity = enemyVelocity;

		GD.Print(Velocity);
		MoveAndSlide();
	
	}
	private async Task Movement()
	{	
		Random rnd = new Random();
		int whatway = rnd.Next(0,4);

		switch(whatway)
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

		for (int i = 0; i < 1; i++)
        {
            Velocity = enemyVelocity;
            await GlobalFunc.Instance.WaitForSeconds(1); 
        }
	}
	

	private void Sprite()
	{
	if(enemyVelocity.X < 0)
		{
			animatedSprite2D.FlipH = true;
			animatedSprite2D.Play("walk_sideways");
		}
		else if(enemyVelocity.Y < 0)
		{
			animatedSprite2D.FlipH = false;
			animatedSprite2D.Play("walk_up");
		}	
		else if(enemyVelocity.X > 0)
		{	
			animatedSprite2D.FlipH = false;
			animatedSprite2D.Play("walk_sideways");
		}	
		else if(enemyVelocity.Y > 0)
		{
			animatedSprite2D.FlipH = false;
			animatedSprite2D.Play("walk_down");
		}	
		
	}

}
