using Godot;
using System;

public partial class Transition : ColorRect
{
	private Node transitionInstance;
	AnimationPlayer player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// if (transitionInstance == null || !transitionInstance.IsInsideTree())
        // {
        //     PackedScene TransitionScreen = GD.Load<PackedScene>("res://scenes/menus/transition.tscn");
        //     transitionInstance = TransitionScreen.Instantiate();
        //     AddChild(_instance); // Add it to the scene tree
        // }
		player = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Attack"))
		{
			swipeLeft();
		}
	}

	public void swipeLeft()
	{
		player.Play("swipe_left");
	}

	public void swipeRight()
	{
		player.PlayBackwards("swipe_right");
	}
	public void swipeUp()
	{
		player.Play("swipe_up");
	}
	public void swipeDown()
	{
		player.Play("swipe_down");
	}
}
