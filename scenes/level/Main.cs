using Game.UI;
using Godot;

namespace Game;

public partial class Main : Node
{

	private readonly StringName ACTION_RESET = "r";
	private readonly StringName ACTION_START = "space";

	private Area2D[] _scoreAreas = new Area2D[2];
	private ScoreUI _scoreUI;
	private Ball _ball;

	public override void _Ready()
	{

		_ball = GetNode<Ball>("Ball");
		_scoreUI = GetNode<ScoreUI>("%ScoreUI");

		_scoreAreas[1] = GetNode<Area2D>("RightScoreArea");
		_scoreAreas[1].BodyEntered += (Node2D body) => FinishRound(0);

		_scoreAreas[0] = GetNode<Area2D>("LeftScoreArea");
		_scoreAreas[0].BodyEntered += (Node2D body) => FinishRound(1);
	}

	public override void _Process(double delta)
	{

		if (Input.IsActionPressed(ACTION_RESET))
			GetTree().ReloadCurrentScene();
		if (Input.IsActionPressed(ACTION_START))
			_ball.StartMoving();
	}

	public void FinishRound(int winningSide)
	{

		GD.Print("finishing this round");
		_scoreUI.IncrementScore(winningSide);
		_ball.Reset();
	}
}
