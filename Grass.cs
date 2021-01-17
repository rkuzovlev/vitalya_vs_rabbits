using Godot;
using System;

public class Grass : TileMap
{
	private readonly Random _rnd = new Random();

	public override void _Ready()
	{
	}

	private int GetRandomTile()
	{
		var tileIDs = TileSet.GetTilesIds();
		
		return (int)tileIDs[_rnd.Next(0, tileIDs.Count - 1)];
	}

	public void GenerateTiles(int xCount, int yCount)
	{
		for (var x = -1; x < xCount; x++)
		{
			for (var y = -1; y < yCount; y++)
			{
				var randomTileId = GetRandomTile();
				var isFlipped = _rnd.Next(0, 1) == 1;
				SetCell(x, y, randomTileId, isFlipped);
			}
		}
	}
	
	public override void _Process(float delta)
	{
		
	}
}
