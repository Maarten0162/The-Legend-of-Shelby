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
    public PackedScene DeathScreen = (PackedScene)ResourceLoader.Load("res://scenes/death_screen.tscn");


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
            LoadSaveFromServer.Instance.StartDownload();
            //GlobalPosition = GlobalFunc.Instance.LoadGame();
        }
         if (Input.IsActionJustPressed("Kill"))
        {
            Health -=5;
            TakeDamage();
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
            playerVelocity = Vector2.Zero;
            Velocity = Vector2.Zero;
        }

        Velocity = playerVelocity;

        if(!IsInstanceValid(this)){
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
                {   KnockBack(collision.GetPosition());
                    TakeDamage();
                    
                }
                else if ((collisionLayer & (1 << 2)) != 0) // Layer 3 corresponds to bit 2 Dit is
                {
                    GD.Print("Collided with something on Layer 3!");
                }
            }
        }
        }

        if (Velocity == Vector2.Zero)
        {
            animatedSprite2D.Pause();
        }
        GD.Print("uit loop");
    }


    private void KnockBack(Vector2 EnemyPosition)
    {
        Vector2 knockbackDirection = (GlobalPosition - EnemyPosition).Normalized();
        _knockbackVelocity = knockbackDirection * KnockbackStrength;
        // Set knockback duration
        _knockbackTimeRemaining = KnockbackDuration;


    }

    // Movement handling

    private async Task HandleInput()
    {
        if (isAttacking)
        {
            playerVelocity = Vector2.Zero;
            return;
        }

        // Handle attack input
        if (Input.IsActionJustPressed("Attack"))
        {
            await Attack(facing); // Use the last known facing direction
            weaponSprite.Hide();
            return;
        }

        // Handle movement input
        Vector2 inputVelocity = Vector2.Zero;
        if (Input.IsActionPressed("D_up"))
        {
            inputVelocity = new Vector2(0, -1);
        }
        else if (Input.IsActionPressed("D_down"))
        {
            inputVelocity = new Vector2(0, 1);
        }
        else if (Input.IsActionPressed("D_left"))
        {
            inputVelocity = new Vector2(-1, 0);
        }
        else if (Input.IsActionPressed("D_right"))
        {
            inputVelocity = new Vector2(1, 0);
        }

        if (inputVelocity != Vector2.Zero)
        {  
            playerVelocity = inputVelocity.Normalized() * movementSpeed;
            facing = ChangeDirections();
        }
        else playerVelocity = Vector2.Zero;
        GD.Print("uit hndleinput");
    }

    public void TakeDamage()
    {   Health -=1;
        GlobalVar.Instance.playerHealth = Health;
        GD.Print(GlobalVar.Instance.playerHealth);
        if(Health <= 0){
                   
            GD.Print("player died, switching to deathscreen");            
            GetTree().ChangeSceneToPacked(DeathScreen);
        }
        GD.Print("uit takedamage");
  
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

        GD.Print("in attack");

        // Stop movement
        playerVelocity = Vector2.Zero;
        Velocity = Vector2.Zero;

        switch (direction)
        {
            case FacingDirection.left:
                animatedSprite2D.Play("attack_sideways");
                await changeSwordPosition(-8f, 2f, -90);
                break;
            case FacingDirection.right:
                animatedSprite2D.Play("attack_sideways");
                await changeSwordPosition(8f, 2f, 90);
                break;
            case FacingDirection.up:
                animatedSprite2D.Play("attack_up");
                await changeSwordPosition(0f, -12f, 0);
                break;
            case FacingDirection.down:
                animatedSprite2D.Play("attack_down");
                await changeSwordPosition(0f, 12f, 180);
                break;
        }
        while (animatedSprite2D.IsPlaying())
        {
            await Task.Delay(50); // This checks every 50ms if the animation is still playing
        }

        if (facing == FacingDirection.left)
        {
            animatedSprite2D.FlipH = true;
            animatedSprite2D.Play("move_sideways");
        }
        else if (facing == FacingDirection.up)
        {
            animatedSprite2D.FlipH = false;
            animatedSprite2D.Play("move_up");
        }
        else if (facing == FacingDirection.right)
        {
            animatedSprite2D.FlipH = false;
            animatedSprite2D.Play("move_sideways");
        }
        else if (facing == FacingDirection.down)
        {
            animatedSprite2D.FlipH = false;
            animatedSprite2D.Play("move_down");
        }


        animatedSprite2D.Frame = 0;
        animatedSprite2D.Pause();
        isAttacking = false;
        GD.Print("in Attack");
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
