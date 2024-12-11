using Godot;
using System;

public partial class Pound : Area2D
{   [Export] int RuPoundSize = 3;
	/// <summary>
    /// spawn a pound, the first param is the position, the second param is the value of the pound
    /// </summary>
    /// <param name="position"></param>
    /// <param name="amount"></param>
   public Pound(Vector2 Position, string size)   
   {   
        

        TextureRect poundtexture = new();
        if(size == "small"){
             poundtexture.Texture = (Texture2D)GD.Load("res://assets/Sprites/Bronze_RuPound.png");
             this.amount = 5;
        }
        else if(size == "medium"){
             poundtexture.Texture = (Texture2D)GD.Load("res://assets/Sprites/Green_RuPound.png");
             this.amount = 10;
        }
        else if(size == "large"){
             poundtexture.Texture = (Texture2D)GD.Load("res://assets/Sprites/Purple_RuPound.png");
             this.amount = 20;
        }
       
        AddChild(poundtexture);   
        poundtexture.Size = poundtexture.Texture.GetSize();     
        poundtexture.Size *= RuPoundSize;
        CollisionShape2D hitbox = new();
        RectangleShape2D hitboxshape = new();    
        hitboxshape.Size = poundtexture.GetSize();    
        hitbox.Shape = hitboxshape;
        AddChild(hitbox);
        
        BodyEntered += (Player) => Hit();
        this.GlobalPosition = Position;
        
        
    }
    private int amount;
    private Vector2 position;
    public int Amount
    {
        get
        {
            return amount;
        }

    }
    void Hit()
    {
        GlobalVar.Instance.playerCurrency += Amount;
        QueueFree();
    }

}
