using Godot;
using System;
using System.Dynamic;

public partial class Enemy : Node2D
{	
	public Enemy(int Healthinput, int Damageinput, string Typeinput)
	{
		EHealth = Healthinput;
		EDamage = Damageinput;
		Etype = Typeinput;

	}

	private int Ehealth;
	public int EHealth
	{
		get
		{
			return Ehealth;
		}
		set
		{
			Ehealth = value;
		}
	}
	private int Edamage;
	public int EDamage
	{
		get
		{
			return Edamage;
		}
		set
		{
			Edamage = value;
		}
	}
	private string Etype;
	public string EType
	{
		get
		{
			return Etype;
		}
	}
}
