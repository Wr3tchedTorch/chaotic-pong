using Game.Agent;
using Game.Singleton;
using Godot;

namespace Game.Component;

public partial class HealthComponent : Node
{

	public int CurrentHealth;

	[Signal] public delegate void DeathEventHandler();
	[Export] private int _maxHealth = 3;

	private Paddle _parent;

	public override void _Ready()
	{
		GameEvents.Instance.EmitHealthComponentCreated(this, _parent.Side);

		CurrentHealth = _maxHealth;
		_parent = GetOwner<Paddle>();
	}

	public void TakeDamage(int amount)
	{
		CurrentHealth -= amount;
		if (CurrentHealth <= 0)
			EmitSignal(SignalName.Death);
	}
}
