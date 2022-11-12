using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    [SerializeField] int backpackSlotsCount;

    [Header("ToolBar")]
    public Inventory toolBar;
    [SerializeField] int toolBarSlotsCount;

    [Header("Shop")]
    public Shop shop;

    private void Awake()
    {
        backpack = new Inventory(backpackSlotsCount);
        toolBar = new Inventory(toolBarSlotsCount);

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolBar);

        for (int i = 0; i < GameManager.instance.data.backpackSlots.Length; i++)
        {
            inventoryByName["Backpack"].slots[i] = GameManager.instance.data.backpackSlots[i];
        }

        for (int i = 0; i < GameManager.instance.data.toolbarSlots.Length; i++)
        {
            inventoryByName["Toolbar"].slots[i] = GameManager.instance.data.toolbarSlots[i];
        }

    }

    public void Add(string inventoryName, Item item)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item);

            GameManager.instance.data.backpackSlots = inventoryByName["Backpack"].slots.ToArray();
            GameManager.instance.data.toolbarSlots = inventoryByName["Toolbar"].slots.ToArray();
            GameManager.instance.Save();
        }
    }

    public Inventory GetInventoryByName(string inventoryName)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }

        return null;
    }

    public bool IsInventoryFull(string inventoryName, string itemName, int maxCount)
    {
        return inventoryByName[inventoryName].IsFull(itemName, maxCount);
    }

    public void BuyItem(Shop_Slot_UI slot)
    {
        Item itemToBuy = GameManager.instance.ItemManager.GetItemByName(shop.slots[slot.index].title);
        if (GameManager.instance.player.money >= shop.slots[slot.index].price)
        {
            Add("Backpack", itemToBuy);
            GameManager.instance.data.backpackSlots = inventoryByName["Backpack"].slots.ToArray();
            GameManager.instance.UpdatePlayerMoney(-shop.slots[slot.index].price);
        }

    }
}
