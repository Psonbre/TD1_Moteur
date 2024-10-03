using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class SaveManager : Manager
{
	public void SaveGame()
	{
		using var saveFile = Godot.FileAccess.Open("user://savegame.save", Godot.FileAccess.ModeFlags.Write);

		var saveNodes = SubsystemManager.Get().Root.GetTree().GetNodesInGroup("Persist");
		foreach (Node saveNode in saveNodes)
		{
			if (string.IsNullOrEmpty(saveNode.SceneFilePath))
			{
				GD.Print($"persistent node '{saveNode.Name}' is not an instanced scene, skipped");
				continue;
			}

			if (!saveNode.HasMethod("Save"))
			{
				GD.Print($"persistent node '{saveNode.Name}' is missing a Save() function, skipped");
				continue;
			}

			var nodeData = saveNode.Call("Save");

			var jsonString = Json.Stringify(nodeData);

			saveFile.StoreLine(jsonString);
		}
	}

	public void LoadGame()
	{
		if (!Godot.FileAccess.FileExists("user://savegame.save"))
		{
			return;
		}

		var saveNodes = SubsystemManager.Get().Root.GetTree().GetNodesInGroup("Persist");
		foreach (Node saveNode in saveNodes)
		{
			saveNode.QueueFree();
		}

		using var saveFile = Godot.FileAccess.Open("user://savegame.save", Godot.FileAccess.ModeFlags.Read);

		while (saveFile.GetPosition() < saveFile.GetLength())
		{
			var jsonString = saveFile.GetLine();

			var json = new Json();
			var parseResult = json.Parse(jsonString);
			if (parseResult != Error.Ok)
			{
				GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
				continue;
			}

			var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);

			var newObjectScene = GD.Load<PackedScene>(nodeData["Filename"].ToString());
			var newObject = newObjectScene.Instantiate<Node>();
			SubsystemManager.Get().Root.GetNode(nodeData["Parent"].ToString()).AddChild(newObject);
			((Saveable)newObject).Load(nodeData);
		}
	}
}

