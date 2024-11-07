using Godot;

namespace Game.Scripts.Extension;

public static partial class NodeExtensions
{

    public static void RemoveChildren(this Node node, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            node.GetChild<Node>(0).QueueFree();
        }
    }

    public static void AddChildren(this Node node, int quantity, PackedScene childrenScene)
    {
        for (int i = 0; i < quantity; i++)
        {
            var child = childrenScene.Instantiate();
            node.AddChild(child);
        }
    }
}
