using Game.Component;
using Godot;

namespace Game.Paddle;

public partial class BasePaddle : StaticBody2D
{

    public float Height { get; private set; }
    public bool IsInvincible { get; private set; }

    [Export(PropertyHint.Range)] private float _dashSpeedMultiplier = 2f;

    protected MovementComponent _movementComponent;
    private Timer _dashDurationTimer;
    private Timer _dashCooldownTimer;
    private float _defaultSpeed;
    private bool _canDash = true;

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

        _defaultSpeed = _movementComponent.Speed;
        _dashDurationTimer.Timeout += () =>
        {
            _movementComponent.Speed = _defaultSpeed;
            IsInvincible = false;
            _dashCooldownTimer.Start();
        };
        _dashCooldownTimer.Timeout += () => _canDash = true;
    }    

    public void Dash()
    {
        if (!_canDash || !_dashDurationTimer.IsStopped())
            return;
        
        _canDash = false;
        IsInvincible = true;
        _movementComponent.Speed *= _dashSpeedMultiplier;
        _dashDurationTimer.Start();
    }
}
