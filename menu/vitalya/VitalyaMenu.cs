using Godot;
using System;

public class VitalyaMenu : Node2D
{
	private AnimationPlayer _animationPlayer;
	private Vector2 _position;
	
	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.CurrentAnimation = "Headbanging";
	}

	public void SetNewPosition(Vector2 position)
	{
		_position = position;
	}

	public override void _Process(float delta)
	{
		Position = Position.LinearInterpolate(_position, 0.1f);
	}
}
