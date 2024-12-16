using Godot;
using System;

public partial class puzzle : Area2D
{
    public void OnBodyEntered(Node body)
    {

        if (body is RigidBody2D body2)
        {
            GlobalVar.Instance.PuzzleSolved = true;
        }
    }
}

