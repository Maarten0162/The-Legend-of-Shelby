using Godot;
using System;

public partial class Shop : Node
{
    public void item1entered(Node body){
        if(GlobalVar.Instance.playerCurrency >= 5){
            GlobalVar.Instance.playerCurrency -= 5;
            GlobalVar.Instance.playerHealth += 1;
            GetNode<Area2D>("Item1").QueueFree();
        }
    }
     public void item2entered(Node body){
          if(GlobalVar.Instance.playerCurrency >= 5){
            GlobalVar.Instance.playerHealth += 1;
             GlobalVar.Instance.playerCurrency -= 5;
            GetNode<Area2D>("Item2").QueueFree();
        }
        
    }
     public void item3entered(Node body){
          if(GlobalVar.Instance.playerCurrency >= 5){
            GlobalVar.Instance.playerHealth += 1;
             GlobalVar.Instance.playerCurrency -= 5;
           GetNode<Area2D>("Item3").QueueFree();;
        }
        
    }
}
