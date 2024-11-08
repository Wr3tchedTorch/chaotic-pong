using Game.Agent;
using Game.UI;
using Godot;

namespace Game.Manager;

public partial class GameManager : Node
{

	[Export] private ScoreUI _scoreUI;
	[Export] private Ball _ball;
	[Export] private Area2D[] _scoreAreas = new Area2D[2];

	public override void _Ready()
	{
		_scoreAreas[0].BodyEntered += (Node2D body) => RightSideWon();
		_scoreAreas[1].BodyEntered += (Node2D body) => LeftSideWon();
	}

	public void StartGame()
	{
		_ball.StartMoving();
	}

	public void ResetGame()
	{
		_ball.Reset();
	}

	public void LeftSideWon()
	{
		_scoreUI.IncrementScore((int)GameSide.Left);
		ResetGame();
	}

	public void RightSideWon()
	{
		_scoreUI.IncrementScore((int)GameSide.Right);
		ResetGame();
	}
}
