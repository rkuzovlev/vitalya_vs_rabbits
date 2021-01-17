using Godot;
using System;

public class Saber : Node2D
{
	private Tween _tween;
	private Sprite _saber;
	private bool isCutted = false;
	private AudioStreamPlayer2D _soundEffect;

	public override void _Ready()
	{
		_tween = GetNode<Tween>("Tween");
		_saber = GetNode<Sprite>("saber");
		_soundEffect = GetNode<AudioStreamPlayer2D>("SoundEffect");
	}

	public void Cut()
	{
		isCutted = false;
		
		GD.Print("Cut");
		
		_soundEffect.Play();
			
		_tween.InterpolateProperty(
			this,
			"_saber:position:x",
			33.5,
			14,
			0.1f
		);
		
		_tween.InterpolateProperty(
			this,
			"_saber:position:y",
			31.7,
			0,
			0.1f
		);
		_tween.Start();
	}

	private void OnTweenTweenAllCompleted()
	{
		if (isCutted)
		{
			_soundEffect.Stop();
			return;
		}

		isCutted = true;
		
		_tween.StopAll();
		_tween.InterpolateProperty(
			this,
			"_saber:position:x",
			14,
			33.5,
			0.1f
		);
		
		_tween.InterpolateProperty(
			this,
			"_saber:position:y",
			0,
			31.7,
			0.1f
		);
		_tween.Start();
	}
}
