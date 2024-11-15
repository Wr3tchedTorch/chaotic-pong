using System;
using Game.Effect;
using Godot;

namespace Game.Agent;

[GlobalClass]
public partial class Ball : CharacterBody2D
{

    private readonly StringName ANIMATION_BOUNCE = "bounce";
    private readonly StringName ANIMATION_DESTROY = "destroy";
    private readonly StringName ANIMATION_SPAWN = "spawn";
    private readonly StringName ANIMATION_RESET = "RESET";

    private readonly float PRE_BOUNCE_DELAY = .1f;
    private readonly float MAX_BOUNCE_ANGLE = 75;

    [Export] public int ScoreAmount = 1;
    [Export] private float _speed = 650;
    [Export] private PackedScene _deathParticleScene;

    private EffectManager _effectManager;
    private GpuParticles2D _hitParticles;
    private AnimationPlayer _animationPlayer;
    private readonly Random _RNG = new();
    private Vector2 _currentDir = Vector2.Zero;

    private float RandomAngle => (float)((_RNG.NextDouble() * 75) - (_RNG.NextDouble() * 75));

    public override void _Notification(int what)
    {

        if (what == NotificationSceneInstantiated)
            AddToGroup(nameof(Ball));
    }

    public override void _Ready()
    {
        _hitParticles = GetNode<GpuParticles2D>("HitParticles");
        _effectManager = GetNode<EffectManager>("EffectManager");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        _animationPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override async void _PhysicsProcess(double delta)
    {

        LookAt(GlobalPosition + Velocity.Normalized() * 100);
        KinematicCollision2D collider = MoveAndCollide(Velocity * _speed * (float)delta);

        if (!IsInstanceValid(collider) || Velocity == Vector2.Zero)
            return;

        _hitParticles.Restart();
        _animationPlayer.Play(ANIMATION_BOUNCE);

        Vector2 oldVelocity = Velocity;
        Velocity = Vector2.Zero;

        if (collider.GetCollider() is Paddle paddle)
            paddle.Hit();

        await ToSignal(GetTree().CreateTimer(PRE_BOUNCE_DELAY), "timeout");
        Velocity = oldVelocity.Bounce(collider.GetNormal());
    }

    public void StartMoving()
    {

        if (Velocity != Vector2.Zero)
            return;

        var randomDir = _RNG.NextDouble() > 0.5 ? 1 : -1;
        var randomAngleInRad = Mathf.DegToRad(RandomAngle);
        Velocity = new Vector2(randomDir * Mathf.Cos(randomAngleInRad), Mathf.Sin(randomAngleInRad)).Normalized();
    }

    public void Destroy()
    {
        GpuParticles2D deathParticle = _deathParticleScene.Instantiate<GpuParticles2D>();
        GetParent().AddChild(deathParticle);
        deathParticle.GlobalPosition = GlobalPosition;
        deathParticle.Rotate(Velocity.Angle());
        deathParticle.Emitting = true;
        _animationPlayer.Play(ANIMATION_DESTROY);

        Velocity = Vector2.Zero;
    }

    private void Reset()
    {
        _animationPlayer.Play(ANIMATION_SPAWN);
        GlobalPosition = GetViewportRect().Size / 2;
        Velocity = Vector2.Zero;
    }

    private float GetAngleFromPaddleCollision(Paddle paddle)
    {
        float normalizedPosition = GetDistanceFromPaddleCenter(paddle) / (paddle.Height / 2);
        return normalizedPosition * MAX_BOUNCE_ANGLE;
    }

    private float GetDistanceFromPaddleCenter(Node2D paddle)
    {
        return GlobalPosition.Y - paddle.GlobalPosition.Y;
    }

    private void OnConsoleManagerFireBallEffectSignal()
    {
        _effectManager.TransitionTo(EffectList.Fire);
    }

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == ANIMATION_DESTROY)
            Reset();
    }
}
