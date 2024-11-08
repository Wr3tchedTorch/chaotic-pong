using Game.Manager;
using Godot;

namespace Game;

public partial class Main : Node
{

	private GameManager _gameManager;

	private readonly StringName ACTION_RESET = "r";
	private readonly StringName ACTION_START = "space";

	public override void _Ready()
	{
		_gameManager = GetNode<GameManager>("GameManager");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed(ACTION_RESET))
			_gameManager.ResetGame();
		if (Input.IsActionPressed(ACTION_START))
			_gameManager.StartGame();
	}
}
