using Godot;

namespace Game.Particle;

public partial class KetchupParticles : GpuParticles2D
{

	public override void _Ready()
	{

		Finished += OnParticlesEnd;
	}

	private void OnParticlesEnd()
	{
		QueueFree();
	}
}
