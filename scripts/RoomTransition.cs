using Godot;
using System;
using System.Threading.Tasks;

public partial class RoomTransition : Area2D
{
	private Node _instance;
    private Node _instance2;
	[Export] public string Target; // Path to the next room/scene
	string exit;
	AnimationPlayer aniPlayer;
    ColorRect aniRect;
    CanvasLayer ui;

   
	public override void _Ready()
	{
		ui = GetNode<CanvasLayer>("../CanvasLayer");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_body_entered(Node body)
	{	
		GlobalVar.Instance.exit = GetNode<RoomTransition>(".").Name;
		


		CallDeferred(nameof(ChangeScene));
	}


	private async void ChangeScene()
    {
        GD.Print(GlobalVar.Instance.exit + " was the exit used");
        await sceneTransition();


        GetTree().ChangeSceneToFile($"res://scenes/rooms/{Target}.tscn"); // Change to the target scene
        await GlobalFunc.Instance.WaitForSeconds(0.5f);
        
        

    }

    private async Task sceneTransition()
    {
        
        if (_instance == null)
        {
            PackedScene TransitionScreen = GD.Load<PackedScene>("res://scenes/menus/transition.tscn");
            _instance = TransitionScreen.Instantiate();
            AddChild(_instance); // Add it to the scene tree
            aniPlayer = GetNode<AnimationPlayer>("TransitionCanvasLayer/ColorRect/AnimationPlayer");
            aniRect = GetNode<ColorRect>("../TransitionCanvasLayer/ColorRect");
            ui.Hide();
            if (GlobalVar.Instance.exit.Contains("north")) // switch voor zuid
			{
                aniPlayer.PlayBackwards("swipe_down");
            }
            else if (GlobalVar.Instance.exit.Contains("south")) // switch voor zuid
			{
                aniPlayer.PlayBackwards("swipe_up");
            }
            else if (GlobalVar.Instance.exit.Contains("east")) // switch voor zuid
			{
                aniPlayer.PlayBackwards("swipe_left");
            }
            else if (GlobalVar.Instance.exit.Contains("west")) // switch voor zuid
			{
                aniPlayer.PlayBackwards("swipe_right");
            }
            else switch (GlobalVar.Instance.exit)
				{
					case "Dungeon_1":
						aniPlayer.PlayBackwards("swipe_up");				
						break;
					case "Room1Treestump":
						aniPlayer.PlayBackwards("swipe_down");
						break;
				}
            await GlobalFunc.Instance.WaitForSeconds(0.25f);
        }
        else
        {
            AddChild(_instance); // Add it to the scene tree
            aniPlayer = GetNode<AnimationPlayer>("TransitionCanvasLayer/ColorRect/AnimationPlayer");
            aniPlayer.PlayBackwards("swipe_up");
            await GlobalFunc.Instance.WaitForSeconds(0.5f);
        }
    }
}

