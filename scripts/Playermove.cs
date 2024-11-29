using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

public partial class Playermove : CharacterBody2D
{
	Vector2 playerVelocity;
    AnimatedSprite2D animatedSprite2D;

    CollisionShape2D weapon;
    Sprite2D weaponSprite;

    FacingDirection facing;

    enum FacingDirection {up, down, left, right}

	[Export]    int movementSpeed = 500;

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        weapon = GetNode<CollisionShape2D>("sword/CollisionShape2D");
        weaponSprite = GetNode<Sprite2D>("sword/Sprite2D");

        weaponSprite.Hide();
        weapon.Disabled = true;


    }

    public override void _PhysicsProcess(double delta)
    {
        HandleInput();
        Velocity = playerVelocity;
     
        MoveAndCollide(Velocity * (float)delta);
    }

    private async void HandleInput()
    {
        
        if (Input.IsActionJustPressed("Attack"))
        {

            animatedSprite2D.Play("attack");
            await Attack(facing);
            weaponSprite.Hide();
            animatedSprite2D.Play("default");

        }
        if (Input.IsActionPressed("ui_up")) 
        {
            playerVelocity = new Vector2(0, -1);
        } else if (Input.IsActionPressed("ui_down")) 
        {
           playerVelocity = new Vector2(0, 1); 
        } else if (Input.IsActionPressed("ui_left")) 
        {
            playerVelocity = new Vector2(-1, 0);
        } else if (Input.IsActionPressed("ui_right"))
        {
            playerVelocity = new Vector2(1, 0);
        } else playerVelocity = Vector2.Zero; 

		playerVelocity = playerVelocity *= movementSpeed;
        
        facing= ChangeDirections();
    }
    private FacingDirection ChangeDirections()
	{
        if(playerVelocity.X < 0)
            {
                animatedSprite2D.FlipH = true;
                animatedSprite2D.Play("move_sideways");
                return FacingDirection.left;
            }
            else if(playerVelocity.Y < 0)
            {
                animatedSprite2D.FlipH = false;
                animatedSprite2D.Play("move_up");
                return FacingDirection.up;
            }	
            else if(playerVelocity.X > 0)
            {	
                animatedSprite2D.FlipH = false;
                animatedSprite2D.Play("move_sideways");
                return FacingDirection.right;
            }	
            else if(playerVelocity.Y > 0)
            {
                animatedSprite2D.FlipH = false;
                animatedSprite2D.Play("move_down");
                return FacingDirection.down;
            }
            else animatedSprite2D.Pause();
            return FacingDirection.up;
		
	}

    async Task Attack(FacingDirection direction) 
    {


        GD.Print("in attack ");

        switch (direction){
            case FacingDirection.left:
                GD.Print("val aan");
                Velocity = Vector2.Zero;
			    animatedSprite2D.FlipH = true;
                weaponSprite.Show();
                weapon.Disabled = false;
			    animatedSprite2D.Play("attack_sideways");
                await GlobalFunc.Instance.WaitForSeconds(1f);
                weapon.Disabled = true;
                weaponSprite.Hide();
                break;
            case FacingDirection.right:
                GD.Print("val aan");
                Velocity = Vector2.Zero;
			    animatedSprite2D.FlipH = false;
                weaponSprite.Show();
			    animatedSprite2D.Play("attack_sideways");
                await GlobalFunc.Instance.WaitForSeconds(1f);
                break;
            case FacingDirection.up:
                break;
            case FacingDirection.down:
                break;
        }
       if(facing == FacingDirection.left)
		{
            

		}
		else if(facing == FacingDirection.right)
		{
			animatedSprite2D.FlipH = false;
			//animatedSprite2D.Play("move_up");
		}	
		else if(facing == FacingDirection.up)
		{	
			animatedSprite2D.FlipH = false;
			//animatedSprite2D.Play("move_sideways");
		}	
		else if(facing == FacingDirection.down)
		{
			animatedSprite2D.FlipH = false;
			//animatedSprite2D.Play("move_down");
		}
    }

}
