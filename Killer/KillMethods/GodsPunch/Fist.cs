using Godot;
using System;

public class Fist : Sprite
{
    private Vector2 _targetPosition;
    private bool _isKilling = false;
    private Killer _killer;
    
    private static readonly Vector2 HideFistVector = new Vector2(0, -1500);

    public override void _Ready()
    {
        Hide();
    }

    public void Positioning(Killer killer)
    {
        _killer = killer;
    }
    
    public void Kill()
    {
        Show();
        _isKilling = true;
    }
    
    public void HideFist()
    {
        _isKilling = false;
    }
    
    public override void _PhysicsProcess(float delta)
    {
        if (_killer == null || _killer._rabbit == null)
        {
            return;
        }

        var pos = Godot.OS.WindowPosition;
        _targetPosition = _killer._rabbit.Position - pos - _killer.Position + (_isKilling ? Vector2.Zero : HideFistVector);
    }
    
    public override void _Process(float delta)
    {
        Position = Position.LinearInterpolate(_targetPosition, 0.3f);
    }
}