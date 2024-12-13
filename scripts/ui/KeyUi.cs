using Godot;
using System;

public partial class KeyUi : HBoxContainer
{

	TextureRect Key1;
	TextureRect Key2;
	TextureRect Key3;
	string keypath = "res://assets/Sprites/";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Key1 = GetNode<TextureRect>("Key1");
		
		Key2 = GetNode<TextureRect>("Key2");
		Key3 = GetNode<TextureRect>("Key3");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(GlobalVar.Instance.HasGreenKey){
			Key1.Show();
			
		}
		if(GlobalVar.Instance.HasRedKey){
			Key2.Show();
			
		}
	}
	
}
