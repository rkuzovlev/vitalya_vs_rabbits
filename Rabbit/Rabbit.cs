using Godot;
using System;

public class Rabbit : Node2D
{
	public KillMethodType KillMethodType;
		
	private AnimatedSprite _sprite;
	private Game _game;
	private readonly Random _rnd = new Random();
	private Tween _tween;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite>("Button/AnimatedSprite");
		_game = GetNode<Game>("/root/Game");
		_tween = GetNode<Tween>("Tween");
		
		var isFlipped = _rnd.Next(0, 10) > 5;
		_sprite.FlipH = isFlipped;
		
		var randomFrame = _rnd.Next(0, 5);
		_sprite.Frame = randomFrame;
	}

	public override void _Process(float delta)
	{
	}
	
	private void OnRabbitPressed()
	{
		_game.OnKillRabbit(this);
	}

	public void FadeOut()
	{
		_tween.InterpolateProperty(
			this,
			"modulate:a",
			1, 
			0,
			1f
		);
		_tween.Start();
	}
	
	public void OnRabbitKill(KillMethodType killMethodType)
	{
		switch (killMethodType)
		{
			case KillMethodType.LaserBeam:
			{
				_sprite.Animation = "burn";
				break;
			}
			
			case KillMethodType.Samurai:
			{
				_sprite.Animation = "samurai";
				break;
			}
			
			case KillMethodType.GodsPunch:
			{
				_sprite.Animation = "godsPunch";
				break;
			}
		}
	}
}
