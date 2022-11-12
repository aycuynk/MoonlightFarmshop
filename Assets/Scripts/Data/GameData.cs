using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string saveName;

    public List<Inventory.Slot> backpackSlots;
    public List<Inventory.Slot> toolbarSlots;
    public List<Vector3Int> interactedTiles;

    public List<CropData> cropDatas;

    public PlayerData playerData;
}
