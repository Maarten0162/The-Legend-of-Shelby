using Godot;
using System;
using System.Threading.Tasks;

public partial class Main : Node2D
{
	Player player;


	Label posLabel;

	public override void _Ready()
	{
		posLabel = GetNode<Label>("CanvasLayer/Label");
		player = GetNode<Player>("Y-Sort/Player");






		if (GlobalVar.Instance.exit != null)
		{
			if (GlobalVar.Instance.exit.Contains("north")) // switch voor zuid
			{
				switch (GlobalVar.Instance.exit)
				{
					case "Room1north":
						player.GlobalPosition = GetNode<Area2D>("Room2south").GlobalPosition;
						break;
						case "Room2north":
						player.GlobalPosition = GetNode<Area2D>("Room1Treestump").GlobalPosition;
						break;

				}

			}
			if (GlobalVar.Instance.exit.Contains("south")) // switch voor north
			{
				switch (GlobalVar.Instance.exit)
				{
					case "Room2south":
						player.GlobalPosition = GetNode<Area2D>("Room1north").GlobalPosition;
						break;

				}
				if (GlobalVar.Instance.exit.Contains("west")) // switch voor east
				{
					switch (GlobalVar.Instance.exit)
					{
						case "Room2west":
							player.GlobalPosition = GetNode<Area2D>("Room2east2").GlobalPosition;
							break;
						

					}

				}
				if (GlobalVar.Instance.exit.Contains("east")) // switch voor west
				{
					switch (GlobalVar.Instance.exit)
					{
						case "Room2east1":
							player.GlobalPosition = GetNode<Area2D>("Room2west").GlobalPosition;
							break;
							case "Room2east2":
							player.GlobalPosition = GetNode<Area2D>("Room2west").GlobalPosition;
							break;


					}

				}
			}


		}
		else
		{
			player.GlobalPosition = new Vector2(100, 600);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		posLabel.Text = "position: " + player.GlobalPosition;


	}

}
