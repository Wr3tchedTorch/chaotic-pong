using Godot;

namespace Game;

public partial class Main : Node
{

	private readonly StringName ACTION_RESET = "reset";

	public override void _Process(double delta)
	{

		if (Input.IsActionPressed(ACTION_RESET))
			GetTree().ReloadCurrentScene();
	}
}
