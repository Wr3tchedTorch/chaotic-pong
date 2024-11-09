using System;
using Game.Resources;
using Godot;

namespace Game.Effect;

public partial class EffectManager : Node
{

	[Export] private EffectResource[] _effects;
	[Export] private Area2D _hitBoxArea;

	private BaseEffect _currentEffect;

	public override void _Process(double delta)
	{
		if (!IsInstanceValid(_currentEffect))
			return;
		_currentEffect.Update(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!IsInstanceValid(_currentEffect))
			return;
		_currentEffect.PhysicsUpdate(delta);
	}

	public void ClearEffects()
	{
		_currentEffect.Exit();
		GetChildren()[0].QueueFree();
	}

	public void TransitionTo(EffectList effectType)
	{

		if (IsInstanceValid(_currentEffect))
		{
			_currentEffect.Exit();
			_currentEffect.QueueFree();
		}

		Callable.From(() => InstantiateEffect(effectType)).CallDeferred();
	}

	private async void InstantiateEffect(EffectList effectType)
	{
		var effectResource = Array.Find(_effects, effect => effect._effectType == effectType);
		var effectScene = effectResource._effectScene;
		var effect = effectScene.Instantiate<BaseEffect>();

		AddChild(effect);
		_hitBoxArea.BodyEntered += effect.OnCollision;
		effect.Enter();

		_currentEffect = effect;
		await ToSignal(GetTree().CreateTimer(effectResource._effectDuration), "timeout");
		ClearEffects();
	}
}
