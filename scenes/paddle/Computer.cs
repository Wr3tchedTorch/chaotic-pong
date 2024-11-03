using Godot;

namespace Game.Paddle;

public partial class Computer : BasePaddle
{

	private readonly float Y_MOVEMENT_THRESHOLD = 25;
	private readonly float X_MOVEMENT_THRESHOLD = 700;

	private Node2D _ball;
	private bool _canMove = true;

	public override void _Ready()
	{
		base._Ready();

		_ball = (Node2D)GetTree().GetFirstNodeInGroup(nameof(Ball));
	}

	public override void _PhysicsProcess(double delta)
	{

		if (!_canMove || !IsInstanceValid(_ball))
			return;

		if ((_ball.GlobalPosition - GlobalPosition).Length() > X_MOVEMENT_THRESHOLD)
			return;

		_movementComponent.MoveVertically(GetDirectionFromBall(), delta);
	}

	private int GetDirectionFromBall()
	{

		if (Mathf.Abs(_ball.GlobalPosition.Y - GlobalPosition.Y) > Y_MOVEMENT_THRESHOLD)
		{
			return _ball.GlobalPosition.Y > GlobalPosition.Y ? 1 : -1;
		}
		return 0;
	}
}
