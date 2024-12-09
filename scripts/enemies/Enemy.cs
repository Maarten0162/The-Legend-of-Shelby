using Godot;
using System;
using System.Dynamic;
using System.Threading.Tasks;

public abstract partial class Enemy : CharacterBody2D
{
	[Export] float Waittime;
	[Export] public int Speed;

	private int Health;
	private int Damage;
	private bool isdead;

	public Vector2 up;
	public Vector2 down;
	public Vector2 left;
	public Vector2 right;
	public AnimatedSprite2D animatedSprite2D;

	public Vector2 enemyVelocity;


	public void Death()
	{
		if (isdead) return;
		isdead = true;
		SpawnGold();
		GD.Print("in death");
		QueueFree();

	}
	private void SpawnGold(){
	Random rnd = new();
	string RuPound = GlobalVar.Instance.Rupounds[rnd.Next(0,3)];
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

				if ((collisionLayer & (1 << 3)) != 0) // Check if it's a weapon (Layer 4)
				{
					TakeDamage();
				}
			}
		}
	}
	public async Task Movement() //randomised movement
	{
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
		Sprite();
		Velocity = enemyVelocity;

		await GlobalFunc.Instance.WaitForSeconds(Waittime);
		if (isdead || !IsInstanceValid(this)) return;
		Velocity = Vector2.Zero;
		animatedSprite2D.Pause();
		await GlobalFunc.Instance.WaitForSeconds(0.5f);
		if (isdead || !IsInstanceValid(this)) return;
		
		await Movement();



	}
	public void Sprite() //sprite update
	{
		if (!GetTree().Paused)
		{


			if (enemyVelocity.X < 0)
			{
				animatedSprite2D.FlipH = true;
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
	private void TakeDamage()
	{
		// Health -= Player.weapon.damage;
		Health -= 100;
		if (Health <= 0)
		{
			Death();
		}
	}
}