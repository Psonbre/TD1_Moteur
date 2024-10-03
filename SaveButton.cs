using Godot;
using System;

public partial class SaveButton : Button
{
	public override void _Pressed()
	{
		base._Pressed();
		SubsystemManager.GetManager<SaveManager>().SaveGame("user://savegame.save");
		SubsystemManager.GetManager<LevelManager>().LoadLevel("MainMenu.tscn");
	}
}
