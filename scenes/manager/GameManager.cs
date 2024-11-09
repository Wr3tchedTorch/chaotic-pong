using Game.Agent;
using Game.UI;
using Godot;

namespace Game.Manager;

public partial class GameManager : Node
{

	[Export] private ScoreUI _scoreUI;
	[Export] private Ball _ball;
	[Export] private Area2D _leftScoreArea;
	[Export] private Area2D _rightScoreArea;

	public override void _Ready()
	{
		// Function call is inverted
		// this is because when the ball enters the left area
		// it means the right side won the round, and vice versa.
		_leftScoreArea.BodyEntered += (Node2D body) => RightSideWon();
		_rightScoreArea.BodyEntered += (Node2D body) => LeftSideWon();
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
		_scoreUI.IncrementScore((int)GameSide.Left, _ball.ScoreAmount);
		ResetGame();
	}

	public void RightSideWon()
	{
		_scoreUI.IncrementScore((int)GameSide.Right, _ball.ScoreAmount);
		ResetGame();
	}
}
