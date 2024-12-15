using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalVar : Node
{

	public static GlobalVar Instance {get; private set;}

	public Vector2 playerPos{ get; set; }
	public int? playerHealth{ get; set; }
	public int playerCurrency{ get; set; }
	public List<string> Keys{ get; set; }
	public string exit{ get; set; }
	public string entrance{ get; set; }
	public string roomPath{ get; set; }
	public bool Hasitemspace{ get; set; }
	public string[] Rupounds;
	public bool HasSword{ get; set; }
	public bool HasBow{ get; set; }
	public bool HasGreenKey{ get; set; }
	public bool OpenendGreenDoor{ get; set; }
	public bool HasRedKey{ get; set; }
	public bool OpenendRedDoor{ get; set; }
	public bool KilledHydra{ get; set; }
	public bool OpenendHydraDoor{ get; set; }
	public bool PuzzleSolved{ get; set; }

	public bool GameEnd{ get; set; }


	//settings
	public int Volume{ get; set; }
	public Vector2I Resolution{ get; set; }

	public string savePath = "user://saves/savefile.save";  // Local file path (you can keep this to create the file)
    public string encryptionCode = "AKLE69";

  	public string webDavUrl = "http://192.168.128.149/nextcloud/remote.php/dav/files/admin/GameSaves/";

	public string davSavePath = "http://192.168.128.149/nextcloud/remote.php/dav/files/admin/GameSaves/savefile.save";
   	public string username = "admin";
   	public string password = "Welkom123!";

	public string MusicLocation = "TitleScreen";


	public override void _Ready()
	{	Keys = new();

		if (Instance == null)
        {
            Instance = this; 
        }
		HasGreenKey = false;
		OpenendGreenDoor = false;
		HasRedKey = false;
		OpenendRedDoor = false;
		KilledHydra = false;
		OpenendHydraDoor = false;
		Volume = 100;
		// Resolution = new Vector2I(1920, 1080);//settings get saved with the otherstuff, make a separate file
		// DisplayServer.WindowSetSize(Resolution);
		DisplayServer.WindowSetSize(new Vector2I(1152, 648));

		Rupounds = new string[]{"small", "medium", "large"};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
