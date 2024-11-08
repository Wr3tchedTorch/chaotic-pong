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
		_scoreAreas[1].BodyEntered += (Node2D body) => FinishRound(GameSide.Left);
		_scoreAreas[0].BodyEntered += (Node2D body) => FinishRound(GameSide.Right);
	}

	public void StartGame()
	{
		_ball.StartMoving();
	}

	public void FinishRound(GameSide? whichSide)
	{
		_ball.Reset();
		if (whichSide == null)
			return;
		_scoreUI.IncrementScore((int)whichSide);
	}
}
