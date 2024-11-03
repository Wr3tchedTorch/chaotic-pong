using Godot;

namespace Game.Paddle;

public partial class Player : BasePaddle
{

	[Export] private StringName _actionUp = "w";
	[Export] private StringName _actionDown = "s";

	public override void _PhysicsProcess(double delta)
	{

		_movementComponent.MoveVertically(GetVerticalDirection(), delta);
	}

	private float GetVerticalDirection()
	{
		return Input.GetAxis(_actionUp, _actionDown);
	}
}
