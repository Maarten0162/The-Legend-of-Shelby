using Godot;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public partial class GlobalFunc : Node
{
    // Declare variables

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


    //
    //
    //make a enum saved way, so it will display either local or external, as a good way to differenciate where the file is from.
    //
    //
    //

    // Function to save the game locally
    public async Task SaveGameLocally()
    {
        string directoryPath = "user://saves";

        if (!Directory.Exists(ProjectSettings.GlobalizePath(directoryPath)))
        {
            Directory.CreateDirectory(ProjectSettings.GlobalizePath(directoryPath));
        }

        var file = Godot.FileAccess.Open(GlobalVar.Instance.savePath, Godot.FileAccess.ModeFlags.Write);

        Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary
        {
            ["health"] = GlobalVar.Instance.playerHealth,
            ["playerPosition"] = GlobalVar.Instance.playerPos
        };

        file.StoreVar(saveData);
        file.Close();

        GD.Print("Game saved locally!");
    }

    // Function to upload the saved game to the server
    public async Task SaveGameToServer()
    {
        HttpRequest httpRequest = new HttpRequest();  // Create an HttpRequest to upload the file
        AddChild(httpRequest);

        string authHeader = "Basic " + System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{GlobalVar.Instance.username}:{GlobalVar.Instance.password}"));
        string[] headers =
        {
            "Authorization: " + authHeader
        };

        // Read the saved file into a byte array
        byte[] saveFileBytes = File.ReadAllBytes(ProjectSettings.GlobalizePath(GlobalVar.Instance.savePath));

        string fileName = "savefile.save";  // Keep the same file name or change if needed

        // Perform PUT request (upload the file to WebDAV)
        Error err = httpRequest.RequestRaw(GlobalVar.Instance.webDavUrl + fileName, headers, HttpClient.Method.Put, saveFileBytes);

        if (err != Error.Ok)
        {
            GD.Print("Error sending request: " + err);
            return;
        }

        // Wait for the request to complete
        await ToSignal(httpRequest, "request_completed");

        GD.Print("Game saved to Nextcloud!");
    }

    public Vector2 LoadGame()
    {
        if (Godot.FileAccess.FileExists(GlobalVar.Instance.savePath))
        {
            var file = Godot.FileAccess.Open(GlobalVar.Instance.savePath, Godot.FileAccess.ModeFlags.Read);
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
