using Godot;
using System;

public partial class WeaponPickup : Node
{
    Player player;

    public override void _Ready()
	{
        player = GetNode<Player>("../Y-Sort/Player/");
    }
    private async void Obtain(Node body){
        GlobalVar.Instance.HasSword = true;
        PackedScene SwordScenes = (PackedScene)GD.Load("res://scenes/weapons/sword.tscn");
        Node sceneInstance = SwordScenes.Instantiate();
        player.AddChild(sceneInstance);
        Weapon sword = GetNode<Weapon>("../Y-Sort/Player/weapon");
        sword.Hide();
        GetNode<Area2D>("weapon").Hide();
        GetNode<Label>("../Y-Sort/Player/Label").Text = "You found a sword!";
        await GlobalFunc.Instance.WaitForSeconds(2);
         GetNode<Label>("../Y-Sort/Player/Label").Text = "";
        await GlobalFunc.Instance.WaitForSeconds(0.05f);
        QueueFree();
    }
}
