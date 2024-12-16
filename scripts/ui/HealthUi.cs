using Godot;
using System;

public partial class HealthUi : TextureRect
{


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
		if (GlobalVar.Instance.HealthUpgrade)
		{
			switch (GlobalVar.Instance.playerHealth)
			{
				case 8:
					Texture = (Texture2D)GD.Load("res://assets/Hearts/hearthsUpgrade/extraHeart8Hearts.png");
					break;
				case 7:
					Texture = (Texture2D)GD.Load("res://assets/Hearts/hearthsUpgrade/extraHeart7Hearts.png");
					break;
				case 6:
					Texture = (Texture2D)GD.Load("res://assets/Hearts/hearthsUpgrade/extraHeart6Hearts.png");
					break;
				case 5:
					Texture = (Texture2D)GD.Load("res://assets/Hearts/hearthsUpgrade/extraHeart5Hearts.png");
					break;
				case 4:
					Texture = (Texture2D)GD.Load("res://assets/Hearts/hearthsUpgrade/extraHeart4Hearts.png");
					break;
				case 3:
					Texture = (Texture2D)GD.Load("res://assets/Hearts/hearthsUpgrade/extraHeart3Hearts.png");
					break;
				case 2:
					Texture = (Texture2D)GD.Load("res://assets/Hearts/hearthsUpgrade/extraHeart2Hearts.png");
					break;
				case 1:
					Texture = (Texture2D)GD.Load("res://assets/Hearts/hearthsUpgrade/extraHeart1Hearts.png");
					break;
				case 0:
					Texture = (Texture2D)GD.Load("res://assets/Hearts/hearthsUpgrade/extraHeart0Hearts.png");
					break;
			}
			Size = new Vector2(127.5f, 80);
		}
		else
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
			Size = new Vector2(102, 80);
		}

	}
}
