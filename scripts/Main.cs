using Godot;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public partial class Main : Node2D
{
	Player player;
	string thisenemy;


	Label posLabel;

	CanvasLayer ui;
	PackedScene settingsScene = (PackedScene)GD.Load("res://scenes/menus/pause_menu.tscn");
	AnimationPlayer aniPlayer;
	ColorRect aniRect;
	PackedScene transitionScene = (PackedScene)GD.Load("res://scenes/menus/transition.tscn");

	public override async void _Ready()
	{
		ui = GetNode<CanvasLayer>("CanvasLayer");
		posLabel = GetNode<Label>("CanvasLayer/Position");
		player = GetNode<Player>("Y-Sort/Player");
		GlobalVar.Instance.roomPath = this.SceneFilePath;
		SoundFx.instance.changeMusic("Overworld");
		

		Node transitionSceneInstance = transitionScene.Instantiate();
            
		// Add the instance as a child of the current node
		AddChild(transitionSceneInstance);	
		
		aniPlayer = GetNode<AnimationPlayer>("TransitionCanvasLayer/ColorRect/AnimationPlayer");
		aniRect = GetNode<ColorRect>("TransitionCanvasLayer/ColorRect");
		
		aniRect.Show();
	





		
		Node settingsSceneInstance = settingsScene.Instantiate();
            
            // Add the instance as a child of the current node
        AddChild(settingsSceneInstance);
		
		





		if (GlobalVar.Instance.exit != null)
		{
			
			if (GlobalVar.Instance.exit.Contains("north")) // switch voor zuid
			{
				
				

				GD.Print("in north");
				switch (GlobalVar.Instance.exit)
				{
					case "Room1north":
						player.GlobalPosition = GetNode<Area2D>("Room2south").GlobalPosition;
						aniPlayer.Play("swipe_up");
            			await GlobalFunc.Instance.WaitForSeconds(0.25f);
						break;
					

				}
				
				

			}
			if (GlobalVar.Instance.exit.Contains("south"))
			{ // switch voor north
				
				switch (GlobalVar.Instance.exit)
				{
					case "Room2south":
						player.GlobalPosition = GetNode<Area2D>("Room1north").GlobalPosition;
						aniPlayer.Play("swipe_down");
            			await GlobalFunc.Instance.WaitForSeconds(0.25f);
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
						aniPlayer.Play("swipe_left");
            			await GlobalFunc.Instance.WaitForSeconds(0.25f);
						break;
					case "Room3west1":
						player.GlobalPosition = GetNode<Area2D>("Room2east1").GlobalPosition;
						aniPlayer.Play("swipe_left");
            			await GlobalFunc.Instance.WaitForSeconds(0.25f);
						break;
					case "Room3west2":
						player.GlobalPosition = GetNode<Area2D>("Room2east2").GlobalPosition;
						aniPlayer.Play("swipe_left");
            			await GlobalFunc.Instance.WaitForSeconds(0.25f);
						break;


				}

			}
			if (GlobalVar.Instance.exit.Contains("east")) // switch voor west
			{
				aniPlayer.Play("swipe_right");
            	await GlobalFunc.Instance.WaitForSeconds(0.25f);
				GD.Print("in east");
				switch (GlobalVar.Instance.exit)
				{
					case "Room2east1":
						player.GlobalPosition = GetNode<Area2D>("Room3west1").GlobalPosition;
						aniPlayer.Play("swipe_right");
            			await GlobalFunc.Instance.WaitForSeconds(0.25f);
						GD.Print("in room2 east 1");
						break;
					case "Room2east2":
						player.GlobalPosition = GetNode<Area2D>("Room3west2").GlobalPosition;
						player.GlobalPosition = GetNode<Area2D>("Room3west1").GlobalPosition;
						aniPlayer.Play("swipe_right");
						GD.Print("in room2 east 1");
						break;


				}

			}
			else switch (GlobalVar.Instance.exit)
				{
					case "Dungeon_1":
						player.GlobalPosition = GetNode<Area2D>("Room1Treestump").GlobalPosition;
						aniPlayer.Play("swipe_up");
            			await GlobalFunc.Instance.WaitForSeconds(0.25f);					
						break;
					case "Room1Treestump":
						player.GlobalPosition = GetNode<Area2D>("Dungeon_1").GlobalPosition;
						aniPlayer.Play("swipe_down");
            			await GlobalFunc.Instance.WaitForSeconds(0.25f);
						break;
				}



		}
		else
		{
			player.GlobalPosition = new Vector2(100, 600);
		}
		aniRect.Hide();
		ui.Show();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		posLabel.Text = "position: " + player.GlobalPosition;


	}

}
