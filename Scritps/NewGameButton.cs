using Godot;
using System;

public partial class NewGameButton : Button
{
	public override void _Pressed()
	{
		base._Pressed();
		SubsystemManager.GetManager<LevelManager>().LoadLevel("Scenes/Levels/game.tscn");
	}
}
