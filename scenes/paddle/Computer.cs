using Godot;

namespace Game.Paddle;

public partial class Computer : StaticBody2D
{

	public override void _Ready()
	{

		AddToGroup(nameof(Paddle));
	}
}
