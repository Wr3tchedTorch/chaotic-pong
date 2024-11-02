using System;
using Godot;

namespace Game;

public partial class Ball : Area2D
{

    [Export] private float _speed = 400;

    private readonly Random _RNG = new();
    private Vector2 _currentDir = Vector2.Zero;

    private float RandomAxisDirection => (float)(_RNG.NextDouble() - _RNG.NextDouble());

    public override void _Ready()
    {

        StartMoving();
        BodyEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {

        if (_currentDir == Vector2.Zero)
            return;

        GlobalPosition += _currentDir * _speed * (float)delta;
    }

    public void StartMoving()
    {        

        _currentDir = new Vector2(RandomAxisDirection, RandomAxisDirection).Normalized();
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body.IsInGroup(nameof(Paddle)))
            _currentDir.X *= -1;
        else
            _currentDir.Y *= -1;
    }
}
