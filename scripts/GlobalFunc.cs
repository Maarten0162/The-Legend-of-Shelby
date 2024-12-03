using Godot;
using System;
using System.Threading.Tasks;

public partial class GlobalFunc : Node
{
    // Declare variables
    public string savePath = "user://saves/savefile.save";//ERROR ALS DIE FUCKING FILE PATH NIET BESTAAT!!!! 	C:\Users\maart\AppData\Roaming\Godot\app_userdata\The-Legend-of-Shelby\saves


    public static GlobalFunc Instance { get; private set; }

    private Godot.Collections.Dictionary savedData = new Godot.Collections.Dictionary();

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

	public async Task WaitForSeconds(float count) {
		await ToSignal(GetTree().CreateTimer(count), "timeout");
	}

    public override void _Process(double delta)
    {
        
    }

	

    public void SaveGame()
    {
        var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Write);
        Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary();
        saveData["health"] = GlobalVar.Instance.playerHealth;
		saveData["playerPosition"] = GlobalVar.Instance.playerPos;
  
        file.StoreVar(saveData);
        file.Close();

        GD.Print("Game saved!");
    }

    public Vector2 LoadGame()
    {
        if (FileAccess.FileExists(savePath))
        {
            var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Read);
            Godot.Collections.Dictionary loadedData = (Godot.Collections.Dictionary)file.GetVar();
            file.Close();
            GlobalVar.Instance.playerHealth = (int)loadedData["health"];
			GlobalVar.Instance.playerPos = (Vector2)loadedData["playerPosition"];

            GD.Print("Game loaded!");
            GD.Print("health: " + GlobalVar.Instance.playerHealth);
            GD.Print("testvar2: ");
			GD.Print(GlobalVar.Instance.playerHealth);
			GD.Print(GlobalVar.Instance.playerPos);
			return GlobalVar.Instance.playerPos;
        }
        else
        {
            GD.Print("No save file exists");
			GlobalVar.Instance.playerHealth = 6;
			GlobalVar.Instance.playerPos = Vector2.Zero;
			return Vector2.Zero;
        }
    }
}
