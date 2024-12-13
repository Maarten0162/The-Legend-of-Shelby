using Godot;
using System;

public partial class WeaponPickup : Node
{

    private async void Obtain(Node body){
        if (GlobalVar.Instance.GameEnd)
		{
            
		}else
        {
            GlobalVar.Instance.HasSword = true;
            GetNode<Area2D>("weapon").Hide();
            GetNode<Label>("../Y-Sort/Player/Label").Text = "You found a sword!";
            await GlobalFunc.Instance.WaitForSeconds(2);
            GetNode<Label>("../Y-Sort/Player/Label").Text = "";
            await GlobalFunc.Instance.WaitForSeconds(0.05f);
            QueueFree();
        }
        
    }
}
