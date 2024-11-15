using Godot;

namespace Game.UI;

public partial class ScoreUI : Control
{

	private Label _scoreLabel;
	private int[] _score = new int[2];

	public override void _Ready()
	{

		_scoreLabel = GetNode<Label>("%ScoreLabel");
	}

	public void IncrementScore(int side, int amount)
	{
		_score[side] += amount;
		DisplayScore();
	}

	private void DisplayScore()
	{
		_scoreLabel.Text = _score.Join("_");
	}
}
