using Godot;
using System;

public class World : Node2D
{
	private Grass _grass;
	private Rabbits _rabbits;
	private Game _game;
	private const float WorldLerpSpeed = 10.0f;
	private RabbitCounter _rabbitCounter;

	public override void _Ready()
	{
		_grass = GetNode<Grass>("Grass");
		_rabbits = GetNode<Rabbits>("Rabbits");
		_game = GetNode<Game>("/root/Game");

		var (xCount, yCount, minByY) = GetCountTiles();
		_grass.GenerateTiles(xCount, yCount);
		var rabbitsCount = _rabbits.GenerateRabbits(xCount, minByY);
		_game.SetRabbitsCount(rabbitsCount);
		_rabbitCounter = GetNode<RabbitCounter>("/root/Game/RabbitCounter");
		_rabbitCounter.SetCount(rabbitsCount);

		var pos = Godot.OS.WindowPosition;
		Position = -pos;
	}

	private (int xCount, int yCount, int minByY) GetCountTiles()
	{
		var width = 0f;
		var height = 0f;
		var minHeight = 0f;

		for (var i = 0; i < OS.GetScreenCount(); i++)
		{
			var screenSize = OS.GetScreenSize(i);
			width += screenSize.x;
			height = screenSize.y > height ? screenSize.y : height;

			if (minHeight == 0)
			{
				minHeight = screenSize.y;
			}
			else if (screenSize.y < minHeight)
			{
				minHeight = screenSize.y;
			}
		}

		var xCount = (int) (width / 120) + 1;
		var yCount = (int) (height / 120) + 1;
		var minByY = (int) (minHeight / 120);

		return (xCount, yCount, minByY);
	}

	public override void _Process(float delta)
	{
		var pos = Godot.OS.WindowPosition;
		if (Position != -pos)
		{
			Position = Position.LinearInterpolate(-pos, delta * WorldLerpSpeed) ;
		}
	}
}
