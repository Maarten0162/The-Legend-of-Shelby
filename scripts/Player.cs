using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
    Vector2 playerVelocity;
    AnimatedSprite2D animatedSprite2D;
    [Export] int movementSpeed = 500;
    [Export] int Health = 6;


    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedPlayer");
      

    }

    public override void _PhysicsProcess(double delta)
    {
        HandleInput();
        Velocity = playerVelocity;

        var collision = MoveAndCollide(Velocity * (float)delta);
        if (collision != null)
        {
            // Get the collider object
            Node collider = (Node)collision.GetCollider();

            if (collider is PhysicsBody2D body)
            {
                // Retrieve the collider's layer
                int collisionLayer = (int)body.CollisionLayer;


                if ((collisionLayer & (1 << 1)) != 0) // Layer 2 corresponds to bit 1 Dit is een Enemy
                {
                    TakeDamage();
                }
                else if ((collisionLayer & (1 << 2)) != 0) // Layer 3 corresponds to bit 2 Dit is
                {
                    GD.Print("Collided with something on Layer 3!");
                }
            }
        }
    }

    private void HandleInput()
    {


        if (Input.IsActionPressed("ui_up"))
        {
            playerVelocity = new Vector2(0, -1);
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            playerVelocity = new Vector2(0, 1);
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            playerVelocity = new Vector2(-1, 0);
        }
        else if (Input.IsActionPressed("ui_right"))
        {
            playerVelocity = new Vector2(1, 0);
        }
        else playerVelocity = Vector2.Zero;

        playerVelocity = playerVelocity *= movementSpeed;
        Sprite();
    }
    private void Sprite()
    {
        if (playerVelocity.X < 0)
        {
            animatedSprite2D.FlipH = true;
            animatedSprite2D.Play("move_sideways");
        }
        else if (playerVelocity.Y < 0)
        {
            animatedSprite2D.FlipH = false;
            animatedSprite2D.Play("move_up");
        }
        else if (playerVelocity.X > 0)
        {
            animatedSprite2D.FlipH = false;
            animatedSprite2D.Play("move_sideways");
        }
        else if (playerVelocity.Y > 0)
        {
            animatedSprite2D.FlipH = false;
            animatedSprite2D.Play("move_down");
        }
        else animatedSprite2D.Stop();

    }
    public void TakeDamage()
    {
        Health -= 1;
        GD.Print(Health);
    }

}
