using Godot;
using System;

public partial class StartButton : Button
{
	public override void _Pressed()
	{
		base._Pressed();
		SubsystemManager.GetManager<LevelManager>().LoadLevel("game.tscn");
		SubsystemManager.GetManager<SaveManager>().LoadGame("user://savegame.save");
	}
}
