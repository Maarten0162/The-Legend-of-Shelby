using Godot;
using System;
using System.Threading.Tasks;

public partial class Projectile : CharacterBody2D
{
	public const float Speed = 300.0f;
	[Export] int seconds;

	public Vector2 dir;
	public Projectile(Vector2 direction)
	{
		dir = direction;
	}

	public Projectile()
	{
	}

	public override void _Ready()
	{
		AddToGroup("Projectile");

		// Ensure this line does not overwrite what is already set			

		killProjectiles();
	}


	public override void _PhysicsProcess(double delta)
	{
		Velocity = dir * 800;
		MoveAndSlide();
	}

	private async void killProjectiles()
	{
		GD.Print("in kilproj");
		for (int i = 1; i < seconds; i++)
		{
			await GlobalFunc.Instance.WaitForSeconds(1);
		}
		GD.Print("kill proj");
		this.Velocity = Vector2.Zero;
		await GlobalFunc.Instance.WaitForSeconds(0.05f);
		this.QueueFree();
	}
}
