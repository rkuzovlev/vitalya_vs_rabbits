using Godot;
using System;

enum SamuraiState
{
	Show,
	Saber,
	RabbitDie,
	Hide,
	End,
}
public class Samurai : KillMethod
{
	private Timer _timer;
	private Texture _texture;
	private Killer _killer;
	private SamuraiState _state;
	private Saber _saber;

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_saber = GetNode<Saber>("Saber");
		_texture = ResourceLoader.Load<Texture>("res://Killer/KillMethods/Samurai/killer.png");
	}

	public override void Start(Killer killer)
	{
		_killer = killer;
		killer.ShowKiller(_texture);

		_state = SamuraiState.Show;
		
		_timer.WaitTime = 1f;
		_timer.Start();
	}
	
	private void OnTimerTimeout()
	{
		switch (_state)
		{
			case SamuraiState.Show:
			{
				_saber.Cut();
				_state = SamuraiState.Saber;
				_timer.WaitTime = 1f;
				_timer.Start();
				break;
			}
			
			case SamuraiState.Saber:
			{
				_killer._rabbit.OnRabbitKill(KillMethodType.Samurai);
				_state = SamuraiState.RabbitDie;
				_timer.WaitTime = 1f;
				_timer.Start();
				break;
			}
			
			case SamuraiState.RabbitDie:
			{
				_killer.HideKiller();
				_state = SamuraiState.Hide;
				_timer.WaitTime = 1f;
				_timer.Start();
				break;
			}
			
			case SamuraiState.Hide:
			{
				_killer._rabbit.FadeOut();
				_state = SamuraiState.End;
				_timer.WaitTime = 1f;
				_timer.Start();
				break;
			}
			
			case SamuraiState.End:
			{
				_killer.Stop();
				break;
			}
		}
	}
}
