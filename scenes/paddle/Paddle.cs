using Game.Component;
using Godot;

namespace Game.Paddle;

public partial class BasePaddle : StaticBody2D
{

    public float Height { get; private set; }

    protected MovementComponent _movementComponent;

    public override void _Notification(int what)
    {

        if (what == NotificationSceneInstantiated)
            AddToGroup(nameof(Paddle));
    }

    public override void _Ready()
    {

        Height = GetNode<Sprite2D>("Sprite2D").Texture.GetHeight();
        _movementComponent = GetNode<MovementComponent>("MovementComponent");
    }
}
