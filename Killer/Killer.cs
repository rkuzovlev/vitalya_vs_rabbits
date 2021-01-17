using Godot;
using System;

public class Killer : Node2D
{
	public Rabbit _rabbit;
	public Vector2 EyesPosition = new Vector2(52f, 74f);

	private Tween _tween;
	private Sprite _sprite;
	private Vector2 _size;
	private Texture _defaultTexture;
	private Node2D _killMethod;
	private Rabbits _rabbits;
	private Game _game;

	public override void _Ready()
	{
		_tween = GetNode<Tween>("Tween");
		_sprite = GetNode<Sprite>("Killer");
		_game = GetNode<Game>("/root/Game");
		_rabbits = GetNode<Rabbits>("/root/Game/World/Rabbits");
		_defaultTexture = ResourceLoader.Load<Texture>("res://Killer/killer.png");

		var textureSize = _sprite.Texture.GetSize();
		_size = textureSize * _sprite.Scale;

		HideKiller();
	}
	
	private void TweenToNewPosition(Vector2 position, float duration)
	{
		_tween.StopAll();
		_tween.InterpolateProperty(
			this,
			"position",
			Position,
			position,
			duration
		);
		_tween.Start();
	}
	
	private void StartInstance<T>(string scenePath, Rabbit rabbit) where T : KillMethod
	{
		_rabbit = rabbit;
		var scene = ResourceLoader.Load(scenePath) as PackedScene;
		_killMethod = scene.Instance() as T;

		AddChild(_killMethod);

		(_killMethod as T).Start(this);
	}

	public void Stop()
	{
		RemoveChild(_killMethod);
		_rabbits.RemoveChild(_rabbit);
		_rabbit = null;
		_game.OnRabbitKilled();
	}

	public void Start(KillMethodType killMethodType, Rabbit rabbit)
	{
		switch (killMethodType)
		{
			case KillMethodType.LaserBeam: StartInstance<LaserBeam>("res://Killer/KillMethods/LaserBeam/LaserBeam.tscn", rabbit); break;
			case KillMethodType.Samurai: StartInstance<Samurai>("res://Killer/KillMethods/Samurai/Samurai.tscn", rabbit); break;
			case KillMethodType.GodsPunch: StartInstance<GodsPunch>("res://Killer/KillMethods/GodsPunch/GodsPunch.tscn", rabbit); break;

			default:
				throw new ArgumentOutOfRangeException(nameof(killMethodType), killMethodType, null);
		}
	}

	public void HideKiller(float duration = 0.5f)
	{
		var newPosition = new Vector2(
			OS.WindowSize.x - _size.x,
			OS.WindowSize.y + _size.y
		);

		TweenToNewPosition(newPosition, duration);
	}
	
	public void ShowKiller(Texture killerTexture, float duration = 0.5f)
	{
		_sprite.Texture = killerTexture ?? _defaultTexture;
		
		var newPosition = new Vector2(
			OS.WindowSize.x - _size.x,
			OS.WindowSize.y - _size.y
		);
		
		TweenToNewPosition(newPosition, duration);
	}
}
