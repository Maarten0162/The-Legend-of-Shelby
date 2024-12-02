using Godot;
using System;

public partial class GlobalVar : Node
{

	public static GlobalVar Instance {get; private set;}

	public Vector2 playerPos{ get; set; }
	public int playerHealth{ get; set; }

	public override void _Ready()
	{
		if (Instance == null)
        {
            Instance = this; 
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
