using Game.Agent;
using Game.Singleton;
using Godot;

namespace Game.Component;

public partial class HealthComponent : Node
{


	[Signal] public delegate void DeathEventHandler();
	[Signal] public delegate void DamageTakenEventHandler(int amount);

	[Export] public int MaxHealth = 3;

	private int _currentHealth;
	private Paddle _parent;

	public override void _Ready()
	{
		_currentHealth = MaxHealth;
		_parent = GetOwner<Paddle>();

		Callable.From(Initialize).CallDeferred();
	}

	public void TakeDamage(int amount)
	{
		if (_currentHealth > 0)
			EmitSignal(SignalName.DamageTaken, amount);

		_currentHealth -= amount;
		if (_currentHealth <= 0)
			EmitSignal(SignalName.Death);
	}

	private void Initialize()
	{
		GameEvents.Instance.EmitHealthComponentCreated(this, _parent);
	}
}
