using Godot;
using System;

public partial class Key : Area2D
{
    TextureRect Sprite;
    CollisionShape2D Hitbox;
    AudioStreamPlayer SoundPlayer;
    [Export] public string Keytype;

    [Signal]
    public delegate void ItemPickupEventHandler(string keytype);

    public Key(string colour, Vector2 position)
    {
        Keytype = colour;
        this.GlobalPosition = position;

    }
    public Key(){

    }
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
    public override void _Process(double delta)
    {  
        Changetype();
    }
    private void OnBodyEntered(Node body)
    {
        Obtain();
    }
    public void Changetype(){
        
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
    }


    private void Obtain()
    {
        switch (Keytype)
        {
            case "Green":
               GlobalVar.Instance.HasGreenKey = true;
               QueueFree();
                break;
            case "Red":
                 GlobalVar.Instance.HasRedKey = true;
               QueueFree();
                break;
            case "Blue":
                Sprite.Texture = GD.Load<Texture2D>("res://assets/Sprites/Blue_Key.png");
                break;
            case "Yellow":
                Sprite.Texture = GD.Load<Texture2D>("res://assets/Sprites/Yellow_Key.png");
                break;
        }
        
    }
}
