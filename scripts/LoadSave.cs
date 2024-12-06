using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

public partial class LoadSave : Node
{

	int testvar1;
	int testvar2;
	public string savePath = "user://savefile.save";
	public static LoadSave Instance {get; private set;}
	// Called when the node enters the scene tree for the first time.

	Player player;
	public override void _Ready()
	{
		testvar1 = 1;
		testvar2 = 3;
		        // Initialize the singleton instance when the node is ready
        if (Instance == null) 
        {
            Instance = this; // Set the instance to this object
        }

		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		GD.Print(testvar1);
	}
	public async Task WaitForSeconds(float count)
	{	
		await ToSignal(GetTree().CreateTimer(count), "timeout");
	}

	public void SaveGame() {
		var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Write);
		// file.StoreVar();
		file.StoreVar(testvar1);
		file.StoreVar(testvar2);
	}

	public void LoadGame() {
		if (FileAccess.FileExists(savePath)) {
			var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Read);
			testvar1 = (int)file.GetVar();
		}
		else GD.Print("No save file exists");
		
	}
}