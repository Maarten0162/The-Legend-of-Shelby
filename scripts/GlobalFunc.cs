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

    public void SaveGame()
    {

        string directoryPath = "user://saves";

        // Ensure the directory exists
        if (!Directory.Exists(ProjectSettings.GlobalizePath(directoryPath)))
        {
            Directory.CreateDirectory(ProjectSettings.GlobalizePath(directoryPath));
        }

        var file = Godot.FileAccess.Open(savePath, Godot.FileAccess.ModeFlags.Write);

        Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary
        {
            ["health"] = GlobalVar.Instance.playerHealth,
            ["playerPosition"] = GlobalVar.Instance.playerPos
        };

        file.StoreVar(saveData);
        file.Close();

        GD.Print("Game saved!");
    }

    public async void SaveGameToServer()
    {
        string directoryPath = "user://saves";

        // Ensure the directory exists
        if (!Directory.Exists(ProjectSettings.GlobalizePath(directoryPath)))
        {
            Directory.CreateDirectory(ProjectSettings.GlobalizePath(directoryPath));
        }

        // Write save data to the file locally first
        var file = Godot.FileAccess.Open(savePath, Godot.FileAccess.ModeFlags.Write);

        Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary
        {
            ["health"] = GlobalVar.Instance.playerHealth,
            ["playerPosition"] = GlobalVar.Instance.playerPos
        };

        file.StoreVar(saveData);
        file.Close();

        // Now upload the save file to Nextcloud via WebDAV
        HttpRequest httpRequest = new HttpRequest();  // Corrected to HTTPRequest
        AddChild(httpRequest);

        string authHeader = "Basic " + System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        string[] headers =
        {
            "Authorization: " + authHeader
        };

        // Convert byte array to Array<byte>
        byte[] saveFileBytes = File.ReadAllBytes(ProjectSettings.GlobalizePath(savePath));

        // Creating Array<byte>
        Godot.Collections.Array<byte> arraySaveFileBytes = new Godot.Collections.Array<byte>(saveFileBytes);

        string fileName = "savefile.save";  // Keep the same file name or change if needed

        // Perform PUT request (upload the file to WebDAV)
        Error err = httpRequest.Request(webDavUrl + fileName, headers, HttpClient.Method.Put, "PUT");

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
        if (Godot.FileAccess.FileExists(savePath))
        {
            var file = Godot.FileAccess.Open(savePath, Godot.FileAccess.ModeFlags.Read);
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

public async void LoadGameFromServer()
{

    savePath = "user://saves/test/savefile.save";
    // Create the HttpRequest instance
    HttpRequest httpRequest = new HttpRequest();
    AddChild(httpRequest); // Add it as a child to the node tree

    // Setup the authentication header
    string authHeader = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
    string[] headers = { "Authorization: " + authHeader };

    string fileName = "savefile.save";  // The name of the file to download
    string url = webDavUrl + fileName;

    // Request the save file from Nextcloud (GET method)
    Error err = httpRequest.Request(url, headers, HttpClient.Method.Get);

    if (err != Error.Ok)
    {
        GD.Print("Error sending request: " + err);
        return;
    }

    // Wait for the request to complete asynchronously
    var signalArgs = await ToSignal(httpRequest, "request_completed");

    // Handle the request completion
    int result = (int)signalArgs[0];
    int responseCode = (int)signalArgs[1];
    string responseHeaders = (string)signalArgs[2];
    byte[] responseBody = (byte[])signalArgs[3];

    
    GD.Print("Requesting URL: " + url);
    GD.Print("Authorization Header: " + authHeader);



    // Check if the request was successful
    if (responseCode == 200)
    {
        // Save the file locally
        GD.Print("Response Body: " + Encoding.UTF8.GetString(responseBody));

        var file = Godot.FileAccess.Open(savePath, Godot.FileAccess.ModeFlags.Write);
        file.StoreBuffer(responseBody);
        file.Close();
    
        GD.Print("File downloaded and saved locally!");

        // Now read the local file and load the game
        LoadGame();
    }
    else
    {
        GD.Print($"Failed to download the save file. Response Code: {responseCode}");
    }
}

    // Process the response body or other parameters as needed
}