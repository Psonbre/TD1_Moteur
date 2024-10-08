using Godot;
using Godot.Collections;
using System;

public partial class Collectible : Area2D, Saveable
{
    [Export] private Texture2D inventory_icon;
    private bool isReady = false;
    public override void _Ready()
    {
        //On ajoute un timer pour eviter d'instantanement ramasser les objets deposes
        Timer timer = new Timer();
        timer.WaitTime = 0.1f;
        timer.OneShot = true;
        timer.Timeout += () => isReady = true;
        AddChild(timer);
        timer.Start();

        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (!isReady) return;
        if (body is Player player)
        {
            Pickup(player.GetInventory());
        }
    }

    public Texture2D GetIcon()
    {
        return inventory_icon;
    }

    public void Pickup(Inventory inventory) 
    {
        inventory.AddItem(SceneFilePath, 1);
        QueueFree();
    } 

    public Dictionary<string, Variant> Save()
    {
        return new Godot.Collections.Dictionary<string, Variant>()
        {
            { "Filename", SceneFilePath },
            { "Parent", GetParent().GetPath() },
            { "PosX", Position.X },
            { "PosY", Position.Y },
        };
    }
    public void Load(Dictionary<string, Variant> nodeData)
    {
        Position = new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]);
    }
}
