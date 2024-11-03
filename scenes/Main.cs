using System;
using System.Linq;
using Godot;

namespace Game;

public partial class Main : Node
{

	private readonly StringName ACTION_RESET = "reset";

	private Area2D[] _scoreAreas = new Area2D[2];
	private int[] _score = new int[2];

	public override void _Ready()
	{

		_scoreAreas[1] = GetNode<Area2D>("RightScoreArea");
		_scoreAreas[1].BodyEntered += (Node2D body) => _score[0]++;

		_scoreAreas[0] = GetNode<Area2D>("LeftScoreArea");
		_scoreAreas[0].BodyEntered += (Node2D body) => _score[1]++;
	}

	public override void _Process(double delta)
	{

		if (Input.IsActionPressed(ACTION_RESET))
			GetTree().ReloadCurrentScene();

		GD.Print(string.Join(", ", _score));
	}

}
