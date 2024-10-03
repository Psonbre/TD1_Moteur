using Godot;
using System;

public partial class LevelManager : Manager
{
    public void LoadLevel(string levelName)
    {
        Node root = SubsystemManager.Get().Root;

        foreach (Node child in root.GetChildren())
        {
            root.RemoveChild(child);
            child.QueueFree();
        }

        Node newLevel = ResourceLoader.Load<PackedScene>(levelName).Instantiate();

        root.AddChild(newLevel);
    }
}
