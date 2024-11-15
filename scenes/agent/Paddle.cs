using Game.Component;
using Godot;

namespace Game.Agent;

public partial class Paddle : StaticBody2D
{

    public float Height { get; private set; }
    public DashState CurrentDashState { get; private set; } = DashState.Normal;
    public bool IsInvincible { get; private set; } = false;

    [Export(PropertyHint.Range)] private float _dashSpeedMultiplier = 2f;
    [Export] public GameSide Side;

    public HealthComponent HealthComponent;
    protected MovementComponent _movementComponent;
    protected AnimationPlayer _animationPlayer;

    protected readonly StringName ANIMATION_DASH = "paddle/dash";
    protected readonly StringName ANIMATION_HIT = "paddle/hit";
    protected readonly StringName ANIMATION_IDLE = "paddle/idle";

    private const float INVINCILITY_TIMEOUT_DELAY = 0.25f;

    private Timer _dashDurationTimer;
    private Timer _dashCooldownTimer;
    private float _defaultSpeed;
    private float _currentDashDirection;

    public override void _Notification(int what)
    {

        if (what == NotificationSceneInstantiated)
            AddToGroup(nameof(Agent));
    }

    public override void _Ready()
    {

        _dashCooldownTimer = GetNode<Timer>("DashCooldownTimer");
        _dashDurationTimer = GetNode<Timer>("DashDurationTimer");

        _animationPlayer = GetNode<AnimationPlayer>(nameof(AnimationPlayer));
        _movementComponent = GetNode<MovementComponent>("MovementComponent");
        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        Height = GetNode<Sprite2D>("Sprite2D").Texture.GetHeight();

        HealthComponent.Death += OnDeath;
        InitializeDash();
    }

    public override void _PhysicsProcess(double delta)
    {

        if (CurrentDashState == DashState.Dashing)
            _movementComponent.MoveVertically(_currentDashDirection, delta);
    }

    public void Hit()
    {
        _animationPlayer.Play(ANIMATION_HIT);
    }

    protected void Dash(float dashDirection)
    {
        if (CurrentDashState is DashState.Cooldown or DashState.Dashing || dashDirection == 0)
            return;

        _animationPlayer.Play(ANIMATION_DASH);
        IsInvincible = true;
        CurrentDashState = DashState.Dashing;
        _currentDashDirection = dashDirection;
        _movementComponent.Speed *= _dashSpeedMultiplier;
        _dashDurationTimer.Start();
    }

    protected virtual void OnDeath() { }

    private void InitializeDash()
    {
        _defaultSpeed = _movementComponent.Speed;
        _dashDurationTimer.Timeout += OnDashDurationTimerTimeout;
        _dashCooldownTimer.Timeout += () => CurrentDashState = DashState.Normal;
    }

    private async void OnDashDurationTimerTimeout()
    {
        _dashCooldownTimer.Start();
        CurrentDashState = DashState.Cooldown;
        _movementComponent.Speed = _defaultSpeed;

        await ToSignal(GetTree().CreateTimer(INVINCILITY_TIMEOUT_DELAY), "timeout");
        IsInvincible = false;
    }
}
