using Godot;
using System;
using System.Collections;

public partial class DeathScreen : Node2D
{

	
	int i = 0;
	private bool isSceneChanging = false;

    private PackedScene mainScene;
    private PackedScene mainMenuScene;
    private const string MainScenePath = "res://scenes/main.tscn";
    private const string MainMenuScenePath = "res://scenes/Main Menu.tscn";
    private int frameCounter = 0;

    public override void _Ready()
    {      
       

        // Ensure scenes exist before loading
        if (ResourceLoader.Exists(MainScenePath))
        {
            mainScene = (PackedScene)ResourceLoader.Load(MainScenePath);
            GD.Print("Main scene loaded successfully.");
        }
        else
        {
            GD.PrintErr($"Main scene not found at {MainScenePath}!");
        }

        if (ResourceLoader.Exists(MainMenuScenePath))
        {
            mainMenuScene = (PackedScene)ResourceLoader.Load(MainMenuScenePath);
            GD.Print("Main menu scene loaded successfully.");
        }
        else
        {
            GD.PrintErr($"Main menu scene not found at {MainMenuScenePath}!");
        }
       
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
{   GD.Print("in loop" + i);


    if (Input.IsActionJustPressed("Yes"))
    {
        GD.Print("Yes pressed - Changing to main scene...");
        isSceneChanging = true;
        GetTree().ChangeSceneToPacked(mainScene);
    }
    else if (Input.IsActionJustPressed("No"))
    {
        GD.Print("No pressed - Changing to main menu scene...");
        isSceneChanging = true;
       
        GetTree().ChangeSceneToPacked(mainMenuScene);
    }
    i++;
 
}


	

}
