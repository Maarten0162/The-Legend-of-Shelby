using Godot;
using System;



public partial class SoundFx : Node
{
	public static SoundFx instance { get; private set; }
	private AudioStreamPlayer2D audioPlayer;

	// Declare an AudioStream to store the audio track
	public AudioStream audioTrack;

	private string currentAudioTrack;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (instance == null)
		{
			instance = this;
		}
		audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		audioTrack = (AudioStreamWav)GD.Load("res://assets/audio/music/TitleTheme.wav");

		audioPlayer.VolumeDb = 5; //pas dit later aan als hij settings locaal opslaat
		audioPlayer.Play();
		GD.Print("in audio player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public void changeMusic(string location)
	{
		GlobalVar.Instance.MusicLocation = location;
		if (location != currentAudioTrack)
		{
			switch (GlobalVar.Instance.MusicLocation)
			{
				case "TileScreen":
					audioTrack = (AudioStreamWav)GD.Load("res://assets/audio/music/TitleTheme.wav");
					break;
				case "Overworld":
					audioTrack = (AudioStreamWav)GD.Load("res://assets/audio/music/overworld.wav");
					break;
				case "Dungeon":
					audioTrack = (AudioStreamWav)GD.Load("res://assets/audio/music/overworld.wav");
					break;
			}
			currentAudioTrack = location;
			audioPlayer.Stream = audioTrack;
			audioPlayer.Play();
		}
	}
	public void changeMusicVolume(float input)
	{
		audioPlayer.VolumeDb = Mathf.LinearToDb(input);
	}
}
