using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{


    Vector2 playerVelocity;
    AnimatedSprite2D animatedSprite2D;
    [Export] public int GridSize = 32;
    [Export] int movementSpeed = 500;
    [Export] public int Health;
    private Vector2 _knockbackVelocity = Vector2.Zero;
    private float _knockbackTimeRemaining = 0;
    [Export] public float KnockbackStrength = 200; // Adjust strength
    [Export] public float KnockbackDuration = 0.2f; // Duration in seconds
    public PackedScene DeathScreen = (PackedScene)ResourceLoader.Load("res://scenes/menus/death_screen.tscn");


    Area2D weaponBody;
    CollisionShape2D weaponCollision;
    Sprite2D weaponSprite;

    Player player;

    FacingDirection facing;


    bool isAttacking = false;
    bool hasSword;
    bool hasBow;

    enum FacingDirection { up, down, left, right }


    //velocity fix??? miss Velocity ook naar zero zetten. 

    public override async void _Ready()
    {
        if (GlobalVar.Instance.playerHealth == null)
        {
            GlobalVar.Instance.playerHealth = Health;
            GlobalVar.Instance.playerCurrency = 0;
        }

        hasSword = true;

        await giveWeapons();

        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedPlayer");
        weaponCollision = GetNode<CollisionShape2D>("weapon/CollisionShape2D");
        weaponSprite = GetNode<Sprite2D>("weapon/Sprite2D");
        weaponBody = GetNode<Area2D>("weapon/");



        weaponSprite.Hide();
        weaponCollision.Disabled = true;

        

        



    }

    public override async void _PhysicsProcess(double delta)
    {
        if (IsInstanceValid(this))
        {
            GlobalVar.Instance.playerPos = GlobalPosition;


            // Press the "testsave" action to save game
            if (Input.IsActionJustPressed("testsave"))
            {
                await GlobalFunc.Instance.SaveGameLocally();
                await GlobalFunc.Instance.SaveGameToServer();
            }

            // Press the "testload" action to load game
            if (Input.IsActionJustPressed("testload"))
            {
                LoadSaveFromServer.Instance.StartDownload();


                await GlobalFunc.Instance.WaitForSeconds(1);

                GlobalPosition = GlobalFunc.Instance.LoadGame();

            }
            if (Input.IsActionJustPressed("Kill"))
            {
                Health -= 5;
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
                        KnockBack(collision.GetPosition());
                        TakeDamage();

                    }
                    else if ((collisionLayer & (1 << 2)) != 0) // Layer 3 
                    {
                        GD.Print("Collided with something on Layer 3!");
                    }


                }
            }


            if (Velocity == Vector2.Zero)
            {
                animatedSprite2D.Pause();
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
            GlobalPosition = SnapXToGrid(GlobalPosition);
        }
        else if (Input.IsActionPressed("D_down"))
        {
            inputVelocity = new Vector2(0, 1);
            GlobalPosition = SnapXToGrid(GlobalPosition);
        }
        else if (Input.IsActionPressed("D_left"))
        {
            inputVelocity = new Vector2(-1, 0);
            GlobalPosition = SnapYToGrid(GlobalPosition);
        }
        else if (Input.IsActionPressed("D_right"))
        {
            inputVelocity = new Vector2(1, 0);
            GlobalPosition = SnapYToGrid(GlobalPosition);
        }

        if (inputVelocity != Vector2.Zero)
        {
            playerVelocity = inputVelocity.Normalized() * movementSpeed;
            facing = ChangeDirections();
        }
        else playerVelocity = Vector2.Zero;
    }

    public void TakeDamage()
    {
        GlobalVar.Instance.playerHealth -= 1;
        GD.Print(GlobalVar.Instance.playerHealth);
        if (GlobalVar.Instance.playerHealth <= 0)
        {

            GD.Print("player died, switching to deathscreen");
            GetTree().ChangeSceneToPacked(DeathScreen);
        }

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


        // Stop movement
        playerVelocity = Vector2.Zero;
        Velocity = Vector2.Zero;

        switch (direction)
        {
            case FacingDirection.left:
                animatedSprite2D.Play("attack_sideways");
                await changeSwordPosition(-8f, 2f, -180);
                break;
            case FacingDirection.right:
                animatedSprite2D.Play("attack_sideways");
                await changeSwordPosition(8f, 2f, 0);
                break;
            case FacingDirection.up:
                animatedSprite2D.Play("attack_up");
                await changeSwordPosition(0f, -12f, -90);
                break;
            case FacingDirection.down:
                animatedSprite2D.Play("attack_down");
                await changeSwordPosition(0f, 12f, 90);
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

    private Vector2 SnapXToGrid(Vector2 position)
    {
        // Round the position to the nearest grid point
        return new Vector2(
            Mathf.Round(position.X / GridSize) * GridSize,
            position.Y // No need for player.GlobalPosition.Y
        );
    }


    private Vector2 SnapYToGrid(Vector2 position)
    {
        // Round the position to the nearest grid point
        return new Vector2(
            position.X, // No need for player.GlobalPosition.X
            Mathf.Round(position.Y / GridSize) * GridSize
        );
    }

    async Task giveWeapons()
    {
        GD.Print("in giveweapons");
        if (hasSword){
            GD.Print("hasSword");
            PackedScene SwordScenes = (PackedScene)GD.Load("res://scenes/weapons/sword.tscn");
            Node sceneInstance = SwordScenes.Instantiate();
            this.AddChild(sceneInstance);
        }
        if (hasBow){
            GD.Print("hasSword");
            PackedScene SwordScenes = (PackedScene)GD.Load("res://scenes/weapons/arrow.tscn");
            Node sceneInstance = SwordScenes.Instantiate();
            this.AddChild(sceneInstance);
        }
        
    }

}
