using Godot;
using System;

public partial class Weapon : Area2D
{
	private CollisionShape2D colision;
	[Export]	private string WeaponName;
	private enum WeaponType { Meele, Range, Magic }
	private AnimatedSprite2D WeaponSprite;
	private int Weapondamage;
	public int WeaponDamage{
		get{
			return Weapondamage;
		}
		set{
			Weapondamage = value;
		}
	}
	private float Weaponcooldown;
	public float WeaponCooldown{
		get{
			return Weaponcooldown;
		}
		set{
			Weaponcooldown = value;
		}
	}

	public override async void _Ready()
	{
		colision = GetNode<CollisionShape2D>("CollisionShape2D");
		this.BodyEntered += OnBodyEntered;
	}

	public async void _Process()
	{
		if (Input.IsActionJustPressed("Attack2"))
		{
			
		}
		
	}



	public async void Attack(Moblin target)
	{
		WeaponSprite.Show();
		//make attack

		//target.Health -= Weapondamage;
		await GlobalFunc.Instance.WaitForSeconds(1);
		WeaponSprite.Hide();
	}

	public void OnBodyEntered(Node body) 
	{
		GD.Print("entererd body");

		if (GlobalVar.Instance.GameEnd)
		{

		}else if (body is Enemy colider)
		{
			GD.Print("enemy colider");
			int collisionLayer = (int)colider.CollisionLayer;

				if ((collisionLayer & (1 << 1)) != 0) 
				{
					GD.Print("layer 2");
					colider.TakeDamage(100);
				}
				else GD.Print("different layer");
		}
	}}