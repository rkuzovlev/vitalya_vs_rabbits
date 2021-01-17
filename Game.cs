using Godot;
using System;

public class Game : Node2D
{
	private Killer _killer;
	private bool _isKillingActive = false;
	private int _rabbitsCount = 0;
	private RabbitCounter _rabbitCounter;
	private PrepareMessage _prepareMessage;
	
	public override void _Ready()
	{
		_killer = GetNode<Killer>("Killer");
		_rabbitCounter = GetNode<RabbitCounter>("RabbitCounter");
		_prepareMessage = GetNode<PrepareMessage>("PrepareMessage");
		
		_prepareMessage.SetSize(OS.WindowSize);
	}

	public void SetRabbitsCount(int count)
	{
		_rabbitsCount = count;
	}
	
	public void OnRabbitKilled()
	{
		_isKillingActive = false;
		
		_rabbitsCount--;
		_rabbitCounter.SetCount(_rabbitsCount);

		if (_rabbitsCount == 0)
		{
			GetTree().ChangeScene("res://CongratzScene/Congratz.tscn");
		}
	}

	public void OnKillRabbit(Rabbit rabbit)
	{
		if (_isKillingActive)
		{
			return;
		}
		
		_isKillingActive = true;
		
		_killer.Start(rabbit.KillMethodType, rabbit);
	}
}
