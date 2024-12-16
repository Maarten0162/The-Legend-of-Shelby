using Godot;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

public abstract partial class Enemy : CharacterBody2D
{
	[Export] public float Waittime;
	[Export] public int Speed;
	[Export] public int Health = 100;
	[Export] private int damage = 1;

	[Export] int projAmount;


	public bool isdead;
	private int turnAmount = 0;

	public Vector2 up;
	public Vector2 down;
	public Vector2 left;
	public Vector2 right;
	public AnimatedSprite2D animatedSprite2D;

	public Vector2 enemyVelocity;
	private PackedScene projScene = (PackedScene)GD.Load("res://scenes/boss/projectile.tscn");


	public virtual async void Death()
	{
		await GlobalFunc.Instance.WaitForSeconds(0.05f);//zonder dit krijg je die flush error
		if (isdead) return;
		isdead = true;
		SpawnGold();
		GD.Print("in death");
		QueueFree();

	}
	private void SpawnGold()
	{
		Random rnd = new();
		string RuPound = GlobalVar.Instance.Rupounds[rnd.Next(0, 3)];
		AddSibling(new Pound(this.Position, RuPound));
	}
	public void CollisionCheck(double Delta)
	{
		var collision = MoveAndCollide(Velocity * (float)Delta);
		if (collision != null)
		{
			Node collider = (Node)collision.GetCollider();

			if (collider is PhysicsBody2D body)
			{
				int collisionLayer = (int)body.CollisionLayer;

				if ((collisionLayer & (1 << 7)) != 0) // Check if it's a weapon (Layer 4)
				{
					TakeDamage(100);
				}
			}
		}

	}
	public virtual async Task Movement() //randomised movement
	{
		if (isdead) return;
		if (isdead || !IsInstanceValid(this)) return;

		Random rnd = new Random();
		int whatway = rnd.Next(0, 4);

		switch (whatway)
		{
			case 0:
				enemyVelocity = up;
				GD.Print("move up");
				break;
			case 1:
				enemyVelocity = down;
				GD.Print($"move down{down}");
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

	//boss
	public async Task BossMovement() //randomised movement
	{
		GD.Print("is in movement");
		if (isdead)
		{
			GD.Print("is dead");
			return;
		}
		if (isdead || !IsInstanceValid(this)) return;

		Random rnd = new Random();
		int whatwayB = rnd.Next(0, 3);
		GD.Print("whatway = " + whatwayB);
		if (whatwayB == 2)
		{
			enemyVelocity = Vector2.Zero;
			Attack();
			GD.Print("Attack");
		}
		else
		{
			switch (whatwayB)
			{
				case 0:
					turnAmount -= 1;
					enemyVelocity = left;
					break;
				case 1:
					turnAmount += 1;
					enemyVelocity = right;
					break;
			}
		}




		GD.Print(turnAmount);
		Sprite(false);
		Velocity = enemyVelocity;

		await GlobalFunc.Instance.WaitForSeconds(Waittime);
		if (isdead || !IsInstanceValid(this)) return;
		Velocity = Vector2.Zero;
		animatedSprite2D.Pause();
		await GlobalFunc.Instance.WaitForSeconds(0.5f);
		if (isdead || !IsInstanceValid(this)) return;

		await BossMovement();



	}

	public void Sprite(bool FlipH) //sprite update
	{
		if (!GetTree().Paused)
		{


			if (enemyVelocity.X < 0)
			{
				animatedSprite2D.FlipH = FlipH;
				animatedSprite2D.Play("walk_sideways");
			}
			else if (enemyVelocity.Y < 0)
			{
				animatedSprite2D.FlipH = false;
				animatedSprite2D.Play("walk_up");
			}
			else if (enemyVelocity.X > 0)
			{
				animatedSprite2D.FlipH = false;
				animatedSprite2D.Play("walk_sideways");
			}
			else if (enemyVelocity.Y > 0)
			{
				animatedSprite2D.FlipH = false;
				animatedSprite2D.Play("walk_down");
			}
		}

	}
	public virtual void TakeDamage(int damage)
	{
		// Health -= Player.weapon.damage;
		Health -= damage;
		if (Health <= 0)
		{
			Death();

		}
	}

	public async Task Attack()
	{
		for (int i = 0; i < projAmount; i++)
		{
			Node projSceneInstance = projScene.Instantiate();

			if (projSceneInstance is Projectile projectile)
			{
				if (i == 0)
				{
					projectile.dir = new Vector2(-1, 0);
				}
				else if(i == 1)
				{
					projectile.dir = new Vector2(-1, 0.2f);
				}else if (i == 2)
				{
					projectile.dir = new Vector2(-1, -0.2f);
				} else projectile.dir = new Vector2(-1, 0);



			}
			AddChild(projSceneInstance);
		}

	}
}