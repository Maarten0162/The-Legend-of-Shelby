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


    CharacterBody2D weaponBody;
    CollisionShape2D weaponCollision;
    Sprite2D weaponSprite;

    Player player;

    FacingDirection facing;


    bool isAttacking = false;

    enum FacingDirection { up, down, left, right }


    //velocity fix??? miss Velocity ook naar zero zetten. 

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedPlayer");
        weaponCollision = GetNode<CollisionShape2D>("weapon/CollisionShape2D");
        weaponSprite = GetNode<Sprite2D>("weapon/Sprite2D");
        weaponBody = GetNode<CharacterBody2D>("weapon/");

        

        weaponSprite.Hide();
        weaponCollision.Disabled = true;

        GlobalVar.Instance.playerHealth = Health;


    }

    public override async void _PhysicsProcess(double delta)
    {
        GlobalVar.Instance.playerPos = GlobalPosition;


        // Press the "testsave" action to save game
        if (Input.IsActionJustPressed("testsave"))
        {
            GlobalFunc.Instance.SaveGame();
        }

        // Press the "testload" action to load game
        if (Input.IsActionJustPressed("testload"))
        {
            
            GlobalPosition = GlobalFunc.Instance.LoadGame();
        }

        if (_knockbackTimeRemaining > 0)
        {
            // Apply knockback force
            playerVelocity = _knockbackVelocity;
            _knockbackTimeRemaining -= (float)delta;
        }
        else await HandleInput();
        if (isAttacking)//als het een while is crasht de game
        {
            switch (facing)
            {
                case FacingDirection.left:
                    playerVelocity = Vector2.Zero;
                    animatedSprite2D.FlipH = true;
                    animatedSprite2D.Play("attack_sideways");
                    break;
                case FacingDirection.right:
                    playerVelocity = Vector2.Zero;
                    animatedSprite2D.Play("attack_sideways");
                    break;
                case FacingDirection.up:
                    playerVelocity = Vector2.Zero;

                    break;
                case FacingDirection.down:
                    playerVelocity = Vector2.Zero;
                    animatedSprite2D.Play("attack_down");
                    break;

            }
        }

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
        GlobalVar.Instance.playerHealth -= 1;
        GD.Print(GlobalVar.Instance.playerHealth);
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

        isAttacking = true;

        GD.Print("in attack ");

        switch (direction)
        {
            case FacingDirection.left:
                playerVelocity = Vector2.Zero;
                animatedSprite2D.Play("attack_sideways");
                await changeSwordPosition(-8f, 2f, -90);
                break;
            case FacingDirection.right:
                playerVelocity = Vector2.Zero;
                animatedSprite2D.Play("attack_sideways");
                await changeSwordPosition(8f, 2f, 90);
                break;
            case FacingDirection.up:
                playerVelocity = Vector2.Zero;

                break;
            case FacingDirection.down:
                animatedSprite2D.Play("attack_down");
                await changeSwordPosition(0f, 12f, 180);
                break;
        }
        isAttacking = false;
    }
    
    async Task changeSwordPosition(float x, float y, float Rotation)
    {
        weaponBody.Position = new Vector2(x, y);
        weaponBody.Rotation = Mathf.DegToRad(Rotation);
        weaponSprite.Show();
        weaponCollision.Disabled = false;
        await GlobalFunc.Instance.WaitForSeconds(0.2f);
        weaponCollision.Disabled = true;
        weaponSprite.Hide();
    }

}
