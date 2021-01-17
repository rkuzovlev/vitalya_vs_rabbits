using Godot;
using System;

enum GodsPunchState
{
	ShowKiller,
	KillFist,
	HideFist,
	HideKiller,
}

public class GodsPunch : KillMethod
{
	private Fist _fist;
	private Killer _killer;
	private Timer _timer;
	private GodsPunchState _state;
	private AudioStreamPlayer2D _soundEffect;

	public override void _Ready()
	{
		_fist = GetNode<Fist>("Fist");
		_timer = GetNode<Timer>("Timer");
		_soundEffect = GetNode<AudioStreamPlayer2D>("SoundEffect");
	}

	public override void Start(Killer killer)
	{
		_killer = killer;
		killer.ShowKiller(null);

		_timer.WaitTime = 1f;
		_timer.Start();
		_state = GodsPunchState.ShowKiller;
		
		_fist.Positioning(killer);
	}
	
	private void OnTimerTimeout()
	{
		switch (_state)
		{
			case GodsPunchState.ShowKiller:
			{
				_state = GodsPunchState.KillFist;
				_fist.Kill();
				_soundEffect.Play();
				_timer.Start();
				break;
			}
			
			case GodsPunchState.KillFist:
			{
				_state = GodsPunchState.HideFist;
				_killer._rabbit.OnRabbitKill(KillMethodType.GodsPunch);
				_fist.HideFist();
				_soundEffect.Stop();
				_timer.WaitTime = 3f;
				_timer.Start();
				break;
			}
			
			case GodsPunchState.HideFist:
			{
				_state = GodsPunchState.HideKiller;
				_killer._rabbit.FadeOut();
				_killer.HideKiller();
				_timer.WaitTime = 1f;
				_timer.Start();
				break;
			}
			
			case GodsPunchState.HideKiller:
			{
				_killer.Stop();
				break;
			}
		}
	}
}
