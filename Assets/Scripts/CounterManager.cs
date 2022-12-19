using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    [SerializeField] Counter counter;
    [SerializeField] SpriteRenderer[] slotSprites;
    private Inventory inventory;

    private void Start()
    {
        counter = new Counter(18);
        inventory = GameManager.instance.player.inventory.GetInventoryByName("Toolbar");

        for (int i = 0; i < counter.slots.Count; i++)
        {
            SetSlot(GameManager.instance.data.counterSlots[i], i);
        }
    }

    public void PutItemToSell(Vector3 playerPos)
    {
        Item itemToSell = GameManager.instance.ItemManager.GetItemByName(inventory.slots[UI_Manager.selectedSlot.slotID].itemName);
        if (itemToSell != null && Vector3.Distance(playerPos, transform.position) < 1f)
        {
            inventory.Remove(UI_Manager.selectedSlot.slotID);
            GameManager.instance.uiManager.RefreshInventoryUI("Toolbar");

            for (int i = 0; i < counter.slots.Count; i++)
            {
                if (counter.slots[i].itemName == "")
                {
                    SetSlot(itemToSell, i);

                    GameManager.instance.data.counterSlots[i] = counter.slots[i];
                    GameManager.instance.Save();
                    return;
                }
            }

        }
    }

    private void SetSlot(Counter.CounterSlot slot, int index)
    {
        if (counter.slots[index].itemName == "")
        {
            counter.slots[index] = slot;
            slotSprites[index].sprite = SpriteDataProvider.Instance.GetIconByID(counter.slots[index].IconID);
        }
    }

    private void SetSlot(Item item, int index)
    {
        if (counter.slots[index].itemName == "")
        {
            counter.Add(item);
            slotSprites[index].sprite = SpriteDataProvider.Instance.GetIconByID(counter.slots[index].IconID);
        }
    }

    public List<Counter.CounterSlot> GetFilledSlots()
    {
        List<Counter.CounterSlot> filledSlots = new List<Counter.CounterSlot>();
        for (int i = 0; i < counter.slots.Count; i++)
        {
            if (counter.slots[i].itemName != "")
            {
                filledSlots.Add(counter.slots[i]);
            }
        }
        return filledSlots;
    }

    public void SellItem(Counter.CounterSlot slot)
    {
        if (slot != null)
        {
            GameManager.instance.UpdatePlayerMoney(slot.sellPrice);

            counter.Remove(counter.slots.IndexOf(slot));
            slotSprites[counter.slots.IndexOf(slot)].sprite = null; 
        }
    }
}
