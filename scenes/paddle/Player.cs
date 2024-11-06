using Godot;

namespace Game.Paddle;

public partial class Player : BasePaddle
{

	private readonly StringName ACTION_DASH = "c";

	[Export] private StringName _actionUp = "w";
	[Export] private StringName _actionDown = "s";

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		float direction = GetVerticalDirection();

		if (Input.IsActionPressed(ACTION_DASH))
			Dash(direction);
		if (CurrentDashState != DashState.Dashing)	
			_movementComponent.MoveVertically(direction, delta);
	}

	private float GetVerticalDirection()
	{
		return Input.GetAxis(_actionUp, _actionDown);
	}
}
