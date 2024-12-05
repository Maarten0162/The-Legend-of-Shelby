using Godot;
using System;

public partial class HealthUi : TextureRect
{	Player player;
	TextureRect HealthBar;
	public override void _Ready()
	{
		
	
		HealthBar = GetNode<TextureRect>(".");
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{Player player = GetNode<Player>("/root/Node2D/Y-Sort/Player");
	
		switch(GlobalVar.Instance.playerHealth){
			case 6:
			HealthBar.Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/Full Health.png");
			break;
			case 5:
			HealthBar.Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/5Hearts.png");
			break;
			case 4:
			HealthBar.Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/4Hearts.png");
			break;
			case 3:
			HealthBar.Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/3Hearts.png");
			break;
			case 2:
			HealthBar.Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/2Hearts.png");
			break;
			case 1:
			HealthBar.Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/1Hearts.png");
			break;
			case 0:
			HealthBar.Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/0Hearts.png");
			break;

		}
	}
}
