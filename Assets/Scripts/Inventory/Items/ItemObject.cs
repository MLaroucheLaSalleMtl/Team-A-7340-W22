using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Healing,
    Quest
}

public abstract class ItemObject : ScriptableObject
{
    public int ID;
    public Sprite iconDisplay;
    public ItemType type;
    [TextArea(5,10)]
    public string description;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int ID;

    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.ID;
    }
}

