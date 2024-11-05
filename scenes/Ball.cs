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

    private float RandomAngle => (float)((_RNG.NextDouble() * 75) - (_RNG.NextDouble() * 75));

    public override void _Notification(int what)
    {        

        if (what == NotificationSceneInstantiated)
            AddToGroup(nameof(Ball));
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

        if (Velocity != Vector2.Zero)
            return;

        var randomDir = _RNG.NextDouble() > 0.5 ? 1 : -1;
        var randomAngleInRad = Mathf.DegToRad(RandomAngle);
        Velocity = new Vector2(randomDir * Mathf.Cos(randomAngleInRad), Mathf.Sin(randomAngleInRad)).Normalized();
    }

    public void Reset()
    {
        GlobalPosition = GetViewportRect().Size/2;
        Velocity = Vector2.Zero;
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
