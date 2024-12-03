using Godot;
using System;

public partial class Weapon : Node2D
{
	private string Wtype;
	public string WType{
		get{
			return Wtype;
		}
		set{
			Wtype = value;
		}
	}
	private string Wdamage;
	public string WDamage{
		get{
			return Wdamage;
		}
		set{
			Wdamage = value;
		}
	}
}
