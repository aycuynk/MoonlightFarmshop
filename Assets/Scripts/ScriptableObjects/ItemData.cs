using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public ItemTypes itemType;
    public string itemName = "Item Name";
    public int iconID;
    public int sellPrice;
    public Sprite icon;
    public Tile[] cropTiles;
}
