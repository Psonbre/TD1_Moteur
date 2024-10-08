using Godot;
using System;

public partial class UIInventorySlot : Button
{
    private string item;
    private Inventory inventory;

    public override void _Ready()
    {
        Pressed += _on_Clicked;
    }
    public void UpdateSlot(Inventory inventory, string item, int amount)
    {
        this.item = item;
        this.inventory = inventory;

        if (amount > 1)
        {
            GetNode<Label>("Count").Text = "x " + amount.ToString();
        }
        else
        {
            GetNode<Label>("Count").Text = "";
        }

        //Instancier l'objet juste pour obtenir l'icone ce n'est pas top mais c'est le mieu que j'ai trouve sans ajoute trop d'infrastructure.
        Collectible tempCollectible = GD.Load<PackedScene>(item).Instantiate<Collectible>();
        GetNode<Sprite2D>("Icon").Texture = tempCollectible.GetIcon();
        tempCollectible.QueueFree();
    }

    public void _on_Clicked()
    {
        var item_node = inventory.RemoveItem(item, 1).Instantiate<Node2D>();
        item_node.Position = inventory.GetOwner().Position;
        SubsystemManager.Get().Root.AddChild(item_node);
    }
}
