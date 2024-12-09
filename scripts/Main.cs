using Godot;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public partial class Main : Node2D
{
	Player player;
	string thisenemy;


	Label posLabel;

	public override void _Ready()
	{
		posLabel = GetNode<Label>("CanvasLayer/Label");
		player = GetNode<Player>("Y-Sort/Player");
		GlobalVar.Instance.roomPath = this.SceneFilePath;
		






		if (GlobalVar.Instance.exit != null)
		{
			if (GlobalVar.Instance.exit.Contains("north")) // switch voor zuid
			{
				GD.Print("in north");
				switch (GlobalVar.Instance.exit)
				{
					case "Room1north":
						player.GlobalPosition = GetNode<Area2D>("Room2south").GlobalPosition;
						break;


				}

			}
			if (GlobalVar.Instance.exit.Contains("south"))
			{ // switch voor north

				switch (GlobalVar.Instance.exit)
				{
					case "Room2south":
						player.GlobalPosition = GetNode<Area2D>("Room1north").GlobalPosition;
						break;

				}
			}
			if (GlobalVar.Instance.exit.Contains("west")) // switch voor east
			{
				GD.Print("in west");
				switch (GlobalVar.Instance.exit)
				{
					case "Room2west1":
						player.GlobalPosition = GetNode<Area2D>("Room2east2").GlobalPosition;
						break;
						case "Room3west1":
						player.GlobalPosition = GetNode<Area2D>("Room2east1").GlobalPosition;
						break;
						case "Room3west2":
						player.GlobalPosition = GetNode<Area2D>("Room2east2").GlobalPosition;
						break;


				}

			}
			if (GlobalVar.Instance.exit.Contains("east")) // switch voor west
			{
				GD.Print("in east");
				switch (GlobalVar.Instance.exit)
				{
					case "Room2east1":
						player.GlobalPosition = GetNode<Area2D>("Room3west1").GlobalPosition;
						GD.Print("in room2 east 1");
						break;
					case "Room2east2":
						player.GlobalPosition = GetNode<Area2D>("Room3west2").GlobalPosition;
						GD.Print("in room2 east 1");
						break;


				}

			}
			else GD.Print("is niks");



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
