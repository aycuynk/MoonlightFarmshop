using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CropManager : MonoBehaviour
{
    private Inventory inventory;

    [SerializeField] Tilemap cropMap, interactableMap;
    [SerializeField] Tile cropTile;
    [SerializeField] List<Crop> crops;
    private List<CropData> cropDatas;

    private void Start()
    {
        inventory = GameManager.instance.player.inventory.GetInventoryByName("Toolbar");

        cropDatas = GameManager.instance.data.cropDatas;

        for (int i = 0; i < cropDatas.Count; i++)
        {
            PlantSeed(cropDatas[i].cropPosition, cropDatas[i].plantedCrop, cropDatas[i].growIndex);
            cropMap.SetTile(cropDatas[i].cropPosition, cropDatas[i].plantedCrop.data.cropTiles[cropDatas[i].growIndex]);
        }
    }

    private void SetCropTile(Tile cropTile)
    {
        this.cropTile = cropTile;
    }

    private bool IsPlantable(Vector3Int position, ItemTypes itemType)
    {
        TileBase tile = interactableMap.GetTile(position);

        if (tile != null)
        {
            if (tile.name == "Interacted" && itemType == ItemTypes.SEED)
            {
                return true;
            }
        }

        return false;
    }

    public void PlantSeed(Vector3Int position)
    {
        Item itemToPlant = GameManager.instance.ItemManager.GetItemByName(inventory.slots[UI_Manager.selectedSlot.slotID].itemName);

        if (itemToPlant != null && IsPlantable(position, itemToPlant.data.itemType) && cropMap.GetTile(position) == null)
        {
            SetCropTile(itemToPlant.data.cropTiles[0]);
            cropMap.SetTile(position, cropTile);
            inventory.Remove(UI_Manager.selectedSlot.slotID);
            GameManager.instance.uiManager.RefreshInventoryUI("Toolbar");
            Crop crop = Instantiate(itemToPlant.GetComponent<Crop>(), position, Quaternion.identity);
            crops.Add(crop);
            crop.StartToGrow();

            CropData cropData = new CropData();
            cropData.plantedCrop = itemToPlant;
            cropData.cropPosition = position;
            cropDatas.Add(cropData);
            GameManager.instance.Save();
        }
    }

    public void PlantSeed(Vector3Int position, Item itemToPlant, int growIndex)
    {
        if (itemToPlant != null && IsPlantable(position, itemToPlant.data.itemType) && cropMap.GetTile(position) == null)
        {
            SetCropTile(itemToPlant.data.cropTiles[0]);
            cropMap.SetTile(position, cropTile);
            Crop crop = Instantiate(itemToPlant.GetComponent<Crop>(), position, Quaternion.identity);
            crops.Add(crop);
            crop.ContinueToGrow(growIndex);
        }
    }

    public void GrowSeed(Crop crop, int fromIndex, int toIndex)
    {
        for (int i = 0; i < cropDatas.Count; i++)
        {
            cropMap.SetTile(cropDatas[i].cropPosition, cropDatas[i].plantedCrop.data.cropTiles[toIndex]);
            if (cropDatas.IndexOf(cropDatas[i]) == crops.IndexOf(crops[i]))
            {
                cropDatas[i].growIndex = crops[i].cropIndex;
            }
        }
    }

    public void Harvest(Vector3Int position)
    {
        if (crops.Count <= 0) return;

        for (int i = 0; i < crops.Count; i++)
        {
            Vector3Int cropPos = new Vector3Int((int)crops[i].transform.position.x, (int)crops[i].transform.position.y, 0);
            if (crops[i].isSeedGrown && position == cropPos)
            {
                cropMap.SetTile(cropPos, null);
                crops[i].EndGrow(cropPos);
                crops.Remove(crops[i]);
            }
        }
    }
}
