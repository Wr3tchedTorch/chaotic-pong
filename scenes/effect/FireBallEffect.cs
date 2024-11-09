using Game.Agent;
using Godot;

namespace Game.Effect;

public partial class FireBallEffect : BaseEffect
{

	[Export] private int _healthDamage = 1;
	[Export] private int _scoreAmount = 2;

	private int _defaultScoreAmount;
	private Ball _parent;
	private EffectManager _effectManager;

	public override void Enter()
	{
		_effectManager = GetParent<EffectManager>();
		_parent = _effectManager.GetParent<Ball>();
		_parent.GetNode<Sprite2D>("Sprite2D").Modulate = Colors.Red;

		_defaultScoreAmount = _parent.ScoreAmount;
		_parent.ScoreAmount = _scoreAmount;
	}

	public override void Exit()
	{
		_parent.GetNode<Sprite2D>("Sprite2D").Modulate = Colors.White;
		_parent.ScoreAmount = _defaultScoreAmount;
	}

	public override void OnCollision(Node2D body)
	{
		GD.Print("Colidindo!");
		if (body is Paddle paddle)
		{
			if (paddle.CurrentDashState == DashState.Dashing)
				return;
			paddle.HealthComponent.TakeDamage(_healthDamage);
			_effectManager.ClearEffects();
		}
	}
}
