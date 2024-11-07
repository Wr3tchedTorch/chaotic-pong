using Game.Singleton;
using Godot;

namespace Game.Agent;

public partial class Player : Paddle
{

	private readonly StringName ACTION_DASH = "c";

	[Export] private StringName _actionUp = "w";
	[Export] private StringName _actionDown = "s";	

	public override void _Ready()
	{
		base._Ready();

		if (Side == GameSide.Left)
			GameEvents.Instance.RightSideScored += () => _healthComponent.TakeDamage(1);
		if (Side == GameSide.Right)
			GameEvents.Instance.LeftSideScored += () => _healthComponent.TakeDamage(1);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		float direction = GetVerticalDirection();

		if (Input.IsActionPressed(ACTION_DASH))
			Dash(direction);
		if (CurrentDashState != DashState.Dashing)
			_movementComponent.MoveVertically(direction, delta);
	}

	protected override void OnDeath()
	{
		GD.Print("Player: Oh no! I just died!");
	}

	private float GetVerticalDirection()
	{
		return Input.GetAxis(_actionUp, _actionDown);
	}

}
