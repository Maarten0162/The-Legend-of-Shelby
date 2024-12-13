using Godot;
using System;
using System.Threading.Tasks;


public partial class Moblin : Enemy
{
	public Moblin()
	{

	}
	PackedScene key = GD.Load<PackedScene>("res://scenes/Items/key.tscn");
	[Export] public bool HasRedKey;
	public Moblin(Vector2 positon)
	{
		this.GlobalPosition = positon;
	}
	public override async void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedEnemy");
		up = new Vector2(0, 1 * Speed);
		down = new Vector2(0, -1 * Speed);
		left = new Vector2(-1 * Speed, 0);
		right = new Vector2(1 * Speed, 0);
		enemyVelocity = Vector2.Zero;

		await Movement();
	}

	public override void _PhysicsProcess(double delta)
	{

		CollisionCheck(delta);
		if (Health <= 0 && HasRedKey && !GlobalVar.Instance.OpenendRedDoor)
		{

			Node jaja = key.Instantiate();

			if (jaja is Key ku)
			{

				ku.Position = this.Position;
				GetParent().AddChild(ku);
				ku.Keytype = "Red";

			}
		}


	}






}
