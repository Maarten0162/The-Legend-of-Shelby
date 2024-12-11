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

	}
	// private void CheckKey(string type)
	// {
	// 	switch (type)
	// 	{
	// 		case "green":
	// 			GlobalVar.Instance.Hasitemspace = UpdateInv(keypath + "Green.png");
	// 			break;
	// 		case "Red":
	// 			GlobalVar.Instance.Hasitemspace = UpdateInv(keypath + "Red.png");
	// 			break;
	// 		case "Blue":
	// 			GlobalVar.Instance.Hasitemspace = UpdateInv(keypath + "Blue.png");
	// 			break;
	// 		case "Purple":
	// 			GlobalVar.Instance.Hasitemspace = UpdateInv(keypath + "Purple.png");
	// 			break;
	// 	}
	// }
	// private bool UpdateInv(string file)
	// {
	// 	for (int i = 1; i <= 3; i++)
	// 	{
	// 		var keyNode = GetNode<TextureRect>($"Key{i}");
	// 		if (keyNode.Texture == null)
	// 		{
	// 			keyNode.Texture = GD.Load<Texture2D>(file);
	// 			return true; ;
	// 		}
	// 	}
		
	// 	return false;
		

	// }
}
