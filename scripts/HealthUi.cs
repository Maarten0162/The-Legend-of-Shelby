using Godot;
using System;

public partial class HealthUi : TextureRect
{
	Player player;
	TextureRect HealthBar;
	public override void _Ready()
	{


		
		UpdateHealth();
		this.Show();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateHealth();
	}
	void UpdateHealth()
	{
		switch (GlobalVar.Instance.playerHealth)
		{
			case 6:
				Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/Full Health.png");
				break;
			case 5:
				Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/5Hearts.png");
				break;
			case 4:
				Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/4Hearts.png");
				break;
			case 3:
				Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/3Hearts.png");
				break;
			case 2:
				Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/2Hearts.png");
				break;
			case 1:
				Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/1Hearts.png");
				break;
			case 0:
				Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/0Hearts.png");
				break;
		}

	}
}
