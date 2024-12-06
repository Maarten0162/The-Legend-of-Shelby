using Godot;
using System;

public partial class Settings : Control
{
	Label volumeLabel;
	HSlider volumeSlider;
	Vector2I oldResolution; 
	public override void _Ready()
	{

		//settings stuff

		//volume
		volumeLabel = GetNode<Label>("VBoxContainer/VolumeLabel");
		volumeSlider = GetNode<HSlider>("VBoxContainer/Volume");
		volumeSlider.Value = GlobalVar.Instance.Volume;
		volumeLabel.Text = $"Volume: {GlobalVar.Instance.Volume}%";

		//resolution
		Vector2I oldResolution = GlobalVar.Instance.Resolution;
		//initialization
	}

	public override void _Process(double delta)
	{GD.Print("settings loop");
	}
	private void ReturnToMainMenuButton()
	{
		GetTree().ChangeSceneToFile("res://scenes/Main Menu.tscn");
		
	}

	private void OnVolumeSliderValueChanged(float value)
    {
		volumeLabel.Text = $"Volume: {value}%";
		AudioServer.SetBusVolumeDb(0,value);
    }

	private void ResolutionSelected(int index)
    {
        switch (index) {
			case 1: 
				DisplayServer.WindowSetSize(new Vector2I(3840, 2160));
				break;
			case 2: 
				DisplayServer.WindowSetSize(new Vector2I(2560, 1440));
				break;
			case 3: 
				DisplayServer.WindowSetSize(new Vector2I(1920, 1080));
				break;
			case 4: 
				DisplayServer.WindowSetSize(new Vector2I(1600, 900));
				break;
			case 5: 
				DisplayServer.WindowSetSize(new Vector2I(1280, 720));
				break;

			case 0:
				DisplayServer.WindowSetSize(new Vector2I(1152, 648));
				break;
		}
    }
	private async void changeRes(Vector2I resolution)
	{
		
		await GlobalFunc.Instance.WaitForSeconds(10f);
		DisplayServer.WindowSetSize(resolution);
	}
}
