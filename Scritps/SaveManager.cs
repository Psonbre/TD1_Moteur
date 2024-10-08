using Godot;

public partial class SaveManager : Manager
{
	public void SaveGame(string path)
	{
		using var saveFile = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);

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

	public void LoadGame(string path)
	{
		if (!Godot.FileAccess.FileExists(path))
		{
			return;
		}

		var saveNodes = SubsystemManager.Get().Root.GetTree().GetNodesInGroup("Persist");
		foreach (Node saveNode in saveNodes)
		{
			saveNode.QueueFree();
		}

		using var saveFile = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);

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

    public bool SaveExists(string savePath)
    {
        return Godot.FileAccess.FileExists(savePath);
    }
}

