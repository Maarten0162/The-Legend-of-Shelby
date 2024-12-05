using Godot;
using System;
using System.Threading.Tasks;

public partial class Main : Node2D
{
	Player player;
	

	Label posLabel;
	public override  void _Ready()
	{
		player = GetNode<Player>("/root/Node2D/Y-Sort/Player");
		posLabel = GetNode<Label>("CanvasLayer/Label");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{				posLabel.Text = "position: " + player.GlobalPosition;
	GD.Print("main loop");

	}

	
}
