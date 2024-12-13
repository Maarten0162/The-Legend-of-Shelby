using Godot;
using System;

public partial class Push : CharacterBody2D{
[Export] public float PushStrength = 100f; // Adjust this value for the pushing force
private Vector2 _velocity = Vector2.Zero; // Current velocity of the block
private float _drag = 10f; // Drag scalar to slow down the block gradually
private float _stopThreshold = 0.1f; // Threshold for stopping the block when velocity is small

public void _PhysicsProcess(float delta)
{
    // Apply drag to reduce velocity gradually if the block is moving
    if (_velocity.LengthSquared() > _stopThreshold * _stopThreshold)
    {
        // Slow down the velocity by drag
        _velocity = _velocity.MoveToward(Vector2.Zero, _drag * delta);
    }
    else
    {
        // Stop movement when velocity is small enough
        _velocity = Vector2.Zero;
    }

    // Move the block using velocity if the velocity is significant
    if (_velocity.LengthSquared() > 0.01f) // Only move if velocity is significant
    {
        // Apply the velocity to move the block
        MoveAndCollide(_velocity * delta);
    }
}

public void Pushh(Vector2 force)
{
    // Apply force to the block
    _velocity += force * PushStrength;
}}