using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

public partial class Playermove : CharacterBody2D
{
	Vector2 playerVelocity;
    AnimatedSprite2D animatedSprite2D;
	[Export]    int movementSpeed = 500;

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    }

    public override async void _PhysicsProcess(double delta)
    {
        HandleInput();
        Velocity = playerVelocity;
        //GD.Print(playerVelocity);
        MoveAndSlide();
    }

    private void HandleInput()
    {
        playerVelocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		playerVelocity = playerVelocity *= movementSpeed;
        Sprite();
    }
    private void Sprite()
	{
	if(playerVelocity.X < 0)
		{
			animatedSprite2D.FlipH = true;
			animatedSprite2D.Play("move_sideways");
		}
		else if(playerVelocity.Y < 0)
		{
			animatedSprite2D.FlipH = false;
			animatedSprite2D.Play("move_up");
		}	
		else if(playerVelocity.X > 0)
		{	
			animatedSprite2D.FlipH = false;
			animatedSprite2D.Play("move_sideways");
		}	
		else if(playerVelocity.Y > 0)
		{
			animatedSprite2D.FlipH = false;
			animatedSprite2D.Play("move_down");
		}
        else animatedSprite2D.Stop();
		
	}

}
