using Godot;
using System;

public class PrepareMessage : MarginContainer
{
	private Tween _tween;
	private Timer _timer;
	
	public override void _Ready()
	{
		_tween = GetNode<Tween>("Tween");
		_timer = GetNode<Timer>("Timer");
	
		_timer.Start();
	}

	private void OnTimerTimeout()
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
	
	private void OnTweenTweenAllCompleted()
	{
		Hide();
	}
}
