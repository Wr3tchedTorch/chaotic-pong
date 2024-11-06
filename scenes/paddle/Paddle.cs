using Game.Component;
using Godot;

namespace Game.Paddle;

public partial class BasePaddle : StaticBody2D
{

    public float Height { get; private set; }
    public bool IsInvincible { get; private set; }

    [Export(PropertyHint.Range)] private float _dashSpeedMultiplier = 2f;

    protected MovementComponent _movementComponent;
    protected bool _canMove = true;

    private const float INVINCILITY_TIMEOUT_DELAY = 0.25f;

    private Timer _dashDurationTimer;
    private Timer _dashCooldownTimer;
    private float _defaultSpeed;
    private bool _canDash = true;
    private float _currentDashDirection;

    public override void _Notification(int what)
    {

        if (what == NotificationSceneInstantiated)
            AddToGroup(nameof(Paddle));
    }

    public override void _Ready()
    {

        _dashCooldownTimer = GetNode<Timer>("DashCooldownTimer");
        _dashDurationTimer = GetNode<Timer>("DashDurationTimer");

        _movementComponent = GetNode<MovementComponent>("MovementComponent");

        Height = GetNode<Sprite2D>("Sprite2D").Texture.GetHeight();
        InitializeDashVariables();
        _defaultSpeed = _movementComponent.Speed;
        _dashDurationTimer.Timeout += OnDashDurationTimerTimeout;
        _dashCooldownTimer.Timeout += () => _canDash = true;
    }

    public override void _PhysicsProcess(double delta)
    {

        if (!_dashDurationTimer.IsStopped())
            _movementComponent.MoveVertically(_currentDashDirection, delta);
    }

    public void Dash(float dashDirection)
    {
        if (!_canDash || !_dashDurationTimer.IsStopped())
            return;

        _currentDashDirection = dashDirection;
        IsInvincible = true;
        _canDash = false;
        _canMove = false;
        _movementComponent.Speed *= _dashSpeedMultiplier;
        _dashDurationTimer.Start();
    }

    private void InitializeDashVariables()
    {

    }

    private async void OnDashDurationTimerTimeout()
    {
        _canMove = true;
        _dashCooldownTimer.Start();
        _movementComponent.Speed = _defaultSpeed;

        await ToSignal(GetTree().CreateTimer(INVINCILITY_TIMEOUT_DELAY), "timeout");
        IsInvincible = false;
    }
}
