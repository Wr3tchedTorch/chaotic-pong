using Game.Agent;
using Game.Component;
using Godot;

namespace Game.Singleton;

public enum GameSide
{
	Left,
	Right
}

public partial class GameEvents : Node
{

	public static GameEvents Instance { get; private set; }

	[Signal] public delegate void RightSideScoredEventHandler();
	[Signal] public delegate void LeftSideScoredEventHandler();

	[Signal] public delegate void HealthComponentCreatedEventHandler(HealthComponent healthComponent, Paddle parent);

	public override void _Notification(int what)
	{

		if (what == NotificationSceneInstantiated)
			Instance = this;
	}

	public void EmitSideScored(GameSide whichSide)
	{
		if (whichSide == GameSide.Right)
			EmitSignal(SignalName.RightSideScored);
		else if (whichSide == GameSide.Left)
			EmitSignal(SignalName.LeftSideScored);
	}

	public void EmitHealthComponentCreated(HealthComponent healthComponent, Paddle parent)
	{
		EmitSignal(SignalName.HealthComponentCreated, healthComponent, parent);
	}
}
