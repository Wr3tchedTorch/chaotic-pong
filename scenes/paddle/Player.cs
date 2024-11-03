using Godot;
using Game.Component;

namespace Game.Paddle;

public partial class Player : BasePaddle
{

	private readonly StringName ACTION_UP = "up";
	private readonly StringName ACTION_DOWN = "down";

	public override void _PhysicsProcess(double delta)
	{

		_movementComponent.MoveVertically(GetVerticalDirection(), delta);
	}

	private float GetVerticalDirection()
	{
		return Input.GetAxis(ACTION_UP, ACTION_DOWN);
	}
}
