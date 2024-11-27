using Godot;
using System;
using System.Threading.Tasks;

public partial class Main : Node2D
{
	public override async void _Ready()
	{
		GD.Print("begin");
        await WaitForSeconds(6);
        GD.Print("fghjk");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{

	}
	 public async Task WaitForSeconds(float seconds)
    {
        await ToSignal(GetTree().CreateTimer(seconds), SceneTreeTimer.SignalName.Timeout);

    }
	
}
