using Godot;
using System;
using System.Collections.Generic;

public class Inventory
{
    private Dictionary<string, int> inventory;
    private InventoryUI inventoryUI;
    private Node2D owner;

    public Inventory(Node2D owner = null, InventoryUI inventoryUI = null)
    {
        inventory = new Dictionary<string, int>();
        this.inventoryUI = inventoryUI;
        this.owner = owner;
    }

    public void AddItem(string item, int amount)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += amount;
        }
        else
        {
            inventory.Add(item, amount);
        }
        inventoryUI?.UpdateUI(this);
    }

    public PackedScene RemoveItem(string item, int amount)
    {
        if (inventory.ContainsKey(item))
        {

            inventory[item] -= amount;
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
            inventoryUI?.UpdateUI(this);
            return GD.Load<PackedScene>(item);
        }
        return null;
    }

    public Node2D GetOwner()
    {
        return owner;
    }

    public void SetUI(InventoryUI inventoryUI)
    {
        this.inventoryUI = inventoryUI;
        inventoryUI.UpdateUI(this);
    }

    public Godot.Collections.Dictionary<string, Variant> Save()
    {
        Godot.Collections.Dictionary<string, Variant> data = new();
        foreach (KeyValuePair<string, int> item in inventory)
        {
            data.Add(item.Key, item.Value);
        }
        return data;
    }

    public void Load(Godot.Collections.Dictionary<string, Variant> data)
    {
        foreach (KeyValuePair<string, Variant> item in data)
        {
            inventory.Add(item.Key, (int)item.Value);
        }
    }

    public Dictionary<string, int> GetInventory()
    {
        return inventory;
    }
}
