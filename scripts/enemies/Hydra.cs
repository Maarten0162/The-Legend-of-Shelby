using Godot;
using System;
using System.Threading.Tasks;


public partial class Hydra : Enemy
{

    bool iframes;

    Timer hittimer;
    Timer iframetimer;

    public Hydra()
    {

    }

    public Hydra(Vector2 positon)
    {
        this.GlobalPosition = positon;
    }
    public override async void _Ready()
    {
        iframetimer = GetNode<Timer>("iframeTimer");
        hittimer = GetNode<Timer>("hitTimer");
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedEnemy");
        left = new Vector2(-1 * Speed, 0);
        right = new Vector2(1 * Speed, 0);

        enemyVelocity = Vector2.Zero;


        await BossMovement();

    }

    public override void _PhysicsProcess(double delta)
    {

        CollisionCheck(delta);


    }

    public override void TakeDamage(int damage)
    {
        if (!iframes)
        {
            iframes = true;
            iframetimer.Start();
            animatedSprite2D.Modulate = new Color("ff4233");
            hittimer.Start();
            Health -= damage;
            if (Health <= 0)
            {
                animatedSprite2D.Modulate = new Color("FFFFFF");
                Death();
            }
        }
    }

    void IframeTimerTimeout()
    {
        iframes = false;
        GD.Print("iframes op false");
    }

    void HitTimerTimeout()
    {
        animatedSprite2D.Modulate = new Color("FFFFFF");
    }



}
