using Godot;
using System;

public partial class Key : Area2D
{
    TextureRect Sprite;
    CollisionShape2D Hitbox;
    AudioStreamPlayer SoundPlayer;
    [Export] string Keytype;

    [Signal]
    public delegate void ItemPickupEventHandler(string keytype);
    public override void _Ready()
    {
        SetCollisionLayerValue(5, true);
        Sprite = GetNode<TextureRect>("TextureRect");
        Hitbox = GetNode<CollisionShape2D>("CollisionShape2D");
      
        switch (Keytype)
        {
            case "Green":
                Sprite.Texture = GD.Load<Texture2D>("res://assets/Sprites/Green_Key.png");
                break;
            case "Red":
                Sprite.Texture = GD.Load<Texture2D>("res://assets/Sprites/Red_Key.png");
                break;
            case "Blue":
                Sprite.Texture = GD.Load<Texture2D>("res://assets/Sprites/Blue_Key.png");
                break;
            case "Yellow":
                Sprite.Texture = GD.Load<Texture2D>("res://assets/Sprites/Yellow_Key.png");
                break;
        }
this.BodyEntered += OnBodyEntered;
        
    }

  private void OnBodyEntered(Node body)
	{	
		Obtain();
	}


    private void Obtain()   
    {   GD.Print("in key");
        GlobalVar.Instance.Keys.Add(Keytype);
        EmitSignal(SignalName.ItemPickup, Keytype);
        if (GlobalVar.Instance.Hasitemspace)
        {
            QueueFree();
        }
    }
}
