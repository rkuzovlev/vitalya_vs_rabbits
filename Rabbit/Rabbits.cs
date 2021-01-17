using Godot;
using System;

public class Rabbits : Node2D
{
    private PackedScene _rabbitScene;
    private readonly Random _rnd = new Random();

    public override void _Ready()
    {
        _rabbitScene = ResourceLoader.Load("res://Rabbit/Rabbit.tscn") as PackedScene;
    }

    public int GenerateRabbits(int maxCellsByX, int maxCellsByY)
    {
        var killMethodTypesArray = Enum.GetValues(typeof (KillMethodType));

        for (var i = 0; i < killMethodTypesArray.Length; i++)
        {
            var xCell = _rnd.Next(2, maxCellsByX - 2);
            var yCell = _rnd.Next(2, maxCellsByY - 2);
            
            var rabbit = _rabbitScene.Instance() as Rabbit;
            rabbit.Transform = new Transform2D(0, new Vector2(xCell * 120 - 60, yCell * 120 - 60));

            rabbit.KillMethodType = (KillMethodType) killMethodTypesArray.GetValue(i);
            
            AddChild(rabbit);
        }

        return killMethodTypesArray.Length;
    }

    public override void _Process(float delta)
    {
    }
}