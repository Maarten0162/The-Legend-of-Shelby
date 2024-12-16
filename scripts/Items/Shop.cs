using Godot;
using System;

public partial class Shop : Node
{
    public void item1entered(Node body)
    {
        if (GlobalVar.Instance.playerCurrency >= 25)
        {

            GlobalVar.Instance.HasBow = true;
            GlobalVar.Instance.playerCurrency -= 25;
            GetNode<Area2D>("Item1").QueueFree();
        }
    }
    public void item2entered(Node body)
    {
        if (GlobalVar.Instance.playerCurrency >= 5)
        {
            GlobalVar.Instance.playerHealth += 2;
            if (GlobalVar.Instance.HealthUpgrade && GlobalVar.Instance.playerHealth > 8)
            {
                GlobalVar.Instance.playerHealth = 8;
            }
            else if (!GlobalVar.Instance.HealthUpgrade && GlobalVar.Instance.playerHealth > 6)
            {
                GlobalVar.Instance.playerHealth = 6;
            }
            GlobalVar.Instance.playerCurrency -= 5;
            GetNode<Area2D>("Item2").QueueFree(); ;
        }


    }
    public void item3entered(Node body)
    {
        if (GlobalVar.Instance.playerCurrency >= 5)
        {
            GlobalVar.Instance.playerHealth += 2;
            if (GlobalVar.Instance.HealthUpgrade && GlobalVar.Instance.playerHealth > 8)
            {
                GlobalVar.Instance.playerHealth = 8;
            }
            else if (!GlobalVar.Instance.HealthUpgrade && GlobalVar.Instance.playerHealth > 6)
            {
                GlobalVar.Instance.playerHealth = 6;
            }
            GlobalVar.Instance.playerCurrency -= 5;
            GetNode<Area2D>("Item3").QueueFree(); ;
        }

    }
}
