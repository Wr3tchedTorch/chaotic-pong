using Godot;

namespace Game.Resources;

[GlobalClass]
public partial class EffectResource : Resource
{
	[Export] public float _effectDuration;
	[Export] public EffectList _effectType;
	[Export] public PackedScene _effectScene;
}
