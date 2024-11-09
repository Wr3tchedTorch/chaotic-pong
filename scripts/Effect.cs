using Godot;

namespace Game;

public partial class BaseEffect : Node
{
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update(double delta) { }
    public virtual void PhysicsUpdate(double delta) { }
    public virtual void OnCollision(Node2D body) { }
}
