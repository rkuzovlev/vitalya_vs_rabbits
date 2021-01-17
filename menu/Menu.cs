using Godot;
using System;

enum MenuState
{
	ShowVitalya,
	ShowVs,
	ShowRabbits,
	ShowRest,
}

public class Menu : Node2D
{
	private MenuState _state = MenuState.ShowVitalya;
	private Sprite _vitalyaSprite;
	private Sprite _vsSprite;
	private Sprite _rabbitsSprite;
	private Tween _tween;
	private Timer _timer;
	private AudioStreamPlayer _bgPlayer;
	private AudioStreamPlayer _effectsPlayer;
	private TextureButton _fightButton;
	private RabbitMenu _rabbitMenu;
	private VitalyaMenu _vitalyaMenu;

	public override void _Ready()
	{
		_vitalyaSprite = GetNode<Sprite>("vitalya");
		_vsSprite = GetNode<Sprite>("vs");
		_rabbitsSprite = GetNode<Sprite>("rabbits");
		_tween = GetNode<Tween>("Tween");
		_timer = GetNode<Timer>("Timer");
		_bgPlayer = GetNode<AudioStreamPlayer>("BgPlayer");
		_effectsPlayer = GetNode<AudioStreamPlayer>("EffectsPlayer");
		_fightButton = GetNode<TextureButton>("Fight");
		_rabbitMenu = GetNode<RabbitMenu>("RabbitMenu");
		_vitalyaMenu = GetNode<VitalyaMenu>("VitalyaMenu");

		var wSize = OS.WindowSize;

		_vitalyaSprite.Position = new Vector2(wSize.x / 2, 60);
		_vsSprite.Position = new Vector2(wSize.x / 2, 170);
		_rabbitsSprite.Position = new Vector2(wSize.x / 2, 280);

		_vitalyaSprite.Scale = Vector2.Zero;
		_vsSprite.Scale = Vector2.Zero;
		_rabbitsSprite.Scale = Vector2.Zero;
		
		var newRabbitPosition = new Vector2(-100, wSize.y - 50);
		_rabbitMenu.Position = newRabbitPosition;
		_rabbitMenu.SetNewPosition(newRabbitPosition);
		
		var newVitalyaPosition = new Vector2(wSize.x  + 100, wSize.y - 40);
		_vitalyaMenu.Position = newVitalyaPosition;
		_vitalyaMenu.SetNewPosition(newVitalyaPosition);
		
		var fSize = _fightButton.RectSize;
		_fightButton.SetGlobalPosition(new Vector2(
			wSize.x / 2 - fSize.x / 2,
			wSize.y - fSize.y - 20
		));
		_fightButton.Hide();
		
		_timer.WaitTime = 1f;
		_timer.Start();
	}

	private void AnimateText(Sprite text)
	{
		_tween.InterpolateProperty(
			text,
			"scale",
			Vector2.Zero,
			Vector2.One,
			0.7f,
			Tween.TransitionType.Circ,
			Tween.EaseType.Out
		);
		_tween.Start();
	}

	private void AnimateNext()
	{
		switch (_state)
		{
			case MenuState.ShowVitalya:
			{
				_effectsPlayer.Play();
				AnimateText(_vitalyaSprite);
				_state = MenuState.ShowVs;
				break;
			}

			case MenuState.ShowVs:
			{
				_effectsPlayer.Play();
				AnimateText(_vsSprite);
				_state = MenuState.ShowRabbits;
				break;
			}

			case MenuState.ShowRabbits:
			{
				_effectsPlayer.Play();
				AnimateText(_rabbitsSprite);
				_state = MenuState.ShowRest;
				break;
			}

			case MenuState.ShowRest:
			{
				_effectsPlayer.Stop();
				_bgPlayer.Play();
				_fightButton.Show();
				_rabbitMenu.SetNewPosition(new Vector2(100, OS.WindowSize.y - 50));
				_vitalyaMenu.SetNewPosition(new Vector2(OS.WindowSize.x - 100, OS.WindowSize.y - 40));
				break;
			}
		}
	}

	private void OnTweenAllCompleted()
	{
		AnimateNext();
	}

	private void OnTimerTimeout()
	{
		AnimateNext();
	}

	private void OnFightPressed()
	{
		GetTree().ChangeScene("res://main.tscn");
	}
}
