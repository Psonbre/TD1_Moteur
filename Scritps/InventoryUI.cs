using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryUI : HBoxContainer
{
	[Export] private PackedScene inventory_slot_scene;

	public void UpdateUI(Inventory inventory)
	{
        var inventoryContent = inventory.GetInventory();

        foreach (Node child in GetChildren())
		{
            child.QueueFree();
        }

        foreach (var item in inventoryContent)
        {
            UIInventorySlot slot = inventory_slot_scene.Instantiate<UIInventorySlot>();
            slot.UpdateSlot(inventory, item.Key, item.Value);
            AddChild(slot);
        }
    }
}
