using Godot;
using System;

public partial class puzzle : Area2D
{
    public void OnBodyEntered(Node body){
        if (body is PhysicsBody2D body2)
                    {
                        // Retrieve the collider's layer
                        int collisionLayer = (int)body2.CollisionLayer;


                        if ((collisionLayer & (1 << 20)) != 0) // Layer 2 corresponds to bit 1 Dit is een Enemy
                        {
                            

                        }
                       
                    }
    }
}

