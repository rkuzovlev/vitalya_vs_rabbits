using Godot;
using System;

public class Congratz : MarginContainer
{
	public override void _Ready()
	{
		
	}
	
	private void OnTimerTimeout()
	{
		GetTree().ChangeScene("res://FinalScreen/FinalScreen.tscn");
	}
}
