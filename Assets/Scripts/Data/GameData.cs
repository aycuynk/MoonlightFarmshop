using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string saveName;

    public Inventory.Slot[] backpackSlots = new Inventory.Slot[21];
    public Inventory.Slot[] toolbarSlots = new Inventory.Slot[9];
    public List<Vector3Int> interactedTiles;
    public Counter.CounterSlot[] counterSlots = new Counter.CounterSlot[18];

    public List<CropData> cropDatas;

    public PlayerData playerData;
}
