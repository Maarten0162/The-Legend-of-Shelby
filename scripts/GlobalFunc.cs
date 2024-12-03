using Godot;
using System;
using System.Threading.Tasks;
using System.IO;

public partial class GlobalFunc : Node
{
    // Declare variables
    public string savePath = "user://saves/savefile.save";
    private string encryptionCode = "AKLE69";

    public static GlobalFunc Instance { get; private set; }

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public async Task WaitForSeconds(float count)
    {
        await ToSignal(GetTree().CreateTimer(count), "timeout");
    }

    public override void _Process(double delta)
    {
    }

    public void SaveGame()
    {
        // Get the directory path
        string directoryPath = "user://saves";

        // Ensure the directory exists
        if (!Directory.Exists(ProjectSettings.GlobalizePath(directoryPath)))
        {
            Directory.CreateDirectory(ProjectSettings.GlobalizePath(directoryPath));
        }

        // Open the file for writing
        var file = Godot.FileAccess.OpenEncryptedWithPass(savePath, Godot.FileAccess.ModeFlags.Write, encryptionCode);

        // Save data
        Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary
        {
            ["health"] = GlobalVar.Instance.playerHealth,
            ["playerPosition"] = GlobalVar.Instance.playerPos
        };

        file.StoreVar(saveData);
        file.Close();

        GD.Print("Game saved!");
    }

    public Vector2 LoadGame()
    {
        if (Godot.FileAccess.FileExists(savePath))
        {
            var file = Godot.FileAccess.OpenEncryptedWithPass(savePath, Godot.FileAccess.ModeFlags.Read, encryptionCode);
            Godot.Collections.Dictionary loadedData = (Godot.Collections.Dictionary)file.GetVar();
            file.Close();

            GlobalVar.Instance.playerHealth = (int)loadedData["health"];
            GlobalVar.Instance.playerPos = (Vector2)loadedData["playerPosition"];

            GD.Print("Game loaded!");
            GD.Print("health: " + GlobalVar.Instance.playerHealth);
            GD.Print("playerPosition: " + GlobalVar.Instance.playerPos);

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
