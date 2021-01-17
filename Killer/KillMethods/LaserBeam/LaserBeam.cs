using Godot;
using System;

enum LaserBeamState
{
	Show,
	Shoot,
	ShootEnd,
	Hide,
}

public class LaserBeam : KillMethod
{
	private Particles2D _beginParticles;
	private Particles2D _endParticles;
	private Killer _killer;
	private Tween _tween;
	private Timer _timer;
	private LaserBeamState _state;
	private Line2D _line;
	private AudioStreamPlayer2D _soundEffect;
	
	public override void _Ready()
	{
		_line = GetNode<Line2D>("Line");
		_beginParticles = GetNode<Particles2D>("BeginParticles");
		_endParticles = GetNode<Particles2D>("EndParticles");
		_tween = GetNode<Tween>("Tween");
		_timer = GetNode<Timer>("Timer");
		_soundEffect = GetNode<AudioStreamPlayer2D>("SoundEffect");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (_killer == null || _killer._rabbit == null)
		{
			return;
		}

		var pos = Godot.OS.WindowPosition;

		_line.Points = new[]
		{
			_killer.EyesPosition,
			_killer._rabbit.Position - pos - _killer.Position
		}; 
		
		_beginParticles.Position = _line.Points[0];
		_endParticles.Position = _line.Points[1];
	}

	public override void Start(Killer killer)
	{
		_killer = killer;
		
		Hide();
		
		_killer.ShowKiller(null);

		_state = LaserBeamState.Show;
		
		_timer.WaitTime = 1f;
		_timer.Start();
	}
	
	private void OnTweenAllCompleted()
	{
		
	}

	private void OnTimerTimeout()
	{
		switch (_state)
		{
			case LaserBeamState.Show:
			{
				_state = LaserBeamState.Shoot;
				Show();
				_killer._rabbit.OnRabbitKill(KillMethodType.LaserBeam);
				_timer.WaitTime = 1f;
				_timer.Start();
				_soundEffect.Play();
				break;
			}

			case LaserBeamState.Shoot:
			{
				_state = LaserBeamState.ShootEnd;
				Hide();
				_timer.WaitTime = 1f;
				_timer.Start();
				_soundEffect.Stop();
				break;
			}
			
			case LaserBeamState.ShootEnd:
			{
				_state = LaserBeamState.Hide;
				_killer._rabbit.FadeOut();
				_timer.WaitTime = 1f;
				_timer.Start();
				break;
			}
			
			case LaserBeamState.Hide:
			{
				_killer.HideKiller();
				_killer.Stop();
				break;
			}
		}
	}
}
