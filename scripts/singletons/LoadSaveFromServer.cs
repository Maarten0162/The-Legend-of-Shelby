using Godot;
using System;
using System.Text;

public partial class LoadSaveFromServer : Node
{



    private HttpRequest httpRequest;
    
	public static LoadSaveFromServer Instance { get; private set; }
    public override void _Ready()
    {

		if (Instance == null)
        {
            Instance = this;
        }
        // Create an HttpRequest instance
        httpRequest = new HttpRequest();
        AddChild(httpRequest); // Add it as a child to the node tree

        // Connect the signal for when the request is complete
        httpRequest.RequestCompleted += OnRequestCompleted;
    }
	
    public void StartDownload()
    {
        // Create the headers for the request (if needed)
        string[] headers = new string[]
        {
            "Authorization: Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{GlobalVar.Instance.username}:{GlobalVar.Instance.password}")) // Basic auth
        };

        // Send the HTTP GET request
        Error err = httpRequest.Request(ProjectSettings.GlobalizePath(GlobalVar.Instance.davSavePath), headers, HttpClient.Method.Get);
		
        if (err != Error.Ok)
        {
            GD.Print("Error sending request: " + err);
        }
        else
        {
            GD.Print("Download started...");
        }
    }

    private void OnRequestCompleted(long result, long responseCode, string[] responseHeaders, byte[] responseBody)
    {
        // Handle the response after the request is completed
        if (responseCode == 200) // Successful HTTP request (OK)
        {
            GD.Print("Download successful!");

            // Save the file locally using FileAccess
            var file = FileAccess.Open(GlobalVar.Instance.savePath, FileAccess.ModeFlags.Write);
            if (file != null)
            {
                file.StoreBuffer(responseBody);
                file.Close();
                GD.Print("File saved locally to: " + GlobalVar.Instance.savePath);
            }
            else
            {
                GD.Print("Failed to open file for writing: " + GlobalVar.Instance.savePath);
            }
        }
        else
        {
            GD.Print($"Failed to download file. HTTP Response Code: {responseCode}");
        }
    }
}

