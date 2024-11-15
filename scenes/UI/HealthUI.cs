using System.Collections.Generic;
using Game.Agent;
using Game.Component;
using Game.Scripts.Extension;
using Game.Singleton;
using Godot;

public partial class HealthUI : Control
{

	private readonly StringName ANIMATION_DESTROY = "destroy";

	[Export] private PackedScene _healthSprite;

	private readonly Dictionary<GameSide, Node> _UIHealthGroups = new();

	public override void _Ready()
	{

		GameEvents.Instance.HealthComponentCreated += OnHealthComponentCreated;

		_UIHealthGroups[GameSide.Left] = GetNode<Node>("%LeftSideHealthGroup");
		_UIHealthGroups[GameSide.Right] = GetNode<Node>("%RightSideHealthGroup");
	}

	private void RemoveHealth(GameSide whichSide, int amount)
	{
		if (amount <= 0)
			return;
		for (int i = 0; i < amount; i++)
		{
			_UIHealthGroups[whichSide].GetChild<Node>(i).GetNode<AnimationPlayer>(nameof(AnimationPlayer)).Play(ANIMATION_DESTROY);
		}
	}

	private void AddHealth(GameSide whichSide, int amount)
	{
		if (amount <= 0)
			return;
		_UIHealthGroups[whichSide].AddChildren(amount, _healthSprite);
	}

	private void InitializeHealthUI(GameSide side, int maxHealth)
	{
		int healthDifference = _UIHealthGroups[side].GetChildren().Count - maxHealth;
		if (healthDifference == 0)
			return;
		if (healthDifference > 0)
		{
			RemoveHealth(side, healthDifference);
			return;
		}
		AddHealth(side, Mathf.Abs(healthDifference));
	}

	private void OnHealthComponentCreated(HealthComponent healthComponent, Paddle _parent)
	{
		GameSide side = _parent.Side;
		healthComponent.DamageTaken += (amount) => RemoveHealth(_parent.Side, amount);
		InitializeHealthUI(_parent.Side, healthComponent.MaxHealth);
	}
}
