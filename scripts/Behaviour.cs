using Godot;
using System;
using System.Threading.Tasks;

public partial class Behaviour : CharacterBody2D
{
	[Export]	private int Speed;
	[Export] private float Waittime = 2f;	
	AnimatedSprite2D animatedSprite2D;

	Vector2 up;
	Vector2 down;
	Vector2 left;
	Vector2 right;
	Vector2 enemyVelocity;
		public override void _Ready(){
			animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");		
			up = new Vector2(0,1000 * Speed);
			down = new Vector2(0,-1000 * Speed);
			left = new Vector2(-1000,0 * Speed);
			right = new Vector2(1000,0 * Speed);
		}

	public override void _PhysicsProcess(double delta)
	{	Movement();
		Velocity = enemyVelocity;
		Sprite();
		
		
		MoveAndSlide();
	
		
		
	}
	private void Movement()
	{	
		// enemyVelocity = Vector2.Zero;
		Random rnd = new Random();
		int whatway = rnd.Next(0,4);
		switch(whatway)
		{
			case 0:
				enemyVelocity = up *= Speed; 
				break;
			case 1:
				enemyVelocity = down *= Speed; 
				break;
			case 2:
				enemyVelocity = left *= Speed; 
				break;
			case 3:
				enemyVelocity = right *= Speed; 
				break;
		}
	}
	

	async void Sprite()
	{
	if(enemyVelocity.X > 0)
		{
			animatedSprite2D.Play("walk_sideways");
		}
		else if(enemyVelocity.Y > 0)
		{
			animatedSprite2D.Play("walk_up");
		}	
		else if(enemyVelocity.X < 0)
		{	animatedSprite2D.FlipH = true;
			animatedSprite2D.Play("walk_sideways");
			animatedSprite2D.FlipH = false;
		}	
		else if(enemyVelocity.Y < 0)
		{
			animatedSprite2D.Play("walk_down");
		}	
		await WaitForSeconds(2);
	}
	private async Task WaitForSeconds(float seconds)
    {
        GD.Print("in waitforseconds");
        await ToSignal(GetTree().CreateTimer(seconds), "timeout");
        GD.Print("na waitforseconds");
    }
}
