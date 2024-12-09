using Godot;
using System;

public partial class Pound : Area2D
{
	/// <summary>
    /// spawn a pound, the first param is the position, the second param is the value of the pound
    /// </summary>
    /// <param name="position"></param>
    /// <param name="amount"></param>
   public Pound(Vector2 Position, int amount)   
   {   
        
        SetCollisionLayerValue(1, false);
        SetCollisionLayerValue(5, true);
        TextureRect poundtexture = new();
        poundtexture.Texture = (Texture2D)GD.Load("res://assets/Hearts/PNG/basic/0Hearts.png");
        AddChild(poundtexture);
        CollisionShape2D hitbox = new();
        RectangleShape2D hitboxshape = new();    
        hitboxshape.Size = poundtexture.Texture.GetSize();    
        hitbox.Shape = hitboxshape;
        AddChild(hitbox);
        
        BodyEntered += (Player) => Hit();
        this.GlobalPosition = Position;
        this.amount = amount;
        
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
