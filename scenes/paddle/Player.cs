using Game.Component;
using Godot;

namespace Game.Paddle;

public partial class Player : StaticBody2D
{

	private readonly StringName ACTION_UP = "up";
	private readonly StringName ACTION_DOWN = "down";

	private MovementComponent _movementComponent;

	public override void _Ready()
	{

		AddToGroup(nameof(Paddle));

		_movementComponent = GetNode<MovementComponent>("MovementComponent");
	}

	public override void _PhysicsProcess(double delta)
	{

		_movementComponent.MoveVertically(GetVerticalDirection(), delta);
	}

	private float GetVerticalDirection()
	{
		return Input.GetAxis(ACTION_UP, ACTION_DOWN);
	}
}
