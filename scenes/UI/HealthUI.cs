using System.Collections.Generic;
using Game.Component;
using Game.Scripts.Extension;
using Game.Singleton;
using Godot;

public partial class HealthUI : Control
{

	[Export] private PackedScene _healthSprite;

	private readonly Dictionary<GameSide, HealthComponent> _healthComponents;
	private readonly Dictionary<GameSide, Node> _UIHealthGroups;

	public override void _Ready()
	{

		GameEvents.Instance.HealthComponentCreated += OnHealthComponentCreated;

		_UIHealthGroups[GameSide.Right] = GetNode<Node>("%RightSideHealthGroup");
		_UIHealthGroups[GameSide.Left] = GetNode<Node>("%LeftSideHealthGroup");
	}

	private void UpdateSideDisplay(GameSide whichSide)
	{

		var UIHealthGroup = _UIHealthGroups[whichSide];
		var UIHealthGroupChildren = UIHealthGroup.GetChildren();

		int healthDifference = UIHealthGroupChildren.Count - _healthComponents[whichSide].CurrentHealth;
		if (healthDifference == 0)
			return;

		if (healthDifference > 0)
			UIHealthGroup.RemoveChildren(healthDifference);
		if (healthDifference < 0)
			UIHealthGroup.RemoveChildren(healthDifference);
	}

	private void OnHealthComponentCreated(HealthComponent healthComponent, GameSide side)
	{
		_healthComponents[side] = healthComponent;
	}
}
