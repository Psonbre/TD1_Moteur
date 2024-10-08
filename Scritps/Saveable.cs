using Godot;
using Godot.Collections;
using System;

public interface Saveable
{
	void Load(Dictionary<string, Variant> nodeData);
	public Godot.Collections.Dictionary<string, Variant> Save();
}
