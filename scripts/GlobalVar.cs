using Godot;
using System;

public partial class GlobalVar : Node
{

	public static GlobalVar Instance {get; private set;}

	public Vector2 playerPos{ get; set; }
	public int playerHealth{ get; set; }


	//settings
	public int Volume{ get; set; }
	public Vector2I Resolution{ get; set; }


	public override void _Ready()
	{
		if (Instance == null)
        {
            Instance = this; 
        }

		Volume = 100;
		// Resolution = new Vector2I(1920, 1080);//settings get saved with the otherstuff, make a separate file
		// DisplayServer.WindowSetSize(Resolution);
		DisplayServer.WindowSetSize(new Vector2I(1152, 648));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
