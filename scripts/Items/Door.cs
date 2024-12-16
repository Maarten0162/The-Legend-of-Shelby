using Godot;
using System;

public partial class Door : Node
{   [Export]public string Key;

    public void OnBodyEntered(Node body){
        if(GlobalVar.Instance.HasGreenKey && Key == "Green"){
            GlobalVar.Instance.OpenendGreenDoor = true;
            QueueFree();
        }
        if(GlobalVar.Instance.HasRedKey && Key == "Red"){
             GlobalVar.Instance.OpenendRedDoor = true;
            QueueFree();
        }
        if(GlobalVar.Instance.KilledHydra && Key == "Boss"){
             GlobalVar.Instance.OpenendHydraDoor = true;
            QueueFree();
        }
        if(GlobalVar.Instance.PuzzleSolved && Key == "Puzzle"){
             GlobalVar.Instance.Openendpuzzledoor = true;
            QueueFree();
    }
}}
