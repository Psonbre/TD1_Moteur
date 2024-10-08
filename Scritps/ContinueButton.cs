using Godot;
using System;

public partial class ContinueButton : Button
{
    public override void _Ready()
    {
        base._Ready();
        if (!SubsystemManager.GetManager<SaveManager>().SaveExists("user://savegame.save")) Disabled = true;
    }
    public override void _Pressed()
	{
		base._Pressed();
		SubsystemManager.GetManager<LevelManager>().LoadLevel("Scenes/Levels/game.tscn");
		SubsystemManager.GetManager<SaveManager>().LoadGame("user://savegame.save");
	}
}
