using Godot;
using System;

public partial class finalcutscene : Area2D
{
	AnimationPlayer aniPlayerMove;
	AnimationPlayer aniFadeOut;
	// Called when the node enters the scene tree for the first time.

	public override async void _Ready()
    {
       aniPlayerMove = GetNode<AnimationPlayer>("../Y-Sort/CharacterBody2D/AnimationPlayer");
	   aniFadeOut = GetNode<AnimationPlayer>("../CanvasLayer2/ColorRect/AnimationPlayer");


    }
	private async void PlayMoveAni(Node body)
	{
        
        aniPlayerMove.Play("ani_end");
    }

	private async void PLayFadeOut(Node body)
	{
        
        aniFadeOut.Play("fade_out");
    }
}
