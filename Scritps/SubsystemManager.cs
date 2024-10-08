using Godot;
using System;

[GlobalClass]
public partial class SubsystemManager : SceneTree
{
	private static LevelManager levelManager;
	private static SaveManager saveManager;

	public override void _Finalize()
	{
		base._Finalize();
	}

	public static SubsystemManager Get()
	{
		return (SubsystemManager)Engine.GetMainLoop();
	}

	public static T GetManager<T>() where T : Manager, new()
	{
		if (typeof(T) == typeof(LevelManager))
		{
			if (levelManager == null)
			{
				levelManager = new LevelManager();
			}
			return (T)(Manager)levelManager;
		}
		else if (typeof(T) == typeof(SaveManager))
		{
			if (saveManager == null)
			{
				saveManager = new SaveManager();
			}
			return (T)(Manager)saveManager;
		}

		return default;
	}
}
