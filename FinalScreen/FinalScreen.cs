using Godot;
using System;

public class FinalScreen : Node2D
{
	private Sprite _bg;
	private Vector2 _ws;
	private AnimationPlayer _animation;
	private AudioStreamPlayer _audioStreamPlayer;

	public override void _Ready()
	{
		_bg = GetNode<Sprite>("bg");
		_audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		_animation = GetNode<AnimationPlayer>("AnimationPlayer");
		
		_ws = OS.WindowSize;
		var textureSize = _bg.Texture.GetSize();
		
		_bg.Position = new Vector2(
			_ws.x / 2 - textureSize.x / 2 * _bg.Scale.x,
			_ws.y / 2 - textureSize.y / 2 * _bg.Scale.y
		);

		_animation.Play("main");
	}
	
	private void OnTimerTimeout()
	{
		_audioStreamPlayer.Stop();
	}
}
