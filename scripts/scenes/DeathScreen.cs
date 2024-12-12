using Godot;
using System;
using System.Collections;

public partial class DeathScreen : Node2D
{




    private PackedScene mainScene;
    private PackedScene mainMenuScene;
    private const string MainScenePath = "res://scenes/rooms/Room_01.tscn";
    private const string MainMenuScenePath = "res://scenes/menus/Main Menu.tscn";

    public override void _Ready()
    {
        mainScene = (PackedScene)ResourceLoader.Load(MainScenePath);
        mainMenuScene = (PackedScene)ResourceLoader.Load(MainMenuScenePath);
        GlobalVar.Instance.playerHealth = null;


    }

    public override void _Process(double delta)
    {


        if (Input.IsActionJustPressed("Yes"))
        {
            GD.Print("Yes pressed - Changing to main scene...");
         

            GetTree().ChangeSceneToPacked(mainScene);
        }
        else if (Input.IsActionJustPressed("No"))
        {
            GD.Print("No pressed - Changing to main menu scene...");
           

            GetTree().ChangeSceneToPacked(mainMenuScene);
        }
        

    }




}
