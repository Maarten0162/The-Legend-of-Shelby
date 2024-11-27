using Godot;
using System;
using System.Dynamic;

public partial class Enemy : Node2D
{	
	public Enemy(int Healthinput, int Damageinput, string Typeinput)
	{
		EHealth = Healthinput;
		EDamage = Damageinput;
		etype = Typeinput;
	}
	public Enemy()
	{
		EHealth = 100;
		EDamage = 10;
		etype = "mob";
	}

	[Export]	private int ehealth;
	
	public int EHealth
	{
		get
		{
			return ehealth;
		}
		set
		{
			ehealth = value;
		}
	}
	[Export]	private int edamage;
	
	public int EDamage
	{
		get
		{
			return edamage;
		}
		set
		{
			edamage = value;
		}
	}
	private string etype;
	public string EType
	{
		get
		{
			return etype;
		}
	}
}
