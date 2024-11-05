using Godot;

namespace Game.Paddle;

public partial class Player : BasePaddle
{

	private readonly StringName ACTION_DASH = "c";

	[Export] private StringName _actionUp = "w";
	[Export] private StringName _actionDown = "s";

	public override void _PhysicsProcess(double delta)
	{

		if (Input.IsActionPressed(ACTION_DASH))
			Dash();

		_movementComponent.MoveVertically(GetVerticalDirection(), delta);
	}

	private float GetVerticalDirection()
	{
		return Input.GetAxis(_actionUp, _actionDown);
	}
}
