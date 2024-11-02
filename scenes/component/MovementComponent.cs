using Godot;

namespace Game.Component;

public partial class MovementComponent : Node
{

	[Export] private float _speed = 270;

	private Node2D _parent;
	private float _parentHeight;

	public override void _Ready()
	{

		_parent = GetOwner<Node2D>();
		_parentHeight = _parent.GetNode<Sprite2D>("Sprite2D").Texture.GetHeight();
	}

	public void MoveVertically(float direction, double delta)
	{

		if (direction == 0)
			return;

		Vector2 globalPosition = _parent.GlobalPosition;
		globalPosition.Y += direction * _speed * (float)delta;
		globalPosition.Y = ClampYPositionInsideViewport(globalPosition.Y);
		_parent.GlobalPosition = globalPosition;
	}

	private float ClampYPositionInsideViewport(float position)
	{
		return Mathf.Clamp(position, _parentHeight / 2, GetViewport().GetVisibleRect().Size.Y - _parentHeight / 2);
	}
}
