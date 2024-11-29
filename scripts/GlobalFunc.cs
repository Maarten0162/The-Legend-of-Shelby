using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

public partial class GlobalFunc : Node
{
	public static GlobalFunc Instance {get; private set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		        // Initialize the singleton instance when the node is ready
        if (Instance == null)
        {
            Instance = this; // Set the instance to this object
        }

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public async Task WaitForSeconds(float count)
	{	
		await ToSignal(GetTree().CreateTimer(count), "timeout");
	}
}
