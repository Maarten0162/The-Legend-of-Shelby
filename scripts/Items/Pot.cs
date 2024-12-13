using Godot;
using System;
using System.Runtime.Serialization;

public partial class Pot : Node
{   PackedScene key = GD.Load<PackedScene>("res://scenes/Items/key.tscn");
    Node2D thisarea;
    public async void Hit(Area2D body){
            thisarea = GetNode<Node2D>("Node2D");           
            Node jaja =  key.Instantiate();
            
            if(jaja is Key ku){
                
                ku.Position = thisarea.GlobalPosition;
                GetParent().AddChild(ku);
                ku.Keytype = "Green";
                
            }
            
         
           
            QueueFree();
        
    }
}
