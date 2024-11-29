using Godot;
using System;
using System.Dynamic;
using System.Threading.Tasks;

public abstract partial class Enemy : CharacterBody2D
{
	public abstract void Sprite();
	public abstract Task Movement();

	private int Health;
	private int Damage;

	public Vector2 up;
	public Vector2 down;
	public Vector2 left;
	public Vector2 right;
	public AnimatedSprite2D animatedSprite2D;

	public Vector2 enemyVelocity;



}
