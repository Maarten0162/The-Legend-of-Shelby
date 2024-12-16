using Godot;
using System;
using System.Threading.Tasks;

public partial class Lynel : Enemy
{


    bool iframes;

    Timer hittimer;
    Timer iframetimer;
    CollisionShape2D hitbox;

    public Lynel()
    {

    }

    public Lynel(Vector2 positon)
    {
        this.GlobalPosition = positon;
    }
    public override async void _Ready()
    {   hitbox = GetNode<CollisionShape2D>("CollisionEnemy");
        iframetimer = GetNode<Timer>("iframeTimer");
        hittimer = GetNode<Timer>("hitTimer");
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedEnemy");
        left = new Vector2(-1 * Speed, 0);
        right = new Vector2(1 * Speed, 0);
        up = new Vector2(0, 1 * Speed);
        down = new Vector2(0, -1 * Speed);

        enemyVelocity = Vector2.Zero;


        await Movement();

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

    public override async Task Movement() //randomised movement
	{
		if (isdead) return;
		if (isdead || !IsInstanceValid(this)) return;

		Random rnd = new Random();
		int whatway = rnd.Next(0, 4);

		switch (whatway)
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
		Sprite(true);
		Velocity = enemyVelocity;

		await GlobalFunc.Instance.WaitForSeconds(Waittime);
		if (isdead || !IsInstanceValid(this)) return;
		Velocity = Vector2.Zero;
		animatedSprite2D.Pause();
		await GlobalFunc.Instance.WaitForSeconds(0.5f);
		if (isdead || !IsInstanceValid(this)) return;

		await Movement();



	}
    public override async void Death()
	{
		await GlobalFunc.Instance.WaitForSeconds(0.05f);//zonder dit krijg je die flush error
		if (isdead) return;
		isdead = true;
		GlobalVar.Instance.HealthUpgrade = true;
        GlobalVar.Instance.playerHealth = 8;
		GD.Print("in death");
       this.Hide();
       hitbox.Disabled = true;
       
          
            GetNode<Label>("../Player/Label").Text = "You gained a heart container!";
            await GlobalFunc.Instance.WaitForSeconds(2);
            GetNode<Label>("../Y-Sort/Player/Label").Text = "";
            await GlobalFunc.Instance.WaitForSeconds(0.05f);
            QueueFree();
		QueueFree();

	}




}
