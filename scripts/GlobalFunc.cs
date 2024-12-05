using Godot;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public partial class GlobalFunc : Node
{
    // Declare variables
    public string savePath = "user://saves/savefile.save";  // Local file path (you can keep this to create the file)
    private string encryptionCode = "AKLE69";

    private string webDavUrl = "http://192.168.128.149/nextcloud/remote.php/dav/files/admin/GameSaves/";
    private string username = "admin";
    private string password = "Welkom123!";

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

    public async void SaveGame()
    {
        string directoryPath = "user://saves";

        // Ensure the directory exists
        if (!Directory.Exists(ProjectSettings.GlobalizePath(directoryPath)))
        {
            Directory.CreateDirectory(ProjectSettings.GlobalizePath(directoryPath));
        }

        // Write save data to the file locally first
        var file = Godot.FileAccess.OpenEncryptedWithPass(savePath, Godot.FileAccess.ModeFlags.Write, encryptionCode);

        Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary
        {
            ["health"] = GlobalVar.Instance.playerHealth,
            ["playerPosition"] = GlobalVar.Instance.playerPos
        };

        file.StoreVar(saveData);
        file.Close();

        // Now upload the save file to Nextcloud via WebDAV
        HttpRequest httpRequest = new HttpRequest();  // Corrected to HttpRequest
        AddChild(httpRequest);

        string authHeader = "Basic " + System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        string[] headers =
        {
            "Authorization: " + authHeader
        };

        // Read the saved file into a byte array
        byte[] saveFileBytes = File.ReadAllBytes(ProjectSettings.GlobalizePath(savePath));

        string fileName = "savefile.save";  // Keep the same file name or change if needed

        // Perform PUT request (upload the file to WebDAV)
        // Use RequestRaw instead of Request to send the byte[] directly
        Error err = httpRequest.RequestRaw(webDavUrl + fileName, headers, HttpClient.Method.Put, saveFileBytes);

        if (err != Error.Ok)
        {
            GD.Print("Error sending request: " + err);
            return;
        }

        // Wait for the request to complete
        await ToSignal(httpRequest, "request_completed");

        GD.Print("Game saved to Nextcloud!");
    }
}
