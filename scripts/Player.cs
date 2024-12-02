using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{


    Vector2 playerVelocity;
    AnimatedSprite2D animatedSprite2D;
    [Export] int movementSpeed = 500;
    [Export] public int Health = 6;
    private Vector2 _knockbackVelocity = Vector2.Zero;
    private float _knockbackTimeRemaining = 0;
    [Export] public float KnockbackStrength = 200; // Adjust strength
    [Export] public float KnockbackDuration = 0.2f; // Duration in seconds

    CollisionShape2D weapon;
    Sprite2D weaponSprite;

    FacingDirection facing;


    enum FacingDirection { up, down, left, right }


    public override void _Ready()
    {  
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedPlayer");
        weapon = GetNode<CollisionShape2D>("weapon/CollisionShape2D");
        weaponSprite = GetNode<Sprite2D>("weapon/Sprite2D");

        weaponSprite.Hide();
        weapon.Disabled = true;


    }

    public override async void _PhysicsProcess(double delta)
    {
        if (_knockbackTimeRemaining > 0)
        {
            // Apply knockback force
            playerVelocity = _knockbackVelocity;
            _knockbackTimeRemaining -= (float)delta;
        }
        else await HandleInput();

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
                    KnockBack(collision.GetPosition());
                }
                else if ((collisionLayer & (1 << 2)) != 0) // Layer 3 corresponds to bit 2 Dit is
                {
                    GD.Print("Collided with something on Layer 3!");
                }
            }
        }
    }
    private void KnockBack(Vector2 EnemyPosition)
    {
        Vector2 knockbackDirection = (GlobalPosition - EnemyPosition).Normalized();
        _knockbackVelocity = knockbackDirection * KnockbackStrength;
        // Set knockback duration
        _knockbackTimeRemaining = KnockbackDuration;


    }
    private async Task HandleInput()
    {
        if (Input.IsActionJustPressed("Attack"))
        {
            playerVelocity = Vector2.Zero;
            animatedSprite2D.Play("attack");
            await Attack(facing);
            weaponSprite.Hide();
            animatedSprite2D.Play("default");

        }
        else if (Input.IsActionPressed("ui_up"))
        {
            playerVelocity = new Vector2(0, -1);
            facing = ChangeDirections();
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            playerVelocity = new Vector2(0, 1);
            facing = ChangeDirections();
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
        facing = ChangeDirections();
    }
    public void TakeDamage()
    {
        Health -= 1;
        GD.Print(Health);
    }

    private FacingDirection ChangeDirections()
    {
        if (playerVelocity.X < 0)
        {
            animatedSprite2D.FlipH = true;
            animatedSprite2D.Play("move_sideways");
            return FacingDirection.left;
        }
        else if (playerVelocity.Y < 0)
        {
            animatedSprite2D.FlipH = false;
            animatedSprite2D.Play("move_up");

            return FacingDirection.up;
        }
        else if (playerVelocity.X > 0)
        {
            animatedSprite2D.FlipH = false;
            animatedSprite2D.Play("move_sideways");
            return FacingDirection.right;
        }
        else if (playerVelocity.Y > 0)
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

        switch (direction)
        {
            case FacingDirection.left:
                GD.Print("val aan");
                Velocity = Vector2.Zero;
                animatedSprite2D.FlipH = true;
                weaponSprite.Show();
                weapon.Disabled = false;
                animatedSprite2D.Play("attack_sideways");
                await GlobalFunc.Instance.WaitForSeconds(2f);
                weapon.Disabled = true;
                weaponSprite.Hide();
                break;
            case FacingDirection.right:
                GD.Print("val aan");
                Velocity = Vector2.Zero;
                animatedSprite2D.FlipH = false;
                weaponSprite.Show();
                weapon.Disabled = false;
                animatedSprite2D.Play("attack_sideways");
                await GlobalFunc.Instance.WaitForSeconds(2f);
                weapon.Disabled = true;
                weaponSprite.Hide();
                break;
            case FacingDirection.up:
                break;
            case FacingDirection.down:
                break;
        }
    }

}
