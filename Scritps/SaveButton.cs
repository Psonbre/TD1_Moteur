using Godot;
using System;

public partial class SaveButton : Button
{
	public override void _Pressed()
	{
		base._Pressed();

		// En realite les methodes save et load des deux managers pourrait etre statiques et ne necessiterait pas d'utiliser le SubSystemManager,
		// mais on le garde pour les besoins du TP.
		SubsystemManager.GetManager<SaveManager>().SaveGame("user://savegame.save");
		SubsystemManager.GetManager<LevelManager>().LoadLevel("Scenes/Levels/MainMenu.tscn");
	}
}
