using Godot;
using System;

public partial class SaveButton : Button
{
	public override void _Pressed()
	{
		base._Pressed();
		SubsystemManager.GetManager<SaveManager>().SaveGame();
		SubsystemManager.GetManager<LevelManager>().LoadLevel("MainMenu.tscn");
	}
}
