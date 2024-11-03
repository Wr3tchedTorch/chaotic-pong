using System;
using Game.Paddle;
using Godot;

namespace Game;

public partial class Ball : CharacterBody2D
{

    private readonly float MAX_BOUNCE_ANGLE = 75;

    [Export] private float _speed = 650;

    private readonly Random _RNG = new();
    private Vector2 _currentDir = Vector2.Zero;

    private float RandomAxisDirection => (float)(_RNG.NextDouble() - _RNG.NextDouble());

    public override void _Notification(int what)
    {

        if (what == NotificationSceneInstantiated)
            AddToGroup(nameof(Ball));
    }

    public override void _Ready()
    {

        StartMoving();
    }

    public override void _PhysicsProcess(double delta)
    {

        KinematicCollision2D collider = MoveAndCollide(Velocity * _speed * (float)delta);

        if (!IsInstanceValid(collider))
            return;

        Velocity = Velocity.Bounce(collider.GetNormal());
    }

    public void StartMoving()
    {

        Velocity = new Vector2(RandomAxisDirection, Mathf.Clamp(RandomAxisDirection, -0.5f, 0.5f)).Normalized();
    }

    private float GetAngleFromPaddleCollision(BasePaddle paddle)
    {
        float normalizedPosition = GetDistanceFromPaddleCenter(paddle) / (paddle.Height / 2);
        return normalizedPosition * MAX_BOUNCE_ANGLE;
    }

    private float GetDistanceFromPaddleCenter(Node2D paddle)
    {
        return GlobalPosition.Y - paddle.GlobalPosition.Y;
    }
}
