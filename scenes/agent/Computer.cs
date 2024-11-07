using Game.Singleton;
using Godot;

namespace Game.Agent;

public partial class Computer : Paddle
{

	private readonly float Y_MOVEMENT_THRESHOLD = 25;
	private readonly float X_MOVEMENT_THRESHOLD = 700;

	private Node2D _ball;

	public override void _Ready()
	{
		base._Ready();		

		_ball = (Node2D)GetTree().GetFirstNodeInGroup(nameof(Ball));
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (CurrentDashState == DashState.Dashing || !IsInstanceValid(_ball))
			return;

		if ((_ball.GlobalPosition - GlobalPosition).Length() > X_MOVEMENT_THRESHOLD)
			return;

		_movementComponent.MoveVertically(GetDirectionFromBall(), delta);
	}

	protected override void OnDeath()
	{
		GD.Print("Computer: Oh no! I just died!");
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
